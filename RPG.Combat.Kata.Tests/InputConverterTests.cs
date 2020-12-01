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
        [InlineData("1", true)]
        [InlineData("2", true)]
        [InlineData("3", false)]
        [InlineData("a", false)]
        [InlineData("1a", false)]
        public void CharacterChoiceInpuitValidationOnly1And2AreValideStringInputs(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, inputConverter.IsValidCharacterChoice(input));
        }
    }
}