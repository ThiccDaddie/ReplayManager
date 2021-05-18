// <copyright file="ReplayInfo.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;

namespace ReplayManager.Models
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "It's a record you dumbass")]
	public record ReplayInfo(
		string Directory,
		string RelativeFilePath,
		bool IsFilePresent,
		string PlayerVehicle,
		string ClientVersionFromExe,
		string RegionCode,
		int PlayerID,
		string ServerName,
		string MapDisplayName,
		int BattleType,
		string PlayerName,
		bool IsFavorite)
	{
		public DateTime DateTime { get; set; }
	}
}
