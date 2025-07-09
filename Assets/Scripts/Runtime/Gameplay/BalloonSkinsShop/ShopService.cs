using Runtime.Gameplay.HelperServices.Shop;
using Runtime.Gameplay.HelperServices.UserData.Data;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    public class ShopService : ISetShopSetup
    {
        private readonly IUserInventoryService _userInventoryService;

        private ShopConfig _shopConfig;

        public ShopService(IUserInventoryService userInventoryService) =>
                _userInventoryService = userInventoryService;

        public void SetShopSetup(ShopConfig shopConfig) =>
                _shopConfig = shopConfig;

        public void PurchaseShopItem(ShopItemDisplay shopItemDisplay)
        {
            _userInventoryService.AddPurchasedGameItemID(shopItemDisplay.GetShopItemModel().ShopItem.ItemID);
            _userInventoryService.UpdateUsedGameItemID(shopItemDisplay.GetShopItemModel().ShopItem.ItemID);
            _userInventoryService.AddBalance(-shopItemDisplay.GetShopItemModel().ShopItem.Price);
        }

        public bool CanPurchaseItem(ShopItem shopItem) => _userInventoryService.GetBalance() >= shopItem.Price;

        public bool IsPurchased(ShopItem shopItem) => _userInventoryService.GetPurchasedGameItemsIDs().Contains(shopItem.ItemID);

        public bool IsSelected(ShopItem shopItem) =>
                _userInventoryService.GetUsedGameItemID() == shopItem.ItemID;
    }
}