using System;
namespace RPG.Combat.Kata
{
    public class World
    {
        private int _width;
        public Space[,] map; 
        
        public World(int width)
        {
            _width = width;
           map = new Space[_width, _width];
           InitialMapSetup();
            
        }

        public bool CharacterIsInRange(Character instigator, IHaveHealth target)
        {    
            return (instigator.AttackRange >= GetDistanceBetweenCharacters(instigator, target));
        }

        private double GetDistanceBetweenCharacters(Character actioningCharacter, IHaveHealth targetCharacter)
        {
            var actioningCharacterX = GetLocationOf(actioningCharacter).Item1;
            var targetCharacterX = GetLocationOf(targetCharacter).Item1;
            
            return Math.Abs( actioningCharacterX - targetCharacterX );
        }

        public IHaveHealth SpaceOccupiedBy(int v1, int v2)
        {
           return map[v1,v2].OccupiedBy;
        }

        public void SetCharacterPosition(int xCoordinate, int yCooordinate, IHaveHealth thing)
        {
            map[xCoordinate, yCooordinate].OccupiedBy = thing;
        }
        
        public void ResetWorldSpace(int xCoordinate, int yCooordinate)
        {
            map[xCoordinate, yCooordinate].OccupiedBy = new Nothing();
        }

        public void InitialMapSetup()
        {
            for(int i = 0; i < _width; i++)
            {
                for(int j = 0; j < _width; j++)
                {
                    map[i,j] = new Space(new Nothing());
                }
            }
        }

        public Tuple<int, int> GetLocationOf(IHaveHealth worldObject)
        {
            Tuple<int, int> result = new Tuple<int, int>(0,0);

            for(int i = 0; i < _width; i++)
            {
                for(int j = 0; j < _width; j++)
                {
                    if (map[i,j].OccupiedBy == worldObject)
                    {
                        result = new Tuple<int, int>(i, j);
                         break;
                    }
                }
            }

            return result; 
        }

    }
}