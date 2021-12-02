using Fluxor;
using Microsoft.EntityFrameworkCore;
using ReplayManager.DataAccess;
using ReplayManager.Models;

namespace ReplayManager.Store.FetchReplaysUseCase
{
	public class Effects
	{
		[EffectMethod]
		public async Task HandleGetFilteredReplaysAction(GetFilteredReplaysAction action, IDispatcher dispatcher)
		{
			var filter = action.Filter;
			var replays = new List<ReplayInfo>();
			int totalReplayCount = 0;
			using ReplaysContext context = new();
			IQueryable<ReplayInfo>? queryable;
			if (context.Replays is not null)
			{
				totalReplayCount = await context.Replays.CountAsync();
				queryable = context.Replays;
				if (filter is not null)
				{
					queryable = filter(context.Replays);
					if (queryable is null)
					{
						queryable = context.Replays;
					}
				}

				replays = await
					queryable
					.Skip(action.Skip)
					.Take(action.Take)
					.ToListAsync();

				dispatcher.Dispatch(new GetFilteredReplaysResultAction(replays, totalReplayCount));
			}
		}
	}
}
