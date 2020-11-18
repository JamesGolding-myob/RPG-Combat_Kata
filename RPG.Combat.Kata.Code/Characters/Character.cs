using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Combat.Kata
{

    public class Character : IHaveHealth, IAttack
    { 
        private DamageController _damageController;
        public int AttackRange{get; set;}
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > CharacterConstants.MinHealth;
        public double XPosition{get; private set;}
        public double Speed {get; set;}
        public List<Factions> Faction { get; set; } = new List<Factions>();

        public Character(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel, double speed = CharacterConstants.defaultSpeed)
        {
            Health = health;
            Level = level;
            AttackRange = 1;
            this.JoinFaction(Factions.Unaligned);      
            Speed = speed;
            _damageController = new DamageController();
        }

        public void TakeAction(ActionType action, IHaveHealth target, bool inRange)//character is currently having too much influence on other Characters
       {
           if(action == ActionType.Attack)
           {
               AttemptToAttack(target);
           }
           else if(IsValidHeal(action, target))
           {
               target.ChangeHealth(CharacterConstants.HealAmount);
           }
           else if(IsValidMove(action, target))
           {
               this.XPosition = XPosition + this.Speed;
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

        public void SetPosition(double newPos)
        {
            XPosition = newPos;
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

        public void AttemptToAttack(IHaveHealth target)
        {
            if(IsValidAttack(target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(CharacterConstants.DamageAmount, this.Level, target.Level);

                _damageController.InflictDamage(target, damageToInflict);
           }
        }
    }
}
