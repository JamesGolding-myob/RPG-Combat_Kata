using System;
namespace RPG.Combat.Kata
{
    public class World
    {
        private int _width;
  
        public World(int width)
        {
            _width = width;
        }

        public bool CharacterIsInRange(Character instigator, Character target)
        {    
            return (instigator.AttackRange >= GetDistanceBetweenCharacters(instigator, target));
        }

        private double GetDistanceBetweenCharacters(Character actioningCharacter, Character targetCharacter)
        {
            return Math.Abs(actioningCharacter.XPosition - targetCharacter.XPosition);
        }

        public bool IsCharacterNewPositionInWorld(Character character)
        {
            return character.XPosition + character.Speed <= _width;
        }
    }
}