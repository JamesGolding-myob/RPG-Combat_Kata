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
        
        instigator.SetCharacterPosition(0);
        target.SetCharacterPosition(3);

        Assert.False(world.CharacterIsInRange(instigator, target));
        
    }

    [Fact]
    public void Characters2mAwayAreInRangeOfMeleeCharacters()
    {
        var instigator = new MeleeCharacter(world);
        var target = new MeleeCharacter(world);

        instigator.SetCharacterPosition(1);
        target.SetCharacterPosition(3);

        Assert.True(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOver20mAwayAreOutOfRangeOfRangedCharacters()
    {
        var instigator = new RangedCharacter(world);
        var target = new MeleeCharacter(world);
        
        instigator.SetCharacterPosition(9);
        target.SetCharacterPosition(30);

        Assert.False(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOutOfRangeOfACharacterCanNotBeHurtByAttacks()
    {
        var instigator = new RangedCharacter(world);
        var target = new RangedCharacter(world, health: 900);

        instigator.SetCharacterPosition(0);
        target.SetCharacterPosition(30);

        instigator.TakeAction(ActionType.Attack, target, world);

        Assert.Equal(900, target.Health);
    }

    [Fact]
    public void CharactersOutOfRangeCanNotBeHealed()
    {
        var instigator = new MeleeCharacter(world);
        var characterStartingWith600Health = new RangedCharacter(world, health: 600);

        instigator.SetCharacterPosition(0);
        characterStartingWith600Health.SetCharacterPosition(3);
        instigator.TakeAction(ActionType.Heal, characterStartingWith600Health, world);

        Assert.Equal(600, characterStartingWith600Health.Health);
    }

    [Fact]
    public void CharactersCanTakeAnActionToMoveInsideTheWorldWithDefaultSpeedOf5()
    {
        var runner = new Character(world);

        runner.SetCharacterPosition(0);

        runner.TakeAction(ActionType.Move, runner, world);

        Assert.Equal(5, runner.XCoordinate);
    }

    // [Fact]
    // public void CharactersCanNotMoveIntoSpaceOccupiedByTheAnotherCharacter()
    // {
        
    // }
        
}