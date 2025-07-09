using System;
using System.Collections.Generic;
using Runtime.Gameplay.BalloonSkinsShop;

namespace Runtime.Gameplay.HelperServices.UserData.Data
{
    public interface IUserInventoryService
    {
        event Action<int> OnBalanceChanged;
        
        void AddBalance(int amount);

        int GetBalance();

        void UpdateUsedGameItemID(int userGameItemID);
        
        int GetUsedGameItemID();

        void AddPurchasedGameItemID(int userGameItemID);

        List<int> GetPurchasedGameItemsIDs();
        
        List<ShopItem> GetPurchasedShopItems();
    }
}