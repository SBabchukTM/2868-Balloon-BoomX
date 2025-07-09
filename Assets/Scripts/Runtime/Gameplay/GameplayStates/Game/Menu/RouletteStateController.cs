using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.HelperServices.UserData.Data;
using Runtime.Gameplay.Roulette;
using Runtime.Gameplay.ScreensPopups.Popup;
using Runtime.Gameplay.ScreensPopups.Screen;
using UnityEngine;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class RouletteStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly RouletteSpinner _rouletteSpinner;
        private readonly RouletteItemSelector _rouletteItemSelector;
        private readonly UserLoginService _userLoginService;
        private readonly IUserInventoryService _userInventoryService;

        private DailyRouletteScreen _screen;

        private List<RouletteItemModel> _itemModels;
        
        public RouletteStateController(ILogger logger, IUiService uiService,
            RouletteSpinner rouletteSpinner, UserLoginService userLoginService,
            RouletteItemSelector rouletteItemSelector, IUserInventoryService userInventoryService) : base(logger)
        {
            _uiService = uiService;
            _rouletteSpinner = rouletteSpinner;
            _userLoginService = userLoginService;
            _rouletteItemSelector = rouletteItemSelector;
            _userInventoryService = userInventoryService;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.DailyRouletteScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<DailyRouletteScreen>(ConstScreens.DailyRouletteScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
            if(!_userLoginService.CanSpinRoulette())
                _screen.ShowError();

            _itemModels = _rouletteItemSelector.CreateRouletteItems(_screen.RouletteItemDisplays);
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MainScreenStateController>();
            _screen.OnSpinPressed += Spin;
        }

        private async void Spin()
        {
            _screen.DisableFlow();
            
            int itemIndex = Random.Range(0, _itemModels.Count);
            await _rouletteSpinner.Spin(_screen.RouletteTransform, itemIndex);
            
            var itemModel = _itemModels[itemIndex];
            RecordReward(itemModel);
            
            AchievementEvents.InvokeUsedDailySpin();
            
            await ShowRewardPopup(itemModel);
        }

        private RouletteItemModel RecordReward(RouletteItemModel itemModel)
        {
            _userLoginService.RecordSpinDate();
            AddReward(itemModel);
            return itemModel;
        }

        private void AddReward(RouletteItemModel rouletteItemModel)
        {
            if (rouletteItemModel.ItemType == RouletteItemType.Coins)
                _userInventoryService.AddBalance(rouletteItemModel.Value);
            else
            {
                _userInventoryService.AddPurchasedGameItemID(rouletteItemModel.Value);
                AchievementEvents.InvokeWonSkinFromDailySpin();
            }
        }

        private async Task ShowRewardPopup(RouletteItemModel itemModel)
        {
            RouletteRewardPopup popup = await _uiService.ShowPopup(ConstPopups.RouletteRewardPopup) as RouletteRewardPopup;
            
            popup.SetData(itemModel);
            popup.OnClaimPressed += async () =>
            {
                popup.DestroyPopup();
                await GoTo<MainScreenStateController>();
            };
        }
    }
}