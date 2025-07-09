using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Gameplay.Game.Abilities
{
    public class PlayerAbilityTrigger : MonoBehaviour
    {
        [SerializeField] private Button _triggerButton;
        [SerializeField] private Image _cooldownImage;
    
        private BaseAbilityTriggerCondition _condition;
        private BaseAbility _ability;

        [Inject]
        private void Construct(BaseAbilityTriggerCondition condition, BaseAbility ability)
        {
            _condition = condition;
            _ability = ability;
        }
    
        private void Awake()
        {
            _triggerButton.onClick.AddListener(TriggerAbility);
            _ability.ResetCooldown();
        }

        private void Update()
        {
            _cooldownImage.fillAmount = _ability.CooldownPercent;
        }

        public virtual void TriggerAbility()
        {
            _condition.RequestTrigger();
        }
    }
}