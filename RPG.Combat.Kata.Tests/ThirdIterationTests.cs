using Xunit;
using RPG.Combat.Kata;

public class ThirdIterationTests
{
    [Fact]
    public void Characters3AwayAreOutOfRangeOfMeleeCharacters()
    {
        
        var instigator = new MeleeCharacter();
        var target = new MeleeCharacter();
        var world = new World(10, instigator, target);

        instigator.SetPosition(0);
        target.SetPosition(3);

        Assert.False(world.CharacterIsInRange(instigator, target));
        
    }

    [Fact]
    public void Characters2mAwayAreInRangeOfMeleeCharacters()
    {
        var instigator = new MeleeCharacter();
        var target = new MeleeCharacter();
        var world = new World(20, instigator, target);

        instigator.SetPosition(5);
        target.SetPosition(3);

        Assert.True(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOver20mAwayAreOutOfRangeOfRangedCharacters()
    {
        var instigator = new RangedCharacter();
        var target = new MeleeCharacter();
        var world = new World(30, instigator, target);

        instigator.SetPosition(9);
        target.SetPosition(30);

        Assert.False(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOutOfRangeOfACharacterCanNotBeHurtByAttacks()
    {
        var instigator = new RangedCharacter();
        var target = new RangedCharacter(health: 1000);
        var world = new World(30, instigator, target);

        instigator.SetPosition(9);
        target.SetPosition(30);

        instigator.TakeAction(ActionType.Attack, target, world.CharacterIsInRange(instigator, target));

        Assert.Equal(1000, target.Health);
    }
        
}