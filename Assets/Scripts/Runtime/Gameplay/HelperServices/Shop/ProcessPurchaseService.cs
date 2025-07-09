using System.Threading;
using Core.Services.Audio;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.HelperServices.Audio;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Popup;
using Runtime.Gameplay.ScreensPopups.Popup.Data;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public class ProcessPurchaseService : IProcessPurchaseService
    {
        private readonly ShopService _shopService;
        private readonly IPurchaseEffectsService _purchaseEffectsService;
        private readonly ISelectPurchaseItemService _selectPurchaseItemService;
        private readonly IUiService _uiService;
        private readonly IShopItemsDisplayService _shopItemsDisplayService;
        private readonly IAudioService _audioService;
        
        private ShopConfig _shopConfig;

        public ProcessPurchaseService(ShopService shopService, IPurchaseEffectsService purchaseEffectsService, 
                ISelectPurchaseItemService selectPurchaseItemService, IUiService uiService, 
                IShopItemsDisplayService shopItemsDisplayService, IAudioService audioService)
        {
            _shopService = shopService;
            _purchaseEffectsService = purchaseEffectsService;
            _selectPurchaseItemService = selectPurchaseItemService;
            _uiService = uiService;
            _shopItemsDisplayService = shopItemsDisplayService;
            _audioService = audioService;
        }

        public void SetShopSetup(ShopConfig shopConfig) =>
                _shopConfig = shopConfig;

        public void ProcessPurchaseAttempt(ShopItemDisplay shopItemDisplay, CancellationToken cancellationToken)
        {
            var shopItemModel = shopItemDisplay.GetShopItemModel();
            
            switch (shopItemModel.ItemState)
            {
                case ShopItemState.NotPurchased:
                    ProcessPurchase(shopItemDisplay, cancellationToken);
                    break;
                case ShopItemState.Purchased:
                    SelectItem(shopItemModel);
                    UpdateStatus();
                    break;
                case ShopItemState.Selected:
                    UpdateStatus();
                    break;
            }
        }

        private async void ProcessPurchase(ShopItemDisplay shopItemDisplay, CancellationToken cancellationToken)
        {
            if(!_shopService.CanPurchaseItem(shopItemDisplay.GetShopItemModel().ShopItem))
            {
                _purchaseEffectsService.PlayFailedPurchaseAttemptEffect(shopItemDisplay, cancellationToken);
                return;
            }

            if (_shopConfig.ConfirmPurchase)
            {
                var popup = await _uiService
                    .ShowPopup(ConstPopups.ItemPurchasePopup,
                        new ItemPurchasePopupData { ShopItem = shopItemDisplay.GetShopItemModel().ShopItem },
                        cancellationToken) as ItemPurchasePopup;

                Subscribe(shopItemDisplay, popup);
            }
            else
                AcceptPurchase(shopItemDisplay);
        }

        private void Subscribe(ShopItemDisplay shopItemDisplay, ItemPurchasePopup popup)
        {
            popup.OnAcceptPressedEvent += () => { OnAcceptButtonPressed(shopItemDisplay, popup); };
            popup.OnDenyPressedEvent += () => OnDenyButtonPressed(shopItemDisplay, popup);
        }

        private void SelectItem(ShopItemDisplayModel shopDisplayModel)
        {
            PlaySound(ConstAudio.SelectSound, _shopConfig.PurchaseEffectSettings.PlaySoundOnSelectPurchased);
            _selectPurchaseItemService.SelectPurchasedItem(shopDisplayModel);
        }

        private void PlaySound(string sound, bool condition)
        {
            if (condition)
                _audioService.PlaySound(sound);
        }

        private void UpdateStatus() =>
                _shopItemsDisplayService.UpdateItemsStatus();

        private static void DestroyPopup(ItemPurchasePopup popup) =>
                popup.DestroyPopup();

        private void OnDenyButtonPressed(ShopItemDisplay shopItemDisplay, ItemPurchasePopup popup)
        {
            popup.OnAcceptPressedEvent -= () => { OnAcceptButtonPressed(shopItemDisplay, popup); };
            DestroyPopup(popup);
        }

        private void AcceptPurchase(ShopItemDisplay shopItemDisplay)
        {
            _shopService.PurchaseShopItem(shopItemDisplay);

            SelectItem(shopItemDisplay.GetShopItemModel());
            PlaySound(ConstAudio.PurchaseSound, condition: _shopConfig.PurchaseEffectSettings.PlaySoundOnPurchase);
            UpdateStatus();
            
            AchievementEvents.InvokeFirstSkinPurchased();
        }
        
        private void OnAcceptButtonPressed(ShopItemDisplay shopItemDisplay, ItemPurchasePopup popup)
        {
            AcceptPurchase(shopItemDisplay);
            DestroyPopup(popup);
        }
    }
}