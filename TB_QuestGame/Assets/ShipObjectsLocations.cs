using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame.Assets
{
     public static partial class ShipObjectsLocations
    {  
            public static List<Location> Locations = new List<Location>()
            {

                new Location
                {
                    CommonName = "Common Passener Rooms",
                    LocationID = 1,
                    UniversalDate = 4151915,
                    ShipLocation = "P-3, SS-278, G-2976, LS-3976",
                    Description = " Small rooms near the bottom of the ship, where the common passengers reside. ",

                    GeneralContents = "The room is small, and cold" +
                        " There are two small beds, a large desk, and two small lockers that make up the room.\n",
                    Accessable = true,
                    ExperiencePoints = 10
                },

                new Location
                {
                    CommonName = "Upper Deck",
                    LocationID = 2,
                    UniversalDate = 4151915,
                    ShipLocation = "P-2, SS-85, G-2976, LS-3976",
                    Description = ".",
                    GeneralContents = "- stuff in the room -",
                    Accessable = true,
                    ExperiencePoints = 10
                },

                new Location
                {
                    CommonName = "Kitchen",
                    LocationID = 3,
                    UniversalDate = 4151915,
                    ShipLocation = "P-6, SS-3978, G-2976, LS-3976",
                    Description = " The kitchen is large and well lit. " +
                                  "There are many kitchen staff that are busily preparing the next meal. ",
                    GeneralContents = "- stuff in the room -",
                    Accessable = false,
                    ExperiencePoints = 30
                },

                  new Location
                  {
                    CommonName = "Grand Dining Room",
                    LocationID = 4,
                    UniversalDate = 4151915,
                    ShipLocation = "P-6, SS-3978, G-2976, LS-3976",
                    Description = " The grand dining room is lined with large tables. Set up for passengers" +
                                  " to eat their next meal.",
                    GeneralContents = "- stuff in the room -",
                    Accessable = false,
                    ExperiencePoints = 50
                  },

                  new Location
                  {
                        CommonName = "Captians Corridors",
                        LocationID = 5,
                        UniversalDate = 4151915,
                        ShipLocation = "P-6, SS-3978, G-2976, LS-3976",
                        Description = " A large room that is dimly lit. " +
                                      "  " +
                                      ".",
                        GeneralContents = "- stuff in the room -",
                        Accessable = false,
                        ExperiencePoints = 70
                  }
            };
        }

    }

