namespace ReplayManager.Store.LoadReplaysUseCase
{
	public record LoadReplaysResultAction
	{
		public int TotalReplaysLoaded { get; init; }

		public LoadReplaysResultAction(int totalReplaysLoaded)
		{
			TotalReplaysLoaded = totalReplaysLoaded;
		}
	}
}
