namespace RPG.Combat.Kata
{
    public interface IHealthChanger
    {
        int Health{get;}

        void ChangeHealth(int amount, Action action);
        

    }
}