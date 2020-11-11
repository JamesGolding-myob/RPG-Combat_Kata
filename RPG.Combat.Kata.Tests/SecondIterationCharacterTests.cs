using RPG.Combat.Kata;
using Xunit;

namespace RPG.Combat.Kata.Tests
{
    public class SecondIterationCharacterTests
    {
        ActionController actionController = new ActionController();
        [Fact]
        public void CharacterCanNotAttackThemSelves()
        {
            var character = new Character();

            character.TakeAction(Action.Attack, character, actionController);

            Assert.Equal(1000, character.Health);       
        }

        [Fact]
        public void CharacterCanOnlyHealThemselvesFor100()
        {
            var characterStartingWith500Health = new Character(health: 500);
            var characterTwo = new Character();

            characterTwo.TakeAction(Action.Heal, characterStartingWith500Health, actionController);
            characterStartingWith500Health.TakeAction(Action.Heal, characterStartingWith500Health, actionController);

            Assert.Equal(600, characterStartingWith500Health.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsHigherDeals300DamageInsteadOf600()
        {
            var levelOneCharacter = new Character();
            var levelSixCharacter = new Character(level: 6);

            levelOneCharacter.TakeAction(Action.Attack, levelSixCharacter, actionController);

            Assert.Equal(700, levelSixCharacter.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsBelowDeals900InsteadOf600()
        {
            var levelOneCharacter = new Character();
            var levelSixCharacter = new Character(level: 6);

            levelSixCharacter.TakeAction(Action.Attack, levelOneCharacter, actionController);

            Assert.Equal(100, levelOneCharacter.Health);
        }
    }



    

    
}