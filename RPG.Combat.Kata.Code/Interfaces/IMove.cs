namespace RPG.Combat.Kata
{
    interface IMove
    {
        int Speed{get;}
        void Move(Direction direction);//change parameters to direction and distance
    }
}