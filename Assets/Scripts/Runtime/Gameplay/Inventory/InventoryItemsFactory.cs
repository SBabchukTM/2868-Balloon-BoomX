using System.Collections.Generic;
using Core;
using Core.Factory;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using Runtime.Gameplay.HelperServices.UserData.Data;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Inventory
{
    public class InventoryItemsFactory : IInitializable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUserInventoryService _userInventoryService;
        private readonly ISettingProvider _settingProvider;
        private readonly GameObjectFactory _gameObjectFactory;

        private GameObject _prefab;

        public InventoryItemsFactory(IAssetProvider assetProvider, IUserInventoryService userInventoryService,
            ISettingProvider settingProvider, GameObjectFactory gameObjectFactory)
        {
            _assetProvider = assetProvider;
            _userInventoryService = userInventoryService;
            _settingProvider = settingProvider;
            _gameObjectFactory = gameObjectFactory;
        }
    
        public async void Initialize()
        {
            _prefab = await _assetProvider.Load<GameObject>(ConstPrefabs.InventoryItemPrefab);
        }

        public List<InventoryItemDisplay> CreateInventoryItemDisplayList()
        {
            List<InventoryItemDisplay> inventoryItemDisplayList = new List<InventoryItemDisplay>();

            var shopConfig = _settingProvider.Get<ShopConfig>();
            var purchasedItems = _userInventoryService.GetPurchasedGameItemsIDs();

            int usedGameItem = _userInventoryService.GetUsedGameItemID();
        
            for (int i = 0; i < purchasedItems.Count; i++)
            {
                int id = purchasedItems[i];
                var shopItem = shopConfig.ShopItems[id];

                InventoryItemDisplay display = _gameObjectFactory.Create<InventoryItemDisplay>(_prefab);
                var status = usedGameItem == id ? InventoryItemStatusType.Used : InventoryItemStatusType.Unused;
                display.Initialize(shopItem.Sprite, status , id);
            
                inventoryItemDisplayList.Add(display);
            }
        
            return inventoryItemDisplayList;
        }
    }
}
