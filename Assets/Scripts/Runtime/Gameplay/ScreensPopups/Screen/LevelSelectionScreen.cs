using System;
using Runtime.Gameplay.Game.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class LevelSelectionScreen : UiScreen
    {
        [SerializeField] private Button _backButton;

        [SerializeField, Space] private RectTransform _levelSelectionButtonsParent;

        [SerializeField, Space] private LevelSelectionButtonStatusDisplay _selectedButtonDisplay;
        [SerializeField] private LevelSelectionButtonStatusDisplay _unlockedButtonDisplay;
        [SerializeField] private LevelSelectionButtonStatusDisplay _lockedButtonDisplay;

        public event Action OnBackPressed;
        public event Action<int> OnSelectedLevelChanged;

        private int _lastSelectedButtonID = 0;

        private LevelSelectionButton[] _levelSelectionButtons;

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Initialize(int lastUnlockedID)
        {
            SubscribeToEvents();
            FindLevelSelectionButtons();
            InitializeButtons(lastUnlockedID);
        }

        private void SubscribeToEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }

        private void UnsubscribeFromEvents()
        {
            _backButton.onClick.RemoveAllListeners();

            int size = _levelSelectionButtons.Length;
            for (int i = 0; i < size; i++)
                _levelSelectionButtons[i].OnLevelSelected -= UpdateSelectedLevel;
        }

        private void FindLevelSelectionButtons()
        {
            _levelSelectionButtons = _levelSelectionButtonsParent.GetComponentsInChildren<LevelSelectionButton>();
        }

        private void InitializeButtons(int lastUnlockedLevelID)
        {
            int size = _levelSelectionButtons.Length;

            _lastSelectedButtonID = lastUnlockedLevelID;

            for (int i = 0; i < size; i++)
            {
                bool locked = i > lastUnlockedLevelID;
                bool selected = i == lastUnlockedLevelID;

                var button = _levelSelectionButtons[i];
                InitializeButton(locked, selected, button, i);
                button.OnLevelSelected += UpdateSelectedLevel;
            }
        }

        private void InitializeButton(bool locked, bool selected, LevelSelectionButton button, int id)
        {
            button.Initialize(locked, id);
            if (selected)
                SetButtonStatusDisplay(button, _selectedButtonDisplay);
            else if (locked)
                SetButtonStatusDisplay(button, _lockedButtonDisplay);
            else
                SetButtonStatusDisplay(button, _unlockedButtonDisplay);
        }

        private void SetButtonStatusDisplay(LevelSelectionButton button, LevelSelectionButtonStatusDisplay display)
        {
            button.SetColor(display.Color);
            if(display.Sprite)
                button.SetSprite(display.Sprite);
        }

        private void UpdateSelectedLevel(int level)
        {
            SetButtonStatusDisplay(_levelSelectionButtons[_lastSelectedButtonID], _unlockedButtonDisplay);
            OnSelectedLevelChanged?.Invoke(level);

            _lastSelectedButtonID = level;
            SetButtonStatusDisplay(_levelSelectionButtons[_lastSelectedButtonID], _selectedButtonDisplay);
        }
    }

    [Serializable]
    public class LevelSelectionButtonStatusDisplay
    {
        [Header("If Sprite is Null, it won't be set")]
        public Sprite Sprite;
        public Color Color = Color.white;
    }
}