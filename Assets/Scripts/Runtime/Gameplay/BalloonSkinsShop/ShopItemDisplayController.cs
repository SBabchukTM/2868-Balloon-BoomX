using Core.Factory;
using Runtime.Gameplay.HelperServices.Shop;
using UnityEngine;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    public class ShopItemDisplayController 
    {
        private readonly ShopItemDisplayModel _shopItemDisplayModel;
        private readonly GameObjectFactory _gameObjectFactory;
        private readonly IShopItemsStorage _shopItemsStorage;
        private readonly ISelectPurchaseItemService _selectPurchaseItemService;
        
        private ShopService _shopService;

        public ShopItemDisplayController(GameObjectFactory gameObjectFactory, IShopItemsStorage shopItemsStorage,
                ISelectPurchaseItemService selectPurchaseItemService)
        {
            _gameObjectFactory = gameObjectFactory;
            _shopItemsStorage = shopItemsStorage;
            _selectPurchaseItemService = selectPurchaseItemService;
        }

        public void SetShop(ShopService shopService) =>
                _shopService = shopService;

        public void CreateItemDisplayView(ShopItem shopItem)
        {
            var shopItemDisplay = _gameObjectFactory.Create(shopItem.ShopItemDisplay);
            _shopItemsStorage.AddItem(shopItemDisplay);
            shopItemDisplay.SetShopItem(shopItem);
            SetItemState(shopItemDisplay.GetShopItemModel());
        }

        public void UpdateItemStates()
        {
            var itemsDisplays = _shopItemsStorage.GetItemDisplay();

            for (int i = itemsDisplays.Count - 1; i >= 0; i--)
            {
                var itemDisplay = itemsDisplays[i];
                var shopItemDisplayModel = itemDisplay.GetShopItemModel();

                if (shopItemDisplayModel.ItemState != ShopItemState.NotPurchased)
                {
                    itemsDisplays.Remove(itemDisplay);
                    Object.Destroy(itemDisplay.gameObject);
                }
            }
        }

        private void SetItemState(ShopItemDisplayModel shopItemDisplayModel)
        {
            if(_shopService.IsSelected(shopItemDisplayModel.ShopItem))
                _selectPurchaseItemService.SelectPurchasedItem(shopItemDisplayModel);

            else
                shopItemDisplayModel.SetShopItemState(_shopService.IsPurchased(shopItemDisplayModel.ShopItem) ? ShopItemState.Purchased : ShopItemState.NotPurchased);
        }
    }
}