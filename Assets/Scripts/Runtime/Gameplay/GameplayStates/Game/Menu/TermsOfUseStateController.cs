using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class TermsOfUseStateController : StateController
    {
        private readonly IUiService _uiService;

        private TermsOfUseScreen _screen;
        
        public TermsOfUseStateController(ILogger logger, IUiService uiService) : base(logger)
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
            await _uiService.HideScreen(ConstScreens.TermsOfUseScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<TermsOfUseScreen>(ConstScreens.TermsOfUseScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MenuStateController>();
        }
    }
}