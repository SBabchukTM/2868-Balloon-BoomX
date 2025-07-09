using Runtime.Gameplay.Game.Movement;
using Runtime.Gameplay.Game.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Camera
{
    public class PlayerCameraFollow : MonoBehaviour, IInitializeSystem, IEnableable, IResettable
    {
        private const float ZPos = -10;
        private bool _followPlayer = false;
        private float _topYPos = 0f;

        private Transform _targetTransform;
        private Vector3 _currentVelocity;

        [SerializeField] private float _smoothTime = 0.3f;

        [Inject]
        private void Construct(SystemsManager systemsManager, PlayerHealthTracker playerHealthTracker)
        {
            systemsManager.RegisterSystem(this);
        
            playerHealthTracker.OnPlayerHpChanged += _ => Reset();
        }
    

        private void LateUpdate()
        {
            if (!_followPlayer || !_targetTransform)
                return;

            MoveToTargetPos();
        }

        public void Reset()
        {
            transform.position = new Vector3(0, 0, ZPos);
            _topYPos = 0;
            _currentVelocity = Vector3.zero;
        }

        public void Enable(bool enable)
        {
            _followPlayer = enable;
        }

        private void MoveToTargetPos()
        {
            Vector3 targetPos = _targetTransform.position;
            targetPos.x = 0;
            targetPos.z = ZPos;

            if (targetPos.y > _topYPos)
            {
                _topYPos = targetPos.y;

                Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref _currentVelocity, _smoothTime);
                transform.position = newPos;
            }
        }

        public void Initialize()
        {
            _targetTransform = FindObjectOfType<PlayerBalloonMovement>(true).transform;
        }
    }
}
