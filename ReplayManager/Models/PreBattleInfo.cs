// <copyright file="PreBattleInfo.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

namespace ReplayManager.Models.Pre
{

#pragma warning disable SA1300 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
	public class PreBattleInfo
	{
		public PreBattleInfo(
			string playerVehicle,
			string clientVersionFromXml,
			string clientVersionFromExe,
			string regionCode,
			int playerID,
			string serverName,
			string mapDisplayName,
			string dateTime,
			string mapName,
			string gameplayID,
			int battleType,
			bool hasMods,
			string playerName)
		{
			this.playerVehicle = playerVehicle;
			this.clientVersionFromXml = clientVersionFromXml;
			this.clientVersionFromExe = clientVersionFromExe;
			this.regionCode = regionCode;
			this.playerID = playerID;
			this.serverName = serverName;
			this.mapDisplayName = mapDisplayName;
			this.dateTime = dateTime;
			this.mapName = mapName;
			this.gameplayID = gameplayID;
			this.battleType = battleType;
			this.hasMods = hasMods;
			this.playerName = playerName;
		}

		public Guid PreBattleInfoId { get; set; }

		public string playerVehicle { get; set; }

		public string clientVersionFromXml { get; set; }

		public string clientVersionFromExe { get; set; }

		public Dictionary<string, PreVehicle> vehicles { get; set; } = new();

		public string regionCode { get; set; }

		public int playerID { get; set; }

		public string serverName { get; set; }

		public string mapDisplayName { get; set; }

		public string dateTime { get; set; }

		public string mapName { get; set; }

		public string gameplayID { get; set; }

		public int battleType { get; set; }

		public bool hasMods { get; set; }

		public string playerName { get; set; }

		public Guid ReplayInfoId { get; set; }

		public ReplayInfo ReplayInfo { get; set; } = null!;
	}

	public class PreVehicle
	{
		public Guid PreVehicleId { get; set; }

		public int wtr { get; set; }

		public string vehicleType { get; set; }

		public int isAlive { get; set; }

		//public object[] personalMissionIDs { get; set; }
		//public object[] vehPostProgression { get; set; }
		//public Personalmissioninfo personalMissionInfo { get; set; }
		public bool forbidInBattleInvitations { get; set; }

		public string fakeName { get; set; }

		public int maxHealth { get; set; }

		public int igrType { get; set; }

		public string clanAbbrev { get; set; }

		//public int[] ranked { get; set; }

		public int isTeamKiller { get; set; }

		public int customRoleSlotTypeId { get; set; }

		public int team { get; set; }

		//public Events events { get; set; }
		public int overriddenBadge { get; set; }

		public string avatarSessionID { get; set; }

		//public int[][] badges { get; set; }

		public string name { get; set; }
	}

	public class Personalmissioninfo
	{
	}

	public class Events
	{
	}
}
#pragma warning disable SA1300 // Naming Styles
#pragma warning restore IDE1006 // Naming Styles