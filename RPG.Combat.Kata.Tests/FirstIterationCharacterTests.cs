using Xunit;

namespace RPG.Combat.Kata.Tests
{
    public class FirstIterationCharacterTests
    {
        ActionController actionController = new ActionController();

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

            characterOne.TakeAction(Action.Attack, targetCharacter, actionController);
            
            Assert.Equal(400, targetCharacter.Health);
        }

        [Fact]
        public void CharactersCanHealHurtCharactersFor100()
        {
            var character = new Character(health:200);

            character.TakeAction(Action.Heal, character, actionController);

            Assert.Equal(300, character.Health);
        }

        [Fact]
        public void ACharacterCanDieWhenHealthReachesZeroOrBelow()
        {
            Character characterOne = new Character();
            Character targetCharacter = new Character(health: 600);

           characterOne.TakeAction(Action.Attack, targetCharacter, actionController);
            
            Assert.False(targetCharacter.IsAlive);
        }

        [Fact]
        public void DeadCharactersCanNotBeHealed()
        {
            var characterOne = new Character(0);

            characterOne.TakeAction(Action.Heal, characterOne, actionController);

            Assert.False(characterOne.Health > 0);
        }

        [Fact]
        public void CharacterHealthCanNotBecomeNegative()
        {
            var characterOne = new Character();
            var targetCharacter = new Character(health: 1);

            characterOne.TakeAction(Action.Attack, targetCharacter, actionController);

            Assert.Equal(0, targetCharacter.Health);
        }

        [Fact]
        public void HealingCannotMakeCharacterHealthGreaterThan1000()
        {
            Character playerOne = new Character();

            playerOne.TakeAction(Action.Heal, playerOne, actionController);

            Assert.Equal(1000, playerOne.Health);
        }

    }
}
