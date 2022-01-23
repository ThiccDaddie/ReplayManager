namespace ReplayManager.Store.FitReplaysToWindowUseCase
{
	public record SetMaxReplaysAction
	{
		public int MaxReplays { get; init; }

		public SetMaxReplaysAction(int maxReplays) => MaxReplays = maxReplays;
	}
}
