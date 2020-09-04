using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThiccDaddie.ReplayManager.Server.Hubs;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Server.Services
{
	public class ReplayNotificationService
	{
		private readonly IHubContext<ReplayHub> _replayInfoHubContext;

		public ReplayNotificationService(IHubContext<ReplayHub> replayInfoHubContext)
		{
			_replayInfoHubContext = replayInfoHubContext;
		}

		public async Task NotifyClientsOfLatestReplays(List<ReplayInfo> replays)
		{
			await _replayInfoHubContext.Clients.All.SendAsync("ReceiveLatest", replays);
		}
		public async Task NotifyClientOfLatestReplays(string connectionId, List<ReplayInfo> replays)
		{
			await _replayInfoHubContext.Clients.Client(connectionId).SendAsync("ReceiveLatest", replays);
		}
	}
}
