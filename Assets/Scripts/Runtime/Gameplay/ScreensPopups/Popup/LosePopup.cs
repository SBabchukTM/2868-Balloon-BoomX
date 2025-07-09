using System;
using System.Threading;
using Core.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Popup
{
    public class LosePopup : BasePopup
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _dataTypeText;
        [SerializeField] private TextMeshProUGUI _dataText;
        
        public event Action OnHomeButtonPressed;

        public event Action OnRetryButtonPressed;

        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            _homeButton.onClick.AddListener(() => OnHomeButtonPressed?.Invoke());
            _retryButton.onClick.AddListener(() => OnRetryButtonPressed?.Invoke());
            return base.Show(data, cancellationToken);
        }

        public void SetRaceData(int coins, int place)
        {
            _coinsText.text = coins.ToString();
            _dataTypeText.text = "Place";
            _dataText.text = FormatPlace(place);
        }

        private string FormatPlace(int place)
        {
            switch (place)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                case 4:
                    return "4th";
                default:
                    return "None";
            }
        }
    }
}