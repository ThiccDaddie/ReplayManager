// <copyright file="IReplayReader.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using ReplayManager.Models;

namespace ReplayManager.Reader
{
	public interface IReplayReader
	{
		Task<ReplayInfo?> GetReplayInfoFromFileAsync(string path, CancellationToken cancellationToken = default);
	}
}
