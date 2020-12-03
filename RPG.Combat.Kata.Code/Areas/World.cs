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
            var xPosition = Math.Clamp(v1, EdgeMinimum, EdgeMaximum);
            var yPosition = Math.Clamp(v2, EdgeMinimum, EdgeMaximum);

           return map[xPosition, yPosition].OccupiedBy;
        }


        public void SetWorldObjectPosition(int xCoordinate, int yCoordinate, IHaveHealth gameObject)
        {
            xCoordinate = Math.Clamp(xCoordinate, EdgeMinimum, EdgeMaximum);
            yCoordinate = Math.Clamp(yCoordinate, EdgeMinimum, EdgeMaximum);
            
            map[xCoordinate, yCoordinate].OccupiedBy = gameObject;
        }
        
        public void ResetWorldSpace(int xCoordinate, int yCooordinate)
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

        public (int, int) GetCharacterPosition()
        {
           int xCoordinate = 0;
           int yCooordinate = 0;

            for(int row = 0; row <= EdgeMaximum; row++)
            {
                for(int column = 0; column <= EdgeMaximum; column++)
                {
                    var thing = SpaceOccupiedBy(column, row);
                    if(thing is MeleeCharacter || thing is RangedCharacter )
                    {
                        xCoordinate = column;
                        yCooordinate = row;
                        break;
                    }
                }
            }

            return (xCoordinate, yCooordinate);
        }

        public bool IsValidMove(Direction direction, int counter, Tuple<int, int> position, int characterSpeed)
            {
                var currentPosition = position;
                var canMoveToSpot = false;

               if(direction == Direction.Down)
               {
                    if(currentPosition.Item2 - counter > EdgeMinimum && SpaceOccupiedBy(currentPosition.Item1, currentPosition.Item2 - counter) is EmptySpace && counter < characterSpeed)
                    {
                        canMoveToSpot = true;
                    }

               }
               else if(direction == Direction.Up)
               {
                   if(currentPosition.Item2 + counter < EdgeMaximum && SpaceOccupiedBy(currentPosition.Item1, currentPosition.Item2 + counter) is EmptySpace && counter < characterSpeed)
                    {
                        canMoveToSpot = true;
                    }
               }
               else if(direction == Direction.Left)
               {
                   if(currentPosition.Item1 - counter > EdgeMinimum && SpaceOccupiedBy(currentPosition.Item1 - counter, currentPosition.Item2) is EmptySpace && counter < characterSpeed)
                   {
                       canMoveToSpot = true;
                   }
               }
               else
               {
                   if(currentPosition.Item1 + counter < EdgeMaximum && SpaceOccupiedBy(currentPosition.Item1 + counter, currentPosition.Item2) is EmptySpace && counter < characterSpeed)
                   {
                       canMoveToSpot = true;
                   }
               }  
                return canMoveToSpot;
            }
    }
}