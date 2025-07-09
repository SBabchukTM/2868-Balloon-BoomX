using System;
using System.Collections.Generic;
using Runtime.Gameplay.Roulette;
using Runtime.Gameplay.UserProfile;

namespace Runtime.Gameplay.HelperServices.UserData.Data
{
    [Serializable]
    public class UserData
    {
        public SettingsData SettingsData = new SettingsData();
        public GameData GameData = new GameData();
        public UserInventory UserInventory = new UserInventory();
        public UserProgressData UserProgressData = new UserProgressData();
        public UserAccountData UserAccountData = new UserAccountData();
        public UserLoginData UserLoginData = new UserLoginData();
        public UserAchievementsData UserAchievementsData = new UserAchievementsData();
    }
}