using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ThiccDaddie.ReplayManager.Server.DataAccess;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Server.Services
{
	public class ReplayService
	{
		private readonly OptionsService _optionsService;

		private readonly ReplayNotificationService _replayNotificationService;
		private Task ExecutingTask { get; set; }
		private CancellationTokenSource CancellationTokenSource { get; set; }
		private int TotalReplaysCount { get; set; }

		public ReplayService(OptionsService optionsService, ReplayNotificationService replayNotificationService)
		{
			_optionsService = optionsService;
			_replayNotificationService = replayNotificationService;
		}

		public async Task NotifyClientsOfLatestReplayInfos()
		{
			if (_optionsService.IsInitialised)
			{
				using var dbContext = new ReplayInfoContext();
				List<ReplayInfo> latestReplayInfos = dbContext.ReplayInfos.OrderByDescending(replayInfo => replayInfo.DateTime).Take(10).ToList();
				await _replayNotificationService.NotifyClientsOfLatestReplays(latestReplayInfos);
			}
		}

		public async Task NotifyClientOfLatestReplayInfos(string connectionId)
		{
			using var dbContext = new ReplayInfoContext();
			List<ReplayInfo> latestReplayInfos = dbContext.ReplayInfos.OrderByDescending(replayInfo => replayInfo.DateTime).Take(10).ToList();
			await _replayNotificationService.NotifyClientOfLatestReplays(connectionId, latestReplayInfos);
		}

		public async Task NotifyClientsOfAllReplayInfos()
		{
			if (_optionsService.IsInitialised)
			{
				using var dbContext = new ReplayInfoContext();
				List<ReplayInfo> allReplayInfos = dbContext.ReplayInfos.ToList();
				await _replayNotificationService.NotifyClientsOfAllReplays(allReplayInfos);
			}
		}

		public async Task NotifyClientOfAllReplayInfos(string connectionId)
		{
			using var dbContext = new ReplayInfoContext();
			List<ReplayInfo> allReplayInfos = dbContext.ReplayInfos.ToList();
			await _replayNotificationService.NotifyClientOfAllReplays(connectionId, allReplayInfos);
		}

		public async Task NotifyClientsOfPercentageReplaysLoaded(int currentReplaysLoadedCount)
		{
			await _replayNotificationService.NotifyClientOfPercentageReplaysLoaded(currentReplaysLoadedCount, TotalReplaysCount);
		}

		public void Startup()
		{
			CheckStatus();
		}

		public void CheckStatus()
		{
			if (ExecutingTask != null && !ExecutingTask.IsCompleted && CancellationTokenSource != null)
			{
				CancellationTokenSource.Cancel();
			}

			CancellationTokenSource = new CancellationTokenSource();

			var cancellationToken = CancellationTokenSource.Token;

			var replayDirectory = _optionsService.PrimaryReplaysDirectory;

			TotalReplaysCount = WotReplay.GetReplayFileCount(_optionsService.ReplayManagerOptions.PrimaryReplaysDirectory);

			if (_optionsService.IsInitialised)
			{
				ExecutingTask = LoadAddedReplays(replayDirectory, cancellationToken);
			}
			else
			{
				ExecutingTask = LoadAllReplays(replayDirectory, cancellationToken);
			}
			ExecutingTask.ContinueWith(async task =>
			{
				if (task.IsCompletedSuccessfully)
				{
					await NotifyClientsOfLatestReplayInfos();
					await NotifyClientsOfAllReplayInfos();
				}
			});
		}

		private async Task LoadAddedReplays(string replayDirectory, CancellationToken cancellationToken)
		{
			using var dbContext = new ReplayInfoContext();
			var savedFileNames = dbContext.ReplayInfos.Select(replayInfo => replayInfo.ReplayInfoId);

			var newFileNames = WotReplay.GetReplayFileNames(replayDirectory).Except(savedFileNames);

			await LoadAndPersistReplays(replayDirectory, newFileNames, dbContext, cancellationToken);
		}

		private async Task LoadAllReplays(string replayDirectory, CancellationToken cancellationToken)
		{
			using var dbContext = new ReplayInfoContext();

			await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM ReplayInfos", cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			var fileNames = WotReplay.GetReplayFileNames(replayDirectory);

			await LoadAndPersistReplays(replayDirectory, fileNames, dbContext, cancellationToken);

			_optionsService.IsInitialised = true;
		}

		private async Task LoadAndPersistReplays(string replayDirectory, IEnumerable<string> fileNames, ReplayInfoContext dbContext, CancellationToken cancellationToken)
		{
			Subject<int> MySubject = new Subject<int>();
			MySubject.Sample(TimeSpan.FromMilliseconds(500))
						  .Subscribe(async events => await NotifyClientsOfPercentageReplaysLoaded(events));
			ConcurrentBag<ReplayInfo> replayInfos = new ConcurrentBag<ReplayInfo>();
			await Dasync.Collections.ParallelForEachExtensions.ParallelForEachAsync(fileNames, async fileName =>
			{
				try
				{
					ReplayInfo replay = await WotReplay.DecodeReplay(replayDirectory + fileName, cancellationToken);
					if (replay != null)
					{
						replay.ReplayInfoId = fileName;
						replayInfos.Add(replay);
					}
					else
					{
						TotalReplaysCount--;
					}
					MySubject.OnNext(replayInfos.Count);
				}
				catch (OperationCanceledException)
				{
					throw;
				}
				catch
				{
					return;
				}
			}, maxDegreeOfParallelism: 4, cancellationToken: cancellationToken);
			MySubject.OnCompleted();

			await dbContext.BulkInsertAsync(replayInfos);
			await dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
