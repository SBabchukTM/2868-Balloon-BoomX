using System;
using System.Collections.Generic;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.UserData.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class ShopScreen : UiScreen
    {
        [SerializeField] private Button _goBackButton;
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private RectTransform _shopItemsParent;
        [SerializeField] private TextMeshProUGUI _errorText;
        
        private IUserInventoryService _userInventoryService;

        public event Action OnBackPressed;

        [Inject]
        public void Construct(IUserInventoryService userInventoryService) =>
                _userInventoryService = userInventoryService;
        
        private void Start()
        {
            SubscribeToEvents();
            UpdateBalance(_userInventoryService.GetBalance());
        }

        private void OnDestroy() =>
                UnSubscribe();

        public void SetShopItems(List<ShopItemDisplay> items)
        {
            _errorText.gameObject.SetActive(items.Count == 0);
            
            foreach (var item in items)
                item.transform.SetParent(_shopItemsParent, false);
        }

        private void UpdateBalance(int balance) => 
                _balanceText.text = balance.ToString();

        private void SubscribeToEvents()
        {
            _goBackButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _userInventoryService.OnBalanceChanged += UpdateBalance;
        }

        private void UnSubscribe()
        {
            _goBackButton.onClick.RemoveAllListeners();
            _userInventoryService.OnBalanceChanged -= UpdateBalance;
        }
    }
}