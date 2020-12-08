using Xunit;
using System.Collections.Generic;
namespace RPG.Combat.Kata.Tests
{
    public class WorldTests
    {
        World world = new World(8);

            [Fact]
            public void AllSpacesAroundCharacterAreEmptyAllPotentialTargetsAreEmptyGoingClockwiseFromTheTop()
            {
                var character = new MeleeCharacter(world);
                world.SetWorldObjectPosition(3,3, character);
                List<IHaveHealth> expectedTargets = new List<IHaveHealth>(){
                    world.SpaceOccupiedBy(3, 5), 
                    world.SpaceOccupiedBy(5, 3), 
                    world.SpaceOccupiedBy(3, 1), 
                    world.SpaceOccupiedBy(1, 3)};

                Assert.Equal(expectedTargets, world.GetPotentialTargetsForCharacter(character));

            }

            [Theory] 
            [InlineData(3, 7, 0)]
            [InlineData(7, 2, 1)]
            [InlineData(4, 0, 2)]
            [InlineData(0, 1, 3)]
            public void TargetsOutsideTheMapAreTreatedAsEmptySpaces_OutOfArrayException(int x, int y, int targetIndex)
            {
                var character = new MeleeCharacter(world);
                world.SetWorldObjectPosition(x, y, character);
                 
                Assert.True(world.GetPotentialTargetsForCharacter(character)[targetIndex] is EmptySpace);
            }

            [Fact]
            public void MeleeCharacterCanTargetAMonster2SpacesAbove()
            {
                var character = new MeleeCharacter(world);
                var monster = new Monster(world);

                world.SetWorldObjectPosition(0, 0, character);
                world.SetWorldObjectPosition(0, 2, monster);
                 
                Assert.Equal(monster, world.GetPotentialTargetsForCharacter(character)[0]);
            }

            [Fact]
            public void MeleeCharacterCanTargetAMonster2SpacesToTheRight()
            {
                var character = new MeleeCharacter(world);
                var monster = new Monster(world);

                world.SetWorldObjectPosition(0, 0, character);
                world.SetWorldObjectPosition(2, 0, monster);
                 
                Assert.Equal(monster, world.GetPotentialTargetsForCharacter(character)[1]);
            }

            [Fact]
            public void MeleeCharacterCanTargetAMonster2SpacesBelow()
            {
                var character = new MeleeCharacter(world);
                var monster = new Monster(world);

                world.SetWorldObjectPosition(0, 2, character);
                world.SetWorldObjectPosition(0, 0, monster);
                 
                Assert.Equal(monster, world.GetPotentialTargetsForCharacter(character)[2]);
            }

            [Fact]
            public void MeleeCharacterCanTargetAMonster2SpacesToTheLeft()
            {
                var character = new MeleeCharacter(world);
                var monster = new Monster(world);

                world.SetWorldObjectPosition(2, 0, character);
                world.SetWorldObjectPosition(0, 0, monster);
                 
                Assert.Equal(monster, world.GetPotentialTargetsForCharacter(character)[3]);
            }

            [Fact]
            public void AllSpacesAroundRangedCharacterAreEmptyAllPotentialTargetsAreEmptyGoingClockwiseFromTheTop()
            {
                var character = new RangedCharacter(world);
                world.SetWorldObjectPosition(4,4, character);
                List<IHaveHealth> expectedTargets = new List<IHaveHealth>(){
                    world.SpaceOccupiedBy(4, 7),
                    world.SpaceOccupiedBy(7, 4), 
                    world.SpaceOccupiedBy(4, 0), 
                   world.SpaceOccupiedBy(0, 4)};

                Assert.Equal(expectedTargets[0], world.GetPotentialTargetsForCharacter(character)[0]);

            }

            [Fact]
            public void ClosestNonEmptyTargetToTheRightInRangeisChosenAsPotentailTarget()
            {
                var character = new RangedCharacter(world);
                var monster = new Monster(world);
                var tree = new Tree();

                world.SetWorldObjectPosition(4, 4, character);
                world.SetWorldObjectPosition(5, 4, tree);
                world.SetWorldObjectPosition(6, 4, monster);
               
                Assert.Equal(tree, world.GetPotentialTargetsForCharacter(character)[1]);
            }

            [Fact]
            public void ClosestNonEmptyTargetToTheLefttInRangeisChosenAsPotentailTarget()
            {
                var character = new RangedCharacter(world);
                var monster = new Monster(world);
                var tree = new Tree();

                world.SetWorldObjectPosition(4, 4, character);
                world.SetWorldObjectPosition(3, 4, tree);
                world.SetWorldObjectPosition(2, 4, monster);
               
                Assert.Equal(tree, world.GetPotentialTargetsForCharacter(character)[3]);
            }

            [Fact]
            public void ClosestNonEmptyTargetAboveInRangeisChosenAsPotentailTarget()
            {
                var character = new RangedCharacter(world);
                var monster = new Monster(world);
                var tree = new Tree();

                world.SetWorldObjectPosition(4, 4, character);
                world.SetWorldObjectPosition(4, 6, tree);
                world.SetWorldObjectPosition(4, 5, monster);
               
                Assert.Equal(monster, world.GetPotentialTargetsForCharacter(character)[0]);
            }

            [Fact]
            public void ClosestNonEmptyTargetBelowInRangeisChosenAsPotentailTarget()
            {
                var character = new RangedCharacter(world);
                var monster = new Monster(world);
                var tree = new Tree();

                world.SetWorldObjectPosition(4, 4, character);
                world.SetWorldObjectPosition(4, 2, tree);
                world.SetWorldObjectPosition(4, 3, monster);
               
                Assert.Equal(monster, world.GetPotentialTargetsForCharacter(character)[2]);
            }

            
            
    }
}