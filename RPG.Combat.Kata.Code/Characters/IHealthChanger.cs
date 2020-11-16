namespace RPG.Combat.Kata
{
    public interface IHealthChanger
    {
        int Health{get;}
        int Level{get;}
        void ChangeHealth(int amount);
        

    }
}