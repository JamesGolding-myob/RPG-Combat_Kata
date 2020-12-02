using System;
using System.Collections.Generic;
namespace RPG.Combat.Kata
{
    public class World
    {
        private int _width;
        public int EdgeMaximum{get => _width -1;}
        public int EdgeMinimum{get => 0;}
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


        public void SetWorldObjectPosition(int xCoordinate, int yCooordinate, IHaveHealth gameObject)
        {
            map[xCoordinate, yCooordinate].OccupiedBy = gameObject;
        }
        
        private void ResetWorldSpace(int xCoordinate, int yCooordinate)
        {
            map[xCoordinate, yCooordinate].OccupiedBy = new EmptySpace();
        }

        internal void InitialMapSetup()
        {
            for(int i = 0; i < _width; i++)
            {
                for(int j = 0; j < _width; j++)
                {
                    map[i,j] = new Space(new EmptySpace());
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

        public void MoveToNextFreeSpace(Direction direction, int xToMoveTo, int yToMoveTo, IHaveHealth person)
        {
            int previousSpot;
            switch (direction)
            {
                case Direction.Right:
                {      
                    previousSpot = Math.Clamp(xToMoveTo -1, EdgeMinimum, EdgeMaximum);

                    SetWorldObjectPosition(xToMoveTo, yToMoveTo, person);
                    ResetWorldSpace(previousSpot, yToMoveTo);
                    break;
                }
                case Direction.Left:
                {
                    previousSpot = Math.Clamp(xToMoveTo + 1, EdgeMinimum, EdgeMaximum);
                   
                    SetWorldObjectPosition(xToMoveTo, yToMoveTo, person);
                    ResetWorldSpace(previousSpot, yToMoveTo);
                     
                    break;
                }
                case Direction.Up:
                {
                    previousSpot = Math.Clamp(yToMoveTo - 1, EdgeMinimum, EdgeMaximum);

                    SetWorldObjectPosition(xToMoveTo, yToMoveTo, person);
                    ResetWorldSpace(xToMoveTo, previousSpot);
                    break;
                }
                case Direction.Down:
                {
                    previousSpot = Math.Clamp(yToMoveTo + 1, EdgeMinimum, EdgeMaximum);
                
                    SetWorldObjectPosition(xToMoveTo, yToMoveTo, person);
                    ResetWorldSpace(xToMoveTo, previousSpot);
                    break;
                }
    
            }
            
        }

        public List<IHaveHealth> GetPotentialTargetsForCharacter(Character character)
        {
            List<IHaveHealth> potentialTargets = new List<IHaveHealth>();
            var currentCharacterPosition = GetLocationOf(character);

            if(currentCharacterPosition.Item1 > EdgeMinimum)
            {
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 - 1, currentCharacterPosition.Item2 ));
            }
            if(currentCharacterPosition.Item1 < EdgeMaximum)
            {
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 + 1, currentCharacterPosition.Item2 ));
            }
            if(currentCharacterPosition.Item2 < EdgeMaximum)
            {
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 , currentCharacterPosition.Item2 + 1));
            }
            if(currentCharacterPosition.Item2 > EdgeMinimum)
            { 
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 , currentCharacterPosition.Item2 - 1));
            }

            if(potentialTargets.Count < 4)
            {
                while(potentialTargets.Count < 4)
                {
                    potentialTargets.Add(new EmptySpace());
                }
            }

            return potentialTargets;

        }
    }
}