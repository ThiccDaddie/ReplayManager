// <copyright file="UpdateReplayFilterAction.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using ReplayManager.Models;

namespace ReplayManager.Store.ReplayFilterUseCase
{
	public record UpdateReplayFilterAction
	{
		public Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?>? Filter { get; init; }

		public UpdateReplayFilterAction(Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>?>? filter)
		{
			Filter = filter;
		}
	}
}
