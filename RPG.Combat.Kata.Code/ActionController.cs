using System;
namespace RPG.Combat.Kata
{
    public class ActionController
    {
        private const int HealingThreshold = 900;
        private const int DamageAmount = 600;
        private const int HealAmount = 100;
        private const int AdditionalDamage = 300;
        private const int LevelDifferenceGap = 5;
       
        public void ProcessAction(Action action, IHealthChanger target, Character instigator)
        {
            if(isValidAttack(action, target, instigator))
           {   
               var damageToInflict = AdjustDamageBasedOnCharacterlevelDifference(DamageAmount, instigator.Level, target.Level);
               
               if(KillingBlow(damageToInflict, target.Health))
               {
                   damageToInflict = (- target.Health);
               }

               target.ChangeHealth(damageToInflict);
           }
           else if(isValidHeal(action, target, instigator))
           {
               target.ChangeHealth(HealAmount);
           }
        }

        
        private int AdjustDamageBasedOnCharacterlevelDifference(int damage, int attackerLevel, int targetLevel)
        {
            int finalDamage = (- damage);
            if(targetLevel >= (attackerLevel + LevelDifferenceGap))
            {
                finalDamage = finalDamage / 2;
            }
            else if(targetLevel <= (attackerLevel - LevelDifferenceGap))
            {
                finalDamage = finalDamage - AdditionalDamage;
            }

            return finalDamage;
        }

        private bool isValidHeal(Action action, IHealthChanger target, Character instigator)
        {
            return(action == Action.Heal && target == instigator && instigator.IsAlive && instigator.Health <= HealingThreshold);      
        }

        private bool isValidAttack(Action action, IHealthChanger target, Character instigator)
        {
            return action == Action.Attack && target != instigator;        
        }

        private bool KillingBlow(int damage, int targetHealth)
        {
            return Math.Abs(damage) >= targetHealth;
        }

    }
}


    
        
    
