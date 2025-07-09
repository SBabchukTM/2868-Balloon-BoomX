using System;
using System.Collections.Generic;
using Runtime.Gameplay.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class InventoryScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _parent;
        
        public event Action OnBackPressed;

        public void Initialize(List<InventoryItemDisplay> inventoryItemDisplayList)
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());

            foreach (var item in inventoryItemDisplayList)
            {
                item.transform.SetParent(_parent, false);
            }
        }
    }
}