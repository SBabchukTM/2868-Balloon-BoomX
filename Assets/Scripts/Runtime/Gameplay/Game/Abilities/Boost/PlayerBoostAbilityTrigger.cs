using Runtime.Gameplay.Game.Achievements;

namespace Runtime.Gameplay.Game.Abilities.Boost
{
    public class PlayerBoostAbilityTrigger : PlayerAbilityTrigger
    {
        public override void TriggerAbility()
        {
            base.TriggerAbility();
            AchievementEvents.InvokeUsedBoost();
        }
    }
}