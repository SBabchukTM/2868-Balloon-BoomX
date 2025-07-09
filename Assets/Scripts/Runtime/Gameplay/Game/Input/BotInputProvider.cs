using UnityEngine;

namespace Runtime.Gameplay.Game.Input
{
    public class BotInputProvider : IInputProvider
    {
        private const float RaycastDistance = 25f;
        private const float EvasionCooldownDuration = 0.2f;
        private readonly Vector2 BoxSize = new(2f, 2f);

        private Transform _botTransform;
        private readonly RaycastHit2D[] _hits;
        private readonly LayerMask _obstacleMask;
        private float _evasionCooldown;
        private Vector2 _lastInput;

        private int _activeEvasionDirection;
        
        private CameraSizeProvider _cameraSizeProvider;

        public BotInputProvider(CameraSizeProvider cameraSizeProvider)
        {
            _hits = new RaycastHit2D[5];
            _obstacleMask = ~LayerMask.GetMask("Balloon");
            _cameraSizeProvider = cameraSizeProvider;
        }

        public void SetTransform(Transform transform)
        {
            _botTransform = transform;
        }

        public Vector2 GetInput()
        {
            if (_evasionCooldown > 0f)
            {
                _evasionCooldown -= Time.deltaTime;
                return _lastInput;
            }

            var hits = Physics2D.BoxCastNonAlloc(
                _botTransform.position,
                BoxSize,
                0f,
                Vector2.up,
                _hits,
                RaycastDistance,
                _obstacleMask
            );

            if (hits > 0)
            {
                var evadeVector = DecideEvadeDirection(hits);
                _lastInput = evadeVector;
                _evasionCooldown = EvasionCooldownDuration;
                return evadeVector;
            }

            _activeEvasionDirection = 0;
            _lastInput = Vector2.up;
            return Vector2.up;
        }

        private Vector2 DecideEvadeDirection(int hitCount)
        {
            var result = Vector2.up;
            Vector2 position = _botTransform.position;

            if (_activeEvasionDirection != 0)
            {
                result.x = _activeEvasionDirection;
                return ClampToScreenBounds(result, position);
            }

            var leftHits = 0;
            var rightHits = 0;

            for (var i = 0; i < hitCount; i++)
            {
                var hitPos = _hits[i].point;
                if (hitPos.x < position.x) leftHits++;
                else rightHits++;
            }

            var desiredDirection = leftHits > rightHits ? 1 : -1;

            var futureX = position.x + desiredDirection * BoxSize.x;
            if (Mathf.Abs(futureX) > _cameraSizeProvider.HalfSize.x - 3f)
                desiredDirection *= -1;

            _activeEvasionDirection = desiredDirection;
            result.x = desiredDirection;

            return ClampToScreenBounds(result, position);
        }

        private Vector2 ClampToScreenBounds(Vector2 move, Vector2 currentPosition)
        {
            var futureX = currentPosition.x + move.x;

            if (futureX > _cameraSizeProvider.HalfSize.x)
            {
                move.x = -1;
                _activeEvasionDirection = -1;
            }
            else if (futureX < -_cameraSizeProvider.HalfSize.x)
            {
                move.x = 1;
                _activeEvasionDirection = 1;
            }

            return move.normalized;
        }
    }
}