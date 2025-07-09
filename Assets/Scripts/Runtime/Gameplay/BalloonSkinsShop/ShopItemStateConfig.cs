using Core;
using UnityEngine;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    [CreateAssetMenu(fileName = "ShopItemsConfig", menuName = "Config/ShopItemsConfig")]
    public class ShopItemStateConfig : BaseSettings
    {
        [SerializeField, Space(20)] private string _statusText;
        [SerializeField] private ShopItemState _shopItemState;
        [SerializeField] private Sprite _stateSprite;
        public ShopItemState ShopItemState => _shopItemState;
    }
}