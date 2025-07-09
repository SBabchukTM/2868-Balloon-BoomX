using UnityEngine;

namespace Runtime.Gameplay.Game.Items
{
    public class WindmillItem : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _rotorTransform;
        [SerializeField] private float _rotateSpeed;
    
        private void Update()
        {
            _rotorTransform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
        }

        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
