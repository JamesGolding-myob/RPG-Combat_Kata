using Xunit;
namespace RPG.Combat.Kata
{
    public class GameTests
    {
        World world = new World(10);
        CharacterCreator characterCreator = new CharacterCreator();

        [Fact]
        public void MeleeCharacterCreatedWhenMeleeCHaracterOptionPicked()
        {           
            var character = characterCreator.CreateCharacter(IHaveHealthOptions.Melee, world);
            Assert.True(character is MeleeCharacter);
            
        }

        [Fact]

        public void RangedCharacterCreatedWhenRangedCharacterOptionChosen()
        {
            var character = characterCreator.CreateCharacter(IHaveHealthOptions.Ranged, world);
            Assert.True(character is RangedCharacter);
        }
    }
}