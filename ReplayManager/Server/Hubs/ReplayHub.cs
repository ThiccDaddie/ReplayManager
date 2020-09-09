using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ThiccDaddie.ReplayManager.Server.Services;

namespace ThiccDaddie.ReplayManager.Server.Hubs
{
	public class ReplayHub : Hub
	{
		private readonly ReplayService _replayService;
		public ReplayHub(ReplayService replayService)
		{
			_replayService = replayService;
		}

		public async Task RequestLatestReplayInfos()
		{
			await _replayService.NotifyClientOfLatestReplayInfos(Context.ConnectionId);
		}

		public async Task RequestAllReplayInfos()
		{
			await _replayService.NotifyClientOfAllReplayInfos(Context.ConnectionId);
		}
	}
}
