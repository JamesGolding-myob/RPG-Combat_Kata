
namespace RPG.Combat.Kata
{
    public class Character : IHealthChanger
    {
        public int Health{get; private set;}
        public int Level{get; private set;}
        public bool IsAlive => Health > 0;
        
 

        public Character(int health = 1000, int level = 1)
        {
            Health = health;
            Level = level;        
        }

        public void TakeAction(Action action, IHealthChanger target, ActionController actionController)
       {
           actionController.ProcessAction(action, target, this);
       }

        public void ChangeHealth(int amountToChange)
        {   
            Health = Health + amountToChange;
            if(Health <= 0)
            {
                Health = 0;;
            }      
        }

    }

 
}
