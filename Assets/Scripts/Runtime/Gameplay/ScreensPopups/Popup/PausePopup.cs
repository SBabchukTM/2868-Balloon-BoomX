using System;
using System.Threading;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Popup
{
    public class PausePopup : BasePopup
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _homeButton;
        
        public event Action OnContinueButtonPressed;
        public event Action OnHomeButtonPressed;

        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            _continueButton.onClick.AddListener(() => OnContinueButtonPressed?.Invoke());
            _homeButton.onClick.AddListener(() => OnHomeButtonPressed?.Invoke());
            
            return base.Show(data, cancellationToken);
        }
    }
}