// <copyright file="ReplayReader.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Text;
using Newtonsoft.Json;
using ReplayManager.Models;

namespace ReplayManager.Reader
{
	public class ReplayReader : IReplayReader
	{
		public async Task<ReplayInfo?> GetReplayInfoFromFileAsync(string path, CancellationToken cancellationToken = default)
		{
			try
			{
				// The offset in bytes at which you can find the "block size"
				int dataBlockSzOffset = 8;

				// The block size in bytes
				byte[] blockSizeInBytes = new byte[4];

				// The block size
				int blockSize;

				// The block of data
				byte[] block;
				using (FileStream sourceStream = File.Open(path, FileMode.Open, FileAccess.Read))
				{
					// skip ahead until the block size
					sourceStream.Seek(dataBlockSzOffset, SeekOrigin.Begin);

					// get the blocksize
					await sourceStream.ReadAsync(blockSizeInBytes.AsMemory(0, 4), cancellationToken);

					// convert the blocksize to int
					blockSize = BitConverter.ToInt32(blockSizeInBytes, 0);

					block = new byte[blockSize];

					// get the block of data
					await sourceStream.ReadAsync(block.AsMemory(0, blockSize), cancellationToken);
				}

				// decode the data
				string replayJson = Encoding.Default.GetString(block);

				var replay = JsonConvert.DeserializeObject<ReplayInfo>(replayJson, new JsonSerializerSettings
				{
					DateFormatString = "dd.MM.yyyy HH:mm:ss",
				});
				return replay;
			}
			catch
			{
			}

			return null;
		}
	}
}
