using System;
using Runtime.Gameplay.Game.Systems;
using Object = UnityEngine.Object;

namespace Runtime.Gameplay.Game
{
    public class PlayerHealthTracker : IInitializeSystem
    {
        public event Action<int> OnPlayerHpChanged;
        public event Action OnPlayerDied;
        
        public PlayerHealthTracker(SystemsManager systemsManager)
        {
            systemsManager.RegisterSystem(this);
        }
        
        public void Initialize()
        {
            Object.FindObjectOfType<PlayerBalloonController>().OnHealthChanged += ProcessHealthChange;
        }

        private void ProcessHealthChange(int healthLeft)
        {
            if(healthLeft == 0)
                OnPlayerDied?.Invoke();
            else
                OnPlayerHpChanged?.Invoke(healthLeft);
        }
    }
}