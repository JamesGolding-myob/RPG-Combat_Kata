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
        public void MonsterCanBeCreatedByCharacterCreator()
        {
            var monster = characterCreator.CreateCharacter(IHaveHealthOptions.Monster, world);
            Assert.True(monster is Monster);
        }

        [Fact]
        public void EndToEndCharacterKillsMonster()
        {
            var ui = new WinningUI();
            
            Game game = new Game(ui, world, characterCreator, inputConverter, new DisplayFormater(), new InputValidator());
            var expectedFinalMessage = "Congratulations you have killed the monster";

            game.Run();
               
            Assert.Equal(expectedFinalMessage, ui.FinalMessage);
        }

        [Fact]
        public void EndToEndMonsterKillsCharacter()
        {
            var ui = new LosingUI();
            
            Game game = new Game(ui, world, characterCreator, inputConverter, new DisplayFormater(), new InputValidator());
            var expectedFinalMessage = "The Monster Has Killed You";

            game.Run();
               
            Assert.Equal(expectedFinalMessage, ui.FinalMessage);
        }

        public class WinningUI : IUI
        {
            Queue<string> queue = new Queue<string>();
          public WinningUI()
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

        public class LosingUI : IUI
        {
            Queue<string> queue = new Queue<string>();
          public LosingUI()
            {
                queue.Enqueue("1");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
                queue.Enqueue("3");
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