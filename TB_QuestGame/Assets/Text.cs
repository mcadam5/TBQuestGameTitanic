using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// class to store static and to generate dynamic text for the message and input boxes
    /// </summary>
    public static class Text
    {
        public static List<string> HeaderText = new List<string>() { "Titanic" };
        public static List<string> FooterText = new List<string>() { "Kerri McAdams, 2017" };

        #region INTITIAL GAME SETUP

        public static string MissionIntro()
        {
            string messageBoxText =
            "You have just awoken aboard the Titanic." +
            "It is April of 1915 and you are currently located in the middle of the " +
            "Pacific. Your job is to work your way up, and inform the captain of the ice berg before it's too late. " +
            " \n" +
            "Press the Esc key to exit the game at any point.\n" +
            " \n" +
            "Your mission begins now.\n" +
            " \n" +
            "\tYour first task will be to set up the initial parameters of your mission.\n" +
            " \n" +
            "\tPress any key to begin the Mission Initialization Process.\n";

            return messageBoxText;
        }

        public static string CurrrentLocationInfo()
        {
            string messageBoxText =
            "You are now in the common passenger corriders located in " +
            "the lower levels of the ship. You currently have access to " +
            "the top deck of the ship. Your fist job is to gain access to a meal card to open up the kitchen. " +
            " \n" +
            "\tChoose from the menu options to proceed.\n";

            return messageBoxText;
        }

        #region Initialize Mission Text

        public static string InitializeMissionIntro()
        {
            string messageBoxText =
                "Before you begin your mission we much gather your base data.\n" +
                " \n" +
                "You will be prompted for the required information. Please enter the information below.\n" +
                " \n" +
                "\tPress any key to begin.";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerName()
        {
            string messageBoxText =
                "Enter your name traveler.\n" +
                " \n" +
                "Please use the name you wish to be referred during your mission.";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerAge(Traveler gameTraveler)
        {
            string messageBoxText =
                $"Very good then, we will call you {gameTraveler.Name} on this mission.\n" +
                " \n" +
                "Enter your age below.\n" +
                " \n" +
                "Please use the common year as your reference.";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerEthnicity(Traveler gameTraveler)
        {
            string messageBoxText =
                $"{gameTraveler.Name}, it will be important for us to know your ethnicity on this mission.\n" +
                " \n" +
                "Enter your ethnicity below.\n" +
                " \n" +
                "Please use the ethnicity classifications below." +
                " \n";

            string raceList = null;

            foreach (Character.EthnicityType race in Enum.GetValues(typeof(Character.EthnicityType)))
            {
                if (race != Character.EthnicityType.None)
                {
                    raceList += $"\t{race}\n";
                }
            }

            messageBoxText += raceList;

            return messageBoxText;
        }

        public static string InitializeMissionTravelerHomeLocation(string name)
        {
            string messageBoxText =
                $"{name}, for our records, and to be able to notify family in case of emergency.\n" +
                "\n" +
                "Please Enter your Home Location.\n";

            return messageBoxText;
        }

        public static string InitializeMissionEchoTravelerInfo(Traveler gameTraveler)
        {
            string messageBoxText =
                $"Very good then {gameTraveler.Name}.\n" +
                " \n" +
                "It appears we have all the necessary data to begin your mission. You will find it" +
                " listed below.\n" +
                " \n" +
                $"\tTraveler Name: {gameTraveler.Name}\n" +
                $"\tTraveler Age: {gameTraveler.Age}\n" +
                $"\tTraveler Ethnicity: {gameTraveler.Ethnicity}\n" +
                $"\tTraveler Home Location: {gameTraveler.HomeLocation}\n" +
                " \n" +
                "Press any key to begin your mission.";

            return messageBoxText;
        }

       
        #endregion

        #endregion

        #region MAIN MENU ACTION SCREENS

        public static string TravelerInfo(Traveler gameTraveler)
        {
            string messageBoxText =
                $"\tTraveler Name: {gameTraveler.Name}\n" +
                $"\tTraveler Age: {gameTraveler.Age}\n" +
                $"\tTraveler Ethnicity: {gameTraveler.Ethnicity}\n" +
                $"\t Traveler Home Location:{gameTraveler.HomeLocation}\n"+
                "\n"+
                "/n"+
                $"\t {gameTraveler.Greeting()} \n"+
                " \n";

            return messageBoxText;
        }

        public static string ListLocations(IEnumerable<Location> locations)
        {
            string messageBoxText =
                "Locations\n" +
                    "\n" +

             //
             //table header
             //
             $"ID".PadRight(10) + "Name".PadRight(30) + "\n" +
             "---".PadRight(10) + "--------------------------".PadRight(30) + "\n";

            //
            // display all locations
            //
            string locationList = null;
            foreach (Location location in locations)
            {
                locationList +=
                    $"{location.LocationID}".PadRight(10) +
                    $"{location.CommonName}".PadRight(30) +
                    $"{location.Accessable}".PadRight(10) +
                    Environment.NewLine;
            }

            messageBoxText += locationList;

            return messageBoxText;
        }

        public static string LookAround(Location location)
        {
            string messageBoxText =
                $"Current Location: {location.CommonName}\n" +
                "\n" +
                location.GeneralContents;

            return messageBoxText;
        }

        public static string Travel(Traveler gameTraveler, List<Location> locations)
        {
            string messageBoxText =
                $"{gameTraveler.Name}, Aion Base will need to know the name of the new location. \n" +
                "\n" +
                "Enter the ID number of your desired location from the table below.\n" +
                "\n" +

            //
            // table header
            //

            "ID".PadRight(10) + "Name".PadRight(30) + "Accessable".PadRight(10) + "\n" +
            "---".PadRight(10) + "------------------------".PadRight(30) + "----------".PadRight(10) + "\n";

            //
            // display travel locaions 
            //
            string locationList = null;

            foreach (Location location in locations)
            {
                if (location.LocationID != gameTraveler.LocationID)
                {
                    locationList +=
                        $"{location.LocationID}".PadRight(10) +
                        $"{location.CommonName}".PadRight(30) +
                        $"{location.Accessable}".PadRight(10) +
                        Environment.NewLine;
                }
            }

            messageBoxText += locationList;

            return messageBoxText;

        }

        public static string CurrentLocationInfo(Location location)
        {
            string messageBoxText =
                $"Current Location:{location.CommonName}\n" +
                "\n" +
                location.Description;

            return messageBoxText;
        }

        public static string VisitedLocations(IEnumerable<Location> locations)
        {
            string messageBoxText =
                "Locations Visited\n" +
                "\n" +

                 //
                 // header
                 //
                 "ID".PadRight(10) + "Name".PadRight(30) + "\n" +
            "---".PadRight(10) + "------------------------".PadRight(30) + "\n";

            //
            //display locations
            //
            string locationList = null;
            foreach (Location location in locations)
            {
                locationList +=
                    $"{location.LocationID}".PadRight(10) +
                        $"{location.CommonName}".PadRight(30) +
                        Environment.NewLine;
            }

            messageBoxText += locationList;

            return messageBoxText;
        }

        public static string ListAllGameObjects(IEnumerable<GameObject> gameObjects)
        {
            //
            // display table name and column headers
            //
            string messageBoxText =
                "Game Objects\n" +
                " \n" +

                //
                // display table header
                //
                "ID".PadRight(10) +
                "Name".PadRight(30) +
                "Location Id".PadRight(10) + "\n" +
                "---".PadRight(10) +
                "----------------------".PadRight(30) +
                "----------------------".PadRight(10) + "\n";

            //
            // display all traveler objects in rows
            //
            string gameObjectRows = null;
            foreach (GameObject gameObject in gameObjects)
            {
                gameObjectRows +=
                    $"{gameObject.Id}".PadRight(10) +
                    $"{gameObject.Name}".PadRight(30) +
                    $"{gameObject.LocationId}".PadRight(10) +
                    Environment.NewLine;
            }

            messageBoxText += gameObjectRows;

            return messageBoxText;
        }

        public static string GameObjectsChooseList(IEnumerable<GameObject> gameObjects)
        {
            //
            //display
            //
            string messageBoxText =
                "Game Objects\n" +
                "\n" +

                 //
                 //table header
                 //
                 "ID".PadRight(10) +
                "Name".PadRight(30) + "\n" +
                "---".PadRight(10) +
                "----------------------".PadRight(30) + "\n";

            //
            //travler objects in rows
            //
            string gameObjectRows = null;
            foreach (GameObject gameObject in gameObjects)
            {
                gameObjectRows +=
                    $"{gameObject.Id}".PadRight(10) +
                    $"{gameObject.Name}".PadRight(30) +
                    Environment.NewLine;
            }

            return messageBoxText;
        }

        public static string LookAt(GameObject gameObject)
        {
            string messageBoxText = "";

            messageBoxText =
                $"{gameObject.Name}\n" +
                "\n" +
                gameObject.Description + "\n" +
                "\n";

            if (gameObject is TravelerObject)
            {
                TravelerObject travelerObject = gameObject as TravelerObject;

                messageBoxText += $"The {travelerObject.Name} has a value of {travelerObject.Value} and";

                if (travelerObject.CanInventory)
                {
                    messageBoxText += "may be added to your inventory.";
                }
                else
                {
                    messageBoxText += "may not be added to your inventory.";
                }
            }
            else
            {
                messageBoxText += $"The {gameObject.Name} may not be added to your inventory.";
            }

            return messageBoxText;
        }

        public static string CurrentInventory(IEnumerable<TravelerObject> inventory)
        {
            string messageBoxText = "";

            //
            // display table header
            //
            messageBoxText =
            "ID".PadRight(10) +
            "Name".PadRight(30) +
            "Type".PadRight(10) +
            "\n" +
            "---".PadRight(10) +
            "----------------------------".PadRight(30) +
            "----------------------".PadRight(10) +
            "\n";

            //
            // display all traveler objects in rows
            //
            string inventoryObjectRows = null;
            foreach (TravelerObject inventoryObject in inventory)
            {
                inventoryObjectRows +=
                    $"{inventoryObject.Id}".PadRight(10) +
                    $"{inventoryObject.Name}".PadRight(30) +
                    $"{inventoryObject.Type}".PadRight(10) +
                    Environment.NewLine;
            }

            messageBoxText += inventoryObjectRows;

            return messageBoxText;
        }

        public static List<string> StatusBox(Traveler traveler, Ship ship)
        {
            List<string> statusBoxText = new List<string>();

            statusBoxText.Add($"Experience Points: {traveler.Experiencepoints}\n");
            statusBoxText.Add($"Health: {traveler.Health}\n");
            statusBoxText.Add($"Lives: {traveler.Lives}\n");

            return statusBoxText;
        }
        #endregion

    }
}
