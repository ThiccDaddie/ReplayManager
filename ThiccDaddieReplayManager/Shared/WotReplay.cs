using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThiccDaddie.ReplayManager.Shared
{
	public class WotReplay
	{
		public static IEnumerable<string> GetReplayFileNames(string replayDirectory)
		{
			Uri rootUri = new Uri(replayDirectory);
			foreach (var fileInfo in Directory
				.EnumerateFiles(replayDirectory, "*.wotreplay", SearchOption.AllDirectories).Select(fileName => new FileInfo(fileName)).OrderByDescending(fileInfo => fileInfo.CreationTime))
			{
				Uri replayUri = new Uri(fileInfo.FullName);
				yield return rootUri.MakeRelativeUri(replayUri).ToString();
			}
		}

		public static int GetReplayFileCount(string replayDirectory)
		{
			return Directory.EnumerateFiles(replayDirectory, "*.wotreplay", SearchOption.AllDirectories).Count();
		}

		public static async Task<ReplayInfo> DecodeReplay(string path, CancellationToken cancellationToken)
		{
			return await ReplayReader.GetReplayInfoFromFile(path, cancellationToken);
		}
	}
}
