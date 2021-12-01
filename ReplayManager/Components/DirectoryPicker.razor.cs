// <copyright file="DirectoryPicker.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Components;
using Ookii.Dialogs.Wpf;
using ReplayManager.Models;

namespace ReplayManager.Components
{
	public partial class DirectoryPicker
	{
		private readonly FormModel formModel = new();
#nullable disable annotations
		private CustomValidator customValidator;
#nullable enable annotations

		[Parameter]
		public HashSet<string>? FoundPaths { get; set; }

		[Parameter]
		public HashSet<string> ChosenPaths { get; set; } = new();

		[Parameter]
		public bool AllowSaveNoPaths { get; set; }

		[Parameter]
		public EventCallback<HashSet<string>> OnFinish { get; set; }

		private void AddChosenPath(string path)
		{
			ChosenPaths.Add(path);
			StateHasChanged();
		}

		private void RemoveChosenPath(string path)
		{
			ChosenPaths.Remove(path);
			StateHasChanged();
		}

		private void ShowDirectoryDialog()
		{
			VistaFolderBrowserDialog browser = new()
			{
				Description = "Select folder(s)",
				UseDescriptionForTitle = true,
			};

			if (browser.ShowDialog() == true)
			{
				formModel.Path = browser.SelectedPath;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:Parameter should not span multiple lines", Justification = "Idk man")]
		private void AddDirectory()
		{
			customValidator.ClearErrors();

			if (string.IsNullOrEmpty(formModel.Path))
			{
				customValidator.DisplayError(
					nameof(formModel.Path),
					new()
					{
						"You donkey, you forgot this!",
					});
			}
			else if (!Directory.Exists(formModel.Path))
			{
				customValidator.DisplayError(
					nameof(formModel.Path),
					new()
					{
						"The path is invalid.",
					});
			}
			else
			{
				ChosenPaths.Add(formModel.Path);
				formModel.Path = string.Empty;
			}
		}

		private class FormModel
		{
			public string? Path { get; set; }
		}
	}
}
