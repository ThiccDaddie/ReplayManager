using ReplayManager.Models;

namespace ReplayManager.Store.FetchReplaysUseCase
{
	public record FetchReplaysState
	{
		public List<ReplayInfo>? Replays { get; init; }

		public int TotalReplayCount { get; init; }
	}
}
