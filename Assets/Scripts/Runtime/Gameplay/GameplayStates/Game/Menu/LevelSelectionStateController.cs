using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class LevelSelectionStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly UserProgressService _userProgressService;
        private readonly GameData _gameData;

        private LevelSelectionScreen _screen;
        
        public LevelSelectionStateController(ILogger logger, IUiService uiService, 
            UserProgressService userProgressService, GameData gameData) : base(logger)
        {
            _uiService = uiService;
            _userProgressService = userProgressService;
            _gameData = gameData;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            return UniTask.CompletedTask;
        }
        
        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.LevelSelectionScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<LevelSelectionScreen>(ConstScreens.LevelSelectionScreen);
            _screen.Initialize(_userProgressService.GetLastUnlockedLevelID());
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<ModeSelectionStateController>();
            _screen.OnSelectedLevelChanged += async (level) =>
            {
                _gameData.LevelID = level;
                await GoTo<GameplayStateController>();
            };
        }
    }
}