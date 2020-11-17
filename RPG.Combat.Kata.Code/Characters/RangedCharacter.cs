namespace RPG.Combat.Kata
{

    public class RangedCharacter: Character
    {

        public RangedCharacter(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel) : base(health, level)
        {
            AttackRange = 20;
        }
    }
}