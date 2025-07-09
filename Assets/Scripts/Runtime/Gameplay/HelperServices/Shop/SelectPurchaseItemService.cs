using Core.Services.Audio;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.UserData.Data;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public class SelectPurchaseItemService : ISelectPurchaseItemService
    {
        private readonly IAudioService _audioService;
        private readonly IUserInventoryService _userInventoryService;
        
        private ShopConfig _shopItemStateConfig;
        private ShopItemDisplayModel _shopItem;
        private ShopItemDisplayModel _previousItem;

        public SelectPurchaseItemService(IAudioService audioService, 
            IUserInventoryService userInventoryService)
        {
            _audioService = audioService;
            _userInventoryService = userInventoryService;
        }

        public void SetShopSetup(ShopConfig shopConfig) =>
                _shopItemStateConfig = shopConfig;

        public void SelectPurchasedItem(ShopItemDisplayModel shopItemModel)
        {
            SetItem(shopItemModel);
            UpdateStates();
        }
        
        private void SetItem(ShopItemDisplayModel shopItemModel)
        {
            if(_shopItem != null)
                _previousItem = _shopItem;

            _shopItem = shopItemModel;
            _userInventoryService.UpdateUsedGameItemID(_shopItem.ShopItem.ItemID);
        }

        private void UpdateStates()
        {
            _shopItem?.SetShopItemState(ShopItemState.Selected);
            _previousItem?.SetShopItemState(ShopItemState.Purchased);
        }
    }
}