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
    public class LosePopupStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly GameData _gameData;
    
        public LosePopupStateController(ILogger logger, IUiService uiService, GameData gameData) : base(logger)
        {
            _uiService = uiService;
            _gameData = gameData;
        }

        public override async UniTask Enter(CancellationToken cancellationToken = default)
        {
            Time.timeScale = 0;

            LosePopup popup = await _uiService.ShowPopup(ConstPopups.LosePopup) as LosePopup;

            popup.SetRaceData(_gameData.Coins, _gameData.PlayerFinishPlace);
        
            popup.OnHomeButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<MainScreenStateController>();
            };

            popup.OnRetryButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<GameplayStateController>();
            };
        }
    }
}
