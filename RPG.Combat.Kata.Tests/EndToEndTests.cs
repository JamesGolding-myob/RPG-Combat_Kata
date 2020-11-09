using Xunit;
using RPG.Combat.Kata;

namespace RPG.Combat.Kata.Tests
{
    public class EndToEndTests
    {
        string inputAttack = "attack";
        string inputHeal = "heal";


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

            playerOne.TakeAction(inputAttack, playerTwo);
            playerTwo.TakeAction(inputHeal, playerTwo);

            Assert.Equal(500, playerTwo.Health);

        }

        [Fact]
        public void FirstIterationACharacterCanDie()
        {
            Character playerOne = new Character();
            Character playerTwo = new Character();

            playerOne.TakeAction(inputAttack, playerTwo);
            playerOne.TakeAction(inputAttack, playerTwo);
            
            Assert.Equal(0, playerTwo.Health);
            Assert.False(playerTwo.IsAlive);
        }
    }
}
