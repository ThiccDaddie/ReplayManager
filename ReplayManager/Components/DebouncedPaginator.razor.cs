// <copyright file="DebouncedPaginator.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using ReplayManager.Helpers;

namespace ReplayManager.Components
{
#nullable disable annotations
	public partial class DebouncedPaginator
	{
		private DebounceDispatcher debounceTimer;

		[Parameter]
		public int Length { get; set; }

		[Parameter]
		public int PageIndex { get; set; }

		[Parameter]
		public string PageSizeText { get; set; }

		[Parameter]
		public int DebounceInterval { get; set; } = 200;

		[Parameter]
		public IReadOnlyList<MatPageSizeOption> PageSizeOptions { get; set; }

		[Parameter]
		public EventCallback<BetterPaginatorPageEvent> OnPage { get; set; }

		protected override void OnInitialized()
		{
			debounceTimer = new();
		}

		private async Task Page(BetterPaginatorPageEvent pageEvent)
		{
			await debounceTimer.Debounce<BetterPaginatorPageEvent>(
				DebounceInterval,
				async data =>
				{
					await InvokeAsync(async () => await OnPage.InvokeAsync(data));
				},
				pageEvent);
		}
	}
}
