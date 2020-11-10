using System;

namespace RPG.Combat.Kata
{
    public class Character : IHealthChanger
    {
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > 0;
        private const int HealingThreshold = 900;
        private const int DamageAmount = 600;
        private const int HealAmount = 100;

        public Character(int health = 1000, int level = 1)
        {
            Health = health;
            Level = level;        
        }

        public void TakeAction(Action action, IHealthChanger target)//character is currently having too much influence on other Characters
       {
           if(isValidAttack(action, target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(DamageAmount, this.Level, target.Level);
               if(Math.Abs(damageToInflict) >= target.Health)
               {
                   damageToInflict = - (target.Health);
               }
               target.ChangeHealth(damageToInflict);
           }
           else if(isValidHeal(action, target))
           {
               target.ChangeHealth(HealAmount);
           }
       }

        public void ChangeHealth(int amountToChange)
        {   
            Health = Health + amountToChange;
            if(Health <= 0)
            {
                Health = 0;;
            }      
        }
 

        private int AdjustDamageBasedOnCharacterlevelDifference(int damage, int attackerLevel, int targetLevel)
        {
            int finalDamage = - (damage);
            if(targetLevel >= (attackerLevel + 5))
            {
                finalDamage = finalDamage / 2;
            }
            else if(targetLevel <= (attackerLevel -5))
            {
                finalDamage = finalDamage - 300;
            }

            return finalDamage;
        }

        private bool isValidHeal(Action action, IHealthChanger target)
        {
            return(action == Action.Heal && target == this && this.IsAlive && Health <= HealingThreshold);      
        }

        private bool isValidAttack(Action action, IHealthChanger target)
        {
            return action == Action.Attack && target != this;        
        }

    }
}
