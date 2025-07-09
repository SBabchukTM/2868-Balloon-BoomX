using Runtime.Gameplay.BalloonSkinsShop;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public interface ISelectPurchaseItemService : ISetShopSetup
    {
        void SelectPurchasedItem(ShopItemDisplayModel shopItemModel);
    }
}