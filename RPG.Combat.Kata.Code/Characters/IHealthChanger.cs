using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public interface IHealthChanger
    {
        int Health{get;}
        
        int Level{get;}
        void ChangeHealth(int amount);
        List<Factions> Faction{get; set;} 
        

    }
}