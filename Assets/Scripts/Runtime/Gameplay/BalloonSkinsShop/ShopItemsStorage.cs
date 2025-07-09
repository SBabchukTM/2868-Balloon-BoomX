using System.Collections.Generic;
using System.Linq;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    public class ShopItemsStorage : IShopItemsStorage
    {
        private readonly List<ShopItemDisplay> _shopItemDisplays = new();
        
        private Dictionary<ShopItemState, ShopItemStateConfig> _itemStates = new();

        public void AddItem(ShopItemDisplay shopItemDisplay) =>
                _shopItemDisplays.Add(shopItemDisplay);

        public List<ShopItemDisplay> GetItemDisplay() => 
                _shopItemDisplays;

        public ShopItemStateConfig GetItemStateConfig(ShopItemState shopItemState) => 
                _itemStates[shopItemState];

        public void SetShopStateConfigs(List<ShopItemStateConfig> shopConfigShopItemStateConfigs) =>
                _itemStates = shopConfigShopItemStateConfigs.ToDictionary(x => x.ShopItemState);

        public void Cleanup()
        {
            _shopItemDisplays.Clear();
            _itemStates.Clear();
        }
    }
}