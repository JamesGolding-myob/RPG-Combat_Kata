using Xunit;
using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public class GameTests
    {
        World world = new World(5);
        CharacterCreator characterCreator = new CharacterCreator();
        InputConverter inputConverter = new InputConverter();

        [Fact]
        public void MeleeCharacterCreatedWhenMeleeCHaracterOptionPicked()
        {           
            var character = characterCreator.CreateCharacter(IHaveHealthOptions.Melee, world);
            Assert.True(character is MeleeCharacter);
            
        }

        [Fact]
        public void RangedCharacterCreatedWhenRangedCharacterOptionChosen()
        {
            var character = characterCreator.CreateCharacter(IHaveHealthOptions.Ranged, world);
            Assert.True(character is RangedCharacter);
        }

        [Fact]
        public void EndToEnd()
        {
            var ui = new SimpleUI();
            
            Game game = new Game(ui, world, characterCreator, inputConverter, new DisplayFormater());
            var expectedFinalMessage = "Congratulations you have killed the monster";

            game.Run();
               
            Assert.Equal(expectedFinalMessage, ui.FinalMessage);
        }

        public class SimpleUI : IUI
        {
            Queue<string> queue = new Queue<string>();
            public SimpleUI()
            {
                queue.Enqueue("1");
                queue.Enqueue("3");
                queue.Enqueue("1");
                queue.Enqueue("1");
                queue.Enqueue("1");
                queue.Enqueue("1");
                queue.Enqueue("1");
                queue.Enqueue("1");
            }
            public string FinalMessage{get; set;}
            public void DisplayToUser(string output)
            {
                FinalMessage = output;
            }

            public string GetResponseFromUser()
            {
                return queue.Dequeue();
            }
        }
    }
}