using Core;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.HelperServices.UserData;

namespace Runtime.Gameplay.Game.Levels
{
    public class UserProgressService
    {
        private readonly UserDataService _userDataService;
        private readonly ISettingProvider _settingProvider;
        private readonly GameData _gameData;

        public UserProgressService(UserDataService userDataService, 
            ISettingProvider settingProvider, GameData gameData)
        {
            _userDataService = userDataService;
            _settingProvider = settingProvider;
            _gameData = gameData;
        }

        public bool NextLevelExists()
        {
            return _gameData.LevelID + 1 < GetLevelsAmountInGame();
        }

        public void SaveRacingProgress()
        {
            int currentLevelID = _gameData.LevelID;
            var progressData = _userDataService.GetUserData().UserProgressData;
        
            if(currentLevelID == 0)
                AchievementEvents.InvokeFirstLevelCleared();
        
            if(currentLevelID == GetLevelsAmountInGame() - 1)
                AchievementEvents.InvokeLastLevelCleared();
        
            int lastUnlockedLevel = GetLastUnlockedLevelID();

            if (currentLevelID != lastUnlockedLevel)
                return;

            if (currentLevelID + 1 >= GetLevelsAmountInGame())
                return;

            progressData.LastUnlockedLevelID = lastUnlockedLevel + 1;
        }

        public void SaveTimeAttackProgress()
        {
            AchievementEvents.InvokeFinishedTimeAttackMode();

            int newTime = _gameData.TimeAttackTime;
        
            if(newTime < 38)
                AchievementEvents.InvokeReachedTopOneTimeAttack();

            var progressData = _userDataService.GetUserData().UserProgressData;
        
            int bestTime = progressData.BestTime;

            if (bestTime == 0)
            {
                progressData.BestTime = newTime;
                return;
            }
        
            if (newTime < bestTime)
                progressData.BestTime = newTime;
        }

        public void SaveCoins()
        {
            _userDataService.GetUserData().UserInventory.Balance += _gameData.Coins;
        }
    
        public int GetLastUnlockedLevelID() => 
            _userDataService.GetUserData().UserProgressData.LastUnlockedLevelID;

        private int GetLevelsAmountInGame() => 
            _settingProvider.Get<GameConfig>().LevelConfigs.Count;
    }
}
