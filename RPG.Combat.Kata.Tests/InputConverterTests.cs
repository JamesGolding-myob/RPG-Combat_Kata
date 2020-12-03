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

        
    }
}