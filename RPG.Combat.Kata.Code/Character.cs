using System;

namespace RPG.Combat.Kata
{
    public class Character : IHealthChanger
    {
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > 0;
        private int _healingThreshold = 900;
        private int _damageAmount = 600;
        private int _healAmount = 100;

        public Character(int health = 1000, int level = 1)
        {
            Health = health;
            Level = level;        
        }

        public void TakeAction(Action action, IHealthChanger target)//character is currently having too much influence on other Characters
       {
           if(isValidAttack(action, target))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevel(_damageAmount, this.Level, target.Level);
               target.ChangeHealth(damageToInflict, action);
           }
           else if(isValidHeal(action, target))
           {
               target.ChangeHealth(_healAmount, action);
           }
       }

        public void ChangeHealth(int amountToChange, Action action)
        {
            if(action == Action.Attack)
            { 
                this.HurtCharacter(amountToChange);    
            }
            else if(action == Action.Heal && Health <= _healingThreshold)
            {
                Health = Health + amountToChange;
            }
            
        }

        private void HurtCharacter(int amountToChange)
        {
              if(amountToChange > Health)
                {
                    Health = 0;
                }
                else
                {
                    Health = Health - amountToChange;
                }
        }

        private int AdjustDamageBasedOnCharacterlevel(int damage, int attackerLevel, int targetLevel)
        {
            int finalDamage = damage;
            if(targetLevel >= (attackerLevel + 5))
            {
                finalDamage = finalDamage / 2;
            }

            return finalDamage;
        }

        private bool isValidHeal(Action action, IHealthChanger target)
        {
            return(action == Action.Heal && target == this && this.IsAlive);
        }

        private bool isValidAttack(Action action, IHealthChanger target)
        {
            return action == Action.Attack && target != this;
        }


    }
}
