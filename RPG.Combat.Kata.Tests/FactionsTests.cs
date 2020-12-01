using Xunit;
namespace RPG.Combat.Kata.Tests
{
    public class FactionsTests
    {
        World world = new World(30);

        [Fact]
        public void CharactersAreInitiallyUnaligned()
        {
            var character = new Character(world);

            Assert.Equal(Factions.Unaligned, character.Faction[0]);
        }

        [Fact]
        public void CharactersCanJoinFactions()
        {
            var character = new Character(world);

            character.JoinFaction(Factions.Eagles);

            Assert.Equal(Factions.Eagles, character.Faction[0]);
            Assert.True(character.Faction.Count > 0);
        }

        [Fact]
        public void CharactersCanJoinMultipleFactions()
        {
            var character = new Character(world);

            character.JoinFaction(Factions.Eagles);
            character.JoinFaction(Factions.Paladins);   

            Assert.True(character.Faction.Count == 2);
        }

        [Fact]
        public void CharactersCanLeaveFactionsTheyPreviouslyJoined()
        {
            var character = new Character(world);

            character.JoinFaction(Factions.Eagles);
            character.JoinFaction(Factions.Paladins);   

            character.LeaveFaction(Factions.Eagles);

            Assert.Equal(1, character.Faction.Count);
        }

        [Fact]
        public void IfCharactersLeaveTheirLastFactionTheyBecomeUnaligned()
        {
            var character = new Character(world);

            character.JoinFaction(Factions.Monsters);
            character.LeaveFaction(Factions.Monsters);

            Assert.Equal(Factions.Unaligned, character.Faction[0]);
            Assert.Equal(1, character.Faction.Count);
        }

        [Fact]
        public void CharactersOfTheSameFactionCanNotDealDamageToEachOther()
        {
            var eagleCharacter = new MeleeCharacter(world);
            var eagleTargetCharacter = new MeleeCharacter(world, health: 1000);

            CharacterJoinFactionHelper(eagleCharacter, eagleTargetCharacter, Factions.Eagles);
            eagleCharacter.TakeAction(Actions.Attack, eagleTargetCharacter);
            
            Assert.Equal(1000, eagleTargetCharacter.Health);
            
        }

        [Fact]
        public void CharactersOfTheSameFactionCanHealEachOther()
        {
            var monsterCharacter = new RangedCharacter(world);
            var hurtMonsterCharacter = new MeleeCharacter(world, health: 500);

            CharacterJoinFactionHelper(monsterCharacter, hurtMonsterCharacter, Factions.Monsters);
            monsterCharacter.TakeAction(Actions.Heal, hurtMonsterCharacter);

            Assert.Equal(600, hurtMonsterCharacter.Health);
        }

        [Fact]
        public void UnalignedCharactersAreConsideredToNotBelongToAFactionSoCanAttackEachOther()
        {
            var instigator = new MeleeCharacter(world);
            var target = new RangedCharacter(world, health:1000);

            instigator.TakeAction(Actions.Attack, target);
            Assert.True(target.Health < 1000);
        }

        [Fact]
        public void UnalignedCharctersAreConsidredToHaveNoFactionSoCanNotHealEachOther()
        {
            var initialHealth = 100;

            var healthyIndividual = new RangedCharacter(world);
            var hurtIndividual = new MeleeCharacter(world, health: initialHealth);

            healthyIndividual.TakeAction(Actions.Heal, hurtIndividual);
            Assert.False(hurtIndividual.Health > initialHealth);
        }

        public void CharacterJoinFactionHelper(Character char1, Character char2, Factions faction)
        {
            char1.JoinFaction(faction);
            char2.JoinFaction(faction);
        }
    }
}