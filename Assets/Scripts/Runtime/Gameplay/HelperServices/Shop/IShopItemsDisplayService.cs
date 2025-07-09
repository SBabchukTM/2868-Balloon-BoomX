
namespace Runtime.Gameplay.HelperServices.Shop
{
    public interface IShopItemsDisplayService : ISetShopSetup
    {
        void CreateShopItems();

        void UpdateItemsStatus();
    }
}