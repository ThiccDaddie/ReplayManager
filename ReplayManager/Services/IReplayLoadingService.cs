using ReplayManager.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace ReplayManager.Services
{
	public interface IReplayLoadingService : INotifyPropertyChanged
	{
		int TotalReplaysCount { get; set; }
		int ReplaysLoaded { get; set; }
		bool IsLoading { get; set; }
		bool DidLoadingSucceed { get; set; }
		Stopwatch ElapsedTime { get; }
		void LoadReplays(IEnumerable<string> filePaths, CancellationToken cancellationToken = default);
		IAsyncEnumerable<ReplayInfo> LoadReplaysSequentially(IEnumerable<string> filePaths, CancellationToken cancellationToken = default);
	}
}
