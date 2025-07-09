using Core;
using UnityEngine;

namespace Runtime.Gameplay.UserProfile
{
    [CreateAssetMenu(fileName = "UserDataValidationConfig", menuName = "Config/UserDataValidationConfig")]
    public class UserDataValidationConfig : BaseSettings
    {
        [SerializeField, Tooltip("2-14 symbols, starts with a letter")] 
        private string _usernameRegex = "^[A-Za-z][A-Za-z0-9]{1,13}$";
    
        public string UsernameRegex => _usernameRegex;
    }
}
