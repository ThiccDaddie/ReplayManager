using Fluxor;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public static class Reducers
	{
		[ReducerMethod(typeof(LoadReplaysAction))]
		public static LoadReplaysState ReduceLoadReplaysAction(LoadReplaysState state)
			=> state with { IsLoading = true, DoneLoading = false };

		[ReducerMethod(typeof(LoadReplaysResultAction))]
		public static LoadReplaysState ReduceLoadReplaysResultAction(LoadReplaysState state)
			=> state with { IsLoading = false, DoneLoading = true };

		[ReducerMethod]
		public static LoadReplaysState ReduceUpdateLoadReplaysCounterAction(LoadReplaysState state, UpdateLoadReplaysCounterAction action)
			=> state with { ReplaysLoaded = action.ReplaysLoaded, TotalReplays = action.TotalReplaysCount };
	}
}
