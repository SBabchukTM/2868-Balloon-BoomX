using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Inventory
{
    public class InventoryItemDisplay : MonoBehaviour
    {
        private const string UsedStatusText = "Used";
        private const string UnusedStatusText = "Use";
    
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Button _button;
        [SerializeField] private Color _unusedColor;

        private int _itemID;
    
        public event Action<int> OnSelected;
    
        public void Initialize(Sprite itemSprite, InventoryItemStatusType type, int itemID)
        {
            _itemImage.sprite = itemSprite;
            _itemID = itemID;
            UpdateDisplay(type);
        
            _button.onClick.AddListener(() => OnSelected?.Invoke(itemID));
        }

        public void UpdateStatus(int usedID)
        {
            UpdateDisplay(_itemID == usedID ? InventoryItemStatusType.Used : InventoryItemStatusType.Unused);
        }

        private void UpdateDisplay(InventoryItemStatusType type)
        {
            _statusText.text = type == InventoryItemStatusType.Used ? UsedStatusText : UnusedStatusText;
            _button.image.color = type == InventoryItemStatusType.Unused ? _unusedColor : Color.white;
        }
    }

    public enum InventoryItemStatusType
    {
        Used,
        Unused,
    }
}