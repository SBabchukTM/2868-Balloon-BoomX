using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.GameplayStates.Game.Menu;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Popup;
using UnityEngine;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Controllers
{
    public class WinPopupStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly UserProgressService _userProgressService;
        private readonly GameData _gameData;

        public WinPopupStateController(ILogger logger, IUiService uiService,
            UserProgressService userProgressService, GameData gameData) : base(logger)
        {
            _uiService = uiService;
            _userProgressService = userProgressService;
            _gameData = gameData;
        }

        public override async UniTask Enter(CancellationToken cancellationToken = default)
        {
            Time.timeScale = 0;

            WinPopup popup = await _uiService.ShowPopup(ConstPopups.WinPopup) as WinPopup;

            if (_gameData.GameplayMode == GameplayMode.Race)
            {
                popup.SetRaceData(_gameData.Coins, 1);
                _userProgressService.SaveRacingProgress();
            }
            else
            {
                popup.SetTimeData(_gameData.Coins, _gameData.TimeAttackTime);
                _userProgressService.SaveTimeAttackProgress();
            }
            
            popup.OnHomeButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<MainScreenStateController>();
            };

            popup.OnNextButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();

                if (_userProgressService.NextLevelExists())
                    _gameData.LevelID++;
                
                await GoTo<GameplayStateController>();
            };        
        }
    }
}