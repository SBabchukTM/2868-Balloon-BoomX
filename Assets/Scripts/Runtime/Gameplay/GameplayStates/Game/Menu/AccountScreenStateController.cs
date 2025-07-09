using System.Threading;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.ScreensPopups.Screen;
using Runtime.Gameplay.UserProfile;
using UnityEngine;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.GameplayStates.Game.Menu
{
    public class AccountScreenStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly UserProfileManager _userProfileManager;
        private readonly AvatarSelectionService _avatarSelectionService;
        private readonly UserDataValidationService _userDataValidationService;

        private AccountScreen _screen;

        private UserAccountData _modifiedData;

        public AccountScreenStateController(ILogger logger, IUiService uiService,
            UserProfileManager userProfileManager,
            AvatarSelectionService avatarSelectionService,
            UserDataValidationService userDataValidationService) : base(logger)
        {
            _uiService = uiService;
            _userProfileManager = userProfileManager;
            _avatarSelectionService = avatarSelectionService;
            _userDataValidationService = userDataValidationService;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CopyData();
            CreateScreen();
            SubscribeToEvents();
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.AccountScreen);
        }

        private void CopyData() => _modifiedData = _userProfileManager.GetAccountDataCopy();
        
        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<AccountScreen>(ConstScreens.AccountScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
            _screen.SetData(_modifiedData);
            _screen.SetAvatar(_userProfileManager.GetUsedAvatar());
        }

        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += GoToMenu;
            _screen.OnSavePressed += SaveAndLeave;
            _screen.OnChangeAvatarPressed += SelectNewAvatar;
            _screen.OnNameChanged += ValidateName;
        }

        private async void GoToMenu() => await GoTo<MainScreenStateController>();

        private void SaveAndLeave()
        {
            _userProfileManager.SaveAccountData(_modifiedData);
            GoToMenu();
        }

        private async void SelectNewAvatar()
        {
            Sprite newAvatar = await _avatarSelectionService.SelectImageFromGallery(512, CancellationToken.None);

            if (newAvatar != null)
            {
                _modifiedData.AvatarBase64 = _userProfileManager.ConvertToBase64(newAvatar);
                _screen.SetAvatar(newAvatar);
            }
        }

        private void ValidateName(string value)
        {
            if (!_userDataValidationService.CheckUsernameValid(value))
                _screen.SetData(_modifiedData);
            else
                _modifiedData.Username = value;
        }
    }
}