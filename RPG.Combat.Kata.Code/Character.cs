using System;

namespace RPG.Combat.Kata
{

    public class Character : IHealthChanger
    {
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > 0;


        public Character(int health = ImportantValues.MaxHealth, int level = 1)
        {
            Health = health;
            Level = level;        
        }

        public void TakeAction(Action action, IHealthChanger target)//character is currently having too much influence on other Characters
       {
           if(IsValidAttack(action, target))
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
            int finalDamage = - damage;
            if(targetLevel >= (attackerLevel + ImportantValues.LevelDifference))
            {
                finalDamage = finalDamage / 2;
            }
            else if(targetLevel <= (attackerLevel - ImportantValues.LevelDifference))
            {
                finalDamage = ImportantValues.LessenedDamage;
            }

            return finalDamage;
        }

        private bool IsValidHeal(Action action, IHealthChanger target)
        {
            return(action == Action.Heal && target == this && this.IsAlive);      
        }

        private bool IsValidAttack(Action action, IHealthChanger target)
        {
            return action == Action.Attack && target != this;        
        }


    }
}
