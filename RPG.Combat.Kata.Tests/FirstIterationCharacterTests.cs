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
            var character = new MeleeCharacter(world);
            
            Assert.Equal(1000, character.Health);
        }
       
        [Fact]
        public void CharacterIsAliveWhenCreated()
        {
            Character character = new RangedCharacter(world);

            Assert.True(character.IsAlive);    
        }

        [Fact]
        public void DefaultCharacterLevelIsOneWhenCreated()
        {
            Character defaultCharacter = new RangedCharacter(world);

            Assert.Equal(1, defaultCharacter.Level);
        }

        [Fact]
        public void CharactersCanAttackCharactersFor600Damage()
        {   
            Character instigator = new MeleeCharacter(world);
            Character targetCharacter = new RangedCharacter(world);

            instigator.TakeAction(Actions.Attack, targetCharacter);
            
            Assert.Equal(400, targetCharacter.Health);
        }

        [Fact]
        public void CharactersCanHealHurtCharactersFor100()
        {
            var activeCharacter = new RangedCharacter(world, health:200);

            activeCharacter.TakeAction(Actions.Heal, activeCharacter);

            Assert.Equal(300, activeCharacter.Health);
        }

        [Fact]
        public void ACharacterCanDieWhenHealthReachesZeroOrBelow()
        {
            Character instigator = new MeleeCharacter(world);
            Character targetCharacter = new MeleeCharacter(world, health: 600);

           instigator.TakeAction(Actions.Attack, targetCharacter );
            
            Assert.False(targetCharacter.IsAlive);
        }

        [Fact]
        public void DeadCharactersCanNotBeHealed()
        {
            var activeCharacter = new MeleeCharacter(world, health: 0);

            activeCharacter.TakeAction(Actions.Heal, activeCharacter);

            Assert.False(activeCharacter.Health > 0);
        }

        [Fact]
        public void CharacterHealthCanNotBecomeNegative()
        {
            var instigator = new RangedCharacter(world);
            var targetCharacter = new MeleeCharacter(world, health: 1);

            instigator.TakeAction(Actions.Attack, targetCharacter);

            Assert.Equal(0, targetCharacter.Health);
        }

        [Fact]
        public void HealingCannotMakeCharacterHealthGreaterThan1000()
        {
            Character activeCharacter = new RangedCharacter(world);

            activeCharacter.TakeAction(Actions.Heal, activeCharacter);

            Assert.Equal(1000, activeCharacter.Health);
        }

    }
}
