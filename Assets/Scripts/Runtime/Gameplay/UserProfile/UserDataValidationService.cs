using System.Text.RegularExpressions;
using Core;

namespace Runtime.Gameplay.UserProfile
{
    public class UserDataValidationService
    {
        private readonly ISettingProvider _settingProvider;

        public UserDataValidationService(ISettingProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }

        public bool CheckUsernameValid(string name)
        {
            UserDataValidationConfig userDataValidationConfig = _settingProvider.Get<UserDataValidationConfig>();
            return !string.IsNullOrWhiteSpace(name) && 
                   Regex.IsMatch(name, userDataValidationConfig.UsernameRegex);
        }
    }   
}
