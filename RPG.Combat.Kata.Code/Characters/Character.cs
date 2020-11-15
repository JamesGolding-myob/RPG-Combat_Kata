using System;

namespace RPG.Combat.Kata
{

    public class Character : IHealthChanger
    {
        public int AttackRange{get; set;}
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > 0;

        public double XPosition{get; private set;}

        public Character(int health = ImportantValues.MaxHealth, int level = 1)
        {
            Health = health;
            Level = level;
            AttackRange = 1;        
        }

        public void TakeAction(ActionType action, IHealthChanger target, bool inRange)//character is currently having too much influence on other Characters
       {
           if(IsValidAttack(action, target) && inRange)
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(ImportantValues.DamageAmount, this.Level, target.Level);

               target.ChangeHealth(damageToInflict);
           }
           else if(IsValidHeal(action, target))
           {
               target.ChangeHealth(ImportantValues.HealAmount);
           }
       }

        public void ChangeHealth(int amountToChange)
        {       
            Health = Math.Clamp(Health + amountToChange, ImportantValues.MinHealth, ImportantValues.MaxHealth);        
        }
 
        private int AdjustDamageBasedOnCharacterlevelDifference(int damage, int attackerLevel, int targetLevel)
        {
            int finalDamage = damage;
            if(targetLevel >= (attackerLevel + ImportantValues.LevelDifference))
            {
                finalDamage = ImportantValues.LessenedDamage;
            }
            else if(targetLevel <= (attackerLevel - ImportantValues.LevelDifference))
            {
                finalDamage = ImportantValues.ExtraDamageAmount;
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
