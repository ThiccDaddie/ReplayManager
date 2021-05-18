// <copyright file="Favorites.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Linq;
using ReplayManager.Models;

namespace ReplayManager.Pages
{
	public partial class Favorites
	{
		private static IQueryable<ReplayInfo> ReplayInfoQueryModifier(IQueryable<ReplayInfo> replays)
		{
			return replays.Where(replay => replay.IsFavorite == true);
		}
	}
}
