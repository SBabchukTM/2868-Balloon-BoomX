using System;
using Runtime.Gameplay.HelperServices.UserData;

namespace Runtime.Gameplay.Roulette
{
    public class UserLoginService
    {
        private readonly UserDataService _userDataService;

        public UserLoginService(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public bool CanSpinRoulette()
        {
            var loginData = _userDataService.GetUserData().UserLoginData;

            if (loginData.LastRouletteSpinDate == String.Empty)
                return true;
            
            var date = Convert.ToDateTime(loginData.LastRouletteSpinDate);
            return DateTime.Now.Date > date.Date;
        }
        
        public void RecordSpinDate() => _userDataService.GetUserData().UserLoginData.LastRouletteSpinDate = DateTime.Now.ToString();
    }
}