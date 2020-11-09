using System;

namespace RPG.Combat.Kata
{
    public class Character : IHealthChanger
    {
        public int Health{get; set;}
        private int _level = 1;
        public bool IsAlive => Health > 0;
        private int _healingThreshold = 900;
        private int _damageAmount = 600;
        private int _healAmount = 100;

        public Character()
        {
            Health = 1000;        
        }

        public void TakeAction(Action action, IHealthChanger target)
       {
           if(action == Action.Attack)
           {   
               target.ChangeHealth(_damageAmount, action);
           }
           else
           {
               target.ChangeHealth(_healAmount, action);
           }
       }

        public void ChangeHealth(int amountToChange, Action action)
        {
            if(action == Action.Attack && Health > 0)
            {
                Health = Health - amountToChange;
                this.IsCharacterDead();
            }
            else if(action == Action.Heal && Health <= _healingThreshold)
            {
                Health = Health + amountToChange;
            }
            
        }

        public void IsCharacterDead()
        {
            if(!IsAlive)
            {
                Health = 0;
            }
            
        }

    }
}
