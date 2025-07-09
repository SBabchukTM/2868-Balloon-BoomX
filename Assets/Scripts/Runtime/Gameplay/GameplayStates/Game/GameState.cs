using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.GameplayStates.Game.Controllers;
using Runtime.Gameplay.GameplayStates.Game.Menu;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game
{
    public class GameState : StateController
    {
        private readonly StateMachine _stateMachine;

        private readonly MenuStateController _menuStateController;
        private readonly ShopStateController _shopStateController;
        private readonly LevelSelectionStateController _levelSelectionController;
        private readonly AccountScreenStateController _accountScreenStateController;
        private readonly UserDataStateChangeController _userDataStateChangeController;
        private readonly InitShopState _initShopState;
        private readonly GameplayStateController _gameplayStateController;
        private readonly HowToPlayStateController _howToPlayStateController;
        private readonly InventoryStateController _inventoryStateController;
        private readonly LeaderboardStateController _leaderboardStateController;
        private readonly MissionsStateController _missionsStateController;
        private readonly ModeSelectionStateController _modeSelectionStateController;
        private readonly PrivacyPolicyStateController _privacyPolicyStateController;
        private readonly RouletteStateController _rouletteStateController;
        private readonly SettingsStateController _settingsStateController;
        private readonly TermsOfUseStateController _termsOfUseStateController;
        private readonly MainScreenStateController _mainScreenStateController;
        private readonly WinPopupStateController _winPopupStateController;
        private readonly LosePopupStateController _losePopupStateController;
        private readonly HPDepletePopupController _hpDepletePopupController;
        private readonly PausePopupStateController _pausePopupStateController;

        public GameState(ILogger logger,
            MenuStateController menuStateController,
            ShopStateController shopStateController,
            LevelSelectionStateController levelSelectionController,
            AccountScreenStateController accountScreenStateController,
            StateMachine stateMachine,
            UserDataStateChangeController userDataStateChangeController,
            InitShopState initShopState,
            GameplayStateController gameplayStateController,
            HowToPlayStateController howToPlayStateController,
            InventoryStateController inventoryStateController,
            LeaderboardStateController leaderboardStateController,
            MissionsStateController missionsStateController,
            ModeSelectionStateController modeSelectionStateController,
            PrivacyPolicyStateController privacyPolicyStateController,
            RouletteStateController rouletteStateController,
            SettingsStateController settingsStateController,
            TermsOfUseStateController termsOfUseStateController,
            MainScreenStateController mainScreenStateController,
            WinPopupStateController winPopupStateController,
            LosePopupStateController losePopupStateController,
            HPDepletePopupController hpDepletePopupController,
            PausePopupStateController pausePopupStateController) : base(logger)
        {
            _stateMachine = stateMachine;
            _menuStateController = menuStateController;
            _shopStateController = shopStateController;
            _levelSelectionController = levelSelectionController;
            _accountScreenStateController = accountScreenStateController;
            _userDataStateChangeController = userDataStateChangeController;
            _initShopState = initShopState;
            _gameplayStateController = gameplayStateController;
            _howToPlayStateController = howToPlayStateController;
            _inventoryStateController = inventoryStateController;
            _leaderboardStateController = leaderboardStateController;
            _missionsStateController = missionsStateController;
            _modeSelectionStateController = modeSelectionStateController;
            _privacyPolicyStateController = privacyPolicyStateController;
            _rouletteStateController = rouletteStateController;
            _settingsStateController = settingsStateController;
            _termsOfUseStateController = termsOfUseStateController;
            _mainScreenStateController = mainScreenStateController;
            _winPopupStateController = winPopupStateController;
            _losePopupStateController = losePopupStateController;
            _hpDepletePopupController = hpDepletePopupController;
            _pausePopupStateController = pausePopupStateController;
        }

        public override async UniTask Enter(CancellationToken cancellationToken)
        {
            await _userDataStateChangeController.Run(default);

            _stateMachine.Initialize(_menuStateController, _shopStateController, 
                _initShopState, _levelSelectionController, _accountScreenStateController,
                _gameplayStateController, _howToPlayStateController, _inventoryStateController, _leaderboardStateController,
                _missionsStateController, _modeSelectionStateController,
                _privacyPolicyStateController, _rouletteStateController, _settingsStateController,
                _termsOfUseStateController, _mainScreenStateController,
                _winPopupStateController, _losePopupStateController, _hpDepletePopupController, _pausePopupStateController);
            _stateMachine.GoTo<MainScreenStateController>().Forget();
        }
    }
}