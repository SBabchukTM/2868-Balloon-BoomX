using System.Threading;
using Runtime.Gameplay.BalloonSkinsShop;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public interface IPurchaseEffectsService : ISetShopSetup
    {
        void PlayFailedPurchaseAttemptEffect(ShopItemDisplay shopItemDisplay, CancellationToken cancellationToken);
    }
}