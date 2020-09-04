using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Client.Services
{

	public class ReplayService : INotifyPropertyChanged
	{
		private readonly HubConnection _hubConnection;

		private List<ReplayInfo> recentReplayInfos;
		public List<ReplayInfo> RecentReplayInfos
		{
			get
			{
				return recentReplayInfos;
			}
			set
			{
				recentReplayInfos = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ReplayService(NavigationManager navigationManager)
		{
			_hubConnection = new HubConnectionBuilder()
			.WithUrl(navigationManager.ToAbsoluteUri("/replayhub"))
			.Build();

			InitializeHub();
			_hubConnection.StartAsync();
			Task.Run(() => RequestLatestReplayInfos());
		}

		public void InitializeHub()
		{
			_hubConnection.On<List<ReplayInfo>>("ReceiveLatest", (replays) =>
			{
				RecentReplayInfos = replays;
			});
		}

		public async Task RequestLatestReplayInfos()
		{
			await _hubConnection.StartAsync();
			await _hubConnection.SendAsync("RequestLatestReplayInfos");
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
