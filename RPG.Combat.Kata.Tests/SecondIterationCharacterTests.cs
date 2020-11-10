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
            var characterOne = new Character();
            var characterTwo = new Character();

            characterTwo.TakeAction(Action.Attack, characterOne);
            characterTwo.TakeAction(Action.Heal, characterOne);
            characterOne.TakeAction(Action.Heal, characterOne);

            Assert.Equal(500, characterOne.Health);
        }
    }



    

    
}