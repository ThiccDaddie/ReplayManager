// <copyright file="ReplayListItem.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components;
using ReplayManager.Helpers;
using ReplayManager.Models;

#nullable disable annotations
namespace ReplayManager.Components.Replays
{
	public partial class ReplayListItem
	{
		private readonly DebounceDispatcher dispatcher = new();

		[Parameter]
		public bool ReloadOnChange { get; set; }

		[Parameter]
		public ReplayInfoOld ReplayInfo { get; set; }

		[Parameter]
		public EventCallback<ReplayInfoOld> OnChange { get; set; }

		private async Task OnToggleFavorite(bool toggled)
		{
			await dispatcher.Debounce(200, async () =>
			{
				await InvokeAsync(async () => await ToggleFavorite(toggled));
			});
		}

		private async Task ToggleFavorite(bool toggled)
		{
			//ReplayInfo = ReplayInfo with { IsFavorite = toggled };
			//using ReplaysContext context = new();
			//context.Replays?.Update(ReplayInfo);
			//await context.SaveChangesAsync();
			//StateHasChanged();
			//if (ReloadOnChange)
			//{
			//	await OnChange.InvokeAsync(ReplayInfo);
			//}
		}
	}
}
