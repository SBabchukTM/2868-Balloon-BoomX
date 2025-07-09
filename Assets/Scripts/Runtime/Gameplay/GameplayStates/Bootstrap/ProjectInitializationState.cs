using System.Threading;
using Core;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.GameplayStates.Bootstrap.Controllers;
using Runtime.Gameplay.GameplayStates.Game;
using Runtime.Gameplay.HelperServices.ScreenOrientation;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.HelperServices.UserData;
using UnityEngine;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Bootstrap
{
    public class ProjectInitializationState : StateController
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUiService _uiService;
        private readonly ISettingProvider _settingProvider;
        private readonly UserDataService _userDataService;
        private readonly AchievementsManager _achievementsManager;
        private readonly AudioInitializationController _audioInitializationController;
        private readonly ScreenOrientationAlertController _screenOrientationAlertController;

        public ProjectInitializationState(IAssetProvider assetProvider,
            IUiService uiService,
            ILogger logger,
            ISettingProvider settingProvider,
            UserDataService userDataService,
            AchievementsManager achievementsManager,
            AudioInitializationController audioInitializationController,
            ScreenOrientationAlertController screenOrientationAlertController) : base(logger)
        {
            _assetProvider = assetProvider;
            _uiService = uiService;
            _settingProvider = settingProvider;
            _userDataService = userDataService;
            _achievementsManager = achievementsManager;
            _audioInitializationController = audioInitializationController;
            _screenOrientationAlertController = screenOrientationAlertController;
        }

        public override async UniTask Enter(CancellationToken cancellationToken)
        {
            Input.multiTouchEnabled = false;

            _userDataService.Initialize();
            _achievementsManager.Initialize();
            await _assetProvider.Initialize();
            await _uiService.Initialize();
            await _settingProvider.Initialize();
            await _screenOrientationAlertController.Run(CancellationToken.None);
            _uiService.ShowScreen(ConstScreens.SplashScreen, cancellationToken).Forget();
            await _audioInitializationController.Run(CancellationToken.None);
            UpdateUserSession();

            GoTo<GameState>().Forget();
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.SplashScreen);
        }

        private void UpdateUserSession()
        {
            _userDataService.GetUserData().GameData.SessionNumber++;
            _userDataService.SaveUserData();
        }
    }
}