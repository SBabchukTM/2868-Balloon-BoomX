using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Roulette
{
    public class RouletteItemDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private RouletteItemModel _rouletteItemModel;

        public RouletteItemModel RouletteItemModel => _rouletteItemModel;
    
        public void Initialize(RouletteItemModel rouletteItemModel)
        {
            _rouletteItemModel = rouletteItemModel;
            _image.sprite = rouletteItemModel.ItemSprite;
        }
    }
}
