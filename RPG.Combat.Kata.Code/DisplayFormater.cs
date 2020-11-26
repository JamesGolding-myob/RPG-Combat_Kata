using System.Text;
namespace RPG.Combat.Kata
{
    public class DisplayFormater
    {
        public string FormatMap(World worldMap)
        {
            string result;
            var tempString = new StringBuilder();

            for(int row = worldMap.EdgeMaximum; row >= worldMap.EdgeMinimum; row--)
            {
                for(int column = worldMap.EdgeMinimum; column<= worldMap.EdgeMaximum; column++)
                {
                    
                    if(worldMap.map[column, row].OccupiedBy is EmptySpace)
                    {
                        tempString.Append(" ");

                    }else 
                    {
                        tempString.Append("🧙");
                    }
                    if(column == worldMap.EdgeMaximum)
                    {
                        tempString.Append("\n");
                    }
                    
                }
            }
                    
            return result = tempString.ToString();
        }
    }
}