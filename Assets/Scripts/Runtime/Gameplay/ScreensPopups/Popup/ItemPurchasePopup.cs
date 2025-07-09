using System;
using System.Threading;
using Core.UI;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.ScreensPopups.Popup.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Popup
{
    public class ItemPurchasePopup : BasePopup
    {
        [SerializeField] private Image _shopItemImage;
        [SerializeField] private TextMeshProUGUI _shopItemPrice;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _denyButton;

        public event Action OnAcceptPressedEvent;
        public event Action OnDenyPressedEvent;

        private void OnDestroy()
        {
            _acceptButton.onClick.RemoveAllListeners();
            _denyButton.onClick.RemoveAllListeners();
        }

        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            SetData(data as ItemPurchasePopupData);
            SubscribeToEvents();
            return base.Show(data, cancellationToken);
        }

        private void SetData(ItemPurchasePopupData data)
        {
            _shopItemImage.sprite = data.ShopItem.Sprite;
            _shopItemPrice.text = data.ShopItem.Price.ToString();
        }

        private void SubscribeToEvents()
        {
            _acceptButton.onClick.AddListener(() => OnAcceptPressedEvent?.Invoke());
            _denyButton.onClick.AddListener(() => OnDenyPressedEvent?.Invoke());
        }
    }
}