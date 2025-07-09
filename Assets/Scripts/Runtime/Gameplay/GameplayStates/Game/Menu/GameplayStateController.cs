using System.Threading;
using Core;
using Core.Services.Audio;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Racing;
using Runtime.Gameplay.Game.Systems;
using Runtime.Gameplay.GameplayStates.Game.Controllers;
using Runtime.Gameplay.HelperServices.Audio;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class GameplayStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly GameSetupController _gameSetupController;
        private readonly RaceManager _raceManager;
        private readonly LosePopupStateController _losePopupStateController;
        private readonly WinPopupStateController _winPopupStateController;
        private readonly GameData _gameData;
        private readonly PlayerHealthTracker _playerHealthTracker;
        private readonly HPDepletePopupController _hpDepletePopupController;
        private readonly PausePopupStateController _pausePopupStateController;
        private readonly UserProgressService _userProgressService;
        private readonly IAudioService _audioService;

        private GameplayScreen _screen;
        
        public GameplayStateController(ILogger logger, IUiService uiService,
            GameSetupController gameSetupController, RaceManager raceManager,
            LosePopupStateController losePopupStateController, WinPopupStateController winPopupStateController,
            GameData gameData, PlayerHealthTracker playerHealthTracker, HPDepletePopupController hpDepletePopupController,
            PausePopupStateController pausePopupStateController, UserProgressService userProgressService,
            IAudioService audioService) : base(logger)
        {
            _uiService = uiService;
            _gameSetupController = gameSetupController;
            _raceManager = raceManager;
            _losePopupStateController = losePopupStateController;
            _winPopupStateController = winPopupStateController;
            _hpDepletePopupController = hpDepletePopupController;
            _gameData = gameData;
            _playerHealthTracker = playerHealthTracker;
            _pausePopupStateController = pausePopupStateController;
            _userProgressService = userProgressService;
            _audioService = audioService;
            
            _raceManager.OnPlayerFinished += ProcessPlayerFinish;
            _playerHealthTracker.OnPlayerDied += ShowHPDepletePopup;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            _gameSetupController.StartGame();
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            _userProgressService.SaveCoins();
            _gameSetupController.EndGame();
            
            await _uiService.HideScreen(ConstScreens.GameplayScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<GameplayScreen>(ConstScreens.GameplayScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnPausePressed += () => _pausePopupStateController.Enter().Forget();
        }

        private void ProcessPlayerFinish()
        {
            if (_gameData.PlayerFinishPlace == 1)
            {
                _audioService.PlaySound(ConstAudio.VictorySound);
                _winPopupStateController.Enter().Forget();
            }
            else
            {
                _audioService.PlaySound(ConstAudio.LoseSound);
                _losePopupStateController.Enter().Forget();
            }
        }

        private void ShowHPDepletePopup()
        {
            _audioService.PlaySound(ConstAudio.LoseSound);
            _hpDepletePopupController.Enter().Forget();
        }
    }
}