using System;
using Runtime.Gameplay.Roulette;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class DailyRouletteScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _spinButton;
        [SerializeField] private GameObject _errorGo;
        [SerializeField] private RectTransform _rouletteTransform;
        [SerializeField] private RouletteItemDisplay[] _rouletteItemDisplays;
        
        public RectTransform RouletteTransform => _rouletteTransform;
        public RouletteItemDisplay[] RouletteItemDisplays => _rouletteItemDisplays;
        
        public event Action OnBackPressed;
        public event Action OnSpinPressed;

        public void Initialize()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _spinButton.onClick.AddListener(() => OnSpinPressed?.Invoke());
        }

        public void ShowError()
        {
            _errorGo.SetActive(true);
            _spinButton.interactable = false;
        }

        public void DisableFlow()
        {
            _backButton.interactable = false;
            _spinButton.interactable = false;
        }
    }
}