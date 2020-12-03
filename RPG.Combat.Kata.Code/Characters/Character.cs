using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Combat.Kata
{

    public abstract class Character : IHaveHealth, IAttack, IMove 
    { 
        public DamageController DamageController{get; private set;}
        private World _world;
        public int AttackRange{get; set;}
        public int Health{get; protected set;}
        public int Level{get; protected set;}
        public bool IsAlive => Health > CharacterConstants.MinHealth;

        public bool canHaveFactions{get => true;}
        public int Speed {get; protected set;}
        public List<Factions> Faction { get; set; } = new List<Factions>();

        public Character(World world, int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel, int speed = CharacterConstants.defaultSpeed)
        {
            Health = health;
            Level = level;
            AttackRange = 1;   
            Speed = speed;
            DamageController = new DamageController();
            _world = world;
        }

        public virtual void TakeAction(Actions action, IHaveHealth target) //move target from here
       {
           if(IsValidAttack(action, target))
           {
               Attack(target); 
           }
       }

       public void TakeAction(Actions action) 
       {    
        Move(GetDirection(action));    
       }

       private Direction GetDirection(Actions action)
       {
           Direction result;
           switch (action)
           {
               case Actions.MoveDown:
               {
                   result = Direction.Down;
                   break;
               }
               case Actions.MoveRight:
               {
                   result = Direction.Right;
                   break;
               }
               case Actions.MoveUp:
               {
                   result = Direction.Up;
                   break;
               }
               default:
               {
                   result = Direction.Left;
                   break;
               }
           }
           return result;
       }

        public virtual void ChangeHealth(int amountToChange)
        {       
            Health = Math.Clamp(Health + amountToChange, CharacterConstants.MinHealth, CharacterConstants.MaxHealth);        
        }

        public virtual bool IsValidAttack(Actions action, IHaveHealth target)
        {
            return action == Actions.Attack && target != this;       
        }

        public virtual void Attack(IHaveHealth target)
        {
            if(_world.CharacterIsInRange(this, target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(CharacterConstants.DamageAmount, this.Level, target.Level);

                DamageController.ApplyDamage(target, damageToInflict);
           }
        }

        private int AdjustDamageBasedOnCharacterlevelDifference(int damage, int attackerLevel, int targetLevel)
        {
            int finalDamage = damage;

            if(targetLevel >= (attackerLevel + CharacterConstants.LevelDifference))
            {
                finalDamage = CharacterConstants.LessenedDamage;
            }
            else if(targetLevel <= (attackerLevel - CharacterConstants.LevelDifference))
            {
                finalDamage = CharacterConstants.ExtraDamageAmount;
            }

            return -finalDamage;
        }

        public void Move(Direction direction)
        {
            Tuple<int, int> currentPosition = _world.GetLocationOf(this);

            int counter = 1;
            while(_world.IsValidMove(direction, counter, currentPosition, Speed))
            {
                counter++;
            }
            
            switch (direction)
            {
                case Direction.Right:
                {
                    if(_world.SpaceOccupiedBy(currentPosition.Item1 + counter, currentPosition.Item2) is EmptySpace || _world.SpaceOccupiedBy(currentPosition.Item1 + counter, currentPosition.Item2) == this)//TODO:Make this ccheck small by creating methods
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, counter, 0);
                    }
                    else
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, counter -1, 0);
                    }
                    break;
                }
                case Direction.Left:
                {
                   if(_world.SpaceOccupiedBy(currentPosition.Item1 - counter, currentPosition.Item2) is EmptySpace || _world.SpaceOccupiedBy(currentPosition.Item1 - counter, currentPosition.Item2) == this)
                   {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, -counter , 0);
                   }
                   else
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, -counter +1 , 0);
                    }
                    break;
                }
                case Direction.Up:
                {
                    if(_world.SpaceOccupiedBy(currentPosition.Item1, currentPosition.Item2 + counter) is EmptySpace || _world.SpaceOccupiedBy(currentPosition.Item1, currentPosition.Item2 + counter) == this)
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, 0, counter);
                    }
                    else
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, 0, counter -1);
                    }
                    break;
                }
                case Direction.Down:
                {
                    if(_world.SpaceOccupiedBy(currentPosition.Item1, currentPosition.Item2 - counter) is EmptySpace || _world.SpaceOccupiedBy(currentPosition.Item1, currentPosition.Item2 - counter) == this)
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, 0, -counter);
                    }
                    else
                    {
                        _world.UpdateCharacterPositionInWorld(this, currentPosition, 0, -counter +1);
                    }  
                    break;
                } 
            }    
        }
        

    }
}
