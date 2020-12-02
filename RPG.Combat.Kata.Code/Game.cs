
namespace RPG.Combat.Kata
{
    public class Game
    {
        private World _gameWorld;
        public IUI UI{get; private set;}
        private CharacterCreator characterCreator;
        private InputConverter _converter;
        private DisplayFormater _displayFormater;
        public Game(IUI ui, World world, CharacterCreator creator, InputConverter inputConverter, DisplayFormater displayFormater)
        {
            UI = ui;
            _gameWorld = world;
            characterCreator = creator;
            _converter = inputConverter;
            _displayFormater = displayFormater;
        }

        public void Run()
        {
            UI.DisplayToUser("Welcome To RPG Adventure: What kind of Character would you like to be?\n" + "1: Melee\n" + "2: Ranged");
                    //pick character type
            string input = UI.GetResponseFromUser();
            
                // if(inputConverter.IsValidCharacterChoice(input))
                // {
                
            Character character = characterCreator.CreateCharacter(_converter.ConvertCharacterChoice(input), _gameWorld);     
           
            var monster = new Monster(_gameWorld);
            _gameWorld.SetWorldObjectPosition(4, 4, monster);
                
            _gameWorld.SetWorldObjectPosition(0, 0, character);
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
                var chosenAction = _converter.ActionsConverter(UI.GetResponseFromUser());

                if(chosenAction == Actions.Attack  || chosenAction == Actions.Heal)
                {
                    UI.DisplayToUser($"Choose a Target: 1:{potentialTargets[0]}\n 2:{potentialTargets[1]}\n 3:{potentialTargets[2]}\n 4:{potentialTargets[3]}\n" );
                     
                    character.TakeAction(chosenAction, _converter.ConvertTarget(UI.GetResponseFromUser(), potentialTargets));
                }
                else
                {
                    character.TakeAction(chosenAction);
                }

                UI.DisplayToUser("you ed");
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
    }
}