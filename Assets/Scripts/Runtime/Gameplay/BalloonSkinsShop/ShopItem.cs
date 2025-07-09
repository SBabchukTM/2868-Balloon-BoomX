using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    [CreateAssetMenu(fileName = "Shop Item", menuName = "Config/Shop Item")]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private int _itemID;
        [FormerlySerializedAs("_itemPrice")] [SerializeField] private int _price;
        [FormerlySerializedAs("_itemSprite")] [SerializeField] private Sprite _sprite;
        [FormerlySerializedAs("_shopItemDisplayView")] [SerializeField] private ShopItemDisplay _shopItemDisplay;
        
        public int ItemID => _itemID;
        public int Price => _price;
        public Sprite Sprite => _sprite;
        public ShopItemDisplay ShopItemDisplay => _shopItemDisplay;
    }
}