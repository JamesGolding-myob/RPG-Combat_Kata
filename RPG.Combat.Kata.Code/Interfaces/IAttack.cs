namespace RPG.Combat.Kata
{
    interface IAttack
    {
        void Attack(IHaveHealth target, World world);
    }
}