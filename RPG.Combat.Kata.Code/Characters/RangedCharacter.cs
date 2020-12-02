using System.Linq;
namespace RPG.Combat.Kata
{
    public class RangedCharacter: Character, IFaction, IHeal
    {
        public RangedCharacter(World world, int health = CharacterConstants.MaxHealth, int level = CharacterConstants.DefaultStartingLevel) : base(world, health, level)
        {
            AttackRange = 20;
            this.JoinFaction(Factions.Unaligned);   
        }

        public bool IsSameFaction(IHaveHealth target)
        {
            var result = false;

            if(target.canHaveFactions)
            {
                if(this.Faction.Contains(Factions.Unaligned) && (target as Character).Faction.Contains(Factions.Unaligned))
                {
                    result = false;
                }
                else
                {
                    result = Faction.Any(x => (target as Character).Faction.Contains(x));
                }
            }
            
           return result;
        }

        public void JoinFaction(Factions factionToJoin)
        {
            if(Faction.Contains(Factions.Unaligned))
            {
                Faction.Remove(Factions.Unaligned);     
            }

            Faction.Add(factionToJoin);
        }

        public void LeaveFaction(Factions factionToLeave)
        {
            Faction.Remove(factionToLeave);

            if(Faction.Count == 0)
            {
                Faction.Add(Factions.Unaligned);
            }
        }

        public override bool IsValidAttack(Actions action, IHaveHealth target)
        {
            return action == Actions.Attack && target != this && !IsSameFaction(target);       
        }

        public void Heal(IHaveHealth target)
        {
            DamageController.ApplyDamage(target, CharacterConstants.HealAmount);
        }
        public bool IsValidHeal(Actions action, IHaveHealth target)
        {
            var result = false;

            if(target != this)
            {
                result = IsSameFaction(target) && target.Health > CharacterConstants.MinHealth;
            }
            else
            {
               result = action == Actions.Heal && target == this && this.IsAlive;
            }

            return result;      
        }

        public override void TakeAction(Actions action, IHaveHealth target)
        {
            base.TakeAction(action, target);
            if(IsValidHeal(action, target))
            {
                Heal(target);
            }
        }
    }
}