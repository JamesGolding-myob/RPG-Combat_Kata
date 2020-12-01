using System;
namespace RPG.Combat.Kata
{
    public interface IUI
    {
        void DisplayToUser(string output);
        

        string GetResponseFromUser();
        
    }
}