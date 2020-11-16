namespace RPG.Combat.Kata
{
    public class MeleeCharacter : Character
    {
        
        public MeleeCharacter(int health = ImportantValues.MaxHealth, int level = ImportantValues.defaultStartingLevel) : base (health, level) 
        {
            AttackRange = 2;   
        }

        

    }
}