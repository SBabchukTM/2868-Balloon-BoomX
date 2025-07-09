using System;

namespace Runtime.Gameplay.Game.Abilities.Boost
{
    public class BoostAbility : BaseAbility
    {
        private const float CooldownTimeTotal = 12f;
        private const float DurationTotal = 5f;
    
        private const float SpeedMultiplier = 1.5f;

        private float _currentSpeedMultiplier = 1;
        private float _duration = 0;

        public event Action OnFinished;
    
        public float CurrentSpeedMultiplier => _currentSpeedMultiplier;
    
        public BoostAbility() : base(CooldownTimeTotal)
        {
        }

        public override bool TryTriggerAbility()
        {
            if(CurrentCooldown > 0)
                return false;
        
            SetCooldown();
            _duration = DurationTotal;
            return true;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if (_duration > 0)
            {
                _duration -= deltaTime;
                _currentSpeedMultiplier = SpeedMultiplier;
            
                if(_duration <= 0)
                    OnFinished?.Invoke();
            }
            else
                _currentSpeedMultiplier = 1;
        }
    
        public override bool IsActive() => _duration > 0;
    }
}
