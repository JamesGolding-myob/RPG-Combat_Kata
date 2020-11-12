using Xunit;
using RPG.Combat.Kata;

public class ThirdIterationTests
{
    [Fact]
    public void MeleeCharactersCanNotAttackACharacterMoreThan2mAway()
    {
        //new up two melee characters
        var instigator = new MeleeCharacter();
        var target = new MeleeCharacter(health: 900);
        var world = new World(10, instigator, target);

        instigator.SetPosition(0);
        target.SetPosition(3);

        Assert.False(world.CharacterIsInRange(instigator, target));
        //could assert if an attack is valid or not by adding a log?
        //assert chracter two helath doesn't change after attamepted attack
    }
}