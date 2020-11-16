namespace RPG.Combat.Kata
{

    public class RangedCharacter: Character
    {

        public RangedCharacter(int health = ImportantValues.MaxHealth, int level = ImportantValues.defaultStartingLevel) : base(health, level)
        {
            AttackRange = 20;
        }
    }
}