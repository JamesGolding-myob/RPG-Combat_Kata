
using System;
namespace RPG.Combat.Kata
{
    public abstract class Object : IHaveHealth
    {
        public int Health{get; private set;} 

        public int Level{get;} = 1;
        public bool IsDestroyed => Health <= 0;
        public bool canHaveFactions{get=> false;}

        public Object(int health = 1000)
        {
            Health = health;
        }
        public void ChangeHealth(int damageTaken)
        {
            Health = Math.Clamp(Health + damageTaken, 0, Health);
        }

    }
}