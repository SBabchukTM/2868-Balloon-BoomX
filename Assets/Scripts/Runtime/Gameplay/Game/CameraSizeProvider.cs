using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game
{
    public class CameraSizeProvider : IInitializable, ITickable
    {
        private UnityEngine.Camera _camera;
        private Vector2 _cameraSize;
        private Vector2 _cameraHalfSize;

        private bool _initialized = false;
    
        public UnityEngine.Camera Camera => _camera;
        public Vector2 CameraSize => _cameraSize;
        public Vector2 HalfSize => _cameraHalfSize;
    
        public void Initialize()
        {
            _camera = UnityEngine.Camera.main;
        }

        public void Tick()
        {
            if(_initialized)
                return;
        
            if(Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
                return;
        
            _cameraSize = GetCameraSize();
            _cameraHalfSize = _cameraSize / 2;
            _initialized = true;
        }

        private Vector2 GetCameraSize()
        {
            Vector2 bottomLeft = _camera.ViewportToWorldPoint(new(0, 0, _camera.nearClipPlane));
            Vector2 topRight = _camera.ViewportToWorldPoint(new(1, 1, _camera.nearClipPlane));
            return new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        }
    }
}
