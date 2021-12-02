using ReplayManager.Models;

namespace ReplayManager.Store.ReplayFilterUseCase
{
	public record ReplayFilterState
	{
		public Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?>? Filter { get; init; }
	}
}
