using System;

namespace Runtime.Gameplay.HelperServices.UserData.Data
{
    [Serializable]
    public class UserAchievementsData
    {
        public bool FirstLevelCleared;
        public bool LastLevelCleared;
        public bool UsedTeleport;
        public bool UsedBoost;
        public bool UsedRocket;
        public bool UsedSpikes;
        public bool FinishedTimeAttackMode;
        public bool ReachedTopOneTimeAttack;
        public bool UsedDailySpin;
        public bool FirstSkinPurchased;
        public bool WonSkinFromDailySpin;
    }
}