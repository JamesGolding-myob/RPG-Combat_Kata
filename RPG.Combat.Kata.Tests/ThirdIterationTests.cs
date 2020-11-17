using Xunit;
using RPG.Combat.Kata;

public class ThirdIterationTests
{
    [Fact]
    public void Characters3AwayAreOutOfRangeOfMeleeCharacters()
    {
        
        var instigator = new MeleeCharacter();
        var target = new MeleeCharacter();
        var world = new World(10);

        instigator.SetPosition(0);
        target.SetPosition(3);

        Assert.False(world.CharacterIsInRange(instigator, target));    
    }

    [Fact]
    public void Characters2mAwayAreInRangeOfMeleeCharacters()
    {
        var instigator = new MeleeCharacter();
        var target = new MeleeCharacter();
        var world = new World(20);

        instigator.SetPosition(5);
        target.SetPosition(3);

        Assert.True(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOver20mAwayAreOutOfRangeOfRangedCharacters()
    {
        var instigator = new RangedCharacter();
        var target = new MeleeCharacter();
        var world = new World(30);

        instigator.SetPosition(9);
        target.SetPosition(30);

        Assert.False(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOutOfRangeOfACharacterCanNotBeHurtByAttacks()
    {
        var instigator = new RangedCharacter();
        var target = new RangedCharacter(health: 1000);
        var world = new World(30);

        instigator.SetPosition(9);
        target.SetPosition(30);

        instigator.TakeAction(ActionType.Attack, target, world.CharacterIsInRange(instigator, target));

        Assert.Equal(1000, target.Health);
    }

    [Fact]
    public void CharactersCanMoveInsideTheWorld()
    {
        var world = new World(10);
        var runner = new RangedCharacter();

        runner.SetPosition(0);
        runner.Move(world);
        
        Assert.Equal(6, runner.XPosition);
    }

    [Fact]
    public void CharactersCanNotMovePastTheEdgeOfTheWorld()
    {
        var world = new World(10);
        var runner = new MeleeCharacter();
        runner.SetPosition(8);

        runner.Move(world);

        Assert.Equal(10, runner.XPosition);
    }
        
}