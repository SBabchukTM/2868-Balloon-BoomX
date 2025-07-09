using UnityEngine;

namespace Runtime.Gameplay.Game.Movement
{
    public class BalloonPositionClamper
    {
        private readonly CameraSizeProvider _cameraSizeProvider;

        public BalloonPositionClamper(CameraSizeProvider cameraSizeProvider)
        {
            _cameraSizeProvider = cameraSizeProvider;
        }

        public void ClampPosition(Transform transform)
        {
            Vector2 cameraHalfSize = _cameraSizeProvider.HalfSize;
        
            Vector3 newPosition = transform.position;
        
            ClampX(ref newPosition, transform, cameraHalfSize.x);
            ClampY(ref newPosition, _cameraSizeProvider.Camera.transform.position, transform, cameraHalfSize.y);
        
            transform.position = newPosition;
        }

        private void ClampX(ref Vector3 position, Transform transform, float halfSize)
        {
            if(transform.position.x < -halfSize)
                position.x = -halfSize;
        
            if(transform.position.x > halfSize)
                position.x = halfSize;
        }

        private void ClampY(ref Vector3 position, Vector3 cameraPos, Transform transform, float halfSize)
        {
            if(transform.position.y < cameraPos.y - halfSize)
                position.y = cameraPos.y - halfSize;
        }
    }
}
