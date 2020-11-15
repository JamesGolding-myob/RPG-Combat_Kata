using RPG.Combat.Kata;
using Xunit;


namespace RPG.Combat.Kata.Tests
{
    public class SecondIterationCharacterTests
    {
        [Fact]
        public void CharacterCanNotAttackThemSelves()
        {
            var character = new Character();

            character.TakeAction(ActionType.Attack, character, true);

            Assert.Equal(1000, character.Health);       
        }

        [Fact]
        public void CharacterCanOnlyHealThemselvesFor100()
        {
            var characterStartingWith500Health = new Character(health: 500);
            var characterTwo = new Character();

            characterTwo.TakeAction(ActionType.Heal, characterStartingWith500Health, true);
            characterStartingWith500Health.TakeAction(ActionType.Heal, characterStartingWith500Health, true);

            Assert.Equal(600, characterStartingWith500Health.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsHigherDeals300DamageInsteadOf600()
        {
            var levelOneCharacter = new Character();
            var levelSixCharacter = new Character(level: 6);

            levelOneCharacter.TakeAction(ActionType.Attack, levelSixCharacter, true);

            Assert.Equal(700, levelSixCharacter.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsBelowDeals900InsteadOf600()
        {
            var levelOneCharacter = new Character();
            var levelSixCharacter = new Character(level: 6);

            levelSixCharacter.TakeAction(ActionType.Attack, levelOneCharacter, true);

            Assert.Equal(100, levelOneCharacter.Health);
        }
    }



    

    
}