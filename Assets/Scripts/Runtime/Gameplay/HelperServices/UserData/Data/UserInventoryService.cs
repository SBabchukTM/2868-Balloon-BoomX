using System;
using System.Collections.Generic;
using Core;
using Runtime.Gameplay.BalloonSkinsShop;

namespace Runtime.Gameplay.HelperServices.UserData.Data
{
    public class UserInventoryService : IUserInventoryService
    {
        private readonly UserDataService _userDataService;
        private readonly ISettingProvider _settingProvider;

        public event Action<int> OnBalanceChanged;

        public UserInventoryService(UserDataService userDataService,
            ISettingProvider settingProvider)
        {
            _userDataService = userDataService;
            _settingProvider = settingProvider;
        }

        public void SetBalance(int balance)
        {
            _userDataService.GetUserData().UserInventory.Balance = balance;
            OnBalanceChanged?.Invoke(balance);
        }

        public void AddBalance(int amount)
        { 
            int balance = _userDataService.GetUserData().UserInventory.Balance + amount;
            SetBalance(balance);
        }

        public int GetBalance() => 
                _userDataService.GetUserData().UserInventory.Balance;

        public void UpdateUsedGameItemID(int userGameItemID) =>
                _userDataService.GetUserData().UserInventory.UsedGameItemID = userGameItemID;

        public ShopItem GetUsedGameItem()
        {
            ShopConfig shopConfig = _settingProvider.Get<ShopConfig>();
            int itemId = GetUsedGameItemID();
            return shopConfig.ShopItems[itemId];
        }

        public int GetUsedGameItemID() =>
                _userDataService.GetUserData().UserInventory.UsedGameItemID;

        public void AddPurchasedGameItemID(int userGameItemID) =>
                _userDataService.GetUserData().UserInventory.PurchasedGameItemsIDs.Add(userGameItemID);

        public List<int> GetPurchasedGameItemsIDs() =>
                _userDataService.GetUserData().UserInventory.PurchasedGameItemsIDs;

        public List<ShopItem> GetPurchasedShopItems()
        {
            List<ShopItem> result = new List<ShopItem>();
            ShopConfig shopConfig = _settingProvider.Get<ShopConfig>();
            
            var purchasedItemIDs = GetPurchasedGameItemsIDs();

            for (int i = 0; i < purchasedItemIDs.Count; i++)
                result.Add(shopConfig.ShopItems[purchasedItemIDs[i]]);
            
            return result;
        }
    }
}