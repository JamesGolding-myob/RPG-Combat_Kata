using System.Collections.Generic;
using System;
namespace RPG.Combat.Kata
{
    public abstract class Object : IHealthChanger
    {
        public int Health{get; private set;} 

        public int Level{get;} = 1;
        public bool IsDestroyed => Health <= 0;

        public List<Factions> Faction { get; set;} = new List<Factions>{Factions.Unaligned};

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