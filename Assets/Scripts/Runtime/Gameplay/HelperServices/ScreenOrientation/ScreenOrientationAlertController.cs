using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.Pause;
using Runtime.Gameplay.HelperServices.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Runtime.Gameplay.HelperServices.ScreenOrientation
{
    public class ScreenOrientationAlertController : BaseController, ITickable
    {
        private readonly IUiService _uiService;
        private readonly ISettingProvider _settingProvider;

        private ScreenOrientationAlertPopup _alertPopup;
        private ScreenOrientationConfig _config;
        private bool _isInitialized;

        public ScreenOrientationAlertController(IUiService uiService, ISettingProvider settingProvider)
        {
            _uiService = uiService;
            _settingProvider = settingProvider;
        }

        public override UniTask Run(CancellationToken cancellationToken)
        {
            Init();
            
            return base.Run(cancellationToken);
        }

        public void Tick()
        {
            if(!_isInitialized)
                return;

            if(!_config || !_config.EnableScreenOrientationPopup)
                return;

            CheckScreenOrientation();
        }

        private void CheckScreenOrientation()
        {
            var currentScreenMode = Screen.orientation;

            if(IsSameScreenMode(currentScreenMode))
            {
                if(!_alertPopup.gameObject.activeSelf)
                    return;

                _alertPopup.Hide();
                PauseTaker.TakePause(GameStateTypeId.RunningState);

                return;
            }

            PauseTaker.TakePause(GameStateTypeId.PausedState);

            if(!_alertPopup.gameObject.activeSelf)
            {
                HideKeyboard();
                _alertPopup.Show(null);
            }
        }

        private static void HideKeyboard()
        {
            EventSystem.current?.SetSelectedGameObject(null);
        }

        private bool IsSameScreenMode(UnityEngine.ScreenOrientation currentScreenMode)
        {
            if(_config.ScreenOrientationTypes == ScreenOrientationTypes.Portrait)
            {
                if(currentScreenMode is UnityEngine.ScreenOrientation.Portrait or UnityEngine.ScreenOrientation.PortraitUpsideDown)
                {
                    return true;
                }
            }

            if(_config.ScreenOrientationTypes != ScreenOrientationTypes.Landscape)
                return (int)currentScreenMode == (int)_config.ScreenOrientationTypes;

            return currentScreenMode is UnityEngine.ScreenOrientation.LandscapeLeft or UnityEngine.ScreenOrientation.LandscapeRight;
        }

        private void Init()
        {
            _config = _settingProvider.Get<ScreenOrientationConfig>();

            if(!_config || !_config.EnableScreenOrientationPopup)
                return;

            _alertPopup = _uiService.GetPopup<ScreenOrientationAlertPopup>(ConstPopups.ScreenOrientationAlertPopup);
            _alertPopup.Hide();

            _isInitialized = true;
        }
    }
}