using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public interface IHaveHealth
    {
        int Health{get;}
        
        int Level{get;}
        void ChangeHealth(int amount);
        List<Factions> Faction{get; set;} 
        int XCoordinate{get;}
        int YCoordinate{get;}
        

    }
}