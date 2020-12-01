using System.Text;
using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public class DisplayFormater
    {
        public string FormatMap(World worldMap)
        {
            
            var tempString = new StringBuilder();

            for(int row = worldMap.EdgeMaximum; row >= worldMap.EdgeMinimum; row--)
            {
                for(int column = worldMap.EdgeMinimum; column<= worldMap.EdgeMaximum; column++)
                {
                    
                    if(worldMap.map[column, row].OccupiedBy is EmptySpace)
                    {
                        tempString.Append(".");

                    }else if(worldMap.map[column, row].OccupiedBy is Monster)
                    {
                        tempString.Append("👾");
                    } 
                    else
                    {
                        tempString.Append("🧙");
                    }
                    if(column == worldMap.EdgeMaximum)
                    {
                        tempString.Append("\n");
                    }
                    
                }
            }
                    
            return tempString.ToString();
        }

        public string DisplayPossibleTargets(List<IHaveHealth> targets)
        {
            return $"Choose a Target: 1:{targets[0].ToString()}\n 2:{targets[1].ToString()}\n 3:{targets[2].ToString()}\n 4:{targets[3].ToString()}\n"; 
        }
    }
}