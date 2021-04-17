using ReplayManager.DataAccess;
using ReplayManager.Reader;
using ReplayManager.Shared;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReplayManager.Services
{
	public class ReplayLoadingService : IReplayLoadingService
	{
		private readonly IReplayReader _replayReader;

		private int _totalReplaysCount = 0;
		private int _replaysLoaded = 0;
		private bool _isLoading = false;
		private bool _didLoadingSucceed = false;

		private CancellationTokenSource cancellationTokenSource;
		private CancellationTokenSource CancellationTokenSource
		{
			get
			{
				return cancellationTokenSource;
			}
			set
			{
				if (cancellationTokenSource is not null)
				{
					cancellationTokenSource.Cancel();
					cancellationTokenSource.Dispose();
				}
				cancellationTokenSource = value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

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

		public bool IsLoading
		{
			get
			{
				return _isLoading;
			}
			set
			{
				if (_isLoading != value)
				{
					_isLoading = value;
					NotifyPropertyChanged();
				}
			}
		}

		public bool DidLoadingSucceed
		{
			get
			{
				return _didLoadingSucceed;
			}
			set
			{
				if (_didLoadingSucceed != value)
				{
					_didLoadingSucceed = value;
					NotifyPropertyChanged();
				}
			}
		}

		public Stopwatch ElapsedTime { get; }

		public ReplayLoadingService(IReplayReader replayReader)
		{
			_replayReader = replayReader;
			ElapsedTime = new();
		}
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public async void LoadReplays(IEnumerable<string> filePaths, CancellationToken cancellationToken = default)
		{
			try
			{
				IsLoading = true;
				DidLoadingSucceed = false;
				ElapsedTime.Restart();
				ReplaysLoaded = 0;
				TotalReplaysCount = 0;

				CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
				cancellationToken = CancellationTokenSource.Token;
				cancellationToken.ThrowIfCancellationRequested();

				TotalReplaysCount = filePaths.Count();
				Log.Information($"Total replay count: {TotalReplaysCount}");
				Log.Information($"Starting to load {TotalReplaysCount} replays");

				ConcurrentBag<ReplayInfo> replayInfos = new();

				Subject<int> MySubject = new();
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
				using ReplaysContext context = new();
				await context.Replays.AddRangeAsync(replayInfos, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				DidLoadingSucceed = true;
			}

			catch (OperationCanceledException)
			{
				IsLoading = false;
			}
		}

		public async IAsyncEnumerable<ReplayInfo> LoadReplaysSequentially(IEnumerable<string> filePaths, [EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			ElapsedTime.Restart();
			TotalReplaysCount = 0;

			CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			cancellationToken = CancellationTokenSource.Token;
			cancellationToken.ThrowIfCancellationRequested();

			TotalReplaysCount = filePaths.Count();
			Log.Information($"Total replay count: {TotalReplaysCount}");

			//Log.Information(@$"Data is {(_options.IsInitialised ? "" : "not")} initialised");
			Log.Information($"Starting to load {TotalReplaysCount} replays");

			foreach (string filePath in filePaths)
			{
				cancellationToken.ThrowIfCancellationRequested();
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
