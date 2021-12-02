// <copyright file="Setup.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using ReplayManager.Helpers;
using ReplayManager.Models;
using ReplayManager.Services;
using ReplayManager.Store.LoadReplaysUseCase;

#nullable disable annotations
namespace ReplayManager.Pages
{
	public sealed partial class Setup : IDisposable
	{
		private IDisposable onOptionsUpdated;

		[Inject]
		public NavigationManager Navigation { get; set; }

		[Inject]
		public IOptionsMonitor<ReplayManagerOptions> OptionsMonitor { get; set; }

		[Inject]
		public IOptionsWriter OptionsService { get; set; }

		[Inject]
		public IDispatcher Dispatcher { get; set; }

		public HashSet<string> ChosenPaths { get; set; } = new();

		public HashSet<string> FoundPaths { get; set; } = new();

		public void Dispose()
		{
			if (onOptionsUpdated is not null)
			{
				onOptionsUpdated.Dispose();
			}
		}

		protected override void OnInitialized()
		{
			FoundPaths = new(FileHelpers.FindReplayFolders());
		}

		private async Task FinishSetup(HashSet<string> paths)
		{
			await OptionsService.WriteOptionsAsync(new(ChosenPaths.ToList()));
			onOptionsUpdated = OptionsMonitor.OnChange((options, _) =>
			{
				if (options.ReplayDirectories.Any())
				{
					var filePaths = options
					.ReplayDirectories
					.SelectMany(path => Directory.EnumerateFiles(path, "*.wotreplay", SearchOption.AllDirectories).Select(file => (path, file)));
					Dispatcher.Dispatch(new LoadReplaysAction(filePaths));
					Navigation.NavigateTo("/");
				}
			});
		}
	}
}
