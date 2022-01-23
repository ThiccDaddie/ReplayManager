namespace ReplayManager.Store.LoadReplaysUseCase
{
	public record UpdateLoadReplaysCounterAction
	{
		public int ReplaysLoaded { get; init; }

		public int TotalReplays { get; init; }

		public UpdateLoadReplaysCounterAction(int replaysLoaded, int totalReplays)
		{
			ReplaysLoaded = replaysLoaded;
			TotalReplays = totalReplays;
		}
	}
}
