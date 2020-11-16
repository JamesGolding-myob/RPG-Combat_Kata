using Xunit;
namespace RPG.Combat.Kata.Tests
{
    public class FactionsTests
    {
        [Fact]
        public void CharactersAreInitiallyUnaligned()
        {
            var character = new Character();

            Assert.Equal(Factions.Unaligned, character.Faction[0]);
        }

        [Fact]
        public void CharactersCanJoinFactions()
        {
            var character = new Character();

            character.JoinFaction(Factions.Eagles);
            Assert.Equal(Factions.Eagles, character.Faction[0]);
            Assert.Equal(1, character.Faction.Count);
        }

        [Fact]
        public void CharactersCanJoinMultipleFactions()
        {
            var character = new Character();

            character.JoinFaction(Factions.Eagles);
            character.JoinFaction(Factions.Paladins);   

            Assert.Equal(2, character.Faction.Count);
        }

        [Fact]
        public void CharactersCanLeaveFactionsThryPreviouslyJoined()
        {
            var character = new Character();

            character.JoinFaction(Factions.Eagles);
            character.JoinFaction(Factions.Paladins);   

            character.LeaveFaction(Factions.Eagles);

            Assert.Equal(1, character.Faction.Count);
        }

        [Fact]
        public void IfCharactersLeaveTheirLAstFactionTheyBecomeUnaligned()
        {
            var character = new Character();

            character.JoinFaction(Factions.Monsters);
            character.LeaveFaction(Factions.Monsters);

            Assert.Equal(Factions.Unaligned, character.Faction[0]);
            Assert.Equal(1, character.Faction.Count);
        }

        [Fact]
        public void CharactersOfTheSameFactionCaNotDealDamageToEachOther()
        {
            var eagleCharacter = new MeleeCharacter();
            var eagleTargetCharacter = new MeleeCharacter(health: 1000);

            CharacterFactionInitiation(eagleCharacter, eagleTargetCharacter, Factions.Eagles);
             eagleCharacter.TakeAction(ActionType.Attack, eagleTargetCharacter, true);
            
            Assert.Equal(1000, eagleTargetCharacter.Health);
            
        }

        [Fact]
        public void CharactersOfTheSameFactionCanHealEachOther()
        {
            var monsterCharacter = new RangedCharacter();
            var hurtMonsterCharacter = new MeleeCharacter(health: 500);

            CharacterFactionInitiation(monsterCharacter, hurtMonsterCharacter, Factions.Monsters);
            monsterCharacter.TakeAction(ActionType.Heal, hurtMonsterCharacter, true);

            Assert.Equal(600, hurtMonsterCharacter.Health);
        }

        public void CharacterFactionInitiation(Character char1, Character char2, Factions faction)
        {
            char1.JoinFaction(faction);
            char2.JoinFaction(faction);
        }
    }
}