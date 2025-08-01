using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class PrivacyPolicyScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        
        public event Action OnBackPressed;

        public void Initialize()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }
    }
}