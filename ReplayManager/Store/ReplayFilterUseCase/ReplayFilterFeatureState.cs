using Fluxor;

namespace ReplayManager.Store.ReplayFilterUseCase
{
	public class ReplayFilterFeatureState : Feature<ReplayFilterState>
	{
		public override string GetName() => nameof(ReplayFilterState);

		protected override ReplayFilterState GetInitialState() => new()
		{
			Filter = null,
		};
	}
}
