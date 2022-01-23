// <copyright file="FileHelpers.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

namespace ReplayManager.Helpers
{
	public static class FileHelpers
	{
		public static IEnumerable<string> GetFilePaths(List<string> directory)
			=> directory.SelectMany(path => Directory.EnumerateFiles(path, "*.wotreplay", SearchOption.AllDirectories));

		public static IEnumerable<string> FindReplayFolders()
			=> DriveInfo.GetDrives()
				.ToList()
				.SelectMany(drive =>
				{
					IEnumerable<string> paths = new List<string>();
					DirectoryInfo directoryInfo = drive.RootDirectory;
					string gamesPath = Path.Combine(directoryInfo.FullName, "games");
					if (Directory.Exists(gamesPath))
					{
						string[] wotFolders = Directory.GetDirectories(gamesPath, "World_of_Tanks*");
						paths = wotFolders
							.Select(wotFolder =>
							{
								return Path.Combine(wotFolder, "replays");
							})
							.Where(path => Directory.Exists(path));
					}

					return paths;
				});
	}
}
