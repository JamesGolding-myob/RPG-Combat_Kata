using System;

namespace RPG.Combat.Kata
{
    public class Monster : Character
    {
        
        public Monster(World world, int health = 800, int level = 1, int speed = 2, int attackRange = 1): base(world, health, level, speed)
        {   
            _worldMap = world;
            AttackRange = attackRange;   
        }

        private World _worldMap;
        

        public void TakeTurn()
        {    
            Move(FindDirectionCharacterIsIn());   
        }

        private Direction FindDirectionCharacterIsIn()
        {
            Tuple<int, int> monsterCurrentPosition = _worldMap.GetLocationOf(this);
            
            (int, int)characterCurrentPosition = _worldMap.GetCharacterPosition();
            Direction directionToMove; 

            var spaceTotheRightOfCharacter = characterCurrentPosition.Item1 + 1;
            var spaceTotheTopOfCharacter = characterCurrentPosition.Item2 + 1;
            var spaceTotheBottomOfCharacter = characterCurrentPosition.Item2 - 1;

            if(monsterCurrentPosition.Item1 >= (spaceTotheRightOfCharacter) && monsterCurrentPosition.Item2 >= characterCurrentPosition.Item2)
            {
                directionToMove = Direction.Left;
            }
            else if(monsterCurrentPosition.Item2 >(spaceTotheTopOfCharacter) && monsterCurrentPosition.Item1 >= characterCurrentPosition.Item1)
            {
                directionToMove = Direction.Down;
            }
            else if(monsterCurrentPosition.Item2 < (spaceTotheBottomOfCharacter))
            {
                directionToMove = Direction.Up;
            }
            else 
            {
                directionToMove = Direction.Right;
            }

            return directionToMove;
        }

        public override void Attack(IHaveHealth target)
        {
            DamageController.ApplyDamage(target, -100);
        }

        public override void ChangeHealth(int amount)
        {
            base.Health = Math.Clamp(Health + amount, CharacterConstants.MinHealth, 800);
        }

        
    }
}