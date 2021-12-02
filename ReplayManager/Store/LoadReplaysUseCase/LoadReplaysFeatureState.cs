using Fluxor;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public class LoadReplaysFeatureState : Feature<LoadReplaysState>
	{
		public override string GetName() => nameof(LoadReplaysState);

		protected override LoadReplaysState GetInitialState() => new()
		{
			IsLoading = false,
			DoneLoading = false,
			ReplaysLoaded = 0,
			TotalReplays = 0,
		};
	}
}
