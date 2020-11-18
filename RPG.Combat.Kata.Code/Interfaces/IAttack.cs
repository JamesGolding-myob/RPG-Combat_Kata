namespace RPG.Combat.Kata
{
    interface IAttack
    {
        void AttemptToAttack(IHaveHealth target);
    }
}