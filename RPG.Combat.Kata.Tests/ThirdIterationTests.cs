using Xunit;
using RPG.Combat.Kata;

public class ThirdIterationTests
{
    [Fact]
    public void Characters3mAwayAreOutOfRangeOfMeleeCharacters()
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
        var target = new RangedCharacter(health: 900);
        var world = new World(30);

        instigator.SetPosition(9);
        target.SetPosition(30);

        instigator.TakeAction(ActionType.Attack, target, world.CharacterIsInRange(instigator, target));

        Assert.Equal(900, target.Health);
    }

    [Fact]
    public void CharactersOutOfRangeCanNotBeHealed()
    {
        var instigator = new MeleeCharacter();
        var characterStartingWith600Health = new RangedCharacter(health: 600);

        instigator.TakeAction(ActionType.Heal, characterStartingWith600Health, false);

        Assert.Equal(600, characterStartingWith600Health.Health);
    }

    [Fact]
    public void CharactersCanTakeAnActionToMoveInsideTheWorldWithDefaultSpeedOf5()
    {
        var runner = new Character();
        var world = new World(10);

        runner.SetPosition(0);

        runner.TakeAction(ActionType.Move, runner, world.IsCharacterNewPositionInWorld(runner));

        Assert.Equal(5, runner.XPosition);
    }

    // [Fact]
    // public void CharactersCanNotMoveIntoSpaceOccupiedByTheAnotherCharacter()
    // {
        
    // }
        
}