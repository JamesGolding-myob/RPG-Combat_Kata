using System;

namespace RPG.Combat.Kata
{
    public class Character : IHealthChanger
    {
        public int Health{get; private set;}
        private int _level = 1;
        public bool IsAlive => Health > 0;
        private int _healingThreshold = 900;
        private int _damageAmount = 600;
        private int _healAmount = 100;

        public Character()
        {
            Health = 1000;        
        }

        public void TakeAction(Action action, IHealthChanger target)//character is currently having too much influence
       {
           if(action == Action.Attack && target != this)
           {   
               target.ChangeHealth(_damageAmount, action);
           }
           else if(action == Action.Heal && target == this)
           {
               target.ChangeHealth(_healAmount, action);
           }
       }

        public void ChangeHealth(int amountToChange, Action action)
        {
            if(action == Action.Attack && IsAlive)
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


    }
}
