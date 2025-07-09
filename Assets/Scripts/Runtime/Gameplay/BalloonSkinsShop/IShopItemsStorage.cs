using System.Collections.Generic;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    public interface IShopItemsStorage
    {
        void AddItem(ShopItemDisplay shopItemDisplay);

        List<ShopItemDisplay> GetItemDisplay();

        void SetShopStateConfigs(List<ShopItemStateConfig> shopConfigShopItemStateConfigs);
        
        void Cleanup();
    }
}