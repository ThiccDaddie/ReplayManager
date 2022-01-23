// <copyright file="FetchReplaysFeatureState.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Fluxor;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public class LoadReplaysFeatureState : Feature<LoadReplaysState>
	{
		public override string GetName() => nameof(LoadReplaysState);

		protected override LoadReplaysState GetInitialState() => new()
		{
			LoadingState = LoadingState.NotLoading,
			//Replays = new(),
			ReplaysLoaded = 0,
			TotalReplays = 0,
		};
	}
}
