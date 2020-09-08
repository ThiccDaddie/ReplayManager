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
		private List<ReplayInfo> allReplayInfos;
		private (int, int) replaysLoaded;

		public List<ReplayInfo> AllReplayInfos
		{
			get
			{
				return allReplayInfos;
			}
			private set
			{
				allReplayInfos = value;
				OnPropertyChanged();
			}
		}

		public (int, int) ReplaysLoaded
		{
			get
			{
				return replaysLoaded;
			}

			private set
			{
				replaysLoaded = value;
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
			Task.Run(() => RequestLatestReplayInfos());
		}

		public void InitializeHub()
		{
			_hubConnection.On<int, int>("ReceivePercentageReplaysLoaded", (current, total) =>
			{
				ReplaysLoaded = (current, total);
			});
			_hubConnection.On<List<ReplayInfo>>("ReceiveAll", (replays) =>
			{
				AllReplayInfos = replays;
			});
		}

		public async Task RequestLatestReplayInfos()
		{
			await _hubConnection.StartAsync();
			await _hubConnection.SendAsync("RequestAllReplayInfos");
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
