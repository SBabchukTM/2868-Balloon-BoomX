using UnityEngine;

namespace Runtime.Gameplay.Game.Items
{
    public class BrickWallItem : MonoBehaviour, IDamageable
    {
        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
