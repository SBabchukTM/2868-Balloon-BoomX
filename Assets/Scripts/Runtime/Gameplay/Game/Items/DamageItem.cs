using UnityEngine;

namespace Runtime.Gameplay.Game.Items
{
    public class DamageItem : MonoBehaviour
    {
        [SerializeField] private int _damage;
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            ProcessCollision(other);
        }

        protected virtual void ProcessCollision(Collision2D other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
            if(damageable != null)
                damageable.TakeDamage(_damage);
        }
    }
}
