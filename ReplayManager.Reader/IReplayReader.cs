using ReplayManager.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace ReplayManager.Reader
{
	public interface IReplayReader
	{
		Task<ReplayInfo> GetReplayInfoFromFile(string path, CancellationToken cancellationToken);
	}
}
