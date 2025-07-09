using System;

namespace Runtime.Gameplay.Game.Achievements
{
    public static class AchievementEvents
    {
        public static event Action FirstLevelCleared;
        public static void InvokeFirstLevelCleared() => FirstLevelCleared?.Invoke();
    
        public static event Action LastLevelCleared;
        public static void InvokeLastLevelCleared() => LastLevelCleared?.Invoke();

        public static event Action UsedTeleport;
        public static void InvokeUsedTeleport() => UsedTeleport?.Invoke();
    
        public static event Action UsedBoost;
        public static void InvokeUsedBoost() => UsedBoost?.Invoke();
    
        public static event Action UsedRocket;
        public static void InvokeUsedRocket() => UsedRocket?.Invoke();
    
        public static event Action UsedSpikes;
        public static void InvokeUsedSpikes() => UsedSpikes?.Invoke();
    
        public static event Action FinishedTimeAttackMode;
        public static void InvokeFinishedTimeAttackMode() => FinishedTimeAttackMode?.Invoke();
    
        public static event Action ReachedTopOneTimeAttack;
        public static void InvokeReachedTopOneTimeAttack() => ReachedTopOneTimeAttack?.Invoke();
    
        public static event Action UsedDailySpin;
        public static void InvokeUsedDailySpin() => UsedDailySpin?.Invoke();
    
        public static event Action FirstSkinPurchased;
        public static void InvokeFirstSkinPurchased() => FirstSkinPurchased?.Invoke();
    
        public static event Action WonSkinFromDailySpin;
        public static void InvokeWonSkinFromDailySpin() => WonSkinFromDailySpin?.Invoke();
    }
}
