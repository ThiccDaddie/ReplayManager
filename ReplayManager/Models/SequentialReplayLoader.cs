using ReplayManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ReplayManager.Models
{
	public class SequentialReplayLoader
	{
		private Func<CancellationToken, IAsyncEnumerable<ReplayInfo>> LoadReplays { get; }
		public int ReplaysLoaded { get; set; }
		public int TotalReplayCount { get; set; }
		public List<ReplayInfo> Replays { get; set; } = new List<ReplayInfo>();

		public SequentialReplayLoader()
		{

		}

		//public async void StartLoading()
		//{
		//	await foreach (ReplayInfo replay in LoadReplays(CancellationToken.None))
		//	{
		//		Replays.Add()
		//	}
		//}
	}
}
