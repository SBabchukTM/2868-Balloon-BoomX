namespace Runtime.Gameplay.Game.Abilities
{
    public abstract class BaseAbility
    {
        private float _cooldownTimeTotal;
        private float _currentCooldown;
    
        public float CurrentCooldown => _currentCooldown;
        public float CooldownPercent => _currentCooldown / _cooldownTimeTotal;

        public BaseAbility(float cooldownTimeTotal)
        {
            _cooldownTimeTotal = cooldownTimeTotal;
        }
    
        public void SetCooldown() => _currentCooldown = _cooldownTimeTotal;

        public void ResetCooldown() => _currentCooldown = 0;

        public virtual void Tick(float deltaTime)
        {
            if(_currentCooldown > 0)
                _currentCooldown -= deltaTime;
        }

        public abstract bool TryTriggerAbility();
    
        public abstract bool IsActive();
    }
}
