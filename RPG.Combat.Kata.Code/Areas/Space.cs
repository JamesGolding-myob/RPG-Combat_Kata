
namespace RPG.Combat.Kata
{
    public class Space
    {

        public IHaveHealth OccupiedBy{get; set;} = new EmptySpace();

        public Space(IHaveHealth thing)
        {
            OccupiedBy = thing;
            
        }

    }
}