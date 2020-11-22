using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public interface IHaveHealth
    {
        int Health{get;}
        
        int Level{get;}
        void ChangeHealth(int amount);
        public bool canHaveFactions{get;}

    }
}