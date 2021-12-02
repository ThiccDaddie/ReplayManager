// <copyright file="PagedReplayList.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.ComponentModel;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using ReplayManager.DataAccess;
using ReplayManager.Models;
using ReplayManager.Services;
using ReplayManager.Store.FitReplaysToWindowUseCase;
using ReplayManager.Store.LoadReplaysUseCase;

namespace ReplayManager.Components.Replays
{
	public sealed partial class PagedReplayList : IDisposable
	{
		private CancellationTokenSource? cts;
		private int pageIndex = 0;
		private int length = 0;
		private int maxReplaysPerPage = 0;

		private List<ReplayInfo>? replays;

		private Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?>? filter;

#nullable disable annotations
		//[Inject]
		//public IReplayLoadingService ReplayLoadingService { get; set; }

		[Parameter]
		public bool ReloadOnItemChanged { get; set; }

		[Inject]
		private IState<FitReplaysToWindowState> FitReplaysToWindowState { get; set; }

		[Inject]
		private IState<LoadReplaysState> LoadReplaysState { get; set; }

		private int PageCount
		{
			get
			{
				if (maxReplaysPerPage > 0)
				{
					return length / maxReplaysPerPage;
				}

				return 0;
			}
		}
#nullable enable annotations

		public void Dispose()
		{
			if (cts is not null)
			{
				cts.Cancel();
				cts.Dispose();
			}

			LoadReplaysState.StateChanged -= OnLoadReplaysStateChanged;
			FitReplaysToWindowState.StateChanged -= OnFitPageNumberChange;
		}

		protected override void OnInitialized()
		{
			LoadReplaysState.StateChanged += OnLoadReplaysStateChanged;
			FitReplaysToWindowState.StateChanged += OnFitPageNumberChange;
		}

		private async Task OnFilterChanged(Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?> filter)
		{
			this.filter = filter;
			await ApplyPaging();
		}

		private async Task UpdateReplays()
		{
			if (cts is not null)
			{
				cts.Cancel();
				cts.Dispose();
			}

			cts = new CancellationTokenSource();
			using ReplaysContext context = new();

			IQueryable<ReplayInfo>? queryable;
			if (context.Replays is not null)
			{
				queryable = context.Replays;
				if (filter is not null)
				{
					queryable = filter(context.Replays);
					if (queryable is null)
					{
						queryable = context.Replays;
					}
				}
			}
			else
			{
				return;
			}

			replays = await
				queryable
				.Skip(pageIndex * maxReplaysPerPage)
				.Take(maxReplaysPerPage)
				.ToListAsync(cts.Token);

			length = await context.Replays.CountAsync();
			await InvokeAsync(StateHasChanged);
		}

		private async Task ApplyPaging()
		{
			await InvokeAsync(UpdateReplays);
			await InvokeAsync(StateHasChanged);
		}

		private async void OnFitPageNumberChange(object? sender, FitReplaysToWindowState state)
		{
			if (state.MaxReplays != maxReplaysPerPage)
			{
				int currentFirstIndex = pageIndex * maxReplaysPerPage;
				maxReplaysPerPage = state.MaxReplays;
				pageIndex = currentFirstIndex == 0 ? 0 : currentFirstIndex / maxReplaysPerPage;
				await ApplyPaging();
			}
		}

		private async void OnLoadReplaysStateChanged(object? sender, LoadReplaysState state)
		{
			if (state.DoneLoading == true)
			{
				await UpdateReplays();
			}
		}

		private async Task ReplayItemChanged()
		{
			if (ReloadOnItemChanged)
			{
				await UpdateReplays();
			}
		}

		private async Task PageChanged(int page)
		{
			pageIndex = page;
			await ApplyPaging();
		}
	}
}
