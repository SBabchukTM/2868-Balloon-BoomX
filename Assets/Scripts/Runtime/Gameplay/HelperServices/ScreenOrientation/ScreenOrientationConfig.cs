using Core;
using UnityEngine;

namespace Runtime.Gameplay.HelperServices.ScreenOrientation
{
    [CreateAssetMenu(fileName = "ScreenOrientationConfig", menuName = "Config/ScreenOrientationConfig")]
    public sealed class ScreenOrientationConfig : BaseSettings
    {
        public ScreenOrientationTypes ScreenOrientationTypes;
        public bool EnableScreenOrientationPopup;
    }
}