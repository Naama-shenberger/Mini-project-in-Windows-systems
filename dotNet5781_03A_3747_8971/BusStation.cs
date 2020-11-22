using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    /// <summary>
    /// Bus station Class
    /// </summary>
    class BusStation
    {
        /// <summary>
        /// variable
        ///Random class
        /// </summary>
        public static Random r = new Random();
        /// <summary>
        /// code Station
        /// </summary>
        private string BusStationKey;
        /// <summary>
        /// Station Address;
        /// </summary>
        private string StationAddress;
        /// <summary>
        /// location field
        /// </summary>
        protected Location Landmark;
        /// <summary>
        ///structure location
        ///Fields: longitude and latitude
        /// </summary>
        public struct Location
        {
            public double Latitude;
            public double Longitude;
        };
        public string STATIONADDRESS
        {
            get { return StationAddress; }
            set { StationAddress = ""; }

        }
        /// <summary>
        /// Lottery of values for longitude and latitude (within Israel)
        /// </summary>
        public Location LANDMARCK
        {
            get { return Landmark; }
            private set
            {
                Landmark = value;
            }
        }
        /// <summary>
        /// set and get for station code
        /// Check station code number not bigger than 6
        /// </summary>
        public string BUS_STATION_KEY
        {
            get { return BusStationKey; }
            set
            {
                BusStationKey = value;
            }
        }
        /// <summary>
        /// Function override 'Tostring' 
        /// The function prints the Details of bus station
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Bus Station Code:{BUS_STATION_KEY} Landmarck:{Landmark.Latitude}°N {Landmark.Longitude}°E";
        }
        /// <summary>
        /// A constructor that initializes the object with a longitude and latitude
        /// </summary>
        public BusStation()
        {
            Landmark.Latitude = r.NextDouble() * (33.3 - 31) + 31;
            Landmark.Longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;
        }
       



    }
}

