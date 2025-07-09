using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class QuestsScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _parent;
        
        public event Action OnBackPressed;

        public void Initialize(List<AchievementDisplay> achievementDisplays)
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());

            foreach (var achievementDisplay in achievementDisplays)
            {
                achievementDisplay.transform.SetParent(_parent,false);
            }
        } 
    }
}