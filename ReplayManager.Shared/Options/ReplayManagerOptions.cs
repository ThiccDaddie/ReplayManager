using System.Collections.Generic;

namespace ReplayManager.Shared
{
	public record ReplayManagerOptions(List<string> ReplayDirectories)
	{
		public ReplayManagerOptions() : this(new List<string>())
		{

		}

		public const string ReplayManager = "ReplayManager";
	}
}
