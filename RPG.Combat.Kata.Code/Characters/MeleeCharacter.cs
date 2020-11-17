namespace RPG.Combat.Kata
{
    public class MeleeCharacter : Character
    {
        
        public MeleeCharacter(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel, double speed = 8) : base(health, level, speed)
        {   
            AttackRange = 2;     
        }
   

    }
}