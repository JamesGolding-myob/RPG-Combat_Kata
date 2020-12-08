
namespace RPG.Combat.Kata
{
    public class Game
    {
        private World _gameWorld;
        public IUI UI{get; private set;}
        private CharacterCreator characterCreator;
        private InputConverter _inputConverter;
        private DisplayFormater _displayFormater;
        private InputValidator _inputValidator;
        public Game(IUI ui, World world, CharacterCreator creator, InputConverter inputConverter, DisplayFormater displayFormater, InputValidator inputValidator)
        {
            UI = ui;
            _gameWorld = world;
            characterCreator = creator;
            _inputConverter = inputConverter;
            _displayFormater = displayFormater;
            _inputValidator = inputValidator;
        }

        public void Run()
        {
            string input;
            do
            {
                UI.DisplayToUser(DisplayConstants.characterSelectionQuestion);
                input = UI.GetResponseFromUser();

            } while (!_inputValidator.IsValidCharacterChoice(input));        
                
            Character character = characterCreator.CreateCharacter(_inputConverter.ConvertCharacterChoice(input), _gameWorld);     
            Monster monster = new Monster(_gameWorld);

            SetGameObjectsInWorld(character, monster);
            PerformCombat(character, monster);
        }

        public void PerformCombat(Character character, Monster monster)
        {
            UI.DisplayToUser(DisplayConstants.combatIntroduction);
            do
            {
                string actionChoice;
                UI.DisplayToUser(_displayFormater.FormatMap(_gameWorld));
                UI.DisplayToUser(DisplayConstants.actionChoices);

                    do
                    {
                        actionChoice = UI.GetResponseFromUser();

                    } while (_inputValidator.ActionChoiceIsInvalid(actionChoice));

                Actions chosenAction = _inputConverter.ActionsConverter(actionChoice);
                
                if(chosenAction == Actions.Attack  || chosenAction == Actions.Heal)
                {
                    var potentialTargets = _gameWorld.GetPotentialTargetsForCharacter(character);
                    string chosenTarget;
                    do
                    {
                        UI.DisplayToUser(_displayFormater.FormatTargets(potentialTargets));
                        chosenTarget = UI.GetResponseFromUser();
                    } while (_inputValidator.TargetChoiceIsInvalid(chosenTarget));

                    character.TakeAction(chosenAction, _inputConverter.ConvertTarget(chosenTarget, potentialTargets));
                }
                else
                {
                    character.TakeAction(chosenAction);
                }
  
                UI.DisplayToUser(_displayFormater.ActionFeedback(chosenAction));
                    
                UI.DisplayToUser(_displayFormater.FormatMap(_gameWorld));

                if(!monster.IsAlive)
                {
                    break;
                }

                monster.TakeTurn();
                UI.DisplayToUser(DisplayConstants.monsterMoved);


            }while(character.IsAlive && monster.IsAlive);
            
            if(!character.IsAlive)
            {
                UI.DisplayToUser(DisplayConstants.characterDeath); 
            }
            else if(!monster.IsAlive)
            {
                UI.DisplayToUser(DisplayConstants.killedMonster);
            }
              
        }

        private void SetGameObjectsInWorld(Character chosenCharacter, Monster monster)
        {  
            _gameWorld.SetWorldObjectPosition(4, 4, monster);      
            _gameWorld.SetWorldObjectPosition(0, 0, chosenCharacter);
        }
    }
}