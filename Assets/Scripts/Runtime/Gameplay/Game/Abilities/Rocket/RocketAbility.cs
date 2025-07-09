using Runtime.Gameplay.Game.Factories;
using UnityEngine;

namespace Runtime.Gameplay.Game.Abilities.Rocket
{
    public class RocketAbility : BaseAbility
    {
        private const float CooldownTimeTotal = 10;
        private readonly RocketFactory _factory;
    
        private float _currentCooldown;

        public RocketAbility(RocketFactory factory) : base(CooldownTimeTotal)
        {
            _factory = factory;
        }

        public override bool TryTriggerAbility()
        {
            if (CurrentCooldown > 0)
                return false;
        
            SetCooldown();
            return true;
        }

        public void ShootRocket(Transform spawnPoint)
        {
            var rocket = _factory.CreateItem();
            rocket.transform.position = spawnPoint.position;
        }
    
        public override bool IsActive() => false;
    }
}
