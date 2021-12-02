namespace ReplayManager.Store.LoadReplaysUseCase
{
	public record UpdateLoadReplaysCounterAction
	{
		public int ReplaysLoaded { get; init; }

		public int TotalReplaysCount { get; init; }

		public UpdateLoadReplaysCounterAction(int replaysLoaded, int totalReplaysCount)
		{
			ReplaysLoaded = replaysLoaded;
			TotalReplaysCount = totalReplaysCount;
		}
	}
}
