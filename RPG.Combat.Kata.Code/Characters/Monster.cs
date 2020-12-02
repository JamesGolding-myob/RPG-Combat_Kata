using System;
using System.Linq;
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
            Tuple<int, int> currentPos = _worldMap.GetLocationOf(this);
            (int, int)charCurrentPos = (0, 0);
            Direction directionToCharacter;

            //get first? - filter with Linq??
           
            for(int row = 0; row <= _worldMap.EdgeMaximum; row++)
            {
                for(int column = 0; column <= _worldMap.EdgeMaximum; column++)
                {
                    var thing = _worldMap.SpaceOccupiedBy(column, row);
                    if(thing is Character && thing != this)
                    {
                        charCurrentPos = (column, row);
                        break;
                    }
                }
            }
            if(currentPos.Item1 >= (charCurrentPos.Item1 + 1) && currentPos.Item2 >= charCurrentPos.Item2)
            {
                directionToCharacter = Direction.Left;
            }
            else if(currentPos.Item2 >(charCurrentPos.Item2 + 1) && currentPos.Item1 >= charCurrentPos.Item1)
            {
                directionToCharacter = Direction.Down;
            }
            else if(currentPos.Item2 < (charCurrentPos.Item2 - 1))
            {
                directionToCharacter = Direction.Up;
            }
            else 
            {
                directionToCharacter = Direction.Right;
            }

            return directionToCharacter;
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