using Fluxor;

namespace ReplayManager.Store.FetchReplaysUseCase
{
	public static class Reducers
	{
		[ReducerMethod(typeof(GetFilteredReplaysAction))]
		public static FetchReplaysState ReduceGetFilteredReplaysAction(FetchReplaysState state)
			=> state;

		[ReducerMethod]
		public static FetchReplaysState ReduceGetFilteredReplaysResultAction(FetchReplaysState state, GetFilteredReplaysResultAction action)
			=> state with { Replays = action.Replays, TotalReplayCount = action.TotalReplayCount };
	}
}
