using System;
using Runtime.Gameplay.Game;
using Runtime.Gameplay.Game.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class GameplayScreen : UiScreen
    {
        [SerializeField] private Button _pauseButton;

        [SerializeField] private GameObject _timerGO;
        [SerializeField] private GameObject _levelGO;
        [SerializeField] private GameObject _heartsGO;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private Image[] _healthImages;
        [SerializeField] private Sprite _damagedHealthSprite;

        private GameData _gameData;
        private PlayerHealthTracker _playerHealthTracker;
        private GameTimer _gameTimer;

        public event Action OnPausePressed;

        [Inject]
        private void Construct(GameData gameData, PlayerHealthTracker playerHealthTracker, GameTimer gameTimer)
        {
            _gameData = gameData;
            _playerHealthTracker = playerHealthTracker;
            _gameTimer = gameTimer;

            _levelText.text = "Level " + (_gameData.LevelID + 1);
            _playerHealthTracker.OnPlayerHpChanged += UpdateHealth;
            _gameTimer.OnSecondPassed += UpdateTime;
            _gameData.OnCoinCollected += UpdateCoins;
            
            _timerGO.SetActive(_gameData.GameplayMode == GameplayMode.TimeAttack);
            _levelGO.SetActive(_gameData.GameplayMode == GameplayMode.Race);
            _heartsGO.SetActive(_gameData.GameplayMode == GameplayMode.Race);
        }

        private void OnDestroy()
        {
            _playerHealthTracker.OnPlayerHpChanged -= UpdateHealth;
            _gameTimer.OnSecondPassed -= UpdateTime;
        }

        private void UpdateHealth(int health)
        {
            _healthImages[health].sprite = _damagedHealthSprite;
        }

        public void Initialize()
        {
            _pauseButton.onClick.AddListener(() => OnPausePressed?.Invoke());
        }

        private void UpdateTime(int time)
        {
            _timeText.text = Tools.Tools.FormatTime(time);
        }

        private void UpdateCoins(int amount)
        {
            _coinsText.text = amount.ToString();
        }
    }
}