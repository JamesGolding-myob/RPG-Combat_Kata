namespace RPG.Combat.Kata
{
    public class DamageController
    {
        public void ApplyDamage(IHaveHealth victim, int damage)
        {
            victim.ChangeHealth(damage);
        }
    }
}