using Xunit;

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
        public void CharacterIsAliveWhenCreated()//think about implied information - like health starting at 1000
        {
            Character character = new Character();
            Assert.True(character.IsAlive);    
        }

        [Fact]
        public void CharactersCanAttackCharacters()
        {   
            Character characterOne = new Character();
            Character characterTwo = new Character();

            var startingHealth = 1000;

            characterOne.TakeAction(Action.Attack, characterTwo);
            
            Assert.True(characterTwo.Health < startingHealth);
        }

        [Fact]
        public void CharactersCanHealCharacters()
        {
            var character = new Character(health:100);

            character.TakeAction(Action.Heal, character);
            Assert.Equal(200, character.Health);
        }

        [Fact]
        public void ACharacterCanDie()
        {
            Character characterOne = new Character();
            Character characterTwo = new Character();

           AttackCharacter(characterTwo, characterOne);
            
            Assert.False(characterTwo.IsAlive);
        }

        [Fact]
        public void DeadCharactersCanNotBeHealed()
        {
            var characterOne = new Character(0);
            characterOne.TakeAction(Action.Heal, characterOne);
            Assert.False(characterOne.Health > 0);
        }

        [Fact]
        public void CharacterHealthCanNotBecomeNegative()
        {
            var characterOne = new Character();
            var characterTwo = new Character();

            AttackCharacter(characterOne, characterTwo);

            Assert.Equal(0, characterOne.Health);
        }

        [Fact]
        public void HealingCannotMakeHealthGreaterThan1000()
        {
            Character playerOne = new Character();

            playerOne.TakeAction(Action.Heal, playerOne);
            Assert.Equal(1000, playerOne.Health);
        }

        public void AttackCharacter(Character targetCharacter, Character attackingCharacter)
        {
            while(targetCharacter.IsAlive)
            {
                attackingCharacter.TakeAction(Action.Attack, targetCharacter);
            }
        }
    }
}
