using Xunit;
namespace RPG.Combat.Kata
{
    public class InputConverterTests
    {
        InputConverter inputConverter = new InputConverter();

        [Fact]
        public void Inputof1ProducesACharacterChoiceOfMeleeCharacter()
        {
            Assert.Equal(IHaveHealthOptions.Melee, inputConverter.ConvertCharacterChoice("1"));
        }

        [Fact]
        public void Inputof2ProducesACharacterChoiceOfRangedCharacter()
        {
           Assert.Equal(IHaveHealthOptions.Ranged, inputConverter.ConvertCharacterChoice("2")); 
        }

        [Theory]
        [InlineData("1", Actions.Attack)]
        [InlineData("2", Actions.Heal)]
        [InlineData("3", Actions.MoveUp)]
        [InlineData("4", Actions.MoveRight)]
        [InlineData("5", Actions.MoveDown)]
        [InlineData("6", Actions.MoveLeft)]
        public void StringInputsGetConvertedToCorrectAction(string input, Actions expectedAction)
        {
            Assert.Equal(expectedAction, inputConverter.ActionsConverter(input));
        }

        
    }
}