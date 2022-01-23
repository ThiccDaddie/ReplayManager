// <copyright file="ReplayInfo.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using ReplayManager.Models.Post;
using ReplayManager.Models.Pre;

namespace ReplayManager.Models
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "It's a record you dumbass")]
	public record ReplayInfoOld(
		string Directory,
		string RelativeFilePath,
		bool IsFilePresent,
		string PlayerVehicle,
		string ClientVersionFromXml,
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

	[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "It's a record you dumbass")]
	public class ReplayInfo
	{
		public Guid ReplayInfoId { get; set; }

		public string Directory { get; set; } = null!;

		public string Path { get; set; } = null!;

		public PreBattleInfo PreBattleInfo { get; set; } = null!;
	}
}
