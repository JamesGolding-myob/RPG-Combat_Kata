namespace RPG.Combat.Kata
{

    public class RangedCharacter: Character
    {

        public RangedCharacter(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.defaultStartingLevel)
        {
            AttackRange = 20;
        }
    }
}