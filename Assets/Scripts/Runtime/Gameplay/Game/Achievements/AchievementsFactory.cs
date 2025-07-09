using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Factory;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using Runtime.Gameplay.HelperServices.UserData;
using Runtime.Gameplay.ScreensPopups;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Achievements
{
    public class AchievementsFactory : IInitializable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameObjectFactory _factory;
        private readonly UserDataService _userDataService;
    
        private GameObject _prefab;

        public AchievementsFactory(IAssetProvider assetProvider, GameObjectFactory factory, UserDataService userDataService)
        {
            _assetProvider = assetProvider;
            _factory = factory;
            _userDataService = userDataService;
        }
    
        public async void Initialize()
        {
            _prefab = await _assetProvider.Load<GameObject>(ConstPrefabs.AchievementPrefab);
        }

        public List<AchievementDisplay> GetAchievementDisplays()
        {
            List<AchievementDisplay> achievementDisplays = new List<AchievementDisplay>();
        
            var achiementsData = _userDataService.GetUserData().UserAchievementsData;
        
            achievementDisplays.Add(CreateAchievementDisplay("Clear first level!", achiementsData.FirstLevelCleared));
            achievementDisplays.Add(CreateAchievementDisplay("Clear last level!", achiementsData.LastLevelCleared));
            achievementDisplays.Add(CreateAchievementDisplay("Use teleporter!", achiementsData.UsedTeleport));
            achievementDisplays.Add(CreateAchievementDisplay("Use boost ability!", achiementsData.UsedBoost));
            achievementDisplays.Add(CreateAchievementDisplay("Use rocket ability!", achiementsData.UsedRocket));
            achievementDisplays.Add(CreateAchievementDisplay("Use spikes ability!", achiementsData.UsedSpikes));
            achievementDisplays.Add(CreateAchievementDisplay("Finish time attack mode!", achiementsData.FinishedTimeAttackMode));
            achievementDisplays.Add(CreateAchievementDisplay("Reach top 1 in time attack mode!", achiementsData.ReachedTopOneTimeAttack));
            achievementDisplays.Add(CreateAchievementDisplay("Use daily spin!", achiementsData.UsedDailySpin));
            achievementDisplays.Add(CreateAchievementDisplay("Win a skin from daily spin!", achiementsData.WonSkinFromDailySpin));
            achievementDisplays.Add(CreateAchievementDisplay("Purchase your first skin!", achiementsData.FirstSkinPurchased));

            achievementDisplays = achievementDisplays.OrderByDescending(x => x.Cleared).ToList();
        
            return achievementDisplays;
        }

        private AchievementDisplay CreateAchievementDisplay(string name, bool cleared)
        {
            var achievementDisplay = _factory.Create<AchievementDisplay>(_prefab);
            achievementDisplay.Initialize(name, cleared);
            return achievementDisplay;
        }
    }
}
