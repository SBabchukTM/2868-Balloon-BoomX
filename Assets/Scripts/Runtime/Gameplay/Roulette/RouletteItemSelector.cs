using System.Collections.Generic;
using System.Linq;
using Core;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.UserData.Data;

namespace Runtime.Gameplay.Roulette
{
    public class RouletteItemSelector
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IUserInventoryService _userInventoryService;

        public RouletteItemSelector(ISettingProvider settingProvider, IUserInventoryService userInventoryService)
        {
            _settingProvider = settingProvider;
            _userInventoryService = userInventoryService;
        }

        public List<RouletteItemModel> CreateRouletteItems(RouletteItemDisplay[] rouletteItemDisplays)
        {
            List<RouletteItemModel> rouletteItemModels = new List<RouletteItemModel>();

        
            int elementsToCreate = rouletteItemDisplays.Length;

            var shopSetup = _settingProvider.Get<ShopConfig>();
            List<ShopItem> purchasedItems = _userInventoryService.GetPurchasedShopItems();
            List<ShopItem> itemsInGame = shopSetup.ShopItems;

            var availableItems = itemsInGame.Except(purchasedItems).ToList();

            int skinsAdded = 0;
            for (int i = 0; i < elementsToCreate; i++)
            {
                if (i % 2 == 0 && skinsAdded < availableItems.Count)
                {
                    rouletteItemModels.Add(CreateSkinReward(shopSetup, availableItems[skinsAdded].ItemID));
                    skinsAdded++;
                }
                else
                    rouletteItemModels.Add(CreateCoinReward(shopSetup));
            
                rouletteItemDisplays[i].Initialize(rouletteItemModels[i]);
            }
        
            return rouletteItemModels;
        }

        private RouletteItemModel CreateCoinReward(ShopConfig shopConfig)
        {
            return new RouletteItemModel(shopConfig.CoinIcon, RouletteItemType.Coins, GetCoinValue());
        }

        private RouletteItemModel CreateSkinReward(ShopConfig shopConfig, int skinId)
        {
            var itemSprite = shopConfig.ShopItems[skinId].Sprite;
            return new RouletteItemModel(itemSprite, RouletteItemType.Skin, skinId);
        }

        private int GetCoinValue()
        {
            float rand = UnityEngine.Random.value;

            if (rand < 0.5f)
                return 100;
        
            if(rand < 0.66f)
                return 200;

            if (rand < 0.77f)
                return 300;
        
            if(rand < 0.88f)
                return 400;
        
            return 500;
        }
    }
}
