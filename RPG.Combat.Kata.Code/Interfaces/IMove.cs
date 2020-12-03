namespace RPG.Combat.Kata
{
    public interface IMove
    {
        int Speed{get;}
        void Move(Direction direction);//change parameters to direction and distance
    }
}