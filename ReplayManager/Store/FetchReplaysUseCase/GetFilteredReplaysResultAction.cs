// <copyright file="GetFilteredReplaysResultAction.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using ReplayManager.Models;

namespace ReplayManager.Store.FetchReplaysUseCase
{
	public record GetFilteredReplaysResultAction
	{
		public List<ReplayInfo> Replays { get; init; }

		public int TotalReplayCount { get; init; }

		public GetFilteredReplaysResultAction(List<ReplayInfo> replays, int totalReplayCount)
		{
			Replays = replays;
			TotalReplayCount = totalReplayCount;
		}
	}
}
