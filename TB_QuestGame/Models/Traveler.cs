using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// the character class the player uses in the game
    /// </summary>
    public class Traveler : Character
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        private List<int> _locationsVisited;

        private int _experiencePoints;
        private int _health;
        private int _lives;
        private List<TravelerObject> _inventory;

        private string  _homeLocation;



        #endregion

        #region PROPERTIES

        public List<int> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        public int Experiencepoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public string HomeLocation
        {
            get { return _homeLocation; }
            set { _homeLocation = value; }
        }

        public List<TravelerObject> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Traveler()
        {
            _locationsVisited = new List<int>();
            _inventory = new List<TravelerObject>();

        }

        public Traveler(string name, EthnicityType ethnicity, int locationID) : base(name, ethnicity, locationID)
        {
            _locationsVisited = new List<int>();
            _inventory = new List<TravelerObject>();
        }

        #endregion

        #region METHODS

        public override string Greeting()
        {
            return $"Hello {base.Name}! Welcome to aboard the Titanic. It looks like you are begining your journey from {_homeLocation}.";
        }

        public bool HasVisited(int _locationID)
        {
            if (LocationsVisited.Contains(_locationID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
