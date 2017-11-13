using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// view class
    /// </summary>
    public class ConsoleView
    {
        #region ENUMS

        private enum ViewStatus
        {
            TravelerInitialization,
            PlayingGame
        }

        #endregion


        #region FIELDS

        //
        // declare game objects for the ConsoleView object to use
        //
        Traveler _gameTraveler;
        Ship _gameShip;

        ViewStatus _viewStatus;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Traveler gameTraveler, Ship gameShip)
        {
            _gameTraveler = gameTraveler;
            _gameShip = gameShip;

            _viewStatus = ViewStatus.TravelerInitialization;

            InitializeDisplay();
        }

        #endregion

        #region METHODS
        /// <summary>
        /// display all of the elements on the game play screen on the console
        /// </summary>
        /// <param name="messageBoxHeaderText">message box header title</param>
        /// <param name="messageBoxText">message box text</param>
        /// <param name="menu">menu to use</param>
        /// <param name="inputBoxPrompt">input box text</param>
        public void DisplayGamePlayScreen(string messageBoxHeaderText, string messageBoxText, Menu menu, string inputBoxPrompt)
        {
            //
            // reset screen to default window colors
            //
            Console.BackgroundColor = ConsoleTheme.WindowBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.WindowForegroundColor;
            Console.Clear();

            ConsoleWindowHelper.DisplayHeader(Text.HeaderText);
            ConsoleWindowHelper.DisplayFooter(Text.FooterText);

            DisplayMessageBox(messageBoxHeaderText, messageBoxText);
            DisplayMenuBox(menu);
            DisplayInputBox();
        }

        /// <summary>
        /// wait for any keystroke to continue
        /// </summary>
        public void GetContinueKey()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// get a action menu choice from the user
        /// </summary>
        /// <returns>action menu choice</returns>
        public TravelerAction GetActionMenuChoice(Menu menu)
        {
            TravelerAction choosenAction = TravelerAction.None;
            Console.CursorVisible = false;

            //
            //array of vail keys for menu dictionary
            //
            char[] validKeys = menu.MenuChoices.Keys.ToArray();

            //
            // TODO validate menu choices
            //
            char keyPressed;
            do
            {
                ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
                keyPressed = keyPressedInfo.KeyChar;

            } while (!validKeys.Contains(keyPressed));
            
            choosenAction = menu.MenuChoices[keyPressed];
            Console.CursorVisible = true;

            return choosenAction;
        }

        /// <summary>
        /// get a string value from the user
        /// </summary>
        /// <returns>string value</returns>
        public string GetString()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        public bool GetInteger(string prompt, int minimumValue, int maximumValue, out int integerChoice)
        {
            bool validResponse = false;
            integerChoice = 0;

            bool validateRange = (minimumValue != 0 || maximumValue != 0);

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (int.TryParse(Console.ReadLine(), out integerChoice))
                {
                    if (integerChoice >= minimumValue && integerChoice <= maximumValue)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                        DisplayInputBoxPrompt(prompt);
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                    DisplayInputBoxPrompt(prompt);
                }
            }

            Console.CursorVisible = false;

            return true;
        }

        /// <summary>
        /// get a character race value from the user
        /// </summary>
        /// <returns>character race value</returns>
        public Character.EthnicityType GetEthnicity()
        {
            Character.EthnicityType ethnicityType;
            Enum.TryParse<Character.EthnicityType>(Console.ReadLine(), out ethnicityType);

            return ethnicityType;
        }

        /// <summary>
        /// display splash screen
        /// </summary>
        /// <returns>player chooses to play</returns>
        public bool DisplaySpashScreen()
        {
            bool playing = true;
            ConsoleKeyInfo keyPressed;

            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;


            Console.SetCursorPosition(0, 10);
            string tabSpace = new String(' ', 35);
            Console.WriteLine(tabSpace + @" _____ _                ______          _           _   ");
            Console.WriteLine(tabSpace + @"|_   _| |               | ___ \        (_)         | |  ");
            Console.WriteLine(tabSpace + @"  | | | |__   ___       | |_/ _ __ ___  _  ___  ___| |_ ");
            Console.WriteLine(tabSpace + @"  | | | '_ \ / _ \      |  __| '__/ _ \| |/ _ \/ __| __|");
            Console.WriteLine(tabSpace + @"  | | | | | |  __/      | |  | | | (_) | |  __| (__| |_ ");
            Console.WriteLine(tabSpace + @"  \_/ |_| |_|\___|      \_|  |_|  \___/| |\___|\___|\__|");
            Console.WriteLine(tabSpace + @"                                      _/ |              ");
            Console.WriteLine(tabSpace + @"                                     |__/             ");

            Console.SetCursorPosition(80, 25);
            Console.Write("Press any key to continue or Esc to exit.");
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                playing = false;
            }

            return playing;
        }

        /// <summary>
        /// initialize the console window settings
        /// </summary>
        private static void InitializeDisplay()
        {
            //
            // control the console window properties
            //
            ConsoleWindowControl.DisableResize();
            ConsoleWindowControl.DisableMaximize();
            ConsoleWindowControl.DisableMinimize();
            Console.Title = "The Titanic";

            //
            // set the default console window values
            //
            ConsoleWindowHelper.InitializeConsoleWindow();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// display the correct menu in the menu box of the game screen
        /// </summary>
        /// <param name="menu">menu for current game state</param>
        private void DisplayMenuBox(Menu menu)
        {
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuBorderColor;

            //
            // display menu box border
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MenuBoxPositionTop,
                ConsoleLayout.MenuBoxPositionLeft,
                ConsoleLayout.MenuBoxWidth,
                ConsoleLayout.MenuBoxHeight);

            //
            // display menu box header
            //
            Console.BackgroundColor = ConsoleTheme.MenuBorderColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(menu.MenuTitle, ConsoleLayout.MenuBoxWidth - 4));

            //
            // display menu choices
            //
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            int topRow = ConsoleLayout.MenuBoxPositionTop + 3;

            foreach (KeyValuePair<char, TravelerAction> menuChoice in menu.MenuChoices)
            {
                if (menuChoice.Value != TravelerAction.None)
                {
                    string formatedMenuChoice = ConsoleWindowHelper.ToLabelFormat(menuChoice.Value.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, topRow++);
                    Console.Write($"{menuChoice.Key}. {formatedMenuChoice}");
                }
            }
        }

        /// <summary>
        /// display the text in the message box of the game screen
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="messageText"></param>
        private void DisplayMessageBox(string headerText, string messageText)
        {
            //
            // display the outline for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxBorderColor;
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MessageBoxPositionTop,
                ConsoleLayout.MessageBoxPositionLeft,
                ConsoleLayout.MessageBoxWidth,
                ConsoleLayout.MessageBoxHeight);

            //
            // display message box header
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBorderColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(headerText, ConsoleLayout.MessageBoxWidth - 4));

            //
            // display the text for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            List<string> messageTextLines = new List<string>();
            messageTextLines = ConsoleWindowHelper.MessageBoxWordWrap(messageText, ConsoleLayout.MessageBoxWidth - 4);

            int startingRow = ConsoleLayout.MessageBoxPositionTop + 3;
            int endingRow = startingRow + messageTextLines.Count();
            int row = startingRow;
            foreach (string messageTextLine in messageTextLines)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, row);
                Console.Write(messageTextLine);
                row++;
            }

        }

        public void DisplayStatusBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            //
            // display the outline for the status box
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.StatusBoxPositionTop,
                ConsoleLayout.StatusBoxPositionLeft,
                ConsoleLayout.StatusBoxWidth,
                ConsoleLayout.StatusBoxHeight);

            //
            // display the text for the status box if playing game
            //
            if (_viewStatus == ViewStatus.PlayingGame)
            {
                //
                // display status box header with title
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("Game Stats", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

                //
                // display stats
                //
                int startingRow = ConsoleLayout.StatusBoxPositionTop + 3;
                int row = startingRow;
                foreach (string statusTextLine in Text.StatusBox(_gameTraveler, _gameShip))
                {
                    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, row);
                    Console.Write(statusTextLine);
                    row++;
                }
            }
            else
            {
                //
                // display status box header without header
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
            }
        }

        /// <summary>
        /// draw the input box on the game screen
        /// </summary>
        public void DisplayInputBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.InputBoxPositionTop,
                ConsoleLayout.InputBoxPositionLeft,
                ConsoleLayout.InputBoxWidth,
                ConsoleLayout.InputBoxHeight);
        }

        /// <summary>
        /// display the prompt in the input box of the game screen
        /// </summary>
        /// <param name="prompt"></param>
        public void DisplayInputBoxPrompt(string prompt)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 1);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.Write(prompt);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the error message in the input box of the game screen
        /// </summary>
        /// <param name="errorMessage">error message text</param>
        public void DisplayInputErrorMessage(string errorMessage)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write(errorMessage);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// clear the input box
        /// </summary>
        private void ClearInputBox()
        {
            string backgroundColorString = new String(' ', ConsoleLayout.InputBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.InputBoxBackgroundColor;
            for (int row = 1; row < ConsoleLayout.InputBoxHeight - 2; row++)
            {
                Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + row);
                DisplayInputBoxPrompt(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
        }

        

        /// <summary>
        /// get the player's initial information at the beginning of the game
        /// </summary>
        /// <returns>traveler object with all properties updated</returns>
        public Traveler GetInitialTravelerInfo()
        {
            Traveler traveler = new Traveler();

            //
            // intro
            //
            DisplayGamePlayScreen("Mission Initialization", Text.InitializeMissionIntro(), ActionMenu.MissionIntro, "");
            GetContinueKey();

            //
            // get traveler's name
            //
            DisplayGamePlayScreen("Mission Initialization - Name", Text.InitializeMissionGetTravelerName(), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt("Enter your name: ");
            traveler.Name = GetString();

            //
            // get traveler's age
            //
            DisplayGamePlayScreen("Mission Initialization - Age", Text.InitializeMissionGetTravelerAge(traveler), ActionMenu.MissionIntro, "");
            int gameTravelerAge;

            GetInteger($"Enter your age {traveler.Name}: ", 0, 1000000, out gameTravelerAge);
            traveler.Age = gameTravelerAge;

            //
            // get traveler's race
            //
            DisplayGamePlayScreen("Mission Initialization - Ethnicity", Text.InitializeMissionGetTravelerEthnicity(traveler), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt($"Enter your ethnicity {traveler.Name}: ");
            traveler.Ethnicity = GetEthnicity();

            //
            //get home location
            //
            DisplayGamePlayScreen("Mission-Initialization - Home Location", Text.InitializeMissionTravelerHomeLocation(traveler.Name), ActionMenu.MissionIntro,"");
            DisplayInputBoxPrompt($"Enter your home location:");
            traveler.HomeLocation = GetString();


            //
            // echo the traveler's info
            //
            DisplayGamePlayScreen("Mission Initialization - Complete", Text.InitializeMissionEchoTravelerInfo(traveler), ActionMenu.MissionIntro, "");
            GetContinueKey();

            return traveler;
        }

        #region ----- display responses to menu action choices -----

        public void DisplayTravelerInfo()
        {
            DisplayGamePlayScreen("Traveler Information", Text.TravelerInfo(_gameTraveler), ActionMenu.MainMenu, "");
        }

        public void DisplayListOfLocations()
        {
            DisplayGamePlayScreen("List: Locations", Text.ListLocations(_gameShip.Locations), ActionMenu.AdminMenu, "");
        }

        public void DisplayLookAround()
        {
            //
            //current location
            //
            Location currentLocation = _gameShip.GetLocationByID(_gameTraveler.LocationID);

            //
            //list of objects in location
            //
            List<GameObject> gameObjectsInCurrentLocation = _gameShip.GetGameObjectsByLocationId(_gameTraveler.LocationID);

            string messageBoxText = Text.LookAround(currentLocation) + Environment.NewLine + Environment.NewLine;
            messageBoxText += Text.GameObjectsChooseList(gameObjectsInCurrentLocation);

            DisplayGamePlayScreen("Current Location", messageBoxText, ActionMenu.MainMenu, "");
        }

        public int DisplayGetNextLocation()
        {
            int locationId = 0;
            bool vaildLocationId = false;

            DisplayGamePlayScreen("Travel to a new Location", Text.Travel(_gameTraveler, _gameShip.Locations), ActionMenu.MainMenu, "");

            while (!vaildLocationId)
            {
                //
                // users int
                // 

                GetInteger($"Enter your next Location {_gameTraveler.Name}:", 1, _gameShip.GetMaxLocationId(), out locationId);

                //
                // Vaild spaceId 
                //

                if (_gameShip.IsValidLocationId(locationId))
                {
                    if (_gameShip.IsAccessibleLocation(locationId))
                    {
                        vaildLocationId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage($"{_gameTraveler.Name} You do not have access to this location yet. Please try again.");
                    }
                }

                else
                {
                    DisplayInputErrorMessage("Sorry. You have entered an invaild location id. Please try again.");
                }

            }

            return locationId;
        }

        public void DisplayLocationsVisited()
        {
            //
            // list of Locations visited
            //

            List<Location> visitedLocations = new List<Location>();
            foreach (int locationId in _gameTraveler.LocationsVisited)
            {
                visitedLocations.Add(_gameShip.GetLocationByID(locationId));

            }
            DisplayGamePlayScreen("Locations Visited", Text.VisitedLocations(visitedLocations), ActionMenu.MainMenu, "");
        }

        public void DisplayListOfAllGameObjects()
        {
            DisplayGamePlayScreen("List: Game Objects", Text.ListAllGameObjects(_gameShip.GameObjects),ActionMenu.AdminMenu,"");
        }

        public void DisplayInventory()
        {
            DisplayGamePlayScreen("Current Inventory", Text.CurrentInventory(_gameTraveler.Inventory), ActionMenu.MainMenu, "");
        }

        public int DisplayGetGameObjectToLookAt()
        {
            int gameObjectId = 0;
            bool validGamerObjectId = false;

            //
            // get a list of game objects in the current location
            //
            List<GameObject> gameObjectsInLocation = _gameShip.GetGameObjectsByLocationId(_gameTraveler.LocationID);

            if (gameObjectsInLocation.Count > 0)
            {
                DisplayGamePlayScreen("Look at a Object", Text.GameObjectsChooseList(gameObjectsInLocation), ActionMenu.MainMenu, "");

                while (!validGamerObjectId)
                {
                    //
                    // get an integer from the player
                    //
                    GetInteger($"Enter the Id number of the object you wish to look at: ", 0, 0, out gameObjectId);

                    //
                    // validate integer as a valid game object id and in current location
                    //
                    if (_gameShip.IsValidGameObjectByLocationId(gameObjectId, _gameTraveler.LocationID))
                    {
                        validGamerObjectId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you entered an invalid game object id. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Look at a Object", "It appears there are no game objects here.", ActionMenu.MainMenu, "");
            }

            return gameObjectId;
        }

        public void DisplayGameObjectInfo(GameObject gameObject)
        {
            DisplayGamePlayScreen("Current Location", Text.LookAt(gameObject), ActionMenu.MainMenu, "");
        }

        public int DisplayGetTravlerObjectToPickUp()
        {
            int gameObjectId = 0;
            bool vaildGameObjectId = false;

            //
            //list of traveler objects in current location
            //

            List<TravelerObject> travelerObjectsInLocation = _gameShip.GetTravelerObjectsByLocationId(_gameTraveler.LocationID);

            if (travelerObjectsInLocation.Count > 0)
            {
                DisplayGamePlayScreen("Pick Up Game Object", Text.GameObjectsChooseList(travelerObjectsInLocation), ActionMenu.MainMenu, "");

                while (!vaildGameObjectId)
                {
                    //
                    //get ineger from player 
                    //
                    GetInteger($"Enter the Id number of the object you wish to add to your inventory:", 0, 0, out gameObjectId);

                    //
                    //vailidate integer
                    //
                    if (_gameShip.IsValidTravelerObjectByLocationId(gameObjectId, _gameTraveler.LocationID))
                    {
                        TravelerObject travelerObject = _gameShip.GetGameObjectById(gameObjectId) as TravelerObject;
                        if (travelerObject.CanInventory)
                        {
                            vaildGameObjectId = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("It appears you cannot currently add this item to your inventory.");
                        }
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you have entered an invaild game object id. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Pick Up Game Object", "It appears there are no available game objects.", ActionMenu.MainMenu, "");
            }

            return gameObjectId;
        }

        public void DisplayConfirmTravelerObjectAddedToInventory(TravelerObject objectAddedToInventory)
        {
            DisplayGamePlayScreen("Pick Up Game Object", $"The {objectAddedToInventory.Name} has been added to your inventory.", ActionMenu.MainMenu, "");
        }

        public int DisplayGetInventoryObjectToPutDown()
        {
            int travelerObjectId = 0;
            bool validInventoryObjectId = false;

            if (_gameTraveler.Inventory.Count > 0)
            {
                DisplayGamePlayScreen("Put Down Game Object", Text.GameObjectsChooseList(_gameTraveler.Inventory), ActionMenu.MainMenu, "");

                while (!validInventoryObjectId)
                {
                    //
                    //get int from user
                    //
                    GetInteger($"Enter the Id number of the object you would like to drop from inventory.", 0, 0, out travelerObjectId);

                    //
                    //find object
                    //
                    TravelerObject objectToPutDown = _gameTraveler.Inventory.FirstOrDefault(o => o.Id == travelerObjectId);

                    //
                    //validate object in inventory 
                    //
                    if (objectToPutDown != null)
                    {
                        validInventoryObjectId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears your inventory does not contain this object. please try again");

                    }

                }
            }
            else
            {
                DisplayGamePlayScreen("Pick Up Game Object", "It appears there are no objects in your inventory", ActionMenu.MainMenu, "");
            }

            return travelerObjectId;
        }

        public void DisplayConfirmTravelerObjectRemovedFromInventory(TravelerObject objectRemovedFromInventory)
        {
            DisplayGamePlayScreen("Put Down Game Object", $"The {objectRemovedFromInventory.Name} has been removed from your inventory.", ActionMenu.MainMenu, "");
        }
        #endregion

        #endregion
    }
}
