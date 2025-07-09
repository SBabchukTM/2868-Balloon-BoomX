using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class LeaderboardScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _parent;
        
        public event Action OnBackPressed;

        public void Initialize(List<UserRecordDisplay> records)
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());

            foreach (var record in records)
                record.transform.SetParent(_parent,false);
        }
    }
}