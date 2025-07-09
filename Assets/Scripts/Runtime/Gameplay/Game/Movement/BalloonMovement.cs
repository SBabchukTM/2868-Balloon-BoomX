using Runtime.Gameplay.Game.Input;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Movement
{
    public class BalloonMovement : MonoBehaviour
    {
        [SerializeField] private float _maxHorizontalForce;
        [SerializeField] private float _maxAscendForce;
        [SerializeField] private float _maxDescendForce;
    
        private Rigidbody2D _rigidbody;
        private IInputProvider _inputProvider;
        private BalloonPositionClamper _positionClamper;

        [Inject]
        public void Construct(IInputProvider inputProvider, BalloonPositionClamper clamper)
        {
            _inputProvider = inputProvider;
            _positionClamper = clamper;
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void Move(float speedMultiplier)
        {
            if(_inputProvider == null) return;
        
            var input = _inputProvider.GetInput();
            _rigidbody.AddForce(CalculateForce(input, speedMultiplier));
            _positionClamper.ClampPosition(transform);
        }

        public void SetSpeedMultiplier(float speedMultiplier)
        {
            _maxHorizontalForce *= speedMultiplier;
            _maxAscendForce *= speedMultiplier;
            _maxDescendForce *= speedMultiplier;
        }
        
        private Vector2 CalculateForce(Vector2 input, float speedMultiplier)
        {
            Vector2 result = Vector2.zero;
            result.x = input.x * _maxHorizontalForce;
            result.y = input.y * (input.y > 0 ? _maxAscendForce : _maxDescendForce);
            return result * speedMultiplier;
        }
    }
}