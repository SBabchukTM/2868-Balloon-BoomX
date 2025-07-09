using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    [CreateAssetMenu(fileName = "ShopSetup", menuName = "Config/ShopSetup")]
    public class ShopConfig : BaseSettings
    {
        [SerializeField] private List<ShopItemStateConfig> _shopItemStateConfigs;
        [SerializeField] private List<ShopItem> _shopItems = new();
        [SerializeField, Space] private bool _confirmPurchase = true;
        [SerializeField, Space] private PurchaseEffectSettings _purchaseEffectSettings;
        [SerializeField] private Sprite _coinIcon;
        
        public List<ShopItem> ShopItems => _shopItems;
        public bool ConfirmPurchase => _confirmPurchase;
        public PurchaseEffectSettings PurchaseEffectSettings => _purchaseEffectSettings;
        public List<ShopItemStateConfig> ShopItemStateConfigs => _shopItemStateConfigs;
        public Sprite CoinIcon => _coinIcon;
    }
}