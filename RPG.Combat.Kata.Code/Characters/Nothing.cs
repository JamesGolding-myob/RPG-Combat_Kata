using System.Collections.Generic;

namespace RPG.Combat.Kata
{
    public class Nothing : IHaveHealth
    {
        
        public int Health => 0;

        public int Level => 0;
         
        public int XCoordinate => 0;

        public int YCoordinate => 0;

        public bool canHaveFactions => false;

        public void ChangeHealth(int amount)
        {
            
        }

        public bool IsSameFaction(IHaveHealth target)
        {
            return false;
        }

        public void JoinFaction(Factions factionToJoin)
        {
            
        }

        public void LeaveFaction(Factions factionToLeave)
        {
            
        }
    }
}