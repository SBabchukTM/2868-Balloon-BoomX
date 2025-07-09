using TMPro;
using UnityEngine;

namespace Runtime.Gameplay.ScreensPopups
{
    public class UserRecordDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _placeText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _timeText;

        public void Initialize(int place, string name, float time)
        {
            _placeText.text = place + ".";
            _nameText.text = name;
            _timeText.text = FormatTime(time);
        }

        private string FormatTime(float time)
        {
            if (time == 0)
                return "No data";

            return Tools.Tools.FormatTime(time);
        }
    }
}
