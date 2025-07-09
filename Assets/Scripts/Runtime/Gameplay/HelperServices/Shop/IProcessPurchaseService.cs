using System.Threading;
using Runtime.Gameplay.BalloonSkinsShop;

namespace Runtime.Gameplay.HelperServices.Shop
{
    public interface IProcessPurchaseService : ISetShopSetup
    {
        void ProcessPurchaseAttempt(ShopItemDisplay shopItemDisplay, CancellationToken cancellationToken);
    }
}