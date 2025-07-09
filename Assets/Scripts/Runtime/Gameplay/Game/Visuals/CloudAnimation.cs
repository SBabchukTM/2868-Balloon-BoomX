using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.Game.Visuals
{
    public class CloudAnimation : MonoBehaviour
    {
        [SerializeField] private float _moveDistance = 2f;
        [SerializeField] private float _moveDuration = 3f;
    
        private void OnEnable()
        {
            transform.DOMoveX(transform.position.x + _moveDistance, _moveDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}
