// <copyright file="UpdateReplayFilterAction.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using ReplayManager.Models;

namespace ReplayManager.Store.ReplayFilterUseCase
{
	public record UpdateReplayFilterAction
	{
		public Func<IQueryable<ReplayInfoOld>, IQueryable<ReplayInfoOld>?>? Filter { get; init; }

		public UpdateReplayFilterAction(Func<IQueryable<ReplayInfoOld>, IQueryable<ReplayInfoOld>?>? filter)
		{
			Filter = filter;
		}
	}
}
