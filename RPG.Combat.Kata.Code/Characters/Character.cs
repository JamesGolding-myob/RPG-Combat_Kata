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

        public void TakeAction(ActionType action, IHaveHealth target)
       {
           if(action == ActionType.Attack)
           {
               Attack(target); 
           }
           else if(IsValidHeal(action, target))
           {
               target.ChangeHealth(CharacterConstants.HealAmount);
           }
           else if(IsValidMove(action, target))
           {
               Move();
           }
       }

        private bool IsValidMove(ActionType action, IHaveHealth target)
        {
            return action == ActionType.Move && target == this;
        }

        public void ChangeHealth(int amountToChange)
        {       
            Health = Math.Clamp(Health + amountToChange, CharacterConstants.MinHealth, CharacterConstants.MaxHealth);        
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

        private bool IsValidHeal(ActionType action, IHaveHealth target)
        {
            var result = false;

            if(target != this)
            {
                result = IsSameFaction(target) && target.Health > CharacterConstants.MinHealth;
            }
            else
            {
               result = action == ActionType.Heal && target == this && this.IsAlive;
            }

            return result;      
        }

        private bool IsValidAttack(IHaveHealth target)
        {
            return target != this && !IsSameFaction(target);       
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

        public void Attack(IHaveHealth target)
        {
            if(IsValidAttack(target) && _world.CharacterIsInRange(this, target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(CharacterConstants.DamageAmount, this.Level, target.Level);

                _damageController.InflictDamage(target, damageToInflict);
           }
        }

        public void Move()
        {
            Tuple<int, int> currentPosition = _world.GetLocationOf(this);
            _world.SetCharacterPosition(currentPosition.Item1, currentPosition.Item2, new Nothing());

            _world.SetCharacterPosition(currentPosition.Item1 + Speed, currentPosition.Item2, this);
        }


    }
}
