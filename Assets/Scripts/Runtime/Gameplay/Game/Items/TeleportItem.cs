using Core.Services.Audio;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.HelperServices.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Items
{
    public class TeleportItem : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _teleportDistance;
    
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            BaseBalloonController balloon = other.gameObject.GetComponent<BaseBalloonController>();
        
            if(balloon == null)
                return;
        
            if(balloon is PlayerBalloonController)
                AchievementEvents.InvokeUsedTeleport();

            _audioService.PlaySound(ConstAudio.TeleportSound);
            balloon.transform.position += new Vector3(0, _teleportDistance, 0);
            TakeDamage(1);
        }

        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
