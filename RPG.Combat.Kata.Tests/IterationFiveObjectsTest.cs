using Xunit;
using RPG.Combat.Kata;
namespace RPG.Combat.Kata.Tests
{
    public class IterationFiveObjectsTest
    {
        [Fact]
        public void CharactersCanAttackInanimateObjectsLikeTrees()
        {
            var tree = new Tree(health:2000);
            var woodCutter = new Character();

            woodCutter.TakeAction(ActionType.Attack, tree, true);

            Assert.Equal(1400, tree.Health);
        }

        [Fact]
        public void ObjectsCanGetDestroyedWhenHealthReachesZero()
        {
            var tree = new Tree(0);

            Assert.True(tree.IsDestroyed);
        }

        [Fact]
        public void ObjectsCanNotBeHealedByCharacters()
        {
            var tree = new Tree(health:500);
            var forester = new MeleeCharacter();

            forester.TakeAction(ActionType.Heal, tree, true);

            Assert.Equal(500, tree.Health);
        }
    }
}