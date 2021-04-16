using Microsoft.Extensions.Options;
using ReplayManager.Reader;
using ReplayManager.Shared;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ReplayManager.Services
{
	public class ReplayLoadingService : IReplayLoadingService
	{
		private readonly IOptionsMonitor<ReplayManagerOptions> _options;
		private readonly IReplayReader _replayReader;

		private int _totalReplaysCount = 0;
		private int _replaysLoaded = 0;

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public int TotalReplaysCount
		{
			get
			{
				return _totalReplaysCount;
			}
			set
			{
				if (value != _totalReplaysCount)
				{
					_totalReplaysCount = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int ReplaysLoaded
		{
			get
			{
				return _replaysLoaded;
			}
			set
			{
				if (value != _replaysLoaded)
				{
					_replaysLoaded = value;
					NotifyPropertyChanged();
				}
			}
		}

		public Stopwatch ElapsedTime { get; }

		public ReplayLoadingService(IOptionsMonitor<ReplayManagerOptions> options, IReplayReader replayReader)
		{
			_options = options;
			_replayReader = replayReader;
			ElapsedTime = new();
		}
		private IEnumerable<string> GetFilePaths(List<string> directory)
		{
			return directory.SelectMany(path => Directory.EnumerateFiles(path, "*.wotreplay", SearchOption.AllDirectories));
		}

		public async Task<IEnumerable<ReplayInfo>> LoadAllReplays(CancellationToken cancellationToken = default)
		{
			return await LoadReplays(GetFilePaths(_options.CurrentValue.ReplayDirectories), cancellationToken);
		}

		public IAsyncEnumerable<ReplayInfo> LoadAllReplaysSequentially(CancellationToken cancellationToken = default)
		{
			return LoadReplaysSequentially(GetFilePaths(_options.CurrentValue.ReplayDirectories), cancellationToken);
		}

		public async Task<IEnumerable<ReplayInfo>> LoadReplays(IEnumerable<string> filePaths, CancellationToken cancellationToken = default)
		{
			ElapsedTime.Restart();
			TotalReplaysCount = 0;

			cancellationToken.ThrowIfCancellationRequested();

			TotalReplaysCount = filePaths.Count();
			Log.Information($"Total replay count: {TotalReplaysCount}");

			//Log.Information(@$"Data is {(_options.IsInitialised ? "" : "not")} initialised");
			Log.Information($"Starting to load {TotalReplaysCount} replays");

			ConcurrentBag<ReplayInfo> replayInfos = new();

			Subject<int> MySubject = new();
			//MySubject.Sample(TimeSpan.FromMilliseconds(500))
			//			.Subscribe(async events => await NotifyClientsOfPercentageReplaysLoaded(events));
			MySubject.Sample(TimeSpan.FromSeconds(1))
						.Subscribe(events => Log.Information($"Loading replays {replayInfos.Count * 100 / TotalReplaysCount}% done"));

			await Dasync.Collections.ParallelForEachExtensions.ParallelForEachAsync(filePaths, async filePath =>
			{
				try
				{
					ReplayInfo replay = await _replayReader.GetReplayInfoFromFile(filePath, cancellationToken);
					if (replay != null)
					{
						replayInfos.Add(replay with { ReplayInfoId = filePath });
						ReplaysLoaded++;
					}
					else
					{
						TotalReplaysCount--;
						Log.Information($"Invalid replay info. Skipping and subtracting total replay count. Current total replay count: {TotalReplaysCount}");
					}
					MySubject.OnNext(replayInfos.Count);
				}
				catch (OperationCanceledException)
				{
					Log.Warning("Loading replays cancelled");
					throw;
				}
				catch (Exception e)
				{
					TotalReplaysCount--;
					Log.Warning(e, "Loading replay failed");
					return;
				}
			}, maxDegreeOfParallelism: 20, cancellationToken: cancellationToken);
			MySubject.OnCompleted();
			Log.Information($"Done loading {TotalReplaysCount} replays");
			ElapsedTime.Stop();
			return replayInfos;
		}

		public async IAsyncEnumerable<ReplayInfo> LoadReplaysSequentially(IEnumerable<string> filePaths, [EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			ElapsedTime.Restart();
			TotalReplaysCount = 0;

			cancellationToken.ThrowIfCancellationRequested();

			TotalReplaysCount = filePaths.Count();
			Log.Information($"Total replay count: {TotalReplaysCount}");

			//Log.Information(@$"Data is {(_options.IsInitialised ? "" : "not")} initialised");
			Log.Information($"Starting to load {TotalReplaysCount} replays");

			foreach (string filePath in filePaths)
			{
				ReplayInfo replay = null;
				try
				{
					replay = await _replayReader.GetReplayInfoFromFile(filePath, cancellationToken);
					if (replay != null)
					{
						replay = replay with { ReplayInfoId = filePath };
					}
					else
					{
						TotalReplaysCount--;
						Log.Information($"Invalid replay info. Skipping and subtracting total replay count. Current total replay count: {TotalReplaysCount}");
					}
				}
				catch (OperationCanceledException)
				{
					Log.Warning("Loading replays cancelled");
				}
				catch (Exception e)
				{
					Log.Warning(e, "Loading replay failed");
				}
				ReplaysLoaded++;
				yield return replay;
			}
			Log.Information($"Done loading {TotalReplaysCount} replays");
			ElapsedTime.Stop();
		}
	}
}
