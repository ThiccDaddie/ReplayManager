// <copyright file="Reducers.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Fluxor;

namespace ReplayManager.Store.ReplayFilterUseCase
{
	public class Reducers
	{
		[ReducerMethod]
		public static ReplayFilterState ReduceUpdateReplayFilterAction(ReplayFilterState state, UpdateReplayFilterAction action)
			=> state with { Filter = action.Filter };
	}
}
