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

            character.TakeAction(Action.Attack, character);

            Assert.Equal(1000, character.Health);  
            
        }

        [Fact]
        public void CharacterCanOnlyHealThemselves()
        {
            var characterStaringWith500Health = new Character(health: 500);
            var characterTwo = new Character();

            characterTwo.TakeAction(Action.Heal, characterStaringWith500Health);
            characterStaringWith500Health.TakeAction(Action.Heal, characterStaringWith500Health);

            Assert.Equal(600, characterStaringWith500Health.Health);//it is only implied tht one heal == 100 Health from previous tests
        }

        [Fact]
        public void AttackingACharacter5LevelsHigherDeals300DamageInsteadOf600()
        {
            var levelOneCharacter = new Character();
            var levelSixCharacter = new Character(level: 6);

            levelOneCharacter.TakeAction(Action.Attack, levelSixCharacter);

            Assert.Equal(700, levelSixCharacter.Health);
        }

        [Fact]
        public void AttackingACharacter5LevelsBelowDeals900InsteadOf600()
        {
            var levelOneCharacter = new Character(health: 900);
            var levelSixCharacter = new Character(level: 6);

            levelSixCharacter.TakeAction(Action.Attack, levelOneCharacter);
            Assert.False(levelOneCharacter.IsAlive);
        }
    }



    

    
}