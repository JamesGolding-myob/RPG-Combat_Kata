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
        
        world.SetCharacterPosition(0,0, instigator);
        world.SetCharacterPosition(3, 0, target);

        Assert.False(world.CharacterIsInRange(instigator, target));
        
    }

    [Fact]
    public void Characters2mAwayAreInRangeOfMeleeCharacters()
    {
        var instigator = new MeleeCharacter(world);
        var target = new MeleeCharacter(world);

        world.SetCharacterPosition(1, 0, instigator);
        world.SetCharacterPosition(3, 0, target);

        Assert.True(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOver20mAwayAreOutOfRangeOfRangedCharacters()
    {
        var instigator = new RangedCharacter(world);
        var target = new MeleeCharacter(world);
        
        world.SetCharacterPosition(9, 0, instigator);
        world.SetCharacterPosition(30, 0, target);

        Assert.False(world.CharacterIsInRange(instigator, target));
    }

    [Fact]
    public void CharactersOutOfRangeOfACharacterCanNotBeHurtByAttacks()
    {
        var instigator = new RangedCharacter(world);
        var target = new RangedCharacter(world, health: 900);

        world.SetCharacterPosition(0, 0, instigator);
        world.SetCharacterPosition(30, 0, target);

        instigator.TakeAction(ActionType.Attack, target);

        Assert.Equal(900, target.Health);
    }

    [Fact]
    public void CharactersOutOfRangeCanNotBeHealed()
    {
        var instigator = new MeleeCharacter(world);
        var characterStartingWith600Health = new RangedCharacter(world, health: 600);

        world.SetCharacterPosition(0, 0, instigator);
        world.SetCharacterPosition(3, 0, characterStartingWith600Health);
        
        instigator.TakeAction(ActionType.Heal, characterStartingWith600Health);

        Assert.Equal(600, characterStartingWith600Health.Health);
    }

    

    [Fact]
    public void CharacterCanOccupyASpaceFiveSpacesToTheRightOfWhereTheyStartByMoving()
    {
        
        var character = new RangedCharacter(world);
        world.SetCharacterPosition(0, 0, character);
        
        character.TakeAction(ActionType.Move, character);

        Assert.True(world.SpaceOccupiedBy(5,0) == character);
        Assert.True(world.SpaceOccupiedBy(0,0) is Nothing);
        
    }
        
}