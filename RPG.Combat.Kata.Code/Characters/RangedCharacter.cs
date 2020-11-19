namespace RPG.Combat.Kata
{

    public class RangedCharacter: Character
    {

        public RangedCharacter(World world, int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel) : base(world, health, level)
        {
            AttackRange = 20;
        }
    }
}