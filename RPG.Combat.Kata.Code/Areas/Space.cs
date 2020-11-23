
namespace RPG.Combat.Kata
{
    public class Space
    {
        public bool isOccupied{get; set;}
        public IHaveHealth OccupiedBy{get; set;} = new EmptySpace();

        public Space(IHaveHealth thing)
        {
            isOccupied = false;
            
        }

    }
}