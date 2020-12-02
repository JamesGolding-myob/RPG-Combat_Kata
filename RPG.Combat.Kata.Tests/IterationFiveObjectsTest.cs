using Xunit;
using RPG.Combat.Kata;
namespace RPG.Combat.Kata.Tests
{
    public class IterationFiveObjectsTest
    {
        World world = new World(25);

        [Fact]
        public void CharactersCanAttackInanimateObjectsLikeTrees()
        {
            var tree = new Tree(health:2000);
            var woodCutter = new MeleeCharacter(world);

            woodCutter.TakeAction(Actions.Attack, tree);

            Assert.Equal(1400, tree.Health);
        }

        [Fact]
        public void ObjectsCanGetDestroyedWhenHealthReachesZero()
        {
            var tree = new Tree(health:0);

            Assert.True(tree.IsDestroyed);
        }

        [Fact]
        public void ObjectsCanNotBeHealedByCharacters()
        {
            var tree = new Tree(health:500);
            var forester = new MeleeCharacter(world);

            forester.TakeAction(Actions.Heal, tree);

            Assert.Equal(500, tree.Health);
        }
    }
}