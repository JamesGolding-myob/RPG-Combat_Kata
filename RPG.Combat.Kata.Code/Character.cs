using System;

namespace RPG.Combat.Kata
{
    public class Character
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

        public void TakeAction(string action, Character target)
       {
           if(action == "attack")
           {
               target.TakeDamage();
           }
           else
           {
               target.GainHealth();
           }
       }

       public void TakeDamage()
       {
           Health = Health - _damageAmount;
           if(Health < 0)
           {
               Health = 0;
           }
       }

       public void GainHealth()
       {    if(Health <= 900)
            {
                Health = Health + _healAmount;
            }
       }
    }
}
