using System;
namespace RPG.Combat.Kata
{
    public class World
    {
        public int Width{get; private set;}


        public World(int width)
        {
            Width = width;
        }

        public bool CharacterIsInRange(Character instigator, Character target)
        {    
            return (instigator.AttackRange >= GetDistanceBetweenCharacters(instigator, target));
        }

        private double GetDistanceBetweenCharacters(Character actioningCharacter, Character targetCharacter)
        {
            return Math.Abs(actioningCharacter.XPosition - targetCharacter.XPosition);
        }


    }
}