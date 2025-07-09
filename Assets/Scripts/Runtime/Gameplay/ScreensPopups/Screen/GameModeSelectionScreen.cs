using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class GameModeSelectionScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _timeAttackButton;
        [SerializeField] private Button _raceButton;
        
        public event Action OnBackPressed;
        public event Action OnTimeAttackPressed;
        public event Action OnRacePressed;

        public void Initialize()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _timeAttackButton.onClick.AddListener(() => OnTimeAttackPressed?.Invoke());
            _raceButton.onClick.AddListener(() => OnRacePressed?.Invoke());
        }
    }
}