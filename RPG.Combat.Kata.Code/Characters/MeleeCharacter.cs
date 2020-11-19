namespace RPG.Combat.Kata
{
    public class MeleeCharacter : Character
    {
        
        public MeleeCharacter(World world, int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel, int speed = 8) : base(world, health, level, speed)
        {   
            AttackRange = 2;     
        }
   

    }
}