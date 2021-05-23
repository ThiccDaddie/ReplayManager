// <copyright file="PagedReplayList.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using ReplayManager.DataAccess;
using ReplayManager.Models;
using ReplayManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ReplayManager.Components.Replays
{
	public sealed partial class PagedReplayList : IDisposable
	{
		private readonly IReadOnlyList<MatPageSizeOption>
			pageSizeOptions = new List<MatPageSizeOption>()
				{
			new(5),
			new(10),
			new(25),
			new(100),
			new(0, "*"),
				};

		private CancellationTokenSource cts;
		private int pageIndex = 0;
		private string pageSizeText = "*";
		private int fitPageNumber = 0;
		private int length = 0;
		private bool isFitToPage = true;
		private List<ReplayInfo> replays;

		private Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>> filter;

		[Inject]
		public IReplayLoadingService ReplayLoadingService { get; set; }

		[Parameter]
		public bool ReloadOnItemChanged { get; set; }

		public Expression FilterExpression
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (cts is not null)
			{
				cts.Cancel();
				cts.Dispose();
			}

			ReplayLoadingService.PropertyChanged -= HandleReplayLoadingServicePropertyChanged;
		}

		protected override void OnInitialized()
		{
			ReplayLoadingService.PropertyChanged += HandleReplayLoadingServicePropertyChanged;
		}

		private int GetPageSize()
		{
			return pageSizeOptions.First(option => option.Text == pageSizeText).Value;
		}

		private void UpdatePageSizeOptions()
		{
			pageSizeOptions.First(option => option.Text == "*").Value = fitPageNumber;
			StateHasChanged();
		}

		//private IQueryable<ReplayInfo> ApplyFilterExpression(IQueryable<ReplayInfo> replays)
		//{
		//	//return replays.OrderBy(r => r.DateTime);
		//	if (FilterExpression is not null)
		//	{
		//		return replays.Provider.CreateQuery<ReplayInfo>(FilterExpression);
		//	}

		//	return replays;
		//}

		private async Task OnFilterChanged(Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>> filter)
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
			int pageSize = GetPageSize();
			using ReplaysContext context = new();
			replays = await
				(filter is null ? context.Replays : filter(context.Replays))
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToListAsync(cts.Token);

			length = await context.Replays.CountAsync();
			await InvokeAsync(StateHasChanged);
		}

		private async Task Page(BetterPaginatorPageEvent pageEvent)
		{
			pageIndex = pageEvent.PageIndex;
			pageSizeText = pageEvent.SizeOption.Text;
			isFitToPage = pageSizeText == "*";
			await ApplyPaging();
		}

		private async Task ApplyPaging()
		{
			await InvokeAsync(UpdateReplays);
			await InvokeAsync(StateHasChanged);
		}

		private async Task HandleFitPageNumberChange(int number)
		{
			if (number != fitPageNumber)
			{
				int pageSize = GetPageSize();
				int currentFirstIndex = pageIndex * pageSize;
				fitPageNumber = number;
				pageIndex = currentFirstIndex == 0 ? 0 : currentFirstIndex / pageSize;
				UpdatePageSizeOptions();
				await ApplyPaging();
			}
		}

		private async Task ReplayItemChanged()
		{
			if (ReloadOnItemChanged)
			{
				await UpdateReplays();
			}
		}

		private async void HandleReplayLoadingServicePropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == nameof(ReplayLoadingService.LoadedSuccesfully) && ReplayLoadingService.LoadedSuccesfully)
			{
				await UpdateReplays();
			}
		}
	}
}
