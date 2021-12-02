using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Fluxor;
using ReplayManager.DataAccess;
using ReplayManager.Models;
using ReplayManager.Reader;
using Serilog;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public class Effects
	{
		private readonly IReplayReader replayReader;

		public Effects(IReplayReader replayReader)
		{
			this.replayReader = replayReader;
		}

		[EffectMethod]
		public async Task HandleLoadReplaysAction(LoadReplaysAction action, IDispatcher dispatcher)
		{
			var filePaths = action.FilePaths;

			int totalReplaysCount = filePaths.Count();
			Log.Information($"Total replay count: {totalReplaysCount}");
			Log.Information($"Starting to load {totalReplaysCount} replays");

			ConcurrentBag<ReplayInfo> replayInfos = new();

			Subject<int> mySubject = new();
			mySubject.Sample(TimeSpan.FromMilliseconds(50))
						.Subscribe(events =>
						{
							Log.Information($"Loading replays {replayInfos.Count * 100 / totalReplaysCount}% done");
							dispatcher.Dispatch(new UpdateLoadReplaysCounterAction(replayInfos.Count, totalReplaysCount));
						});

			await Parallel.ForEachAsync(filePaths, async (filePath, cancellationToken) =>
			{
				try
				{
					var (directory, path) = filePath;
					string relativePath = Path.GetRelativePath(directory, path);
					ReplayInfo? replay = await replayReader.GetReplayInfoFromFileAsync(path, cancellationToken);
					if (replay is not null)
					{
						replayInfos.Add(replay with { Directory = directory, RelativeFilePath = relativePath });
					}
					else
					{
						totalReplaysCount--;
						Log.Information($"Invalid replay: {path}");
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
					totalReplaysCount--;
					Log.Warning(e, "Loading replay failed");
					return;
				}
			});

			mySubject.OnCompleted();
			Log.Information($"Done loading {replayInfos.Count} replays");
			dispatcher.Dispatch(new LoadReplaysResultAction(replayInfos));
		}

		[EffectMethod]
		public async Task HandleLoadReplaysResultAction(LoadReplaysResultAction action, IDispatcher dispatcher)
		{
			var replays = action.Replays;

			using ReplaysContext context = new();
			if (context.Replays is not null)
			{
				await context.RemoveAllReplays();
				await context.Replays.AddRangeAsync(replays/*, cancellationToken*/);
				await context.SaveChangesAsync(/*cancellationToken*/);
			}
		}
	}
}
