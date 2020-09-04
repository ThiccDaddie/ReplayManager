using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThiccDaddie.ReplayManager.Server.DataAccess;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Server.Services
{
	public class ReplayService
	{
		public Task ExecutingTask { get; set; }

		public CancellationTokenSource CancellationTokenSource { get; set; }

		private readonly OptionsService _optionsService;

		private readonly ReplayNotificationService _replayNotificationService;

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
				List<ReplayInfo> latestReplayInfos = dbContext.ReplayInfos.OrderByDescending(replayInfo => replayInfo.DateTime)/*.Take(10)*/.ToList();
				await _replayNotificationService.NotifyClientsOfLatestReplays(latestReplayInfos);
			}
		}

		public async Task NotifyClientOfLatestReplayInfos(string connectionId)
		{
			using var dbContext = new ReplayInfoContext();
			List<ReplayInfo> latestReplayInfos = dbContext.ReplayInfos.OrderByDescending(replayInfo => replayInfo.DateTime)/*.Take(10)*/.ToList();
			await _replayNotificationService.NotifyClientOfLatestReplays(connectionId, latestReplayInfos);
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
			ConcurrentBag<ReplayInfo> replayInfos = new ConcurrentBag<ReplayInfo>();
			await Dasync.Collections.ParallelForEachExtensions.ParallelForEachAsync(fileNames, async fileName =>
			{
				ReplayInfo replay = await WotReplay.DecodeReplay(replayDirectory + fileName, cancellationToken);
				if (replay != null)
				{
					replay.ReplayInfoId = fileName;
					replayInfos.Add(replay);
				}
			}, maxDegreeOfParallelism: 4);
			await dbContext.BulkInsertAsync(replayInfos);
			await dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
