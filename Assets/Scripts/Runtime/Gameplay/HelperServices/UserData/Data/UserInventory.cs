using System;
using System.Collections.Generic;

namespace Runtime.Gameplay.HelperServices.UserData.Data
{
    [Serializable]
    public class UserInventory
    {
        public int Balance = 0;
        public int UsedGameItemID;
        public List<int> PurchasedGameItemsIDs = new() { 0 };
    }
}