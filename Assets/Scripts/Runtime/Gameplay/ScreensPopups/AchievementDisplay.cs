using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups
{
    public class AchievementDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image[] _stars;
        [SerializeField] private Sprite _clearStar;
    
        private bool _cleared;
        public bool Cleared => _cleared;
    
        public void Initialize(string name, bool cleared)
        {
            _nameText.text = name;
            _cleared = cleared;
        
            if (cleared)
            {
                _slider.value = 1;
                for(int i = 0; i < _stars.Length; i++)
                    _stars[i].sprite = _clearStar;
            }
        }
    }
}
