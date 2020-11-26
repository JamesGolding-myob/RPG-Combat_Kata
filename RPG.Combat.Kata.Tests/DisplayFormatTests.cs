using Xunit;

namespace RPG.Combat.Kata
{
    public class DisplayFormatTests
    {
        
        DisplayFormater displayFormater = new DisplayFormater();

        [Theory]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(20)]
        public void EmptyMapOfAnySizeIsConvertedToAGridOfEmptySpaces(int mapSize)
        {
            World map = new World(mapSize);

            string expectedEmptyMap = ExpectedMapSizeTestHelper(mapSize);

            Assert.Equal(expectedEmptyMap, displayFormater.FormatMap(map));
        }

        [Fact]
        public void CharacterGetsFormatedOntoMapAtBottomLeftCorner()
        {
            World mapWithCharacter = new World(3);
            var character = new Character(mapWithCharacter);
            mapWithCharacter.SetWorldObjectPosition(0, 0, character);

            string expectedFormat = CharacterAtOriginMap();

            Assert.Equal(expectedFormat, displayFormater.FormatMap(mapWithCharacter));
        }

        [Fact]
        public void CharacterGetsFormatedOntoMapAtThirdColumnSecondRowOn3x3Map()
        {
             World mapWithCharacter = new World(3);
            var character = new Character(mapWithCharacter);
            mapWithCharacter.SetWorldObjectPosition(2, 1, character);

            string expectedFormat = CharacterAtThirdColuumnSecondRowMap();

            Assert.Equal(expectedFormat, displayFormater.FormatMap(mapWithCharacter));
        }

        

        internal string CharacterAtThirdColuumnSecondRowMap()
        {
            return " " + " " + " \n" +
                   " " + " " + "🧙\n" +
                   " " + " " + " \n" ;
        }

        internal string CharacterAtOriginMap()
        {
            return " " + " " + " \n" +
                   " " + " " + " \n" +
                   "🧙" + " " + " \n" ;
        }

        internal string ExpectedMapSizeTestHelper(int mapSize)
        {
            string result;
            if(mapSize == 5)
            {
                result = " " + " " + " " + " " + " \n" +
                        " " + " " + " " + " " + " \n" +
                        " " + " " + " " + " " + " \n" +
                        " " + " " + " " + " " + " \n" +
                        " " + " " + " " + " " + " \n" ;
            }
            else if(mapSize >5 && mapSize < 20 )
            {
                result = " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " \n" ; 
            }
            else
            {
                result = " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" +
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" +
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" + 
                        " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " \n" ;
            }

            return result;
        }
    }
}