// <copyright file="ReplayLoadingService.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using ReplayManager.DataAccess;
using ReplayManager.Models;
using ReplayManager.Reader;
using Serilog;

namespace ReplayManager.Services
{
	public class ReplayLoadingService : IReplayLoadingService
	{
		private readonly IReplayReader replayReader;

		private int totalReplaysCount;
		private int replaysLoaded = 0;
		private bool isLoading = false;
		private bool loadedSuccesfully = false;
		private NotifyTaskCompletion? replaysLoader;
		private CancellationTokenSource? cancellationTokenSource;

		public ReplayLoadingService(IReplayReader replayReader)
		{
			this.replayReader = replayReader;
			ElapsedTime = new();
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public int TotalReplaysCount
		{
			get
			{
				return totalReplaysCount;
			}

			set
			{
				if (value != totalReplaysCount)
				{
					totalReplaysCount = value;
					NotifyPropertyChanged();
				}
			}
		}

		public int ReplaysLoaded
		{
			get
			{
				return replaysLoaded;
			}

			set
			{
				if (value != replaysLoaded)
				{
					replaysLoaded = value;
					NotifyPropertyChanged();
				}
			}
		}

		public bool IsLoading
		{
			get
			{
				return isLoading;
			}

			set
			{
				if (isLoading != value)
				{
					isLoading = value;
					NotifyPropertyChanged();
				}
			}
		}

		public bool LoadedSuccesfully
		{
			get
			{
				return loadedSuccesfully;
			}

			set
			{
				if (loadedSuccesfully != value)
				{
					loadedSuccesfully = value;
					NotifyPropertyChanged();
				}
			}
		}

		public Stopwatch ElapsedTime { get; }

		public void Start(
			IEnumerable<(string directory, string path)> filePaths,
			CancellationToken cancellationToken = default)
		{
			Cancel();

			cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			cancellationToken = cancellationTokenSource.Token;

			replaysLoader = new(LoadReplaysParallelAsync(filePaths, cancellationToken));
			replaysLoader.PropertyChanged += UpdateStatus;
		}

		public void Cancel()
		{
			if (replaysLoader is not null && cancellationTokenSource is not null)
			{
				cancellationTokenSource.Cancel();
				cancellationTokenSource.Dispose();
			}
		}

		private void UpdateStatus(object? sender, PropertyChangedEventArgs args)
		{
			if (sender is NotifyTaskCompletion taskWatcher)
			{
				IsLoading = !taskWatcher.IsCompleted;
				LoadedSuccesfully = taskWatcher.Status == TaskStatus.RanToCompletion;
			}
		}

		private async Task LoadReplaysParallelAsync(
			IEnumerable<(string directory, string path)> filePaths,
			CancellationToken cancellationToken = default)
		{
			ElapsedTime.Restart();
			ReplaysLoaded = 0;
			TotalReplaysCount = 0;

			TotalReplaysCount = filePaths.Count();
			Log.Information($"Total replay count: {TotalReplaysCount}");
			Log.Information($"Starting to load {TotalReplaysCount} replays");

			ConcurrentBag<ReplayInfo> replayInfos = new();

			Subject<int> mySubject = new();
			mySubject.Sample(TimeSpan.FromSeconds(1))
						.Subscribe(events => Log.Information($"Loading replays {replayInfos.Count * 100 / TotalReplaysCount}% done"));

			await Parallel.ForEachAsync(filePaths, async (filePath, cancellationToken) =>
			{
				try
				{
					var (directory, path) = filePath;
					string relativePath = Path.GetRelativePath(directory, path);
					ReplayInfo? replay = await replayReader.GetReplayInfoFromFileAsync(path, cancellationToken);
					if (replay != null)
					{
						replayInfos.Add(replay with { Directory = directory, RelativeFilePath = relativePath });
						ReplaysLoaded++;
					}
					else
					{
						TotalReplaysCount--;
						Log.Information($"Invalid replay info. Skipping and subtracting total replay count. Current total replay count: {TotalReplaysCount}");
					}

					mySubject.OnNext(replayInfos.Count);
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
			});

			mySubject.OnCompleted();
			Log.Information($"Done loading {TotalReplaysCount} replays");
			ElapsedTime.Stop();
			using ReplaysContext context = new();
			await context.Replays.AddRangeAsync(replayInfos, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
		}

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
