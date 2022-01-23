using ReplayManager.Models;

namespace ReplayManager.Store.FetchReplaysUseCase
{
	public record GetFilteredReplaysAction
	{
		public int Skip { get; init; }

		public int Take { get; init; }

		public Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?>? Filter { get; init; }

		public GetFilteredReplaysAction(int skip, int take, Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?>? filter)
		{
			Skip = skip;
			Take = take;
			Filter = filter;
		}
	}
}
