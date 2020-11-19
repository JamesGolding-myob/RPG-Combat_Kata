using System.Collections.Generic;

namespace RPG.Combat.Kata
{
    public class Cell
    {
        public Dictionary<string, int> Coordinates{get; set;} = new Dictionary<string, int>();

        bool IsOccupied{get; set;}//is occupied might neeed to become a more complex data type to hold what is occupying it - i.e character or object

        public Cell(int xCoordinate, int yCoordinate)
        {
            Coordinates.Add("XCoordinate", xCoordinate);
            Coordinates.Add("YCoordinate", yCoordinate);
        }

 

    }
}