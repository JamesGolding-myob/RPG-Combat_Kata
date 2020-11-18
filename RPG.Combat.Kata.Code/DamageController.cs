namespace RPG.Combat.Kata
{
    public class DamageController
    {
        public void InflictDamage(IHaveHealth victim, int damage)
        {
            victim.ChangeHealth(damage);
        }
    }
}