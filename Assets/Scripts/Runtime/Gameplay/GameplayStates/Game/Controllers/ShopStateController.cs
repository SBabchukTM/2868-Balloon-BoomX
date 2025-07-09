using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.Shop;
using Runtime.Gameplay.HelperServices.UI;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Controllers
{
    public class ShopStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly IShopItemsStorage _shopItemsStorage;
        private readonly IProcessPurchaseService _processPurchaseService;

        private CancellationTokenSource _cancellationTokenSource;

        public ShopStateController(ILogger logger, IUiService uiService, IShopItemsStorage shopItemsStorage, 
                IProcessPurchaseService processPurchaseService) : base(logger)
        {
            _uiService = uiService;
            _shopItemsStorage = shopItemsStorage;
            _processPurchaseService = processPurchaseService;
        }

        public override UniTask Enter(CancellationToken cancellationToken = default)
        {
            _cancellationTokenSource = new();
            
            Subscribe(cancellationToken);
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            _shopItemsStorage.Cleanup();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            
            await _uiService.HideScreen(ConstScreens.ShopScreen);
        }

        private void Subscribe(CancellationToken cancellationToken)
        {
            foreach (var shopItemDisplay in _shopItemsStorage.GetItemDisplay())
            {
                shopItemDisplay.OnPurchasePressed += _ =>  _processPurchaseService.ProcessPurchaseAttempt(shopItemDisplay, cancellationToken);
            }
        }
    }
}