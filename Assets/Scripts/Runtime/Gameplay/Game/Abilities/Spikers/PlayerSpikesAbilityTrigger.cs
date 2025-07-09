using Runtime.Gameplay.Game.Achievements;

namespace Runtime.Gameplay.Game.Abilities.Spikers
{
    public class PlayerSpikesAbilityTrigger : PlayerAbilityTrigger
    {
        public override void TriggerAbility()
        {
            base.TriggerAbility();
            AchievementEvents.InvokeUsedSpikes();
        }
    }
}