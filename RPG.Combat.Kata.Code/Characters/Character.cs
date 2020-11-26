using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Combat.Kata
{

    public class Character : IHaveHealth, IAttack, IMove, IFaction
    { 
        private DamageController _damageController;
        private World _world;
        public int AttackRange{get; set;}
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > CharacterConstants.MinHealth;

        public bool canHaveFactions{get => true;}
        public int Speed {get; set;}
        public List<Factions> Faction { get; set; } = new List<Factions>();

        public Character(World world, int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel, int speed = CharacterConstants.defaultSpeed)
        {
            Health = health;
            Level = level;
            AttackRange = 1;
            this.JoinFaction(Factions.Unaligned);      
            Speed = speed;
            _damageController = new DamageController();
            _world = world;
        }

        public void TakeAction(Actions action, IHaveHealth target)
       {
           if(IsValidAttack(action, target))
           {
               Attack(target); 
           }
           else if(IsValidHeal(action, target))
           {
               Heal(target);
           }
           else if(MoveRequest(action, target))
           {
               Move(action);
           }
       }

        public void ChangeHealth(int amountToChange)
        {       
            Health = Math.Clamp(Health + amountToChange, CharacterConstants.MinHealth, CharacterConstants.MaxHealth);        
        }

        public bool IsSameFaction(IHaveHealth target)
        {
            var result = false;

            if(target.canHaveFactions)
            {
                if(this.Faction.Contains(Factions.Unaligned) && (target as Character).Faction.Contains(Factions.Unaligned))
                {
                    result = false;
                }
                else
                {
                    result = Faction.Any(x => (target as Character).Faction.Contains(x));
                }
            }
            
           return result;
        }

        public void JoinFaction(Factions factionToJoin)
        {
            if(Faction.Contains(Factions.Unaligned))
            {
                Faction.Remove(Factions.Unaligned);     
            }

            Faction.Add(factionToJoin);
        }

        public void LeaveFaction(Factions factionToLeave)
        {
            Faction.Remove(factionToLeave);

            if(Faction.Count == 0)
            {
                Faction.Add(Factions.Unaligned);
            }
        }

        private bool IsValidAttack(Actions action, IHaveHealth target)
        {
            return action == Actions.Attack && target != this && !IsSameFaction(target);       
        }

        public void Attack(IHaveHealth target)
        {
            if(_world.CharacterIsInRange(this, target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(CharacterConstants.DamageAmount, this.Level, target.Level);

                _damageController.ApplyDamage(target, damageToInflict);
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

        private bool MoveRequest(Actions action, IHaveHealth target)
        {
            return action == Actions.MoveRight || action == Actions.MoveLeft || action == Actions.MoveUp || action == Actions.MoveDown && target == this;
        }

        public void Move(Actions action)
        {
            Tuple<int, int> currentPosition = _world.GetLocationOf(this);
            int newYPosition;
            int newXPosition;
 
            switch (action)
            {
                case Actions.MoveRight:
                {
                    newXPosition = currentPosition.Item1 + Speed;

                    for(int i = currentPosition.Item1 + 1; i <= newXPosition; i++)
                    {
                        if(i <= _world.EdgeMaximum)
                        {
                            if(_world.SpaceOccupiedBy(i, currentPosition.Item2) is EmptySpace)
                            {
                                _world.MoveToNextFreeSpace(Direction.Right, i, currentPosition.Item2, this);   
                            } 
                        }
                    }
                    break;
                }
                case Actions.MoveLeft:
                {
                    newXPosition = currentPosition.Item1 - Speed;

                    for(int i = currentPosition.Item1 - 1; i >= newXPosition; i--)
                    {
                        if(i >= _world.EdgeMinimum)
                        {
                            if(_world.SpaceOccupiedBy(i, currentPosition.Item2) is EmptySpace)
                            {
                                _world.MoveToNextFreeSpace(Direction.Left, i, currentPosition.Item2, this);
                            }  
                        }   
                    }
                    break;
                }
                case Actions.MoveUp:
                {
                    newYPosition = currentPosition.Item2 + Speed;
                    for(int i = currentPosition.Item2 + 1; i <= newYPosition; i++)
                    {
                        if(i <= _world.EdgeMaximum)
                        {
                            if(_world.SpaceOccupiedBy(currentPosition.Item1, i) is EmptySpace)
                            {
                                _world.MoveToNextFreeSpace(Direction.Up, currentPosition.Item1, i, this);
                            }
                           
                        }   
                    }
                    break;
                }
                case Actions.MoveDown:
                {
                    newYPosition = currentPosition.Item2 - Speed;

                    for(int i = currentPosition.Item2 - 1; i >= newYPosition; i--)
                    {
                        if(i >= _world.EdgeMinimum)
                        {
                            if(_world.SpaceOccupiedBy(currentPosition.Item1, i) is EmptySpace)
                            {
                                _world.MoveToNextFreeSpace(Direction.Down, currentPosition.Item1, i, this);
                            }    
                        }  
                    }
                    break;
                }
            }

        }

        public void Heal(IHaveHealth target)
        {
            _damageController.ApplyDamage(target, CharacterConstants.HealAmount);
        }
        private bool IsValidHeal(Actions action, IHaveHealth target)
        {
            var result = false;

            if(target != this)
            {
                result = IsSameFaction(target) && target.Health > CharacterConstants.MinHealth;
            }
            else
            {
               result = action == Actions.Heal && target == this && this.IsAlive;
            }

            return result;      
        }

    }
}
