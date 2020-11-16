using Xunit;
namespace RPG.Combat.Kata.Tests
{
    public class FactionsTests
    {
        [Fact]
        public void CharactersAreInitiallyUnaligned()
        {
            var character = new Character();
            Assert.Equal(Factions.None, character.Faction);
        }
        [Fact]
        public void CharactersCanJoinFactions()
        {
            var character = new Character();

            character.JoinFaction(Factions.Eagles);
            Assert.Equal(Factions.Eagles, character.Faction);
        }
    }
}