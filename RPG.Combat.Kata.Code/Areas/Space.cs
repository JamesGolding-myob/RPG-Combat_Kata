
namespace RPG.Combat.Kata
{
    public class Space
    {
        public int XCoordinate{get; private set;}
        public int YCoordinate{get; private set;}

        Nouns OccupiedBy{get; set;}//is occupied might neeed to become a more complex data type to hold what is occupying it - i.e character or object

        public Space(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

 

    }
}