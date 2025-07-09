using System;
using System.Threading;
using Core.UI;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Roulette;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Popup
{
    public class RouletteRewardPopup : BasePopup
    {
        [SerializeField] private Button _claimButton;
        [SerializeField] private Image _prizeImage;
        [SerializeField] private TextMeshProUGUI _amountText;
        
        public event Action OnClaimPressed;
        
        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            _claimButton.onClick.AddListener(() => OnClaimPressed?.Invoke());
            return base.Show(data, cancellationToken);
        }

        public void SetData(RouletteItemModel rouletteItemModel)
        {
            _prizeImage.sprite = rouletteItemModel.ItemSprite;
            if (rouletteItemModel.ItemType == RouletteItemType.Coins)
                _amountText.text = rouletteItemModel.Value.ToString();
        }
    }
}