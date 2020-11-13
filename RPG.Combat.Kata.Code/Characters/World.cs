using System;
namespace RPG.Combat.Kata
{
    public class World
    {
        private int _width;
        private Character _characterOne;
        private Character _characterTwo;
        
        
        public World(int width, Character character1, Character character2)
        {
            _width = width;
            _characterOne = character1;
            _characterTwo = character2;

        }

        public bool CharacterIsInRange(MeleeCharacter instigator, MeleeCharacter target)
        {
            
            return (instigator.AttackRange >= GetDistanceBetweenCharacters(instigator, target));
        }

        public bool CharacterIsInRange(RangedCharacter instigator, MeleeCharacter target)
        {
            
            return (instigator.AttackRange >= GetDistanceBetweenCharacters(instigator, target));
        }
        
        public bool CharacterIsInRange(RangedCharacter instigator, RangedCharacter target)
        {
            
            return (instigator.AttackRange >= GetDistanceBetweenCharacters(instigator, target));
        }
          

        private double GetDistanceBetweenCharacters(Character actioningCharacter, Character targetCharacter)
        {
            return Math.Abs(actioningCharacter.XPosition - targetCharacter.XPosition);
        }
    }
}