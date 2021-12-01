// <copyright file="Settings.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using ReplayManager.DataAccess;
using ReplayManager.Helpers;
using ReplayManager.Models;
using ReplayManager.Services;

#nullable disable annotations
namespace ReplayManager.Pages
{
	public sealed partial class Settings : IDisposable
	{
		private IDisposable onOptionsUpdated;

		[Inject]
		public IOptionsMonitor<ReplayManagerOptions> OptionsMonitor { get; set; }

		[Inject]
		public IOptionsWriter OptionsService { get; set; }

		[Inject]
		public IReplayLoadingService ReplayLoadingService { get; set; }

		private HashSet<string> ChosenPaths { get; set; } = new();

		private HashSet<string> FoundPaths { get; set; } = new();

		public void Dispose()
		{
			if (onOptionsUpdated is not null)
			{
				onOptionsUpdated.Dispose();
			}
		}

		protected override void OnInitialized()
		{
			ChosenPaths = new(OptionsMonitor.CurrentValue.ReplayDirectories);
			FoundPaths = new(FileHelpers.FindReplayFolders());
		}

		private async Task Finish(HashSet<string> paths)
		{
			ReplayManagerOptions oldOptions = OptionsMonitor.CurrentValue;
			await OptionsService.WriteOptionsAsync(oldOptions with { ReplayDirectories = ChosenPaths.ToList() });
			onOptionsUpdated = OptionsMonitor.OnChange(async (newOptions, _) =>
			{
				using ReplaysContext context = new();
				IEnumerable<string> removedDirectories = oldOptions.ReplayDirectories
				.Where(oldPath => !newOptions.ReplayDirectories.Contains(oldPath));
				context.Replays?.RemoveRange(context.Replays.Where(replay => removedDirectories.Contains(replay.Directory)));

				IEnumerable<string> addedDirectories = newOptions.ReplayDirectories
				.Where(newPath => !oldOptions.ReplayDirectories.Contains(newPath));

				if (addedDirectories.Any())
				{
					var filePaths = addedDirectories
					.SelectMany(path => Directory.EnumerateFiles(path, "*.wotreplay", SearchOption.AllDirectories).Select(file => (path, file)));
					ReplayLoadingService.Start(filePaths);
					onOptionsUpdated.Dispose();
				}

				await context.SaveChangesAsync();
			});
		}
	}
}
