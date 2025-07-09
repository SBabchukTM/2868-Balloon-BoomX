using Core;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.UserData.Data;
using UnityEngine;

namespace Runtime.Gameplay.Game
{
    public class SkinProvider
    {
        private readonly IUserInventoryService _userInventoryService;
        private readonly ISettingProvider _settingProvider;

        public SkinProvider(IUserInventoryService userInventoryService, ISettingProvider settingProvider)
        {
            _userInventoryService = userInventoryService;
            _settingProvider = settingProvider;
        }
        
        public Sprite GetPlayerSkin()
        {
            var shopConfig = _settingProvider.Get<ShopConfig>();
            var usedItemID = _userInventoryService.GetUsedGameItemID();
            return shopConfig.ShopItems[usedItemID].Sprite;
        }

        public Sprite GetRandomSkin()
        {
            var shopConfig = _settingProvider.Get<ShopConfig>();
            return shopConfig.ShopItems[Random.Range(0, shopConfig.ShopItems.Count)].Sprite;
        }
    }
}