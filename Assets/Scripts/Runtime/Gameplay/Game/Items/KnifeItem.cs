using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.Game.Items
{
    public class KnifeItem : DamageItem, IDamageable
    {
        [SerializeField] private float _moveDistance = 1f;
        [SerializeField] private float _moveDuration = 1f;

        private void Start()
        {
            float targetY = transform.position.y + _moveDistance;

            transform.DOMoveY(targetY, _moveDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).SetLink(gameObject);
        }

        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
