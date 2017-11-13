using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// controller for the MVC pattern in the application
    /// </summary>
    public class Controller
    {
        #region FIELDS

        private ConsoleView _gameConsoleView;
        private Traveler _gameTraveler;
        private Ship _gameShip;
        private bool _playingGame;
        private Location _currentLocation;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            //
            // setup all of the objects in the game
            //
            InitializeGame();

            //
            // begins running the application UI
            //
            ManageGameLoop();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize the major game objects
        /// </summary>
        private void InitializeGame()
        {
            _gameTraveler = new Traveler();
            _gameShip = new Ship();
            _gameConsoleView = new ConsoleView(_gameTraveler, _gameShip);
            _playingGame = true;

            
            //add items to travelers invy
            //
            _gameTraveler.Inventory.Add(_gameShip.GetGameObjectById(8) as TravelerObject);
            _gameTraveler.Inventory.Add(_gameShip.GetGameObjectById(9) as TravelerObject);


            Console.CursorVisible = false;
        }

        /// <summary>
        /// method to manage the application setup and game loop
        /// </summary>
        private void ManageGameLoop()
        {
            TravelerAction travelerActionChoice = TravelerAction.None;

            //
            // display splash screen
            //
            _playingGame = _gameConsoleView.DisplaySpashScreen();

            //
            // player chooses to quit
            //
            if (!_playingGame)
            {
                Environment.Exit(1);
            }

            //
            // display introductory message
            //
            _gameConsoleView.DisplayGamePlayScreen("Mission Intro", Text.MissionIntro(), ActionMenu.MissionIntro, "");
            _gameConsoleView.GetContinueKey();

            //
            // initialize the mission traveler
            // 
            InitializeMission();

            //
            // prepare game play screen
            //
            _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrrentLocationInfo(), ActionMenu.MainMenu, "");
            _currentLocation = _gameShip.GetLocationByID(_gameTraveler.LocationID);

            //
            // game loop
            //
            while (_playingGame)
            {
                //
                //process flags, events, stats
                //
                UpdateGameStatus();

                //
                //get action choice
                //

                travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu);

                //
                // get next game action from player
                //
                if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.MainMenu)
                {
                    travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu);
                }
                else if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.AdminMenu)
                {
                    travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.AdminMenu);
                }

                //
                // choose an action based on the user's menu choice
                //
                switch (travelerActionChoice)
                {
                    case TravelerAction.None:
                        break;

                    case TravelerAction.TravelerInfo:
                        _gameConsoleView.DisplayTravelerInfo();
                        break;

                    case TravelerAction.ListLocations:
                        _gameConsoleView.DisplayListOfLocations();
                        break;

                    case TravelerAction.Travel:

                        //
                        // new location choice and update current location
                        //
                        _gameTraveler.LocationID = _gameConsoleView.DisplayGetNextLocation();
                        _currentLocation = _gameShip.GetLocationByID(_gameTraveler.LocationID);

                        //
                        // set screen to current location format 
                        //
                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

                        break;

                    case TravelerAction.TravelerLocationsVisited:
                        _gameConsoleView.DisplayLocationsVisited();
                        break;

                    case TravelerAction.LookAt:
                        LookAtAction();
                        break;

                    case TravelerAction.LookAround:
                        _gameConsoleView.DisplayLookAround();
                        break;

                    case TravelerAction.ListGameObjects:
                        _gameConsoleView.DisplayListOfAllGameObjects();
                        break;

                    case TravelerAction.Inventory:
                        _gameConsoleView.DisplayInventory();
                        break;

                    case TravelerAction.PickUp:
                        PickUpAction();
                        break;

                    case TravelerAction.PutDown:
                        PutDownAction();
                        break;

                    case TravelerAction.AdminMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                        _gameConsoleView.DisplayGamePlayScreen("Admin Menu", "Select an Opertation from the menu.", ActionMenu.AdminMenu, "");

                        break;

                    case TravelerAction.ReturnToMainMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        break;

                    case TravelerAction.Exit:
                        _playingGame = false;
                        break;

                    default:
                        break;
                }
            }

            //
            // close the application
            //
            Environment.Exit(1);
        }

        /// <summary>
        /// initialize the player info
        /// </summary>
        private void InitializeMission()
        {
            Traveler traveler = _gameConsoleView.GetInitialTravelerInfo();

            _gameTraveler.Name = traveler.Name;
            _gameTraveler.Age = traveler.Age;
            _gameTraveler.Ethnicity = traveler.Ethnicity;
            _gameTraveler.HomeLocation = traveler.HomeLocation;

            _gameTraveler.Experiencepoints = 0;
            _gameTraveler.Health = 100;
            _gameTraveler.Lives = 3;
        }

        private void UpdateGameStatus()
        {
            if (!_gameTraveler.HasVisited(_currentLocation.LocationID))
            {
                //
                // new location to list of visited if first visit
                //
                _gameTraveler.LocationsVisited.Add(_currentLocation.LocationID);

                //
                //update Experience Points
                //
                _gameTraveler.Experiencepoints += _currentLocation.ExperiencePoints;
            }

            if (_gameTraveler.LocationID == 2)
            {
                _gameTraveler.Health += 50;
            }

            if (_gameTraveler.Health <= 0)
            {
                _gameTraveler.Lives -= 1;
            }

            if (_gameTraveler.Health > 100)
            {
                _gameTraveler.Lives += 1;
                _gameTraveler.Health = 100;
            }
        }

        public void LookAtAction()
        {
            //
            //display list of  traveler objects space-time location -- get a player choice
            //

            int gameObjetToLookAtId = _gameConsoleView.DisplayGetGameObjectToLookAt();

            //
            //game object info
            //

            if (gameObjetToLookAtId != 0)
            {
                //
                //get object form universe
                //
                
                GameObject gameObject = _gameShip.GetGameObjectById(gameObjetToLookAtId);

                //
                //display information for object
                //
                _gameConsoleView.DisplayGameObjectInfo(gameObject);
            }
        }

        private void PickUpAction()
        {
            //
            //Display list of traveler objects in location
            //
            int travelerObjectToPickUpId = _gameConsoleView.DisplayGetTravlerObjectToPickUp();

            //
            //add to inventory
            //
            if (travelerObjectToPickUpId != 0)
            {
                //
                //get game object from universe
                //
                TravelerObject travelerObject = _gameShip.GetGameObjectById(travelerObjectToPickUpId) as TravelerObject;

                //
                //set object location to 0
                //
                _gameTraveler.Inventory.Add(travelerObject);
                travelerObject.LocationId = 0;

                //
                //confirm
                //
                _gameConsoleView.DisplayConfirmTravelerObjectAddedToInventory(travelerObject);

            }
        }

        private void PutDownAction()
        {
            //
            //display list of objects in inventory 
            //
            int inventoryObjectToPutDownId = _gameConsoleView.DisplayGetInventoryObjectToPutDown();

            //
            //get object from universe
            //
            TravelerObject travelerObject = _gameShip.GetGameObjectById(inventoryObjectToPutDownId) as TravelerObject;

            //
            //remove from inventory
            //
            _gameTraveler.Inventory.Remove(travelerObject);
            travelerObject.LocationId = _gameTraveler.LocationID;

            //
            //confirmation
            //
            _gameConsoleView.DisplayConfirmTravelerObjectAddedToInventory(travelerObject);
        }
        #endregion
    }
}
