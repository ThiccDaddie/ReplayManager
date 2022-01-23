// <copyright file="Reducers.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Fluxor;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public static class Reducers
	{
		[ReducerMethod(typeof(LoadReplaysAction))]
		public static LoadReplaysState ReduceLoadReplaysAction(LoadReplaysState state)
			=> state with { LoadingState = LoadingState.LoadingAll };

		[ReducerMethod]
		public static LoadReplaysState ReduceLoadReplaysResultAction(LoadReplaysState state, LoadReplaysResultAction action)
			=> state with { LoadingState = LoadingState.DoneLoading, TotalReplays = action.TotalReplaysLoaded };

		[ReducerMethod]
		public static LoadReplaysState ReduceUpdateLoadReplaysCounterAction(LoadReplaysState state, UpdateLoadReplaysCounterAction action)
			=> state with { ReplaysLoaded = action.ReplaysLoaded, TotalReplays = action.TotalReplays};
	}
}
