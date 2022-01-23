// <copyright file="ReplayReader.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ReplayManager.Models;
using ReplayManager.Models.Post;
using ReplayManager.Models.Pre;
using System;

namespace ReplayManager.Reader
{
	public static class ReplayReader
	{
		private static readonly JsonSerializerOptions JsonSerializerOptions = new()
		{
			Converters =
			{
				new BoolConverter(),
			},
		};

		public static async Task<ReplayInfo?> GetReplayInfoFromFileAsync(string path, CancellationToken cancellationToken = default)
		{
			ReplayInfo? replayInfo = null;
			try
			{
				using FileStream sourceStream = File.Open(path, FileMode.Open, FileAccess.Read);

				// The number of data blocks
				int blockCount;
				{
					byte[] blockCountInBytes = new byte[4];
					sourceStream.Seek(4, SeekOrigin.Begin);
					sourceStream.Read(blockCountInBytes, 0, 4);
					blockCount = BitConverter.ToInt32(blockCountInBytes, 0);
				}

				for (int blockIndex = 0; blockIndex < blockCount; blockIndex++)
				{
					// The block size; how many bytes to read for the current block
					int blockSize;
					{
						byte[] blockSizeInBytes = new byte[4];
						await sourceStream.ReadAsync(blockSizeInBytes.AsMemory(0, 4), cancellationToken);
						blockSize = BitConverter.ToInt32(blockSizeInBytes, 0);
					}

					SubStream sub = new(sourceStream, 0, blockSize);
					if (blockIndex == 0)
					{
						PreBattleInfo? preBattleInfo = await JsonSerializer
							.DeserializeAsync<PreBattleInfo>(sub, cancellationToken: cancellationToken);
						if (preBattleInfo is not null)
						{
							replayInfo = new ReplayInfo { PreBattleInfo = preBattleInfo };
						}
					}
					else if (blockIndex == 1)
					{
						JsonArray? jsonArray = JsonNode.Parse(sub)?.AsArray();
						if (jsonArray is not null)
						{
							PostBattleInfo? postBattleInfo = await GetPostBattleInfo(jsonArray);
							if (replayInfo is not null)
							{
								//replayInfo.PostBattleInfo = postBattleInfo;
							}
						}
					}
				}
			}
			catch
			{
				replayInfo = null;
			}

			return replayInfo;
		}

		private static async Task<PostBattleInfo?> GetPostBattleInfo(JsonArray jsonArray)
		{
			GeneralInfo? generalInfo = null;

			Dictionary<string, Player>? players = null;

			Dictionary<string, PlayerFrags>? playerFrags = null;

			for (int arrayIndex = 0; arrayIndex < jsonArray.Count; arrayIndex++)
			{
				JsonObject? var2 = jsonArray[arrayIndex]?.AsObject();
				if (var2 is null)
				{
					return null;
				}

				using var memoryStream = new MemoryStream();
				using var writer = new Utf8JsonWriter(memoryStream);
				var2.WriteTo(writer);
				await writer.FlushAsync();
				memoryStream.Position = 0;

				switch (arrayIndex)
				{
					case 0:
						generalInfo = await JsonSerializer.DeserializeAsync<GeneralInfo>(memoryStream, JsonSerializerOptions);
						break;
					case 1:
						players = await JsonSerializer.DeserializeAsync<Dictionary<string, Player>>(memoryStream, JsonSerializerOptions);
						break;
					case 2:
						playerFrags = await JsonSerializer.DeserializeAsync<Dictionary<string, PlayerFrags>>(memoryStream);
						break;
					default:
						break;
				}
			}

			if (generalInfo is not null && players is not null && playerFrags is not null)
			{
				return new(generalInfo, players, playerFrags);
			}

			return null;
		}
	}
}
