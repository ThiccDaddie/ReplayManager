﻿using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using ReplayManager.Models;
using ReplayManager.Services;

namespace ReplayManager.Shared
{
	public partial class MainLayout : LayoutComponentBase
	{
		private string replayLoadingStatus;

		[Inject]
		public IReplayLoadingService ReplayLoadingService { get; set; }

		[Inject]
		public IOptionsMonitor<ReplayManagerOptions> Options { get; set; }

		protected override void OnInitialized()
		{
			ReplayLoadingService.PropertyChanged += ReplayLoadingServicePropertyChanged;
		}

		private void ReplayLoadingServicePropertyChanged(object sender, PropertyChangedEventArgs handler)
		{
			if (ReplayLoadingService.LoadedSuccesfully)
			{
				replayLoadingStatus = $"{ReplayLoadingService.ReplaysLoaded} {(ReplayLoadingService.ReplaysLoaded == 1 ? "replay" : "replays")} loaded";
			}
			else if (ReplayLoadingService.IsLoading)
			{
				replayLoadingStatus = @$"Loading {ReplayLoadingService.ReplaysLoaded}/{ReplayLoadingService.TotalReplaysCount} {(ReplayLoadingService.TotalReplaysCount == 1 ? "replay" : "replays")}";
			}
			else
			{
				replayLoadingStatus = string.Empty;
			}

			InvokeAsync(StateHasChanged);
		}
	}
}
