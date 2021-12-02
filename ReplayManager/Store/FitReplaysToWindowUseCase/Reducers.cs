using Fluxor;

namespace ReplayManager.Store.FitReplaysToWindowUseCase
{
	public static class Reducers
	{
		[ReducerMethod]
		public static FitReplaysToWindowState ReduceSetMaxReplaysAction(FitReplaysToWindowState state, SetMaxReplaysAction action)
			=> state with { MaxReplays = action.MaxReplays };
	}
}
