using System.Threading;
using Core;
using Core.Services.Audio;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.HelperServices.UserData;
using Runtime.Gameplay.ScreensPopups.Popup;
using Runtime.Gameplay.ScreensPopups.Popup.Data;

namespace Runtime.Gameplay.GameplayStates.Game.Controllers
{
    public sealed class StartSettingsController : BaseController
    {
        private readonly IUiService _uiService;
        private readonly UserDataService _userDataService;
        private readonly IAudioService _audioService;

        public StartSettingsController(IUiService uiService, UserDataService userDataService, IAudioService audioService)
        {
            _uiService = uiService;
            _userDataService = userDataService;
            _audioService = audioService;
        }

        public override UniTask Run(CancellationToken cancellationToken)
        {
            base.Run(cancellationToken);

            SettingsPopup settingsPopup = _uiService.GetPopup<SettingsPopup>(ConstPopups.SettingsPopup);
            
            var userData = _userDataService.GetUserData();

            var isSoundVolume = userData.SettingsData.SoundVolume;
            var isMusicVolume = userData.SettingsData.MusicVolume;

            settingsPopup.Show(new SettingsPopupData(isSoundVolume, isMusicVolume), cancellationToken).Forget();
            CurrentState = ControllerState.Complete;
            return UniTask.CompletedTask;
        }
    }
}