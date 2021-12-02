// <copyright file="FetchReplaysState.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public enum LoadingState
	{
		NotLoading,
		LoadingAll,
		LoadingNewFolders,
		LoadingSingle,
		DoneLoading,
	}

	public record LoadReplaysState
	{
		public LoadingState LoadingState { get; init; }

		public int ReplaysLoaded { get; init; }

		public int TotalReplays { get; init; }
	}
}
