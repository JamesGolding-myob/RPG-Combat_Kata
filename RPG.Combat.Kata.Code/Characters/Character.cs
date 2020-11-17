using System;

namespace RPG.Combat.Kata
{

    public abstract class Character : IHealthChanger
    {
        public int AttackRange{get; set;}
        public int Health{get; set;}
        public int Level{get; set;}
        public bool IsAlive => Health > 0;
        public double XPosition{get; private set;}

        public Character(int health = CharacterConstants.MaxHealth, int level = CharacterConstants.defaultStartingLevel)
        {
            Health = health;
            Level = level;
        }
      

        public void TakeAction(ActionType action, IHealthChanger target, bool inRange)//character is currently having too much influence on other Characters
       {
           if(IsValidAttack(action, target) && inRange)
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(CharacterConstants.DamageAmount, this.Level, target.Level);

               target.ChangeHealth(damageToInflict);
           }
           else if(IsValidHeal(action, target))
           {
               target.ChangeHealth(CharacterConstants.HealAmount);
           }
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

        private bool IsValidHeal(ActionType action, IHealthChanger target)
        {
            return(action == ActionType.Heal && target == this && this.IsAlive);      
        }

        private bool IsValidAttack(ActionType action, IHealthChanger target)
        {
            return action == ActionType.Attack && target != this;        
        }

        public void SetPosition(double newPos)
        {
            XPosition = newPos;
        }

    }
}
