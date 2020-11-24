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
            Character instigator = new Character(world);
            Character targetCharacter = new Character(world);

            instigator.TakeAction(Actions.Attack, targetCharacter);
            
            Assert.Equal(400, targetCharacter.Health);
        }

        [Fact]
        public void CharactersCanHealHurtCharactersFor100()
        {
            var activeCharacter = new Character(world, health:200);

            activeCharacter.TakeAction(Actions.Heal, activeCharacter);

            Assert.Equal(300, activeCharacter.Health);
        }

        [Fact]
        public void ACharacterCanDieWhenHealthReachesZeroOrBelow()
        {
            Character instigator = new Character(world);
            Character targetCharacter = new Character(world, health: 600);

           instigator.TakeAction(Actions.Attack, targetCharacter );
            
            Assert.False(targetCharacter.IsAlive);
        }

        [Fact]
        public void DeadCharactersCanNotBeHealed()
        {
            var activeCharacter = new Character(world, health: 0);

            activeCharacter.TakeAction(Actions.Heal, activeCharacter);

            Assert.False(activeCharacter.Health > 0);
        }

        [Fact]
        public void CharacterHealthCanNotBecomeNegative()
        {
            var instigator = new Character(world);
            var targetCharacter = new Character(world, health: 1);

            instigator.TakeAction(Actions.Attack, targetCharacter);

            Assert.Equal(0, targetCharacter.Health);
        }

        [Fact]
        public void HealingCannotMakeCharacterHealthGreaterThan1000()
        {
            Character activeCharacter = new Character(world);

            activeCharacter.TakeAction(Actions.Heal, activeCharacter);

            Assert.Equal(1000, activeCharacter.Health);
        }

    }
}
