using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Combat.Kata
{

    public class Character : IHaveHealth, IAttack
    { 
        private DamageController _damageController;
        private World _world;
        public int AttackRange{get; set;}
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > CharacterConstants.MinHealth;
        public int XCoordinate{get; private set;}
        public int YCoordinate{get; private set;}
        
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

        public void TakeAction(ActionType action, IHaveHealth target, World world)//character is currently having too much influence on other Characters
       {//just change bool for is range to be the world - want to use the world to check if the target is in range for a valid attack
           if(action == ActionType.Attack)
           {
               Attack(target, world); //have scewed away from TDD as I'm not sure what I'm actually trying to achieve.. broke my tests to force me to think about it
           }
           else if(IsValidHeal(action, target))
           {
               target.ChangeHealth(CharacterConstants.HealAmount);
           }
           else if(IsValidMove(action, target))
           {
               this.XCoordinate = XCoordinate + this.Speed; //want to use the world to check if the potential new position is free or occupied
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

        private bool IsSameFaction(IHaveHealth target)
        {
            var result = true;

            if(this.Faction.Contains(Factions.Unaligned) && target.Faction.Contains(Factions.Unaligned))
            {
                result = false;
            }
            else
            {
                result = Faction.Any(x => target.Faction.Contains(x));
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

        public void Attack(IHaveHealth target, World world)
        {
            if(IsValidAttack(target) && world.CharacterIsInRange(this, target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(CharacterConstants.DamageAmount, this.Level, target.Level);

                _damageController.InflictDamage(target, damageToInflict);
           }
        }

        public void SetCharacterPosition(int newXPosition)
        {
            XCoordinate = newXPosition;
        }
    }
}
