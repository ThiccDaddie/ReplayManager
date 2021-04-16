using ReplayManager.Shared;
using System.Threading.Tasks;

namespace ReplayManager.Services
{
	public interface IOptionsWriter
	{
		Task WriteOptionsAsync(ReplayManagerOptions options);
	}
}
