using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThiccDaddie.ReplayManager.Shared
{
	public class ReplayReader
	{
		public static async Task<ReplayInfo> GetReplayInfoFromFile(string path, CancellationToken cancellationToken)
		{
			(string replayInfoJson, string replayDetailsJson) = await ReadReplayFile(path, cancellationToken);
			return JsonConvert.DeserializeObject<ReplayInfo>(replayInfoJson, new JsonSerializerSettings
			{
				DateFormatString = "dd.MM.yyyy HH:mm:ss"
			});
		}
		private static async Task<(string, string)> ReadReplayFile(string path, CancellationToken cancellationToken)
		{
			byte[] control = await File.ReadAllBytesAsync(path);

			int blockCount = BitConverter.ToInt32(control.Skip(4).Take(4).ToArray(), 0);

			//if (blockCount < 2)
			//{
			//	throw new Exception();
			//}

			List<byte[]> blocks = new List<byte[]>();

			int dataBlockSzOffset = 8;
			for (int i = 0; i < blockCount; i++)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int blockSize = BitConverter.ToInt32(control.Skip(dataBlockSzOffset).Take(4).ToArray(), 0);
				int dataBlockOffset = dataBlockSzOffset + 4;

				blocks.Add(control.Skip(dataBlockOffset).Take(blockSize).ToArray());

				dataBlockSzOffset = dataBlockOffset + blockSize;
			}

			List<string> test = blocks.Select(block =>
			{
				return Encoding.Default.GetString(block);
			}).ToList();
			return (test.ElementAtOrDefault(0), test.ElementAtOrDefault(1));
		}
	}
}
