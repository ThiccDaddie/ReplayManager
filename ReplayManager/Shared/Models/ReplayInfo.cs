using System;

namespace ThiccDaddie.ReplayManager.Shared
{
	public class ReplayInfo
	{
		public string ReplayInfoId { get; set; }
		public bool IsFilePresent { get; set; }
		public string PlayerVehicle { get; set; }
		public string ClientVersionFromExe { get; set; }
		public string RegionCode { get; set; }
		public int PlayerID { get; set; }
		public string ServerName { get; set; }
		public string MapDisplayName { get; set; }
		public DateTime DateTime { get; set; }
		public int BattleType { get; set; }
		public string PlayerName { get; set; }
	}
}
