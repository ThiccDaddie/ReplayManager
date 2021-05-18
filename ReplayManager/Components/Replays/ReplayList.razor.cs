// <copyright file="ReplayList.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReplayManager.Helpers;
using ReplayManager.Models;
using ReplayManager.Services;

namespace ReplayManager.Components.Replays
{
	public sealed partial class ReplayList : IDisposable
	{
		private readonly DebounceDispatcher dispatcher = new();
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Assigned to in razor file")]
		private ElementReference mainReference;

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		[Parameter]
		public bool IsFitToPage { get; set; }

		[Parameter]
		public int ThrottleInterval { get; set; } = 10;

		[Parameter]
		public bool ReloadOnItemChanged { get; set; }

		[Parameter]
		public List<ReplayInfo> Replays { get; set; }

		[Parameter]
		public EventCallback<int> OnFitPageNumberChanged { get; set; }

		[Parameter]
		public EventCallback<ReplayInfo> OnReplayItemChanged { get; set; }

		public async Task<int> GetFitPageNumber()
		{
			int availableHeight = await BrowserResizeService.GetInnerHeight(JSRuntime, mainReference);
			return (int)((availableHeight - 3) / 60.5);
		}

		public void Dispose()
		{
			BrowserResizeService.OnResize -= BrowserHasResized;
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await BrowserHasResized();
				await JSRuntime.InvokeAsync<object>("browserResize.registerResizeCallback");
				await OnFitPageNumberChanged.InvokeAsync(await GetFitPageNumber());
			}
		}

		protected override void OnParametersSet()
		{
			BrowserResizeService.OnResize -= BrowserHasResized;
			if (IsFitToPage)
			{
				BrowserResizeService.OnResize += BrowserHasResized;
			}
		}

		private async Task BrowserHasResized()
		{
			await dispatcher.Throttle(ThrottleInterval, () => InvokeAsync(async () => await OnFitPageNumberChanged.InvokeAsync(await GetFitPageNumber())));
		}
	}
}
