using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.UserData.Data;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public class ShopItemsDisplayService : IShopItemsDisplayService
    {
        private readonly ShopItemDisplayController _shopItemDisplayController;
        private readonly IUserInventoryService _userInventoryService;

        private ShopConfig _shopConfig;

        public ShopItemsDisplayService(ShopItemDisplayController shopItemDisplayController,
            IUserInventoryService userInventoryService)
        {
            _shopItemDisplayController = shopItemDisplayController;
            _userInventoryService = userInventoryService;
        }

        public void SetShopSetup(ShopConfig shopConfig) =>
                _shopConfig = shopConfig;

        public void CreateShopItems()
        {
            var config = _shopConfig.ShopItems;
            var purchasedIDs = _userInventoryService.GetPurchasedGameItemsIDs();
            
            for (int i = 0; i < config.Count; i++)
            {
                if(!purchasedIDs.Contains(config[i].ItemID))
                    _shopItemDisplayController.CreateItemDisplayView(config[i]);
            }
        }

        public void UpdateItemsStatus() =>
                _shopItemDisplayController.UpdateItemStates();
    }
}