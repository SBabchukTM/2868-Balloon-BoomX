using UnityEngine;

namespace Runtime.Gameplay.Roulette
{
    public class RouletteItemModel
    {
        public RouletteItemType ItemType;
        public Sprite ItemSprite;
        public int Value;

        public RouletteItemModel(Sprite itemSprite, RouletteItemType itemType, int value)
        {
            ItemType = itemType;
            ItemSprite = itemSprite;
            Value = value;
        }
    }

    public enum RouletteItemType
    {
        Coins,
        Skin
    }
}