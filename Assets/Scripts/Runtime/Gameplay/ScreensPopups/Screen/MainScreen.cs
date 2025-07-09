using System;
using Runtime.Gameplay.HelperServices.UserData.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class MainScreen : UiScreen
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _dailyButton;
        [SerializeField] private Button _missionsButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _balanceText;
        
        public event Action OnMenuPressed;
        public event Action OnProfilePressed;
        public event Action OnDailyPressed;
        public event Action OnMissionsPressed;
        public event Action OnLeaderboardPressed;
        public event Action OnSettingsPressed;
        public event Action OnShopPressed;
        public event Action OnStartPressed;

        [Inject]
        private void Construct(IUserInventoryService userInventoryService)
        {
            _balanceText.text = userInventoryService.GetBalance().ToString();
        }

        public void Initialize()
        {
            _menuButton.onClick.AddListener(() => OnMenuPressed?.Invoke());
            _profileButton.onClick.AddListener(() => OnProfilePressed?.Invoke());
            _dailyButton.onClick.AddListener(() => OnDailyPressed?.Invoke());
            _missionsButton.onClick.AddListener(() => OnMissionsPressed?.Invoke());
            _leaderboardButton.onClick.AddListener(() => OnLeaderboardPressed?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsPressed?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopPressed?.Invoke());
            _startButton.onClick.AddListener(() => OnStartPressed?.Invoke());
        }
    }
}