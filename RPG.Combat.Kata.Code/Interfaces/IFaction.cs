using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public interface IFaction
    {
       List<Factions> Faction { get; set; } 
       

       bool IsSameFaction(IHaveHealth target);
       void JoinFaction(Factions factionToJoin);
       void LeaveFaction(Factions factionToLeave);

    }
}