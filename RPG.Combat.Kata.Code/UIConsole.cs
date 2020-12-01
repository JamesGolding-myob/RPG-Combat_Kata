using System;
namespace RPG.Combat.Kata
{
    public class UIConsole : IUI
    {
        public void DisplayToUser(string output)
        {
            Console.WriteLine(output);
        }

        public string GetResponseFromUser()
        {
            return Console.ReadLine();
        }
    }
}