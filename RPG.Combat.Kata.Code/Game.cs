
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
            UI.DisplayToUser(DisplayConstants.characterSelectionQuestion);
            string input;
            do
            {
                input = UI.GetResponseFromUser();
            } while (!_inputValidator.IsValidCharacterChoice(input));        
                
            Character character = characterCreator.CreateCharacter(_inputConverter.ConvertCharacterChoice(input), _gameWorld);     
            
            Monster monster = new Monster(_gameWorld);
            SetGameObjectsInWorld(character, monster);
            PerformCombat(character, monster);
        }

        public void PerformCombat(Character character, Monster monster)
        {
            UI.DisplayToUser("you have been sent to kill the monster terrorising the country side");
            do
            {
                UI.DisplayToUser(_displayFormater.FormatMap(_gameWorld));
                UI.DisplayToUser("Choose an Action 1: Attack, 2: Heal, 3: Move Up, 4: Move Right, 5: Move Down, 6: Move Left");

                var potentialTargets = _gameWorld.GetPotentialTargetsForCharacter(character);
                var chosenAction = _inputConverter.ActionsConverter(UI.GetResponseFromUser());

                if(chosenAction == Actions.Attack  || chosenAction == Actions.Heal)
                {
                    UI.DisplayToUser($"Choose a Target: 1:\n{potentialTargets[0]}\n 2:{potentialTargets[1]}\n 3:{potentialTargets[2]}\n 4:{potentialTargets[3]}\n" );
                     
                    character.TakeAction(chosenAction, _inputConverter.ConvertTarget(UI.GetResponseFromUser(), potentialTargets));
                }
                else
                {
                    character.TakeAction(chosenAction);
                }

                
                UI.DisplayToUser(_displayFormater.ActionFeedback(chosenAction));
                    
                UI.DisplayToUser(_displayFormater.FormatMap(_gameWorld));
                if(!monster.IsAlive)
                {
                    UI.DisplayToUser("Congratulations you have killed the monster");
                    break;
                }
                monster.TakeTurn();
                UI.DisplayToUser("monster Moved");

            }while(character.IsAlive || monster.IsAlive);
              
        }

        private void SetGameObjectsInWorld(Character chosenCharacter, Monster monster)
        {
            
            _gameWorld.SetWorldObjectPosition(4, 4, monster);
                
            _gameWorld.SetWorldObjectPosition(0, 0, chosenCharacter);
        }
    }
}