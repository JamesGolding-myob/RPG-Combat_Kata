using System;
using System.Linq;
namespace RPG.Combat.Kata
{
    public class Monster : IAttack, IHaveHealth, IMove
    {
        
        public Monster(World world, int health = 800, int level = 1, int speed = 2, int attackRange = 1) 
        {
            Speed = speed;
            _worldMap = world;
            AttackRange = attackRange;
            _damageController = new DamageController();
            Health = health;
        }

        public bool IsAlive{get => Health > 0;}
        public int Speed {get; private set;}

        public int Health {get; private set;}

        public int Level {get; private set;}

        public bool canHaveFactions {get => false;}
        public int AttackRange{get; private set;}
        private World _worldMap;
        private DamageController _damageController;

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
                    if(thing is Character)
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
            else if(currentPos.Item2 >=(charCurrentPos.Item2 + 1) && currentPos.Item1 == charCurrentPos.Item1)
            {
                directionToCharacter = Direction.Down;
            }
            else if(currentPos.Item2 <= (charCurrentPos.Item2 + 1) && currentPos.Item1 == charCurrentPos.Item1)
            {
                directionToCharacter = Direction.Up;
            }
            else
            {
                directionToCharacter = Direction.Right;
            }

            return directionToCharacter;
        }

        public void Attack(IHaveHealth target)
        {
            _damageController.ApplyDamage(target, -100);
        }

        public void ChangeHealth(int amount)
        {
            Health = Math.Clamp(Health + amount, CharacterConstants.MinHealth, 800);
        }

        public void Move(Direction direction)
        {
            var currentPos = _worldMap.GetLocationOf(this);
            int newYPos;
            int newXPos;
            if(direction == Direction.Down)
            {
                newYPos = currentPos.Item2 - Speed;
                for(int i = currentPos.Item2; i >= newYPos; i--)
                {
                    if(i >= _worldMap.EdgeMinimum && _worldMap.SpaceOccupiedBy(currentPos.Item1, i) is EmptySpace)
                    {
                        _worldMap.MoveToNextFreeSpace(Direction.Down, currentPos.Item1, i, this);
                        
                    }

                }
                
            }else if(direction == Direction.Up)
            {
                newYPos = currentPos.Item2 + Speed;
                for(int i = currentPos.Item2; i <= newYPos; i++)
                {
                    if(i <= _worldMap.EdgeMaximum && _worldMap.SpaceOccupiedBy(currentPos.Item1, i) is EmptySpace)
                    {
                        _worldMap.MoveToNextFreeSpace(Direction.Up, currentPos.Item1, i, this);
                        
                    }
                }
            }
            else if (direction == Direction.Left)
            {
                newXPos = currentPos.Item1 - Speed;
                for(int i = currentPos.Item1; i >= newXPos; i--)
                {
                    if(i >= _worldMap.EdgeMinimum && _worldMap.SpaceOccupiedBy(i, currentPos.Item2) is EmptySpace)
                    {
                        _worldMap.MoveToNextFreeSpace(Direction.Left, i, currentPos.Item2, this);
                        
                    }

                }
            }
            else
            {
                newXPos = currentPos.Item1 + Speed;
                for(int i = currentPos.Item1; i <= newXPos; i++)
                {
                    if(i <= _worldMap.EdgeMaximum && _worldMap.SpaceOccupiedBy(i, currentPos.Item2) is EmptySpace)
                    {
                        _worldMap.MoveToNextFreeSpace(Direction.Right, i, currentPos.Item2, this);
                    }
                }
            }
        }
    }
}