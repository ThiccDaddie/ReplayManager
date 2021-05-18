// <copyright file="IReplayLoadingService.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace ReplayManager.Services
{
	public interface IReplayLoadingService : INotifyPropertyChanged
	{
		int TotalReplaysCount { get; set; }

		int ReplaysLoaded { get; set; }

		bool IsLoading { get; set; }

		bool LoadedSuccesfully { get; set; }

		Stopwatch ElapsedTime { get; }

		void Start(IEnumerable<(string directory, string path)> filePaths, CancellationToken cancellationToken = default);
	}
}
