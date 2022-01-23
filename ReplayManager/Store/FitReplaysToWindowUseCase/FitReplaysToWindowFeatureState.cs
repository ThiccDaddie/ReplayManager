using Fluxor;

namespace ReplayManager.Store.FitReplaysToWindowUseCase
{
	public class FitReplaysToWindowFeatureState : Feature<FitReplaysToWindowState>
	{
		public override string GetName() => nameof(FitReplaysToWindowState);

		protected override FitReplaysToWindowState GetInitialState() => new()
		{
			MaxReplays = 0,
		};
	}
}
