using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Gameplay.Game
{
    public class BotBalloonController : BaseBalloonController
    {
        private const float ShootAheadDistance = 20;
        private const float CheckRadius = 6.5f;
    
        private const float MinBoostDelay = 3;
        private const float MaxBoostDelay = 10;
    
        private const float FirstSpikeUseDelayMin = 3;
        private const float FirstSpikeUseDelayMax = 6;
    
        private LayerMask _balloonLayerMask;
        private RaycastHit2D[] _hits;

        private float _boostDelay;
        private float _spikesDelay;

        private void Start()
        {
            _hits = new RaycastHit2D[4];
            _balloonLayerMask = LayerMask.GetMask("Balloon");
            SelectBoostDelay();
            SelectSpikesDelay();
        }

        private void LateUpdate()
        {
            TryTriggerBoost();
            TryTriggerSpike();
            TryTriggerRocket();
        }

        private void TryTriggerBoost()
        {
            if(BoostAbility.CooldownPercent > 0)
                return;
        
            _boostDelay -= Time.deltaTime;
        
            if(_boostDelay > 0)
                return;

            SelectBoostDelay();
            BoostAbilityTriggerCondition.RequestTrigger();
        }

        private void TryTriggerSpike()
        {
            if(SpikesAbility.CooldownPercent > 0)
                return;
        
            _spikesDelay -= Time.deltaTime;
        
            if(_spikesDelay > 0)
                return;
        
            if(EnemyBalloonNearby())
                SpikesAbilityTriggerCondition.RequestTrigger();
        }

        private void TryTriggerRocket()
        {
            if(RocketAbility.CooldownPercent > 0)
                return;
        
            if(EnemyBalloonAhead())
                RocketAbilityTriggerCondition.RequestTrigger();
        }

        private bool EnemyBalloonAhead()
        {
            var size = Physics2D.RaycastNonAlloc(transform.position, Vector2.up, _hits, ShootAheadDistance, _balloonLayerMask);
            return size > 1;
        }

        private bool EnemyBalloonNearby()
        {
            var size = Physics2D.CircleCastNonAlloc(transform.position, CheckRadius, Vector2.zero, _hits, _balloonLayerMask);
            return size > 1;
        }

        private void SelectBoostDelay() => _boostDelay = Random.Range(MinBoostDelay, MaxBoostDelay);

        private void SelectSpikesDelay() => _spikesDelay = Random.Range(FirstSpikeUseDelayMin, FirstSpikeUseDelayMax);
    }
}
