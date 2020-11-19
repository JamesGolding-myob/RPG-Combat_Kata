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
            var character = new Character(world);

            character.TakeAction(ActionType.Attack, character, world);

            Assert.Equal(1000, character.Health);       
        }

        [Fact]
        public void CharacterCanOnlyHealThemselvesFor100()
        {
            var characterStartingWith500Health = new Character(world, health: 500);
            var characterTwo = new Character(world);

            characterTwo.TakeAction(ActionType.Heal, characterStartingWith500Health, world);
            characterStartingWith500Health.TakeAction(ActionType.Heal, characterStartingWith500Health, world);

            Assert.Equal(600, characterStartingWith500Health.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsHigherDeals300DamageInsteadOf600()
        {
            var levelOneCharacter = new Character(world);
            var levelSixCharacter = new Character(world, health: 1000, level: 6);

            levelOneCharacter.TakeAction(ActionType.Attack, levelSixCharacter, world);

            Assert.Equal(700, levelSixCharacter.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsBelowDeals900InsteadOf600()
        {
            var levelOneCharacter = new Character(world, health: 1000);
            var levelSixCharacter = new Character(world, level: 6);

            levelSixCharacter.TakeAction(ActionType.Attack, levelOneCharacter, world);

            Assert.Equal(100, levelOneCharacter.Health);
        }
    }



    

    
}