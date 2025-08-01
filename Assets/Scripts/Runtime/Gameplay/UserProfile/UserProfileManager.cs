using System;
using Runtime.Gameplay.HelperServices.UserData;
using UnityEngine;

namespace Runtime.Gameplay.UserProfile
{
    public class UserProfileManager
    {
        private readonly UserDataService _userDataService;
        private readonly ImageProcessingService _imageProcessingService;
    
        public UserProfileManager(UserDataService userDataService, 
            ImageProcessingService imageProcessingService)
        {
            _userDataService = userDataService;
            _imageProcessingService = imageProcessingService;
        }
    
        public UserAccountData GetAccountDataCopy()
        {
            return _userDataService.GetUserData().UserAccountData.Copy();
        }

        public void SaveAccountData(UserAccountData modifiedData)
        {
            var origData = _userDataService.GetUserData().UserAccountData;

            foreach (var field in typeof(UserAccountData).GetFields())
                field.SetValue(origData, field.GetValue(modifiedData));

            _userDataService.SaveUserData();
        }

        public Sprite GetUsedAvatar()
        {
            if (!AvatarExists())
                return null;    
            else
                return _imageProcessingService.CreateAvatarSprite(GetAvatarBase64());
        }

        [Tooltip("Pass in the selected avatar and assign the returned string to the account data")]
        public string ConvertToBase64(Sprite sprite, int maxSize = 512) =>
            _imageProcessingService.ConvertToBase64(sprite, maxSize);


        private bool AvatarExists() => _userDataService.GetUserData().UserAccountData.AvatarBase64 != String.Empty;
        
        private string GetAvatarBase64() => _userDataService.GetUserData().UserAccountData.AvatarBase64;
    }
}
