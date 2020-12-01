using System;

namespace RPG.Combat.Kata
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World(10);
            Game game = new Game(new UIConsole(), world, new CharacterCreator(), new InputConverter(), new DisplayFormater());
            game.Run();

        }
    }
}
