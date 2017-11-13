using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB_QuestGame.Assets;

namespace TB_QuestGame
{
    public class Ship
    {
        #region ***** define all lists to be maintained by the ship object *****

        //
        // list of all locations and game objects
        //
        private List<Location> _locations;
        private List<GameObject> _gameObjects;


        public List<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }

        public List<GameObject> GameObjects
        {
            get { return _gameObjects; }
            set { _gameObjects = value; }
        }
        #endregion

        #region ***** constructor *****

        //
        // default ship constructor
        //
        public Ship()
        {
            //
            // add all of the ships objects to the game
            // 
            IntializeShip();
        }

        #endregion

        #region ***** define methods to initialize all game elements *****

        /// initialize the universe with all of the locations
 
        private void IntializeShip()
        {
            _locations = ShipObjectsLocations.Locations as List<Location>;
            _gameObjects = ShipObjects.gameObjects;
        }

        #endregion

        #region ***** define methods to return game element objects and information *****

        public bool IsValidLocationId(int locationId)
        {
            List<int> locationIds = new List<int>();

            //
            // create a list of location ids
            //
            foreach (Location l in _locations)
            {
                locationIds.Add(l.LocationID);
            }

            //
            // determine if the location id is a valid id and return the result
            //
            if (locationIds.Contains(locationId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 
        // determine if a location is accessible to the player
      
        public bool IsAccessibleLocation(int locationId)
        {
            Location location = GetLocationByID(locationId);
            if (location.Accessable == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // return the next available ID for a SpaceTimeLocation object
        public int GetMaxLocationId()
        {
            int MaxId = 0;

            foreach (Location location in Locations)
            {
                if (location.LocationID > MaxId)
                {
                    MaxId = location.LocationID;
                }
            }

            return MaxId;
        }

        /// get a Location object using an ID   
        public Location GetLocationByID(int ID)
        {
            Location location = null;

            //
            // run through the location list and grab the correct one
            //
            foreach (Location roomLocation in _locations)
            {
                if (roomLocation.LocationID == ID)
                {
                    location = roomLocation;
                }
            }

            //
            // the specified ID was not found in the ship
            // throw and exception
            //
            if (location == null)
            {
                string feedbackMessage = $"The Location ID {ID} does not exist on this ship.";
                throw new ArgumentException(ID.ToString(), feedbackMessage);
            }

            return location;
        }

        public bool IsAccessableLocation(int locationId)
        {
            Location location = GetLocationByID(locationId);
            if (location.Accessable == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetLocationId()
        {
            int MaxId = 0;

            foreach (Location location in Locations)
            {
                if (location.LocationID > MaxId)
                {
                    MaxId = location.LocationID;
                }
            }

            return MaxId;
        }

        public bool IsValidGameObjectByLocationId(int gameObjectId, int currentLocation)
        {
            List<int> gameObjectIds = new List<int>();

            //
            // create a list of game object ids in current location
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.LocationId == currentLocation)
                {
                    gameObjectIds.Add(gameObject.Id);
                }

            }

            //
            // determine if the game object id is a valid id and return the result
            //
            if (gameObjectIds.Contains(gameObjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public GameObject GetGameObjectById(int Id)
        {
            GameObject gameObjectToReturn = null;

            //
            // grab correct object
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.Id == Id)
                {
                    gameObjectToReturn = gameObject;
                }
            }

            //
            //Id was not found----throw an exception
            //
            if (gameObjectToReturn == null)
            {
                string feedbackMessage = $"The Game Object ID {Id} does not exists on this ship.";
                throw new ArgumentException(feedbackMessage, Id.ToString());
            }

            return gameObjectToReturn;
        }

        public List<GameObject> GetGameObjectsByLocationId(int locationId)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            //
            // run through the game object list and grab all that are in the current space-time location
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.LocationId == locationId)
                {
                    gameObjects.Add(gameObject);
                }
            }

            return gameObjects;
        }

        public bool IsValidTravelerObjectByLocationId(int travelerObjectId, int currentLocation)
        {
            List<int> travelerObjectIds = new List<int>();

            //
            // create list of traveler object ids in current location
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.LocationId == currentLocation && gameObject is TravelerObject)
                {
                    travelerObjectIds.Add(gameObject.Id);
                }

            }

            //
            // game object id is a valid id and return the result
            //
            if (travelerObjectIds.Contains(travelerObjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TravelerObject> GetTravelerObjectsByLocationId(int locationId)
        {
            List<TravelerObject> travelerObjects = new List<TravelerObject>();

            //
            // run game object list and grab all that are in the current location
            //
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.LocationId == locationId && gameObject is TravelerObject)
                {
                    travelerObjects.Add(gameObject as TravelerObject);
                }
            }

            return travelerObjects;
        }

        #endregion
    }
}
