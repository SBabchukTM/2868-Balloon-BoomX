using System;
using Core.Services.Audio;
using Runtime.Gameplay.Game.Abilities.Boost;
using Runtime.Gameplay.Game.Abilities.Rocket;
using Runtime.Gameplay.Game.Abilities.Spikers;
using Runtime.Gameplay.Game.Items;
using Runtime.Gameplay.Game.Movement;
using Runtime.Gameplay.Game.Racing;
using Runtime.Gameplay.HelperServices.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game
{
    public class BaseBalloonController : MonoBehaviour, IDamageable
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Sprite _spikesSprite;
        [SerializeField] protected Transform _rocketLaunchPoint;
        [SerializeField] private int _maxHealthPoints = 3;
    
        private BalloonMovement _balloonMovement;

        protected int _currentHealthPoints;
    
        private IAudioService _audioService;
    
        protected RocketAbility RocketAbility;
        protected RocketAbilityTriggerCondition RocketAbilityTriggerCondition;
    
        protected BoostAbility BoostAbility;
        protected BoostAbilityTriggerCondition BoostAbilityTriggerCondition;
    
        protected SpikesAbility SpikesAbility;
        protected SpikesAbilityTriggerCondition SpikesAbilityTriggerCondition;
    
        private Sprite _originalSprite;
    
        public event Action<int> OnHealthChanged;
    
    
        [Inject]
        private void Construct(
            RaceManager raceManager, IAudioService audioService,
            RocketAbility rocketAbility, RocketAbilityTriggerCondition rocketAbilityTriggerCondition,
            BoostAbility boostAbility, BoostAbilityTriggerCondition boostAbilityTriggerCondition,
            SpikesAbility spikesAbility, SpikesAbilityTriggerCondition spikesAbilityTriggerCondition)
        {
            raceManager.RegisterRacer(this);
        
            _audioService = audioService;
        
            RocketAbility = rocketAbility;
            RocketAbilityTriggerCondition = rocketAbilityTriggerCondition;
        
            BoostAbility = boostAbility;
            BoostAbilityTriggerCondition = boostAbilityTriggerCondition;
        
            SpikesAbility = spikesAbility;
            SpikesAbilityTriggerCondition = spikesAbilityTriggerCondition;
        
            _currentHealthPoints = _maxHealthPoints;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!SpikesAbility.IsActive())
                return;
        
            BaseBalloonController otherBalloon = other.gameObject.GetComponent<BaseBalloonController>();
            if(otherBalloon == null)
                return;
        
            otherBalloon.TakeDamage(1);
            SpikesAbility.Disable();
        }

        private void Awake()
        {
            _balloonMovement = GetComponent<BalloonMovement>();

            RocketAbilityTriggerCondition.OnTriggered += ShootRocket;
            BoostAbilityTriggerCondition.OnTriggered += EnableBoost;
            SpikesAbilityTriggerCondition.OnTriggered += EnableSpikes;
            SpikesAbility.OnFinished += DisableSpikes;
            BoostAbility.OnFinished += DisableBoostTrail;
        }

        private void OnDestroy()
        {
            RocketAbilityTriggerCondition.OnTriggered -= ShootRocket;
            BoostAbilityTriggerCondition.OnTriggered -= EnableBoost;
            SpikesAbilityTriggerCondition.OnTriggered -= EnableSpikes;
            SpikesAbility.OnFinished -= DisableSpikes;
            BoostAbility.OnFinished -= DisableBoostTrail;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            RocketAbility.Tick(deltaTime);
            BoostAbility.Tick(deltaTime);
            SpikesAbility.Tick(deltaTime);
        }

        private void FixedUpdate()
        {
            _balloonMovement.Move(BoostAbility.CurrentSpeedMultiplier);
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            _originalSprite = sprite;
        }
    
        public virtual void TakeDamage(int damage)
        {
            _currentHealthPoints -= damage;
            OnHealthChanged?.Invoke(_currentHealthPoints);
        
            _audioService.PlaySound(ConstAudio.PopSound);
        
            if(_currentHealthPoints <= 0)
                Destroy(gameObject);
        }

        public void Teleport(Vector3 position)
        {
            _trailRenderer.Clear();
            transform.position = position;
            _trailRenderer.Clear();
        }
    
        private void ShootRocket()
        {
            if(RocketAbility.TryTriggerAbility())
                RocketAbility.ShootRocket(_rocketLaunchPoint);
        }

        private void EnableBoost()
        {
            if (BoostAbility.TryTriggerAbility())
            {
                _audioService.PlaySound(ConstAudio.BoostSound);
                EnableBoostTrail();
            }
        }

        private void EnableSpikes()
        {
            if (SpikesAbility.TryTriggerAbility())
            {
                _audioService.PlaySound(ConstAudio.CactusSound);
                SetSpikesSkin(true);
            }
        }

        private void DisableSpikes() => SetSpikesSkin(false);

        private void SetSpikesSkin(bool set) => _spriteRenderer.sprite = set ? _spikesSprite : _originalSprite;

        private void EnableBoostTrail() => _trailRenderer.emitting = true;

        private void DisableBoostTrail() => _trailRenderer.emitting = false;
    }
}
