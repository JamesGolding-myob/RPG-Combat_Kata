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

        public IHaveHealth SpaceOccupiedBy(int potentialXCoordinate, int potentialYCoordinate)
        {
            var xPosition = MakeCoordinateInsideWorld(potentialXCoordinate);
            var yPosition = MakeCoordinateInsideWorld(potentialYCoordinate);

           return map[xPosition, yPosition].OccupiedBy;
        }
        private int MakeCoordinateInsideWorld(int point)
        {
            return Math.Clamp(point, EdgeMinimum, EdgeMaximum);
        }

        public void SetWorldObjectPosition(int xCoordinate, int yCoordinate, IHaveHealth gameObject)
        {
            xCoordinate = MakeCoordinateInsideWorld(xCoordinate);
            yCoordinate = MakeCoordinateInsideWorld(yCoordinate);
            
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
            int counter = 1;

            if(YPositionNotOnTheTopEdgeOfTheMap(currentCharacterPosition.Item2))
            {
                while(IsValidMove(Direction.Up, counter,currentCharacterPosition, character.AttackRange))
                {
                    counter++;
                }
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 , currentCharacterPosition.Item2 + counter));
            }
            else
            {
                potentialTargets.Add(new EmptySpace());
            }

            if(XPositionInsideRightEdgeOfTheMap(currentCharacterPosition.Item1))
            {
                while(IsValidMove(Direction.Right, counter,currentCharacterPosition, character.AttackRange))
                {
                    counter++;
                }

                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 + counter, currentCharacterPosition.Item2 ));
            }
            else
            {
                potentialTargets.Add(new EmptySpace());
            }     
            
            if(YPositionInsideBottomEdgeOfTheMap(currentCharacterPosition.Item2))
            { 
                while(IsValidMove(Direction.Down, counter,currentCharacterPosition, character.AttackRange))
                {
                    counter++;
                }
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 , currentCharacterPosition.Item2 - counter));
            }
            else
            {
                potentialTargets.Add(new EmptySpace());
            } 

            if(XPositionInsideLeftEdgeOfTheMap(currentCharacterPosition.Item1))
            {
                while(IsValidMove(Direction.Down, counter,currentCharacterPosition, character.AttackRange))
                {
                    counter++;
                }
                potentialTargets.Add(SpaceOccupiedBy(currentCharacterPosition.Item1 - counter, currentCharacterPosition.Item2 ));
            }
            else
            {
                potentialTargets.Add(new EmptySpace());
            } 

           
            return potentialTargets;

        }

        private bool YPositionNotOnTheTopEdgeOfTheMap(int currentYPos)
        {
            return currentYPos < EdgeMaximum;
        }

        private bool XPositionInsideRightEdgeOfTheMap(int currentXPos)
        {
            return currentXPos < EdgeMaximum;
        }

        private bool YPositionInsideBottomEdgeOfTheMap(int currentYPos)
        {
            return currentYPos > EdgeMinimum;
        }

        private bool XPositionInsideLeftEdgeOfTheMap(int currentXPos)
        {
            return currentXPos > EdgeMinimum;
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

        public bool IsValidMove(Direction direction, int counter, Tuple<int, int> currentPosition, int maxDistance)
            {
                
                var isValid = false;

               if(direction == Direction.Down)
               {
                    if(currentPosition.Item2 - counter > EdgeMinimum && SpaceIsEmptySpace(currentPosition.Item1, currentPosition.Item2 - counter) && counter < maxDistance)
                    {
                        isValid = true;
                    }
               }
               else if(direction == Direction.Up)
               {
                   if(currentPosition.Item2 + counter < EdgeMaximum && SpaceIsEmptySpace(currentPosition.Item1, currentPosition.Item2 + counter)  && counter < maxDistance)
                    {
                        isValid = true;
                    }
               }
               else if(direction == Direction.Left)
               {
                   if(currentPosition.Item1 - counter > EdgeMinimum && SpaceIsEmptySpace(currentPosition.Item1 - counter, currentPosition.Item2) && counter < maxDistance)
                   {
                       isValid = true;
                   }
               }
               else
               {
                   if(currentPosition.Item1 + counter < EdgeMaximum && SpaceIsEmptySpace(currentPosition.Item1 + counter, currentPosition.Item2) && counter < maxDistance)
                   {
                       isValid = true;
                   }
               }  
                return isValid;
            }

            public bool NextPositionIsAvailable(IMove characterTryingToMove, int xPosition, int yPosition)
            {
                return SpaceIsEmptySpace(xPosition, yPosition) || SpaceOccupiedBy(xPosition, yPosition) == characterTryingToMove; 
            }

            private bool SpaceIsEmptySpace(int xPosition, int yPosition)
            {
                return SpaceOccupiedBy(xPosition, yPosition) is EmptySpace;
            }

            public void UpdateCharacterPositionInWorld(IHaveHealth mover, Tuple<int, int> startingPosition, int positionXAdjustment, int positionYAdjustment)
            {
                ResetWorldSpace(startingPosition.Item1, startingPosition.Item2);
                SetWorldObjectPosition(startingPosition.Item1 + positionXAdjustment, startingPosition.Item2 + positionYAdjustment, mover);
                    
            }
    }
}