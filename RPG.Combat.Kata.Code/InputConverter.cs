using System.Collections.Generic;
using System;
namespace RPG.Combat.Kata
{
    public class InputConverter
    {
        public IHaveHealthOptions ConvertCharacterChoice(string input)
        {
            var characterChoice = IHaveHealthOptions.Empty;
            switch (input)
            {
                case "1":
                {
                    characterChoice = IHaveHealthOptions.Melee;
                    break;
                }
                case "2":
                {
                    characterChoice = IHaveHealthOptions.Ranged;
                    break;
                }
     
            }
            return characterChoice;
        }

        public bool IsValidCharacterChoice(string input)
        {
            bool result = false;
            if(input == "1" || input == "2")
            {
                result = true;
            }
            return result;
        }

        public Actions ActionsConverter(string input)
        {
            Actions result;  
            switch (input)
            {
                case "1":
                {
                    result = Actions.Attack;
                    break;
                }
                case "2":
                {
                    result = Actions.Heal;
                    break;
                }
                case "3":
                {
                    result = Actions.MoveUp;                    
                    break;
                }
                case "4":
                {
                    result = Actions.MoveRight;
                    break;
                }
                case "5":
                {
                    result = Actions.MoveDown;
                    break;
                }
                default:
                {
                    result = Actions.MoveLeft;
                    break;
                }
                
            }

            return result;
        }
        public IHaveHealth ConvertTarget(string input, List<IHaveHealth> targets)
        {
            return targets[Int32.Parse(input) -1 ];
        }
    }
}