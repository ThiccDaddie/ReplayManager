using ReplayManager.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ReplayManager.Services
{
	public interface IReplayLoadingService : INotifyPropertyChanged
	{
		int TotalReplaysCount { get; set; }
		int ReplaysLoaded { get; set; }
		public Stopwatch ElapsedTime { get; }
		Task<IEnumerable<ReplayInfo>> LoadReplays(IEnumerable<string> filePaths, CancellationToken cancellationToken = default);
		IAsyncEnumerable<ReplayInfo> LoadReplaysSequentially(IEnumerable<string> filePaths, CancellationToken cancellationToken = default);
		Task<IEnumerable<ReplayInfo>> LoadAllReplays(CancellationToken cancellationToken = default);
		IAsyncEnumerable<ReplayInfo> LoadAllReplaysSequentially(CancellationToken cancellationToken = default);
	}
}
