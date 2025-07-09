using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class MissionsStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly AchievementsFactory _achievementsFactory;

        private QuestsScreen _screen;
        
        public MissionsStateController(ILogger logger, IUiService uiService, AchievementsFactory achievementsFactory) : base(logger)
        {
            _uiService = uiService;
            _achievementsFactory = achievementsFactory;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.QuestsScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<QuestsScreen>(ConstScreens.QuestsScreen);
            _screen.Initialize(_achievementsFactory.GetAchievementDisplays());
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MainScreenStateController>();
        }
    }
}