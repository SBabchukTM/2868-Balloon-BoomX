using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class MenuStateController : StateController
    {
        private readonly IUiService _uiService;

        private MenuScreen _screen;
        
        public MenuStateController(ILogger logger, IUiService uiService) : base(logger)
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
            await _uiService.HideScreen(ConstScreens.MenuScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<MenuScreen>(ConstScreens.MenuScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MainScreenStateController>();
            
            _screen.OnInventoryPressed += async () => await GoTo<InventoryStateController>();
            _screen.OnPrivacyPolicyPressed += async () => await GoTo<PrivacyPolicyStateController>();
            _screen.OnTermsOfUsePressed += async () => await GoTo<TermsOfUseStateController>();
            _screen.OnHowToPlayPressed += async () => await GoTo<HowToPlayStateController>();
        }
    }
}