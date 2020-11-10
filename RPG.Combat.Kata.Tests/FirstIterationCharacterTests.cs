using Xunit;

namespace RPG.Combat.Kata.Tests
{
    public class FirstIterationCharacterTests
    {
       
        [Fact]
        public void FirstIterationCharacterIsAliveWhenCreated()//think about implied information - like health starting at 1000
        {
            Character character = new Character();
            Assert.True(character.IsAlive);
            
        }

        [Fact]
        public void FirstIterationCharactersCanAttackAndHealCharacters()//break into two tests to avoid testing two things in one test
        {   
            Character playerOne = new Character();
            Character playerTwo = new Character();

            playerOne.TakeAction(Action.Attack, playerTwo);
            playerTwo.TakeAction(Action.Heal, playerTwo);

            Assert.Equal(500, playerTwo.Health);

        }

        [Fact]
        public void FirstIterationACharacterCanDieAndHealthIsZero()//break into two tests to avoid testing two things in one test
        {
            Character playerOne = new Character();
            Character playerTwo = new Character();

            playerOne.TakeAction(Action.Attack, playerTwo);
            playerOne.TakeAction(Action.Attack, playerTwo);
            
            Assert.Equal(0, playerTwo.Health);
            Assert.False(playerTwo.IsAlive);
        }

        [Fact]
        public void HealingCannotMakeHealthGreaterThan1000()
        {
            Character playerOne = new Character();

            playerOne.TakeAction(Action.Heal, playerOne);
            Assert.Equal(1000, playerOne.Health);
        }
    }
}
