using Xunit;
using RPG.Combat.Kata;
namespace RPG.Combat.Kata.Tests
{
    public class FirstIterationCharacterTests
    {
        [Fact]
        public void DefaultCharacterHealthIs1000()
        {
            var character = new Character();
            
            Assert.Equal(1000, character.Health);
        }
       
        [Fact]
        public void CharacterIsAliveWhenCreated()
        {
            Character character = new Character();

            Assert.True(character.IsAlive);    
        }

        [Fact]
        public void DefaultCharacterLevelIsOneWhenCreated()
        {
            Character defaultCharacter = new Character();

            Assert.Equal(1, defaultCharacter.Level);
        }

        [Fact]
        public void CharactersCanAttackCharactersFor600Damage()
        {   
            Character characterOne = new Character();
            Character targetCharacter = new Character();

            characterOne.TakeAction(ActionType.Attack, targetCharacter, true);
            
            Assert.Equal(400, targetCharacter.Health);
        }

        [Fact]
        public void CharactersCanHealHurtCharactersFor100()
        {
            var character = new Character(health:200);

            character.TakeAction(ActionType.Heal, character, true);

            Assert.Equal(300, character.Health);
        }

        [Fact]
        public void ACharacterCanDieWhenHealthReachesZeroOrBelow()
        {
            Character characterOne = new Character();
            Character targetCharacter = new Character(health: 600);

           characterOne.TakeAction(ActionType.Attack, targetCharacter, true);
            
            Assert.False(targetCharacter.IsAlive);
        }

        [Fact]
        public void DeadCharactersCanNotBeHealed()
        {
            var characterOne = new Character(0);

            characterOne.TakeAction(ActionType.Heal, characterOne, true);

            Assert.False(characterOne.Health > 0);
        }

        [Fact]
        public void CharacterHealthCanNotBecomeNegative()
        {
            var characterOne = new Character();
            var targetCharacter = new Character(health: 1);

            characterOne.TakeAction(ActionType.Attack, targetCharacter, true);

            Assert.Equal(0, targetCharacter.Health);
        }

        [Fact]
        public void HealingCannotMakeCharacterHealthGreaterThan1000()
        {
            Character playerOne = new Character();

            playerOne.TakeAction(ActionType.Heal, playerOne, true);

            Assert.Equal(1000, playerOne.Health);
        }

    }
}
