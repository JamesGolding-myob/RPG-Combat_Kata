using Xunit;
using RPG.Combat.Kata;

namespace RPG.Combat.Kata.Tests
{
    public class EndToEndTests
    {
       
        [Fact]
        public void FirstIterationBothPlayersAreAliveWhenCreated()
        {
            Character playerOne = new Character();
            Character playerTwo = new Character();

            Assert.True(playerOne.IsAlive);
            Assert.True(playerTwo.IsAlive);
        }

        [Fact]
        public void FirstIterationCharactersCanAttackAndHeal()
        {   
            Character playerOne = new Character();
            Character playerTwo = new Character();

            playerOne.TakeAction(Action.Attack, playerTwo);
            playerTwo.TakeAction(Action.Heal, playerTwo);

            Assert.Equal(500, playerTwo.Health);

        }

        [Fact]
        public void FirstIterationACharacterCanDie()
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
