namespace RPG.Combat.Kata
{
    public class MeleeCharacter : Character
    {
        
        public MeleeCharacter(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.defaultStartingLevel)
        {
            AttackRange = 2;   
        }

        

    }
}