using Fluxor;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	//[FeatureState]
	public record LoadReplaysState
	{
		public bool IsLoading { get; init; }

		public bool DoneLoading { get; init; }

		public int ReplaysLoaded { get; init; }

		public int TotalReplays { get; init; }
	}
}
