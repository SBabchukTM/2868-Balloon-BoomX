﻿using System.Collections.Generic;
using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.GameplayStates.Game.Menu;
using Runtime.Gameplay.HelperServices.Shop;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Controllers
{
    public class InitShopState : StateController
    {
        private readonly IShopItemsStorage _shopItemsStorage;
        private readonly IUiService _uiService;
        private readonly IShopItemsDisplayService _shopItemsDisplayService;
        private readonly ISettingProvider _settingProvider;
        private readonly ShopService _shopService;
        private readonly ShopItemDisplayController _shopItemDisplayController;
        private readonly List<ISetShopSetup> _setShopConfigs;

        public InitShopState(ILogger logger, IShopItemsStorage shopItemsStorage, IUiService uiService, 
                IShopItemsDisplayService shopItemsDisplayService, ISettingProvider settingProvider,
                IProcessPurchaseService processPurchaseService, IPurchaseEffectsService purchaseEffectsService,
                ISelectPurchaseItemService selectPurchaseItemService, ShopService shopService, ShopItemDisplayController shopItemDisplayController) : base(logger)
        {
            _shopItemsStorage = shopItemsStorage;
            _uiService = uiService;
            _shopItemsDisplayService = shopItemsDisplayService;
            _settingProvider = settingProvider;
            _shopService = shopService;
            _shopItemDisplayController = shopItemDisplayController;

            _setShopConfigs = new()
            {
                processPurchaseService,
                purchaseEffectsService,
                selectPurchaseItemService,
                shopItemsDisplayService
            };
        }

        public override UniTask Enter(CancellationToken cancellationToken = default)
        {
            SetShopConfig();
            var screen = CreateScreen();
            Subscribe(screen);
            
            return GoTo<ShopStateController>(cancellationToken);
        }

        private ShopScreen CreateScreen()
        {
            var screen = _uiService.GetScreen<ShopScreen>(ConstScreens.ShopScreen);
            screen.ShowAsync().Forget();
            _shopItemDisplayController.SetShop(_shopService);
            _shopItemsDisplayService.CreateShopItems();
            _shopItemsDisplayService.UpdateItemsStatus();
            screen.SetShopItems(_shopItemsStorage.GetItemDisplay());

            return screen;
        }

        private void Subscribe(ShopScreen screen) =>
                screen.OnBackPressed += () => GoTo<MainScreenStateController>().Forget(); 
        
        private void SetShopConfig()
        {
            var shopConfig = _settingProvider.Get<ShopConfig>();

            _shopItemsStorage.SetShopStateConfigs(shopConfig.ShopItemStateConfigs);

            foreach (var setShopConfig in _setShopConfigs)
                setShopConfig.SetShopSetup(shopConfig);
        }
    }
}