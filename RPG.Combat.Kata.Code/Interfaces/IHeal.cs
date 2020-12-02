namespace RPG.Combat.Kata
{
    public interface IHeal
    {
        void Heal(IHaveHealth target);
        bool IsValidHeal(Actions action, IHaveHealth target);
    }
}