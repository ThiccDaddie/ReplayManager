using Fluxor;

namespace ReplayManager.Store.FetchReplaysUseCase
{
	public class FetchReplaysFeatureState : Feature<FetchReplaysState>
	{
		public override string GetName() => nameof(FetchReplaysState);

		protected override FetchReplaysState GetInitialState() => new()
		{
			Replays = new(),
			TotalReplayCount = 0,
		};
	}
}
