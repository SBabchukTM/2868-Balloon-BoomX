using Core.Services.Audio;
using DG.Tweening;
using Runtime.Gameplay.HelperServices.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Items
{
    public class RocketItem : DamageItem
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _velocity;
        [SerializeField] private float _scaleAnimTime;
    
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
    
        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _scaleAnimTime).SetEase(Ease.OutFlash).SetLink(gameObject);
            _audioService.PlaySound(ConstAudio.RocketShootSound);
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = Vector2.up * _velocity; 
        }

        protected override void ProcessCollision(Collision2D other)
        {
            _audioService.PlaySound(ConstAudio.RocketExplodeSound);
            base.ProcessCollision(other);
            Destroy(gameObject);
        }
    }
}
