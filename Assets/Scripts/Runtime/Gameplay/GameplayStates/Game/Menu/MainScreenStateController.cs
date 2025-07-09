using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.GameplayStates.Game.Controllers;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class MainScreenStateController : StateController
    {
        private readonly IUiService _uiService;

        private MainScreen _screen;
        
        public MainScreenStateController(ILogger logger, IUiService uiService) : base(logger)
        {
            _uiService = uiService;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.MainScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<MainScreen>(ConstScreens.MainScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnMenuPressed += async () => await GoTo<MenuStateController>();
            _screen.OnDailyPressed += async () => await GoTo<RouletteStateController>();
            _screen.OnLeaderboardPressed += async () => await GoTo<LeaderboardStateController>();
            _screen.OnMissionsPressed += async () => await GoTo<MissionsStateController>();
            _screen.OnProfilePressed += async () => await GoTo<AccountScreenStateController>();
            _screen.OnSettingsPressed += async () => await GoTo<SettingsStateController>();
            _screen.OnShopPressed += async () => await GoTo<InitShopState>();
            _screen.OnStartPressed += async () => await GoTo<ModeSelectionStateController>();
        }
    }
}