using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    /// <summary>
    /// base class for the player and all game characters
    /// </summary>
    public class Character
    {
        #region ENUMERABLES

        public enum EthnicityType
        {
            None,
            American,
            British,
            Canadian,
            French,
            Swiss
        }

        #endregion

        #region FIELDS

        private string _name;
        private int _age;
        private EthnicityType _ethnicity;
        private int _locationID;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public EthnicityType Ethnicity
        {
            get { return _ethnicity; }
            set { _ethnicity = value; }
        }

        public int LocationID
        {
            get { return _locationID; }
            set { _locationID = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Character()
        {

        }

        public Character(string name, EthnicityType ethnicity, int locationID)
        {
            _name = name;
            _ethnicity = ethnicity;
            _locationID = locationID;
        }

        #endregion

        #region METHODS

        public virtual string Greeting()
        {
            return $"Hello {_name}! Welcome aboard the Titanic.";
        }

        #endregion
    }
}
