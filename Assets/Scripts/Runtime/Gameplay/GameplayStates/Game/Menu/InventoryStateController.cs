using System.Collections.Generic;
using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.HelperServices.UserData.Data;
using Runtime.Gameplay.Inventory;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class InventoryStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly InventoryItemsFactory _inventoryItemsFactory;
        private readonly IUserInventoryService _userInventoryService;

        private InventoryScreen _screen;
        
        private List<InventoryItemDisplay> _inventoryItemDisplayList;
        
        public InventoryStateController(ILogger logger, IUiService uiService, InventoryItemsFactory inventoryItemsFactory, IUserInventoryService userInventoryService) : base(logger)
        {
            _uiService = uiService;
            _inventoryItemsFactory = inventoryItemsFactory;
            _userInventoryService = userInventoryService;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            _inventoryItemDisplayList = _inventoryItemsFactory.CreateInventoryItemDisplayList();
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.InventoryScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<InventoryScreen>(ConstScreens.InventoryScreen);
            _screen.Initialize(_inventoryItemDisplayList);
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MenuStateController>();

            foreach (var item in _inventoryItemDisplayList)
            {
                item.OnSelected += UpdateItemsStatus;
            }
        }

        private void UpdateItemsStatus(int itemID)
        {
            _userInventoryService.UpdateUsedGameItemID(itemID);
            foreach (var item in _inventoryItemDisplayList)
                item.UpdateStatus(itemID);
        }
    }
}