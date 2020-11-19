using System;
namespace RPG.Combat.Kata
{
    public class World
    {
        private int _width;
        public Cell[] layout;
  
        public World(int width)
        {
            _width = width;
            layout = new Cell[_width]; //layout is 1d array to ease of iterating thorugh it but each cell has coordinates which I'm hopig to set when a world is instaniated - might also change world to be an interface later so we can do different levels/areas to explore
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