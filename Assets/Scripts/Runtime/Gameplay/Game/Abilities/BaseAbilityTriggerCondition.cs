using System;

namespace Runtime.Gameplay.Game.Abilities
{
    public abstract class BaseAbilityTriggerCondition
    {
        public event Action OnTriggered;

        public virtual void RequestTrigger()
        {
            OnTriggered?.Invoke();
        }
    }
}