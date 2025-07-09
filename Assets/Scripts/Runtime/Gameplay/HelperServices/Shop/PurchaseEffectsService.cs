using System.Threading;
using Core.Services.Audio;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.Audio;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public class PurchaseEffectsService : IPurchaseEffectsService
    {
        private readonly IAudioService _audioService;
        
        private ShopConfig _shopItemStateConfig;

        public PurchaseEffectsService(IAudioService audioService) =>
                _audioService = audioService;

        public void SetShopSetup(ShopConfig shopConfig) =>
                _shopItemStateConfig = shopConfig;
        
        public void PlayFailedPurchaseAttemptEffect(ShopItemDisplay shopItemDisplay, CancellationToken cancellationToken)
        {
            if (_shopItemStateConfig.PurchaseEffectSettings.ShakeIfNotEnoughCurrency)
            {
                shopItemDisplay
                        .Shake(cancellationToken, _shopItemStateConfig.PurchaseEffectSettings.PurchaseFailedShakeParameters)
                        .Forget();
            }

            if (_shopItemStateConfig.PurchaseEffectSettings.PlaySoundIfNotEnoughCurrency)
                _audioService.PlaySound(ConstAudio.ErrorSound);
        }
    }
}