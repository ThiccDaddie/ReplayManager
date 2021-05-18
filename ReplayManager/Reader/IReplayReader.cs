// <copyright file="IReplayReader.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using ReplayManager.Models;

namespace ReplayManager.Reader
{
	public interface IReplayReader
	{
		Task<ReplayInfo> GetReplayInfoFromFile(string path, CancellationToken cancellationToken);
	}
}
