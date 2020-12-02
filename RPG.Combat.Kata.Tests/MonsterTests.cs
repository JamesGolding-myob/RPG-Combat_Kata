using Xunit;
namespace RPG.Combat.Kata
{
    public class MonsterTests
    {
        World map = new World(3);

        [Fact]
        public void AMonsterOutsideOfItsAttackRangeWillMoveTowardsTheCharacter()
        {
            var character = new MeleeCharacter(map);
            var monster = new Monster(map);
            map.SetWorldObjectPosition(0, 0, character);
            map.SetWorldObjectPosition(2, 2, monster);

            monster.TakeTurn();
            monster.TakeTurn();

            Assert.True(map.SpaceOccupiedBy(0, 1) == monster);
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingUp()
        {
            var biggerMap = new World(6);
            var character = new MeleeCharacter(biggerMap);
            var monster = new Monster(biggerMap);
            biggerMap.SetWorldObjectPosition( 1, 4, character);
            biggerMap.SetWorldObjectPosition(1, 2, monster);

            monster.TakeTurn();

            Assert.True(biggerMap.SpaceOccupiedBy(1, 3) == monster);
            Assert.False(biggerMap.SpaceOccupiedBy(1, 2) == monster);
            
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingDown()
        {
            var biggerMap = new World(6);
            var character = new MeleeCharacter(biggerMap);
            var monster = new Monster(biggerMap);
            biggerMap.SetWorldObjectPosition( 2, 2, character);
            biggerMap.SetWorldObjectPosition(2, 4, monster);

            monster.TakeTurn();

            Assert.True(biggerMap.SpaceOccupiedBy(2, 3) == monster);
            Assert.False(biggerMap.SpaceOccupiedBy(2, 4) == monster);
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingRight()
        {
            var biggerMap = new World(6);
            var character = new MeleeCharacter(biggerMap);
            var monster = new Monster(biggerMap);
            biggerMap.SetWorldObjectPosition( 5, 4, character);
            biggerMap.SetWorldObjectPosition(1, 4, monster);

            monster.TakeTurn();

            Assert.True(biggerMap.SpaceOccupiedBy(3, 4) == monster);
            Assert.False(biggerMap.SpaceOccupiedBy(1, 4) == monster);
            
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingLeft()
        {
            var biggerMap = new World(6);
            var character = new MeleeCharacter(biggerMap);
            var monster = new Monster(biggerMap);
            biggerMap.SetWorldObjectPosition(0, 2, character);
            biggerMap.SetWorldObjectPosition(3, 2, monster);

            monster.TakeTurn();

            Assert.True(biggerMap.SpaceOccupiedBy(1, 2) == monster);
            Assert.False(biggerMap.SpaceOccupiedBy(3, 2) == monster);
        }

        
    }
}