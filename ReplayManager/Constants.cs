// <copyright file="Constants.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using MatBlazor;

namespace ReplayManager
{
	public static class Constants
	{
		public static MatTheme Theme { get; } = new()
		{
			Primary = MatThemeColors.Cyan.A700.Value,
			Secondary = MatThemeColors.Amber._300.Value,
			OnPrimary = "black",
			OnSecondary = "black",
		};

		public static MatTheme ReverseTheme { get; } = new()
		{
			Primary = MatThemeColors.Amber._300.Value,
			Secondary = MatThemeColors.Cyan.A700.Value,
			OnPrimary = "black",
			OnSecondary = "black",
		};
	}
}
