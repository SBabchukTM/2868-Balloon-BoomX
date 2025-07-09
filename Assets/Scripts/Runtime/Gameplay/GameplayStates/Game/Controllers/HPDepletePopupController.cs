using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.GameplayStates.Game.Menu;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Popup;
using UnityEngine;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Controllers
{
    public class HPDepletePopupController : StateController
    {
        private readonly IUiService _uiService;
    
        public HPDepletePopupController(ILogger logger, IUiService uiService) : base(logger)
        {
            _uiService = uiService;
        }

        public override async UniTask Enter(CancellationToken cancellationToken = default)
        {
            Time.timeScale = 0;
            HPDepletePopup popup = await _uiService.ShowPopup(ConstPopups.HPDepletePopup) as HPDepletePopup;

            popup.OnHomePressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<MainScreenStateController>();
            };

            popup.OnRetryPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<GameplayStateController>();
            };
        }
    }
}
