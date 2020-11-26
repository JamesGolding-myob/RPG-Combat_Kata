using System;
namespace RPG.Combat.Kata
{
    interface IUI
    {
        void DisplayToUser(string output);
        

        int GetResponseFromUser();
        
    }
}