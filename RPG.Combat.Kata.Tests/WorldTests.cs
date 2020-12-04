using Xunit;
using System.Collections.Generic;
namespace RPG.Combat.Kata.Tests
{
    public class WorldTests
    {
        World world = new World(8);
            [Fact]
            public void AllSpacesArouundCharacterAreEmptyAllPotentialTargetsAreEmptyGoingClockwiseFromTheTop()
            {
                var character = new MeleeCharacter(world);
                world.SetWorldObjectPosition(3,3, character);
                List<IHaveHealth> expectedTargets = new List<IHaveHealth>(){
                    world.SpaceOccupiedBy(3, 4), 
                    world.SpaceOccupiedBy(4, 3), 
                    world.SpaceOccupiedBy(3, 2), 
                    world.SpaceOccupiedBy(2, 3)};

                Assert.Equal(expectedTargets, world.GetPotentialTargetsForCharacter(character));

            }

            
    }
}