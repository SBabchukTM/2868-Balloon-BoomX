using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class LeaderboardStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly  UserRecordsFactory _userRecordsFactory;

        private LeaderboardScreen _screen;
        
        public LeaderboardStateController(ILogger logger, IUiService uiService, UserRecordsFactory userRecordsFactory) : base(logger)
        {
            _uiService = uiService;
            _userRecordsFactory = userRecordsFactory;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.LeaderboardScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<LeaderboardScreen>(ConstScreens.LeaderboardScreen);
            _screen.Initialize(_userRecordsFactory.CreateRecordDisplayList());
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MainScreenStateController>();
        }
    }
}