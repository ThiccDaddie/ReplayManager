// <copyright file="ReplayManagerOptions.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace ReplayManager.Models
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "It's a record you dumbass")]
	public record ReplayManagerOptions(List<string> ReplayDirectories)
	{
		public ReplayManagerOptions()
			: this(new List<string>())
		{
		}

		public const string ReplayManager = "ReplayManager";
	}
}
