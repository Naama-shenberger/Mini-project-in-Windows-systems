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
        private string BusStationKey;
        /// <summary>
        ///structure location
        ///Fields: longitude and latitude
        /// </summary>
        public struct location
        {
            public double Latitude;
            public double Longitude;
        };
        protected location Landmark;
        /// <summary>
        /// Lottery of values for longitude and latitude (within Israel)
        /// </summary>
        public location LANDMARCK
        {
            get { return Landmark; }
            private set
            {
                Random r = new Random();
                Landmark.Latitude = r.NextDouble() * (33.3 - 31) + 31;
                Landmark.Longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;
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
                try
                {
                    if (value.Length > 6) throw new System.ArithmeticException();
                    else BusStationKey = value;
                }
                catch (System.ArithmeticException e)
                { e.ToString(); }
            }
        }
        /// <summary>
        /// Function override 'Tostring' 
        /// The function prints the Details of bus station
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Bus Station Code: {BusStationKey}, {Landmark.Latitude}°N {Landmark.Longitude}°E";
        }

    }
}

