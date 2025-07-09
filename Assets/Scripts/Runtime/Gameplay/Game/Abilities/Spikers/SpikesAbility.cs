using System;

namespace Runtime.Gameplay.Game.Abilities.Spikers
{
    public class SpikesAbility : BaseAbility
    {
        private const float CooldownTimeTotal = 14;
        private const float DurationTotal = 4;

        private float _duration;
        
        public event Action OnFinished;
        
        public SpikesAbility() : base(CooldownTimeTotal)
        {
            
        }

        public override bool TryTriggerAbility()
        {
            if (CurrentCooldown > 0)
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
                if(_duration <= 0)
                    OnFinished?.Invoke();
            }
        }
        
        public override bool IsActive() => _duration > 0;
        public void Disable()
        {
            _duration = 0;
            OnFinished?.Invoke();
        }
    }
}