// <copyright file="IOptionsWriter.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ReplayManager.Models;

namespace ReplayManager.Services
{
	public interface IOptionsWriter
	{
		Task WriteOptionsAsync(ReplayManagerOptions options);
	}
}
