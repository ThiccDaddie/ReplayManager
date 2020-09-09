using MatBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Client.Services
{
	public class ReplaySortService
	{
		public IEnumerable<ReplayInfo> OrderedReplayInfos { get; private set; }

		private string SortId { get; set; }
		private MatSortDirection SortDirection { get; set; }

		public ReplaySortService()
		{
			OrderedReplayInfos = new List<ReplayInfo>();
		}

		public void SetReplays(List<ReplayInfo> replays)
		{
			if (replays is null)
			{
				throw new System.ArgumentNullException(nameof(replays));
			}

			SortReplaysDefault(replays);
		}

		public void SortReplays(string sortId, MatSortDirection sortDirection)
		{
			PropertyInfo prop = typeof(ReplayInfo).GetProperty(sortId);
			if (sortId == SortId && sortDirection != SortDirection && sortDirection != MatSortDirection.None && SortDirection != MatSortDirection.None)
			{
				OrderedReplayInfos = OrderedReplayInfos.Reverse();
			}
			else if (sortDirection == MatSortDirection.Desc)
			{
				OrderedReplayInfos = OrderedReplayInfos.OrderBy(x => prop.GetValue(x, null));
			}
			else if (sortDirection == MatSortDirection.Asc)
			{
				OrderedReplayInfos = OrderedReplayInfos.OrderByDescending(x => prop.GetValue(x, null));
			}
			else
			{
				SortReplaysDefault();
			}
			SortId = sortId;
			SortDirection = sortDirection;
		}

		private void SortReplaysDefault(List<ReplayInfo> replays)
		{
			OrderedReplayInfos = replays.OrderByDescending(replay => replay.DateTime);
		}

		private void SortReplaysDefault()
		{
			OrderedReplayInfos = OrderedReplayInfos.OrderByDescending(replay => replay.DateTime);
		}
	}
}
