using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReplayManager.Helpers
{
	public static class FileHelpers
	{
		public static IEnumerable<string> GetFilePaths(List<string> directory) 
			=> directory.SelectMany(path => Directory.EnumerateFiles(path, "*.wotreplay", SearchOption.AllDirectories));
	}
}
