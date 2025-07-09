using Runtime.Gameplay.Game.Achievements;

namespace Runtime.Gameplay.Game.Abilities.Rocket
{
    public class PlayerRocketAbilityTrigger : PlayerAbilityTrigger
    {
        public override void TriggerAbility()
        {
            base.TriggerAbility();
            AchievementEvents.InvokeUsedRocket();
        }
    }
}
