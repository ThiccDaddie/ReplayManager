// <copyright file="PostBattleInfo.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

namespace ReplayManager.Models.Post;

public class PostBattleInfo
{
	public GeneralInfo generalInfo { get; set; }

	public Dictionary<string, Player> players { get; set; }

	public Dictionary<string, PlayerFrags> playerFrags { get; set; }

	public PostBattleInfo(GeneralInfo generalInfo, Dictionary<string, Player> players, Dictionary<string, PlayerFrags> playerFrags)
	{
		this.generalInfo = generalInfo;
		this.players = players;
		this.playerFrags = playerFrags;
	}
}

public class GeneralInfo
{
	public long arenaUniqueID { get; set; }

	public Personal personal { get; set; }

	public Dictionary<string, List<PostVehicle>> vehicles { get; set; }

	public Dictionary<string, Avatar> avatars { get; set; }

	public Dictionary<string, PlayerInfo> players { get; set; }

	public Common common { get; set; }
	//public _203679321 _20367932 { get; set; }
}

public class Personal
{
	public Dictionary<string, PersonalPerformance> performanceByTank { get; set; }

	public PersonalAvatar avatar { get; set; }
}

public class PersonalPerformance
{
	//public object[] eventEventCoinList { get; set; }
	public int vehTypeLockTime { get; set; }

	public int markOfMastery { get; set; }

	public int gold { get; set; }

	public int directHits { get; set; }

	public int creditsToDraw { get; set; }

	public int orderFreeXPFactor100 { get; set; }

	public int orderXPFactor100 { get; set; }

	public int damageAssistedRadio { get; set; }

	public int stunDuration { get; set; }

	//public object freeXPReplay { get; set; }
	public int originalPremSquadCredits { get; set; }

	public int destructiblesHits { get; set; }

	public int xpother { get; set; }

	public int creditsContributionIn { get; set; }

	public int premiumTmenXPFactor100 { get; set; }

	public int eventCredits { get; set; }

	public int piggyBank { get; set; }

	//public object[] eventXPList { get; set; }
	public int stunned { get; set; }

	public int achievementXP { get; set; }

	public int igrXPFactor10 { get; set; }

	public int premiumCreditsFactor100 { get; set; }

	public int originalCreditsContributionIn { get; set; }

	public int winAloneAgainstVehicleCount { get; set; }

	public int originalCreditsPenalty { get; set; }

	public int damagedWhileMoving { get; set; }

	public int kills { get; set; }

	public int eventTMenXP { get; set; }

	public float percentFromTotalTeamDamage { get; set; }

	public int premiumPlusXPFactor100 { get; set; }

	public int bpcoin { get; set; }

	public int originalTMenXP { get; set; }

	//public object[] eventBpcoinList { get; set; }
	public int noDamageDirectHitsReceived { get; set; }

	public int equipmentDamageDealt { get; set; }

	public int premiumPlusTmenXPFactor100 { get; set; }

	public int boosterCredits { get; set; }

	public int originalGold { get; set; }

	//public object[][] eventFreeXPList { get; set; }
	public int tkills { get; set; }

	public int winPoints { get; set; }

	public int shots { get; set; }

	public int team { get; set; }

	public int referral20Credits { get; set; }

	public int deathCount { get; set; }

	public int subtotalEventCoin { get; set; }

	public int referral20XP { get; set; }

	public int stunNum { get; set; }

	public int spotted { get; set; }

	public int subtotalTMenXP { get; set; }

	public int killerID { get; set; }

	public int boosterCreditsFactor100 { get; set; }

	public int damagedHp { get; set; }

	public int soloFlagCapture { get; set; }

	public int marksOnGun { get; set; }

	public int premiumVehicleXPFactor100 { get; set; }

	public int[] autoLoadCost { get; set; }

	public int additionalXPFactor10 { get; set; }

	public int eventBpcoin { get; set; }

	public int subtotalCrystal { get; set; }

	public int killedAndDamagedByAllSquadmates { get; set; }

	public int index { get; set; }

	public int rolloutsCount { get; set; }

	//public object[] eventGoldFactor100List { get; set; }
	//public object[] inBattleAchievements { get; set; }
	public int creditsContributionOut { get; set; }

	public int directEnemyHits { get; set; }

	public int originalCreditsContributionOutSquad { get; set; }

	//public object damageEventList { get; set; }
	public int premiumPlusCreditsFactor100 { get; set; }

	public int health { get; set; }

	public bool stopRespawn { get; set; }

	public int[] achievements { get; set; }

	public int orderFreeXP { get; set; }

	//public object[] eventGoldList { get; set; }
	public int boosterTMenXPFactor100 { get; set; }

	//public object[][] dossierPopUps { get; set; }
	public int tdamageDealt { get; set; }

	public int resourceAbsorbed { get; set; }

	public int credits { get; set; }

	public int[] autoEquipCost { get; set; }

	public Dictionary<string, int> setupsIndexes { get; set; }

	//public object[] eventTMenXPFactor100List { get; set; }
	//public object xpReplay { get; set; }
	public int appliedPremiumXPFactor100 { get; set; }

	//public object goldReplay { get; set; }
	public int damagedWhileEnemyMoving { get; set; }

	public int potentialDamageReceived { get; set; }

	public int damageReceived { get; set; }

	public float percentFromSecondBestDamage { get; set; }

	public int destructiblesDamageDealt { get; set; }

	public int explosionHits { get; set; }

	public int boosterXP { get; set; }

	public int lifeTime { get; set; }

	public int factualFreeXP { get; set; }

	public int dailyXPFactor10 { get; set; }

	//public object tmenXPReplay { get; set; }
	public int damageRating { get; set; }

	public int repair { get; set; }

	public int originalCredits { get; set; }

	public int playerRankXPFactor100 { get; set; }

	public int damageAssistedTrack { get; set; }

	public int xpPenalty { get; set; }

	public int referral20CreditsFactor100 { get; set; }

	public int aogasFactor10 { get; set; }

	public int[][] xpByTmen { get; set; }

	public int orderCredits { get; set; }

	public int sniperDamageDealt { get; set; }

	public int fairplayFactor10 { get; set; }

	public int orderCreditsFactor100 { get; set; }

	public int originalCrystal { get; set; }

	public int damageBlockedByArmor { get; set; }

	public int xp { get; set; }

	public int boosterXPFactor100 { get; set; }

	public int damageReceivedFromInvisibles { get; set; }

	public int refSystemXPFactor10 { get; set; }

	public int orderTMenXP { get; set; }

	public int[] flagActions { get; set; }

	public int originalXPPenalty { get; set; }

	public int orderTMenXPFactor100 { get; set; }

	public int numRecovered { get; set; }

	public int appliedPremiumCreditsFactor100 { get; set; }

	public int maxHealth { get; set; }

	public int premSquadCredits { get; set; }

	public int[] autoEquipBoostersCost { get; set; }

	public int premSquadCreditsFactor100 { get; set; }

	public int subtotalXP { get; set; }

	public int squadXP { get; set; }

	public int originalCreditsContributionOut { get; set; }

	public int premMask { get; set; }

	public int originalFreeXP { get; set; }

	public int movingAvgDamage { get; set; }

	public int xpassist { get; set; }

	public int freeXP { get; set; }

	public int orderXP { get; set; }

	public int premiumVehicleXP { get; set; }

	public int flagCapture { get; set; }

	//public object[] eventCreditsList { get; set; }
	public int eventGold { get; set; }

	//public Questsprogress questsProgress { get; set; }

	//public object bpcoinReplay { get; set; }
	public int originalCreditsContributionInSquad { get; set; }

	public int originalEventCoin { get; set; }

	public int eventXP { get; set; }

	public int factualCredits { get; set; }

	public int eventCoin { get; set; }

	public int subtotalFreeXP { get; set; }

	public int crystal { get; set; }

	//public object crystalReplay { get; set; }
	//public C11nprogress c11nProgress { get; set; }

	public int originalXP { get; set; }

	public int originalCreditsToDrawSquad { get; set; }

	public int numDefended { get; set; }

	public int achievementFreeXP { get; set; }

	public int subtotalCredits { get; set; }

	public int killsBeforeTeamWasDamaged { get; set; }

	public int playerRankXP { get; set; }

	public int creditsPenalty { get; set; }

	public int vehicleNumCaptured { get; set; }

	public int squadXPFactor100 { get; set; }

	public int directTeamHits { get; set; }

	public int damageDealt { get; set; }

	public int referral20XPFactor100 { get; set; }

	public int piercingsReceived { get; set; }

	public int appliedPremiumTmenXPFactor100 { get; set; }

	public int destructiblesNumDestroyed { get; set; }

	public int piercingEnemyHits { get; set; }

	//public object creditsReplay { get; set; }
	public int subtotalBpcoin { get; set; }

	public int piercings { get; set; }

	public int prevMarkOfMastery { get; set; }

	public object[] eventFreeXPFactor100List { get; set; }

	public int serviceProviderID { get; set; }

	public int droppedCapturePoints { get; set; }

	public int eventEventCoin { get; set; }

	public int damaged { get; set; }

	public int typeCompDescr { get; set; }

	public int deathReason { get; set; }

	public int capturePoints { get; set; }

	public int damageBeforeTeamWasDamaged { get; set; }

	public int factualXP { get; set; }

	public int explosionHitsReceived { get; set; }

	public Dictionary<string, Detail> details { get; set; }

	//public object[] eventXPFactor100List { get; set; }
	public int boosterTMenXP { get; set; }

	public int achievementCredits { get; set; }

	public int originalCreditsToDraw { get; set; }

	public bool isPremium { get; set; }

	public int mileage { get; set; }

	public bool committedSuicide { get; set; }

	public int xpattack { get; set; }

	public int eventFreeXP { get; set; }

	//public object avatarDamageEventList { get; set; }
	public int subtotalGold { get; set; }

	public int directHitsReceived { get; set; }

	public int accountDBID { get; set; }

	public int eventCrystal { get; set; }

	//public object flXPReplay { get; set; }
	public int premiumXPFactor100 { get; set; }

	//public object eventCoinReplay { get; set; }
	public int autoRepairCost { get; set; }

	//public object[] eventCreditsFactor100List { get; set; }
	public bool isTeamKiller { get; set; }

	public int tmenXP { get; set; }

	//public object[] eventTMenXPList { get; set; }
	//public object capturingBase { get; set; }
	public int damageAssistedStun { get; set; }

	public int originalCreditsPenaltySquad { get; set; }

	public int boosterFreeXPFactor100 { get; set; }

	public int damageAssistedSmoke { get; set; }

	public int boosterFreeXP { get; set; }

	public int tdestroyedModules { get; set; }

	//public object[] eventCrystalList { get; set; }
	public int damageAssistedInspire { get; set; }

	public int originalBpcoin { get; set; }

	public int battleNum { get; set; }
}

//public class Questsprogress
//{
//	public Dq6119926AQUEST_BONUSC412__4500REtt__1R471__25R409__2000[] dq6119926aQUEST_BONUSc412__4500rett__1r471__25r409__2000 { get; set; }
//}

//public class Dq6119926AQUEST_BONUSC412__4500REtt__1R471__25R409__2000
//{
//	public int bonusCount { get; set; }

//	public int originalXP { get; set; }
//}

//public class C11nprogress
//{
//	public _128636 _128636 { get; set; }

//	public _129404 _129404 { get; set; }
//}

//public class _128636
//{
//	public Progress progress { get; set; }

//	public int level { get; set; }
//}

//public class Progress
//{
//	public int[] a15x15frags { get; set; }
//}

//public class _129404
//{
//	public Progress1 progress { get; set; }

//	public int level { get; set; }
//}

//public class Progress1
//{
//	public int[] a15x15battlesCount { get; set; }
//}

public class Detail
{
	public int spotted { get; set; }

	public int damageAssistedTrack { get; set; }

	public int damageDealt { get; set; }

	public int piercingEnemyHits { get; set; }

	public int damageAssistedRadio { get; set; }

	public int rickochetsReceived { get; set; }

	public int stunDuration { get; set; }

	public int piercings { get; set; }

	public int damageBlockedByArmor { get; set; }

	public int crits { get; set; }

	public int deathReason { get; set; }

	public int directEnemyHits { get; set; }

	public int targetKills { get; set; }

	public int fire { get; set; }

	public int noDamageDirectHitsReceived { get; set; }

	public int damageAssistedStun { get; set; }

	public int damageReceived { get; set; }

	public int damageAssistedSmoke { get; set; }

	public int explosionHits { get; set; }

	public int directHits { get; set; }

	public int damageAssistedInspire { get; set; }

	public int stunNum { get; set; }
}

public class PersonalAvatar
{
	public int basePointsDiff { get; set; }

	public int avatarDamageDealt { get; set; }

	//public object bpcoinReplay { get; set; }

	//public object creditsReplay { get; set; }

	//public object freeXPReplay { get; set; }

	public int sumPoints { get; set; }

	public int[] fairplayViolations { get; set; }

	public int eventBpcoin { get; set; }

	public int?[][] badges { get; set; }

	public Dictionary<string, int> activeRents { get; set; }

	public int eventFreeXP { get; set; }

	public int eventCredits { get; set; }

	//public object xpReplay { get; set; }

	public int crystal { get; set; }

	//public object damageEventList { get; set; }

	public bool eligibleForCrystalRewards { get; set; }

	//public Dogtags dogTags { get; set; }

	public bool isPrematureLeave { get; set; }

	//public object squadBonusInfo { get; set; }

	public int winnerIfDraw { get; set; }

	public int eventCrystal { get; set; }

	public int freeXP { get; set; }

	public int avatarKills { get; set; }

	public int eventTMenXP { get; set; }

	//public object avatarDamageEventList { get; set; }

	//public Pm2progress PM2Progress { get; set; }

	public bool hasBattlePass { get; set; }

	public int totalDamaged { get; set; }

	//public object[] vseBattleResults { get; set; }

	//public object goldReplay { get; set; }

	//public object[] recruitsIDs { get; set; }

	public int eventGold { get; set; }

	//public object tmenXPReplay { get; set; }

	//public object eventCoinReplay { get; set; }

	//public Questsprogress1 questsProgress { get; set; }

	public int accountDBID { get; set; }

	//public object[] avatarAmmo { get; set; }

	public int fareTeamXPPosition { get; set; }

	public int eventXP { get; set; }

	//public object[] fortClanDBIDs { get; set; }

	public int xp { get; set; }

	public int playerRank { get; set; }

	public int avatarDamaged { get; set; }

	public int recruiterID { get; set; }

	//public object progressiveReward { get; set; }

	//public object crystalReplay { get; set; }

	public int rankChange { get; set; }

	public int team { get; set; }

	public int clanDBID { get; set; }

	public int credits { get; set; }

	public int eventEventCoin { get; set; }

	public bool watchedBattleToTheEnd { get; set; }

	//public object flXPReplay { get; set; }
}

//public class Dogtags
//{
//	public object[] upgradedComps { get; set; }

//	public object[] unlockedComps { get; set; }
//}

//public class Pm2progress
//{
//}

//public class Questsprogress1
//{
//	public Dq6119926AQUEST_BONUSC412__4500REtt__1R471__25R409__20001[] dq6119926aQUEST_BONUSc412__4500rett__1r471__25r409__2000 { get; set; }
//}

//public class Dq6119926AQUEST_BONUSC412__4500REtt__1R471__25R409__20001
//{
//	public int bonusCount { get; set; }

//	public int originalXP { get; set; }
//}

public class PostVehicle
{
	public int spotted { get; set; }

	public int vehicleNumCaptured { get; set; }

	public int damageAssistedTrack { get; set; }

	public int xpPenalty { get; set; }

	public int directTeamHits { get; set; }

	public int damageReceived { get; set; }

	public int piercingsReceived { get; set; }

	public int sniperDamageDealt { get; set; }

	public int piercingEnemyHits { get; set; }

	public int damageAssistedRadio { get; set; }

	public int mileage { get; set; }

	public decimal stunDuration { get; set; }

	public int piercings { get; set; }

	public int damageBlockedByArmor { get; set; }

	public int xp { get; set; }

	public int droppedCapturePoints { get; set; }

	public int killerID { get; set; }

	public int xpother { get; set; }

	public int index { get; set; }

	public int directHitsReceived { get; set; }

	public int damageReceivedFromInvisibles { get; set; }

	public int explosionHitsReceived { get; set; }

	public int achievementXP { get; set; }

	public int deathReason { get; set; }

	public int capturePoints { get; set; }

	public int numRecovered { get; set; }

	public int directEnemyHits { get; set; }

	public int maxHealth { get; set; }

	//public object damageEventList { get; set; }

	public int health { get; set; }

	public bool stopRespawn { get; set; }

	public int achievementCredits { get; set; }

	public int[] achievements { get; set; }

	public int xpassist { get; set; }

	public int shots { get; set; }

	public int kills { get; set; }

	public int deathCount { get; set; }

	public int damagedHp { get; set; }

	public int flagCapture { get; set; }

	public int damaged { get; set; }

	public int tdamageDealt { get; set; }

	public int resourceAbsorbed { get; set; }

	public int credits { get; set; }

	public int accountDBID { get; set; }

	public int lifeTime { get; set; }

	public int noDamageDirectHitsReceived { get; set; }

	public int numDefended { get; set; }

	public int stunned { get; set; }

	public int equipmentDamageDealt { get; set; }

	public bool isTeamKiller { get; set; }

	public int typeCompDescr { get; set; }

	public int soloFlagCapture { get; set; }

	public int destructiblesHits { get; set; }

	//public object capturingBase { get; set; }

	public int damageAssistedStun { get; set; }

	public int rolloutsCount { get; set; }

	public int tkills { get; set; }

	public int potentialDamageReceived { get; set; }

	public int damageDealt { get; set; }

	public int destructiblesNumDestroyed { get; set; }

	public int damageAssistedSmoke { get; set; }

	public int destructiblesDamageDealt { get; set; }

	public int[] flagActions { get; set; }

	public int winPoints { get; set; }

	public int explosionHits { get; set; }

	public int team { get; set; }

	public int xpattack { get; set; }

	public int tdestroyedModules { get; set; }

	public int stunNum { get; set; }

	public int damageAssistedInspire { get; set; }

	//public object[] inBattleAchievements { get; set; }

	public int achievementFreeXP { get; set; }

	public int directHits { get; set; }
}

public class Avatar
{
	public int avatarKills { get; set; }

	public int playerRank { get; set; }

	public int basePointsDiff { get; set; }

	public bool hasBattlePass { get; set; }

	public int avatarDamaged { get; set; }

	public int totalDamaged { get; set; }

	public int avatarDamageDealt { get; set; }

	public int sumPoints { get; set; }

	public int[] fairplayViolations { get; set; }

	public int[][] badges { get; set; }
}

public class PlayerInfo
{
	public string name { get; set; }

	public int prebattleID { get; set; }

	public int igrType { get; set; }

	public string clanAbbrev { get; set; }

	public int team { get; set; }

	public int clanDBID { get; set; }

	public string realName { get; set; }
}

public class Common
{
	//public object division { get; set; }

	public int finishReason { get; set; }

	public int guiType { get; set; }

	public int commonNumDefended { get; set; }

	public int commonNumCaptured { get; set; }

	public int commonNumStarted { get; set; }

	public int arenaCreateTime { get; set; }

	public int commonNumDestroyed { get; set; }

	public int duration { get; set; }

	public Dictionary<string, int> teamHealth { get; set; }

	public int arenaTypeID { get; set; }

	public int gasAttackWinnerTeam { get; set; }

	public int winnerTeam { get; set; }

	public int vehLockMode { get; set; }

	public int bonusType { get; set; }

	//public Bots bots { get; set; }

	//public object accountCompDescr { get; set; }
}

//public class Bots
//{
//}

public class Player
{
	public int wtr { get; set; }

	public string vehicleType { get; set; }

	public bool isAlive { get; set; }

	//public object[] personalMissionIDs { get; set; }

	//public object[] vehPostProgression { get; set; }

	//public Personalmissioninfo personalMissionInfo { get; set; }

	public bool forbidInBattleInvitations { get; set; }

	public string fakeName { get; set; }

	public int maxHealth { get; set; }

	public int igrType { get; set; }

	public string clanAbbrev { get; set; }

	public int[] ranked { get; set; }

	public int isTeamKiller { get; set; }

	public int customRoleSlotTypeId { get; set; }

	public int team { get; set; }

	//public Events events { get; set; }

	public int overriddenBadge { get; set; }

	public string avatarSessionID { get; set; }

	public int[][] badges { get; set; }

	public string name { get; set; }
}

public class PlayerFrags
{
	public int frags { get; set; }
}

//public class Personalmissioninfo
//{
//}

//public class Events
//{
//}
