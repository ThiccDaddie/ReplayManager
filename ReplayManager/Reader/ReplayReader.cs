// <copyright file="ReplayReader.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReplayManager.Models;

namespace ReplayManager.Reader
{
	public class ReplayReader : IReplayReader
	{
		public async Task<ReplayInfo> GetReplayInfoFromFile(string path, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			(string replayInfoJson, string _) = await ReadReplayFile(path, cancellationToken);
			return JsonConvert.DeserializeObject<ReplayInfo>(replayInfoJson, new JsonSerializerSettings
			{
				DateFormatString = "dd.MM.yyyy HH:mm:ss",
			});
		}

		private static async Task<(string block1, string block2)> ReadReplayFile(string path, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			byte[] control = await File.ReadAllBytesAsync(path, cancellationToken);

			int blockCount = BitConverter.ToInt32(control.Skip(4).Take(4).ToArray(), 0);

			List<byte[]> blocks = new();

			int dataBlockSzOffset = 8;
			for (int i = 0; i < blockCount; i++)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int blockSize = BitConverter.ToInt32(control.Skip(dataBlockSzOffset).Take(4).ToArray(), 0);
				int dataBlockOffset = dataBlockSzOffset + 4;

				blocks.Add(control.Skip(dataBlockOffset).Take(blockSize).ToArray());

				dataBlockSzOffset = dataBlockOffset + blockSize;
			}

			List<string> data = blocks.Select(block =>
			{
				return Encoding.Default.GetString(block);
			}).ToList();
			return (data.ElementAtOrDefault(0), data.ElementAtOrDefault(1));
		}
	}
}
