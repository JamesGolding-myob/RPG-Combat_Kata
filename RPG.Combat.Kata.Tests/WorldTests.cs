using Xunit;
namespace RPG.Combat.Kata.Tests
{
    public class WorldTests
    {
        [Theory]
        [InlineData(3, 9)]
        [InlineData(10, 100)]
        [InlineData(20, 400)]
        public void WorldIsMadeUpOfGroupsOfCells(int worldSize, int expectedSize)
        {
            var world = new World(worldSize);

            Assert.Equal(expectedSize, world.layout.Length);
        }
    }
}