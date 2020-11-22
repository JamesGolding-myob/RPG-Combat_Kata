using Xunit;
using RPG.Combat.Kata;
namespace RPG.Combat.Kata.Tests
{
    public class FirstIterationCharacterTests
    {
        World world = new World(30);

        [Fact]
        public void DefaultCharacterHealthIs1000()
        {
            var character = new Character(world);
            
            Assert.Equal(1000, character.Health);
        }
       
        [Fact]
        public void CharacterIsAliveWhenCreated()
        {
            Character character = new Character(world);

            Assert.True(character.IsAlive);    
        }

        [Fact]
        public void DefaultCharacterLevelIsOneWhenCreated()
        {
            Character defaultCharacter = new Character(world);

            Assert.Equal(1, defaultCharacter.Level);
        }

        [Fact]
        public void CharactersCanAttackCharactersFor600Damage()
        {   
            Character characterOne = new Character(world);
            Character targetCharacter = new Character(world);

            characterOne.TakeAction(ActionType.Attack, targetCharacter);
            
            Assert.Equal(400, targetCharacter.Health);
        }

        [Fact]
        public void CharactersCanHealHurtCharactersFor100()
        {
            var character = new Character(world, health:200);

            character.TakeAction(ActionType.Heal, character);

            Assert.Equal(300, character.Health);
        }

        [Fact]
        public void ACharacterCanDieWhenHealthReachesZeroOrBelow()
        {
            Character characterOne = new Character(world);
            Character targetCharacter = new Character(world, health: 600);

           characterOne.TakeAction(ActionType.Attack, targetCharacter );
            
            Assert.False(targetCharacter.IsAlive);
        }

        [Fact]
        public void DeadCharactersCanNotBeHealed()
        {
            var characterOne = new Character(world, health: 0);

            characterOne.TakeAction(ActionType.Heal, characterOne);

            Assert.False(characterOne.Health > 0);
        }

        [Fact]
        public void CharacterHealthCanNotBecomeNegative()
        {
            var instigator = new Character(world);
            var targetCharacter = new Character(world, health: 1);

            instigator.TakeAction(ActionType.Attack, targetCharacter);

            Assert.Equal(0, targetCharacter.Health);
        }

        [Fact]
        public void HealingCannotMakeCharacterHealthGreaterThan1000()
        {
            Character playerOne = new Character(world);

            playerOne.TakeAction(ActionType.Heal, playerOne);

            Assert.Equal(1000, playerOne.Health);
        }

    }
}
