// <copyright file="Favorites.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using ReplayManager.Models;

namespace ReplayManager.Pages
{
	public partial class Favorites
	{
		private static IQueryable<ReplayInfoOld> ReplayInfoQueryModifier(IQueryable<ReplayInfoOld> replays)
		{
			return replays.Where(replay => replay.IsFavorite == true);
		}
	}
}
