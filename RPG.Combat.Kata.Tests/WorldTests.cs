using Xunit;
namespace RPG.Combat.Kata.Tests
{
    public class WorldTests
    {
        [Theory]
        [InlineData(3, 3)]
        [InlineData(10, 10)]
        [InlineData(20, 20)]
        public void WorldIsMadeUpOfGroupsOfCells(int worldSize, int expectedSize)
        {
            var world = new World(worldSize);

            Assert.Equal(expectedSize, world.layout.Length);
        }
    }
}