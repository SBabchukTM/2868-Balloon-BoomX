using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.Roulette
{
    public class RouletteSpinner
    {
        private const float AngleBetweenItems = 45f;
        private const float FullRotation = 360f;
        private const float SpinDuration = 5f;
        private const float AdditionalSpins = 4;
        
        public async UniTask Spin(RectTransform roulette, int index)
        {
            float targetAngle = AngleBetweenItems * index;
            float additionalAngle = AdditionalSpins * FullRotation;
            
            Vector3 targetRotation = new Vector3(0, 0, targetAngle + additionalAngle);
            
            roulette.DOLocalRotate(targetRotation, SpinDuration, RotateMode.FastBeyond360).
                SetEase(Ease.OutCubic).SetLink(roulette.gameObject);  
            
            await UniTask.WaitForSeconds(SpinDuration);
        }
    }
}