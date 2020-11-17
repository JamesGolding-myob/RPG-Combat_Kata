namespace RPG.Combat.Kata
{

    public class RangedCharacter: Character
    {

        public RangedCharacter(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.defaultStartingLevel, double speed = 6): base(health, level, speed)
        {
            AttackRange = 20;
        }
    }
}