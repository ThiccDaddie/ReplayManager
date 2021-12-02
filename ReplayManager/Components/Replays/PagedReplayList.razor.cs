// <copyright file="PagedReplayList.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Fluxor;
using Microsoft.AspNetCore.Components;
using ReplayManager.Store.FetchReplaysUseCase;
using ReplayManager.Store.FitReplaysToWindowUseCase;
using ReplayManager.Store.LoadReplaysUseCase;

namespace ReplayManager.Components.Replays
{
	public sealed partial class PagedReplayList
	{
		private int pageIndex = 0;
		private int maxReplaysPerPage = 0;

#nullable disable annotations

		[Parameter]
		public bool ReloadOnItemChanged { get; set; }

		[Inject]
		private IState<FitReplaysToWindowState> FitReplaysToWindowState { get; set; }

		[Inject]
		private IState<LoadReplaysState> LoadReplaysState { get; set; }

		[Inject]
		private IState<FetchReplaysState> FetchReplaysState { get; set; }

		[Inject]
		private IDispatcher Dispatcher { get; set; }

		private int PageCount
		{
			get
			{
				if (maxReplaysPerPage > 0)
				{
					return FetchReplaysState.Value.TotalReplayCount / maxReplaysPerPage;
				}

				return 0;
			}
		}
#nullable enable annotations

		public void Dispose()
		{
			LoadReplaysState.StateChanged -= OnLoadReplaysStateChanged;
			FetchReplaysState.StateChanged -= OnFetchReplaysStateChanged;
			FitReplaysToWindowState.StateChanged -= OnFitPageNumberChange;
		}

		protected override void OnInitialized()
		{
			LoadReplaysState.StateChanged += OnLoadReplaysStateChanged;
			FetchReplaysState.StateChanged += OnFetchReplaysStateChanged;
			FitReplaysToWindowState.StateChanged += OnFitPageNumberChange;
		}

		private void GetReplays()
		{
			int skip = pageIndex * maxReplaysPerPage;
			int take = maxReplaysPerPage;
			Dispatcher.Dispatch(new GetFilteredReplaysAction(skip, take, null));
		}

		private void OnFitPageNumberChange(object? sender, FitReplaysToWindowState state)
		{
			if (state.MaxReplays != maxReplaysPerPage)
			{
				int currentFirstIndex = pageIndex * maxReplaysPerPage;
				maxReplaysPerPage = state.MaxReplays;
				pageIndex = currentFirstIndex == 0 ? 0 : currentFirstIndex / maxReplaysPerPage;
				GetReplays();
			}
		}

		private void OnLoadReplaysStateChanged(object? sender, LoadReplaysState state)
		{
			if (state.LoadingState == LoadingState.DoneLoading || state.LoadingState == LoadingState.NotLoading)
			{
				GetReplays();
			}
		}

		private void OnFetchReplaysStateChanged(object? sender, FetchReplaysState state)
		{
			InvokeAsync(StateHasChanged);
		}

		private void ReplayItemChanged()
		{
			if (ReloadOnItemChanged)
			{
				GetReplays();
			}
		}

		private void PageChanged(int page)
		{
			pageIndex = page;
			GetReplays();
		}
	}
}
