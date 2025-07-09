namespace Runtime.Gameplay.Game.Items
{
    public class CactusItem : DamageItem, IDamageable
    {
        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
