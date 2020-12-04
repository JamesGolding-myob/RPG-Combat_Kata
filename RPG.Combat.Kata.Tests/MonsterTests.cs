using Xunit;
namespace RPG.Combat.Kata
{
    public class MonsterTests
    {
        World map = new World(8);

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
            var character = new MeleeCharacter(map);
            var monster = new Monster(map);
            map.SetWorldObjectPosition( 1, 4, character);
            map.SetWorldObjectPosition(1, 2, monster);

            monster.TakeTurn();

            Assert.True(map.SpaceOccupiedBy(1, 3) == monster);
            Assert.False(map.SpaceOccupiedBy(1, 2) == monster);
            
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingDown()
        {
            var character = new MeleeCharacter(map);
            var monster = new Monster(map);
            map.SetWorldObjectPosition( 2, 2, character);
            map.SetWorldObjectPosition(2, 4, monster);

            monster.TakeTurn();

            Assert.True(map.SpaceOccupiedBy(2, 3) == monster);
            Assert.False(map.SpaceOccupiedBy(2, 4) == monster);
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingRight()
        {
            var character = new MeleeCharacter(map);
            var monster = new Monster(map);
            map.SetWorldObjectPosition( 5, 4, character);
            map.SetWorldObjectPosition(1, 4, monster);

            monster.TakeTurn();

            Assert.True(map.SpaceOccupiedBy(3, 4) == monster);
            Assert.False(map.SpaceOccupiedBy(1, 4) == monster);
            
        }

        [Fact]
        public void MonsterWillStopMovingWhenItRunsIntoACharacterWhenMovingLeft()
        {
            var character = new MeleeCharacter(map);
            var monster = new Monster(map);
            map.SetWorldObjectPosition(0, 2, character);
            map.SetWorldObjectPosition(3, 2, monster);

            monster.TakeTurn();

            Assert.True(map.SpaceOccupiedBy(1, 2) == monster);
            Assert.False(map.SpaceOccupiedBy(3, 2) == monster);
        }

        [Theory]
        [InlineData(2, 3, 900)]
        [InlineData(3, 2, 900)]
        [InlineData(2, 1, 900)]
        [InlineData(1, 2, 900)]
        [InlineData(3, 3, 1000)]
        [InlineData(1, 3, 1000)]
        [InlineData(1, 1, 1000)]
        [InlineData(3, 1, 1000)]
        [InlineData(4, 2, 1000)]
        [InlineData(2, 4, 1000)]
        [InlineData(2, 0, 1000)]
        [InlineData(0, 2, 1000)]

        public void MonsterCanAttackACharacterFor100WhenItIsInRange(int monsterX, int monsterY, int targetExpectedHealth)
        {
            var target = new MeleeCharacter(map, health: 1000);
            var monster = new Monster(map);

            map.SetWorldObjectPosition(2, 2, target);
            map.SetWorldObjectPosition(monsterX, monsterY, monster);
            
            monster.TakeTurn();
            Assert.Equal(targetExpectedHealth, target.Health);
        }

        [Fact]
        public void MonsterCanKillCharacter()
        {
            var target = new MeleeCharacter(map, health: 100);
            var monster = new Monster(map);

            map.SetWorldObjectPosition(0, 0, target);
            map.SetWorldObjectPosition(0, 1, monster);
            
            monster.TakeTurn();
            Assert.False(target.IsAlive);
        }

        
    }
}