using ReplayManager.Models;

namespace ReplayManager.Store.LoadReplaysUseCase
{
	public record LoadReplaysResultAction
	{
		public IEnumerable<ReplayInfo>? Replays { get; init; }

		public LoadReplaysResultAction(IEnumerable<ReplayInfo> replays) => Replays = replays;
	}
}
