namespace RPG.Combat.Kata
{
    public class MeleeCharacter : Character
    {
        
        public MeleeCharacter(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.defaultStartingLevel, double speed = 8 ): base(health, level)
        {
            Speed = speed;
            AttackRange = 2;   
        }

        

    }
}