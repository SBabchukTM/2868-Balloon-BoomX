using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class MenuScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _termsOfUseButton;
        [SerializeField] private Button _PrivacyPolicyButton;
        [SerializeField] private Button _howToPlayButton;
        
        public event Action OnBackPressed;
        public event Action OnInventoryPressed;
        public event Action OnTermsOfUsePressed;
        public event Action OnPrivacyPolicyPressed;
        public event Action OnHowToPlayPressed;

        public void Initialize()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _inventoryButton.onClick.AddListener(() => OnInventoryPressed?.Invoke());
            _termsOfUseButton.onClick.AddListener(() => OnTermsOfUsePressed?.Invoke());
            _PrivacyPolicyButton.onClick.AddListener(() => OnPrivacyPolicyPressed?.Invoke());
            _howToPlayButton.onClick.AddListener(() => OnHowToPlayPressed?.Invoke());
            
        }
    }
}