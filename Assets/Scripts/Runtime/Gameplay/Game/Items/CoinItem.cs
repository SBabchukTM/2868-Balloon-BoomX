using Core.Services.Audio;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.HelperServices.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Items
{
    public class CoinItem : MonoBehaviour, IDamageable
    {
        private GameData _gameData;
    
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
    
        [Inject]
        private void Construct(GameData gameData)
        {
            _gameData = gameData;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            BaseBalloonController balloon = other.gameObject.GetComponent<BaseBalloonController>();
            if (balloon != null && balloon is PlayerBalloonController)
            {
                _audioService.PlaySound(ConstAudio.CoinSound);
                _gameData.Coins++;
                TakeDamage(1);
            }   
        }
    
        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
