namespace RPG.Combat.Kata
{
    public class CharacterCreator
    {
        
        public Character CreateCharacter(IHaveHealthOptions option, World world)
        {
            switch (option)
            {
                case IHaveHealthOptions.Melee:
                {
                    return new MeleeCharacter(world); 
                }
                case IHaveHealthOptions.Ranged:
                {
                    return new RangedCharacter(world);
                }
                default:
                {
                    return new Monster(world);//don't think this ever gets called, may cauuse an error
                }
                
            }
            
        }
    }
}