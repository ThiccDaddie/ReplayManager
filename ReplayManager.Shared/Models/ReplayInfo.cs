using System;

namespace ReplayManager.Shared
{
	public record ReplayInfo(
		string ReplayInfoId,
		bool IsFilePresent,
		string PlayerVehicle,
		string ClientVersionFromExe,
		string RegionCode,
		int PlayerID,
		string ServerName,
		string MapDisplayName,
		DateTime DateTime,
		int BattleType,
		string PlayerName,
		bool IsFavorite
		);
}
