using RPG.Combat.Kata;
using Xunit;
namespace RPG.Combat.Kata.Tests
{
    public class SecondIterationCharacterTests
    {
        World world = new World(15);
        [Fact]
        public void CharacterCanNotAttackThemselves()
        {
            var activeCharacter = new MeleeCharacter(world);

            activeCharacter.TakeAction(Actions.Attack, activeCharacter);

            Assert.Equal(1000, activeCharacter.Health);       
        }

        [Fact]
        public void CharacterCanOnlyHealThemselvesFor100()
        {
            var characterStartingWith500Health = new RangedCharacter(world, health: 500);
            var characterTwo = new RangedCharacter(world);

            characterTwo.TakeAction(Actions.Heal, characterStartingWith500Health);
            characterStartingWith500Health.TakeAction(Actions.Heal, characterStartingWith500Health);

            Assert.Equal(600, characterStartingWith500Health.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsHigherDeals300DamageInsteadOf600()
        {
            var levelOneCharacter = new MeleeCharacter(world);
            var levelSixCharacter = new RangedCharacter(world, health: 1000, level: 6);

            levelOneCharacter.TakeAction(Actions.Attack, levelSixCharacter);

            Assert.Equal(700, levelSixCharacter.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsBelowDeals900InsteadOf600()
        {
            var levelOneCharacter = new RangedCharacter(world, health: 1000);
            var levelSixCharacter = new MeleeCharacter(world, level: 6);

            levelSixCharacter.TakeAction(Actions.Attack, levelOneCharacter);

            Assert.Equal(100, levelOneCharacter.Health);
        }
    }


    
}