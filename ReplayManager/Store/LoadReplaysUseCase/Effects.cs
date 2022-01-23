using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Fluxor;
using Microsoft.EntityFrameworkCore;
using ReplayManager.DataAccess;
using ReplayManager.Models;
using ReplayManager.Reader;
using Serilog;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public class Effects
	{
		[EffectMethod]
		public async Task HandleLoadReplaysAction(LoadReplaysAction action, IDispatcher dispatcher)
		{
			var filePaths = action.FilePaths;

			int totalReplaysCount = filePaths.Count();
			if (totalReplaysCount == 0)
			{
				return;
			}

			Log.Information($"Total replay count: {totalReplaysCount}");
			Log.Information($"Starting to load {totalReplaysCount} replays");

			ConcurrentBag<ReplayInfo> replays = new();

			Subject<int> mySubject = new();
			mySubject.Sample(TimeSpan.FromMilliseconds(50))
						.Subscribe(events =>
						{
							if (totalReplaysCount > 0)
							{
								Log.Information($"Loading replays {replays.Count * 100 / totalReplaysCount}% done");
							}

							dispatcher.Dispatch(new UpdateLoadReplaysCounterAction(replays.Count, totalReplaysCount));
						});

			Stopwatch stopwatch = Stopwatch.StartNew();
			await Parallel.ForEachAsync(filePaths, async (filePath, cancellationToken) =>
			{
				try
				{
					var (directory, path) = filePath;
					string relativePath = Path.GetRelativePath(directory, path);
					ReplayInfo? replay = await ReplayReader.GetReplayInfoFromFileAsync(path, cancellationToken);
					if (replay is not null)
					{
						replay.Directory = directory;
						replay.Path = relativePath;
						replays.Add(replay);
					}
					else
					{
						//totalReplaysCount--;
						Log.Information($"Invalid replay: {path}");
					}

					mySubject.OnNext(replays.Count);
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

			stopwatch.Stop();
			mySubject.OnCompleted();
			Log.Information($"Done loading {replays.Count} replays");
			using ReplaysContext context = new();
			if (context.Replays is not null)
			{
				await context.RemoveAllReplays();
				await context.Replays.AddRangeAsync(replays/*, cancellationToken*/);
				await context.SaveChangesAsync(/*cancellationToken*/);
				totalReplaysCount = await context.Replays.CountAsync();
				dispatcher.Dispatch(new LoadReplaysResultAction(totalReplaysCount));
			}
		}
	}
}
