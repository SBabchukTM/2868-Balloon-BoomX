using System.Threading;
using Core.Services.Audio;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.HelperServices.UserData;
using Runtime.Gameplay.ScreensPopups.Screen;
using AudioType = Core.Services.Audio.AudioType;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class SettingsStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly UserDataService _userDataService;
        private readonly IAudioService _audioService;
        
        private SettingsScreen _screen;
        
        public SettingsStateController(ILogger logger, IUiService uiService, UserDataService userDataService, IAudioService audioService) : base(logger)
        {
            _uiService = uiService;
            _userDataService = userDataService;
            _audioService = audioService;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.SettingsScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<SettingsScreen>(ConstScreens.SettingsScreen);
            _screen.Initialize(_userDataService.GetUserData().SettingsData);
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MainScreenStateController>();
            
            _screen.OnMusicVolumeChanged += UpdateMusicVolume;
            _screen.OnSoundVolumeChanged += UpdateSoundVolume;
        }

        private void UpdateMusicVolume(float volume)
        {
            _audioService.SetVolume(AudioType.Music, volume);
            var userData = _userDataService.GetUserData();
            userData.SettingsData.MusicVolume = volume;
        }

        private void UpdateSoundVolume(float volume)
        {
            _audioService.SetVolume(AudioType.Sound, volume);
            var userData = _userDataService.GetUserData();
            userData.SettingsData.SoundVolume = volume;
        }
    }
}