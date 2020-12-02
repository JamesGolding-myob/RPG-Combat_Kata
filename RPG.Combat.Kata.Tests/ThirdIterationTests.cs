using Xunit;
using RPG.Combat.Kata;

public class ThirdIterationTests
{
    World world = new World(35);

    [Fact]
    public void Characters3mAwayAreOutOfRangeOfMeleeCharacters()
    {
        var instigator = new MeleeCharacter(world);
        var target = new MeleeCharacter(world);
        
        world.SetWorldObjectPosition(0,0, instigator);
        world.SetWorldObjectPosition(3, 0, target);

        Assert.False(world.CharacterIsInRange(instigator, target));
        
    }

    [Fact]
    public void Characters2mAwayAreInRangeOfMeleeCharacters()
    {
        var instigator = new MeleeCharacter(world);
        var target = new MeleeCharacter(world);

        world.SetWorldObjectPosition(1, 0, instigator);
        world.SetWorldObjectPosition(3, 0, target);

        Assert.True(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOver20mAwayAreOutOfRangeOfRangedCharacters()
    {
        var instigator = new RangedCharacter(world);
        var target = new MeleeCharacter(world);
        
        world.SetWorldObjectPosition(9, 0, instigator);
        world.SetWorldObjectPosition(30, 0, target);

        Assert.False(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOutOfRangeOfACharacterCanNotBeHurtByAttacks()
    {
        var instigator = new RangedCharacter(world);
        var target = new RangedCharacter(world, health: 900);

        world.SetWorldObjectPosition(0, 0, instigator);
        world.SetWorldObjectPosition(30, 0, target);

        instigator.TakeAction(Actions.Attack, target);

        Assert.Equal(900, target.Health);
    }

    [Fact]
    public void CharactersOutOfRangeCanNotBeHealed()
    {
        var instigator = new MeleeCharacter(world);
        var characterStartingWith600Health = new RangedCharacter(world, health: 600);

        world.SetWorldObjectPosition(0, 0, instigator);
        world.SetWorldObjectPosition(3, 0, characterStartingWith600Health);
        
        instigator.TakeAction(Actions.Heal, characterStartingWith600Health);

        Assert.Equal(600, characterStartingWith600Health.Health);
    }

    [Fact]
    public void RangedCharacterCanOccupyASpaceFiveSpacesToTheRightOfWhereTheyStartByMovingRight()
    {
        var character = new RangedCharacter(world);
        world.SetWorldObjectPosition(0, 0, character);
        
        character.TakeAction(Actions.MoveRight);

        Assert.True(world.SpaceOccupiedBy(5,0) == character);
        Assert.True(world.SpaceOccupiedBy(0,0) is EmptySpace);
        
    }

    [Fact]
    public void RangedCharacterCanOccupyASpaceFiveSpacesToTheLeftOfWhereTheyStartByMovingLeft()
    {
        var character = new RangedCharacter(world);
        world.SetWorldObjectPosition(5, 0, character);
        
        character.TakeAction(Actions.MoveLeft);

        Assert.True(world.SpaceOccupiedBy(0,0) == character);
        Assert.True(world.SpaceOccupiedBy(5,0) is EmptySpace);
    }

    [Fact]
    public void MeleeCharacterCanOccupyASpaceEightSpacesAboveStartingPositionByMovingUp()
    {
        var character = new MeleeCharacter(world);
        world.SetWorldObjectPosition(0, 0, character);

        character.TakeAction(Actions.MoveUp);

        Assert.True(world.SpaceOccupiedBy(0,8) == character);
        Assert.True(world.SpaceOccupiedBy(0,0) is EmptySpace);
    }

    [Fact]
    public void RangedCharacterCanOccupyASpaceFiveSpacesBelowStartingPositionByMovingDown()
    {
        var character = new RangedCharacter(world);
        world.SetWorldObjectPosition(0, 10, character);

        character.TakeAction(Actions.MoveDown);

        Assert.True(world.SpaceOccupiedBy(0,5) == character);
        Assert.True(world.SpaceOccupiedBy(0,10) is EmptySpace);
    }

    [Fact]
    public void CharactersCanNotMoveIntoTheSameSpaceThatAnotherCharacterIsOccupying()
    {
        var runner = new MeleeCharacter(world);
        var stander = new RangedCharacter(world);

        world.SetWorldObjectPosition(0, 8, runner);
        world.SetWorldObjectPosition(0, 0, stander);

        runner.TakeAction(Actions.MoveDown);
        
        Assert.False(world.SpaceOccupiedBy(0,0) == runner);
        Assert.True(world.SpaceOccupiedBy(0,0) == stander);
        
    }

    [Fact]
    public void CharactersMoveToTheSpaceBeforeObjectsThatStopTheirFullMovement()
    {
        var runner = new RangedCharacter(world);
        var tree = new Tree();

        world.SetWorldObjectPosition(5, 0, tree);
        world.SetWorldObjectPosition(0, 0, runner);

        runner.TakeAction(Actions.MoveRight);

        Assert.True(world.SpaceOccupiedBy(4,0) == runner);
        Assert.True(world.SpaceOccupiedBy(0,0) is EmptySpace);
        
      
    }

    [Fact]
    public void CharactersMoveUpToTheSpaceBeforeAnObjectInThePathOfTheCharacter()
    {
        var runner = new MeleeCharacter(world);
        var tree = new Tree();

        world.SetWorldObjectPosition(0, 4, tree);
        world.SetWorldObjectPosition(0, 0, runner);

        runner.TakeAction(Actions.MoveUp);

        Assert.True(world.SpaceOccupiedBy(0, 3) == runner);
        Assert.True(world.SpaceOccupiedBy(0, 0) is EmptySpace);
        Assert.True(world.SpaceOccupiedBy(0, 8) is EmptySpace);
    }

    [Theory]
    [InlineData(0, 0, Actions.MoveLeft, 0, 0)]
    [InlineData(1, 0, Actions.MoveLeft, 0, 0)]
    [InlineData(4, 0, Actions.MoveLeft, 0, 0)]
    [InlineData(0, 3, Actions.MoveDown, 0, 0)]
    [InlineData(0, 32, Actions.MoveUp, 0, 34)]
    [InlineData(30, 5, Actions.MoveRight, 34, 5)]
    public void CharacterTryingToMovePastTheEdgeOfTheWorldIsStoppedAtTheEdge(int startingX, int startingY, Actions moveAction, int finalX, int finalY)
    {
        var runner = new RangedCharacter(world);

        world.SetWorldObjectPosition(startingX, startingY, runner);
        runner.TakeAction(moveAction);

        Assert.True(world.SpaceOccupiedBy(finalX, finalY) == runner);
        
    }
        
}