using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class ModeSelectionStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly GameData _gameData;

        private GameModeSelectionScreen _screen;
        
        public ModeSelectionStateController(ILogger logger, IUiService uiService, GameData gameData) : base(logger)
        {
            _uiService = uiService;
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
            await _uiService.HideScreen(ConstScreens.GameModeSelectionScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<GameModeSelectionScreen>(ConstScreens.GameModeSelectionScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MainScreenStateController>();
            _screen.OnRacePressed += async () =>
            {
                _gameData.GameplayMode = GameplayMode.Race;
                await GoTo<LevelSelectionStateController>();
            };
            _screen.OnTimeAttackPressed += async () =>
            {
                _gameData.GameplayMode = GameplayMode.TimeAttack;
                await GoTo<GameplayStateController>();
            };
        }
    }
}