// <copyright file="LoadReplaysAction.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public record LoadReplaysAction
	{
		public IEnumerable<(string directory, string path)> FilePaths { get; init; }

		public LoadReplaysAction(IEnumerable<(string directory, string path)> filePaths) => FilePaths = filePaths;
	}
}
