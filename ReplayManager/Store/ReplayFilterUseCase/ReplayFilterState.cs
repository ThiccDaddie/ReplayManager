using ReplayManager.Models;

namespace ReplayManager.Store.ReplayFilterUseCase
{
	public record ReplayFilterState
	{
		public Func<IQueryable<ReplayInfoOld>, IQueryable<ReplayInfoOld>?>? Filter { get; init; }
	}
}
