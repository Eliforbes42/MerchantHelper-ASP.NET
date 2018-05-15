using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheFinder.Models;
namespace TheFinder.Controllers
{
    public class LocationsController : ApiController
    {
        class AnimalFactory
        {
            public AnimalFactory() { }
            public static object getAnimal(int caseNum)
            {
                switch (caseNum)
                {
                    case 1: return new Chicken();
                    case 2: return new Pig();
                    case 3: return new Snek();
                    default: break; //unsupported
                }
                return null;//return null object if unsupported animal
            }
        }
        public Fort[] forts = new Fort[]
        {
            new Fort('K',9, "Hidden Spring Keep"), new Fort('C',7, "Keel Haul Fort"),
            new Fort('O',6,"Kraken Watchtower"), new Fort('J',21,"Lost Gold Fort"),
            new Fort('P',17,"Old Boot Fort"), new Fort('U',5,"Shark Fin Camp"),
            new Fort('E',17,"Sailor's Knot Stronghold"), new Fort('U',11,"Skull Keep"),
            new Fort('S',22,"The Crow's Nest Fortress")
        };
        public Location[] locations = new Location[]
        {
             new Location('T', 19, "Barnacle Cay", new List<Animal>() { new Chicken() }),
             new Location('T', 3, "Black Sands Atoll", new List<Animal>() { new Snek() }),
             new Location('X', 5, "Black Water Enclave", new List<Animal>() { new Chicken() }),
             new Location('S', 6, "Blind Man's Lagoon", new List<Animal>() { new Pig() }),
             new Location('N', 25, "Booty Location", new List<Animal>() { new Snek() }),
             new Location('H', 5, "Boulder Cay", new List<Animal>() { new Pig() }),

             new Location('H', 11, "Cannon Cove", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('M', 16, "Castaway Isle", new List<Animal>() { new Snek() }),
             new Location('K', 19, "Chicken Isle", new List<Animal>() { new Chicken() }),
             new Location('B', 10, "Crescent Isle", new List<Animal>() { new Pig(), new Snek() }),
             new Location('Q', 19, "Crook's Hollow", new List<Animal>() { new Chicken(), new Snek() }),
             new Location('Q', 22, "Cutlass Cay", new List<Animal>() { new Snek() }),

             new Location('U', 24, "Devil's Ridge", new List<Animal>() { new Pig(), new Snek() }),
             new Location('E', 21, "Discovery Ridge", new List<Animal>() { new Chicken(), new Snek() }),

             new Location('K', 17, "Fools Lagoon", new List<Animal>() { new Pig() }),

             new Location('S', 10, "Isle of Last Words", new List<Animal>() { new Snek() }),

             new Location('X', 15, "Kraken's Fall", new List<Animal>() { new Pig(), new Snek() }),

             new Location('D', 15, "Lagoon of Whispers", new List<Animal>() { new Snek() }),
             new Location('Y', 13, "Liar's Backbone", new List<Animal>() { new Snek() }),
             new Location('J', 6, "Lone Cove", new List<Animal>() { new Pig(), new Snek() }),
             new Location('I', 9, "Lonely Isle", new List<Animal>() { new Snek() }),
             new Location('L', 25, "Lookout Point", new List<Animal>() { new Pig() }),

             new Location('V', 3, "Marauder's Arch", new List<Animal>() { new Chicken(), new Snek() }),
             new Location('B', 16, "Mermaid's Hideaway", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('R', 24, "Mutineer Rock", new List<Animal>() { new Chicken(), new Pig() }),

             new Location('Q', 4, "Old Faithful Isle", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('G', 23, "Old Salts Atoll", new List<Animal>() { new Chicken() }),

             new Location('O', 21, "Paradise Spring", new List<Animal>() { new Pig() }),
             new Location('K', 4, "Picaroon Palms", new List<Animal>() { new Snek() }),
             new Location('V', 6, "Plunderer's Plight", new List<Animal>() { new Pig() }),
             new Location('H', 19, "Plunder Valley", new List<Animal>() { new Chicken(), new Pig() }),

             new Location('D', 9, "Rapier Cay", new List<Animal>() { new Chicken() }),
             new Location('J', 11, "Rum Runner Isle", new List<Animal>() { new Pig() }),

             new Location('B', 4, "Sailor's Bounty", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('I', 3, "Salty Sands", new List<Animal>() { new Chicken() }),
             new Location('D', 5, "Sandy Shallows", new List<Animal>() { new Snek() }),
             new Location('N', 4, "Scurvy Location", new List<Animal>() { new Chicken() }),
             new Location('B', 13, "Sea Dog's Rest", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('I', 24, "Shark Bait Cove", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('U', 15, "Shark Tooth Key", new List<Animal>() { new Chicken() }),
             new Location('Q', 12, "Shipwreck Bay", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('V', 13, "Shiver Retreat", new List<Animal>() { new Chicken() }),
             new Location('F', 3, "Smuggler's Bay", new List<Animal>() { new Chicken(), new Snek() }),
             new Location('N', 19, "Snake Location", new List<Animal>() { new Pig(), new Snek() }),

             new Location('T', 13, "The Crooked Masts", new List<Animal>() { new Chicken(), new Snek() }),
             new Location('T', 8, "The Sunken Grove", new List<Animal>() { new Pig(), new Snek() }),
             new Location('P', 25, "Thieves Haven", new List<Animal>() { new Chicken(), new Pig() }),
             new Location('W', 11, "Tri-Rock Isle", new List<Animal>() { new Chicken() }),
             new Location('I', 11, "Twin Groves", new List<Animal>() { new Chicken() }),

             new Location('G', 15, "Wanderer's Refuge", new List<Animal>() { new Chicken(), new Snek() })
        };
        public Location HasAnimals(string animalIds, string userLocation)
        { //use object type to support anything/everything
            string[] ids = animalIds.Split(',');
            List<int> choices = new List<int>(ids.Length);
            List<Location> possibilities = new List<Location>();
            int parseVal = 0;
            foreach (string cmd in ids)
            {
                int.TryParse(cmd, out parseVal);
                choices.Add(parseVal);
            }
            foreach (Location isle in locations)
            {
                switch (ids.Length)
                {
                    case 1:
                        if (isle.HasAnimal(AnimalFactory.getAnimal(choices[0])))
                            possibilities.Add(isle);
                        break;
                    case 2:
                        if (isle.HasAnimal(AnimalFactory.getAnimal(choices[0]))
                            && isle.HasAnimal(AnimalFactory.getAnimal(choices[1])))
                            possibilities.Add(isle);
                        break;
                    case 3: break;
                    default: break; //unsupported #
                }
            }
            //return false;
            string newUserLocation;
            if ('a' <= userLocation[0] && userLocation[0] <= 'z')
                newUserLocation = ((char)(userLocation[0] - 'a' + 'A')).ToString()
                                + userLocation.Substring(1); //handle lowerCase
            else newUserLocation = userLocation;
            // Debug.WriteLine("\n\n\nuserLocation: " + newUserLocation + "\n\n");
            //WriteLine("You are here: " + userLocation);
            int curRow = (userLocation[0] - 'A');
            int curCol = 0;
            double dist = 0.0,
                   minDist = double.MaxValue - 1;
            Location junk;
            int.TryParse(userLocation.Substring(1), out curCol);
            Dictionary<double, Location> distances = new Dictionary<double, Location>();
            foreach (Location poss in possibilities)
            {
                dist = Math.Sqrt(Math.Pow((poss.row - curRow), 2) + Math.Pow((poss.col - curCol), 2));
                if (dist < minDist)
                    minDist = dist;
                if (!distances.TryGetValue(dist, out junk))
                    distances.Add(dist, poss);
            }
            //Console.WriteLine("minDistance: " + minDist);            
            if (distances.TryGetValue(minDist, out junk))
                return junk;
            else
                return null;

        }
        public IEnumerable<Location> GetAllLocations()
        {
            return locations;
        }

        [Route("api/locations/{mashedArg}")]
        [HttpGet]
        public string GetProduct(string mashedArg)
        {
            string[] unmashed = mashedArg.Split('|');
            string test = unmashed[0];
            string location = unmashed[1];
            string[] idStrings = (unmashed[0].Contains(',')) ? unmashed[0].Split(',') : new string[] { unmashed[0] };
            List<int> ids = new List<int>(idStrings.Length);
            //List<Location> possibilities = new List<Location>();
            int idInt = 0;
            foreach (string id in idStrings)
            {
                int.TryParse(id, out idInt);
                ids.Add(idInt);
            }
            Location res = HasAnimals(test, location);        //locations.Where(l => 
            
            return (res.name + " - " + res.fullRowCol);
        }

        [Route("api/locations/{curLocale}/gp")]
        [HttpGet]
        public string GetProductGP(string curLocale)
        {
            int curRow = (curLocale[0] - 'A');
            int curCol = 0;
            double dist = 0.0,
                   minDist = double.MaxValue - 1;
            Fort junk, minDistFort = null;
            int.TryParse(curLocale.Substring(1), out curCol);
            Dictionary<double, Fort> distances = new Dictionary<double, Fort>();
            foreach (Fort poss in forts)
            {
                dist = Math.Sqrt(Math.Pow((poss.row - curRow), 2) + Math.Pow((poss.col - curCol), 2));
                if (dist < minDist)
                {
                    minDist = dist;
                    minDistFort = poss;
                }
                if (!distances.TryGetValue(dist, out junk))
                    distances.Add(dist, poss);
            }

            return (minDistFort.name + " - " + minDistFort.fullRowCol);
        }


        //return res.name + ' ' + '-' + ' ' + res.fullRowCol;
        //if (locResult == null)
        //{
        //    return NotFound();
        //}
        //// string res = (locResult.name + " - " + locResult.fullRowCol);
        //return Ok(locResult);
        //return (locResult.name + " - " + locResult.fullRowCol);      
    }
}
