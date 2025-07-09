using Runtime.Gameplay.HelperServices.UserData;

namespace Runtime.Gameplay.Game.Achievements
{
    public class AchievementsManager
    {
        private readonly UserDataService _userDataService;
    
        public AchievementsManager(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public void Initialize()
        {
            var achievements = _userDataService.GetUserData().UserAchievementsData;

            AchievementEvents.UsedBoost += () => achievements.UsedBoost = true;
            AchievementEvents.UsedRocket += () => achievements.UsedRocket = true;
            AchievementEvents.UsedSpikes += () => achievements.UsedSpikes = true;
            AchievementEvents.UsedTeleport += () => achievements.UsedTeleport = true;
            AchievementEvents.FirstLevelCleared += () => achievements.FirstLevelCleared = true;
            AchievementEvents.LastLevelCleared += () => achievements.LastLevelCleared = true;
            AchievementEvents.FirstSkinPurchased += () => achievements.FirstSkinPurchased = true;
            AchievementEvents.UsedDailySpin += () => achievements.UsedDailySpin = true;
            AchievementEvents.WonSkinFromDailySpin += () => achievements.WonSkinFromDailySpin = true;
            AchievementEvents.ReachedTopOneTimeAttack += () => achievements.ReachedTopOneTimeAttack = true;
            AchievementEvents.FinishedTimeAttackMode += () => achievements.FinishedTimeAttackMode = true;
        }
    }
}
