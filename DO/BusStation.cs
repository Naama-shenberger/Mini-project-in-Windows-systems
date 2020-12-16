using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    class BusStation
    {
        private string BusStationKey;//code Station
        private string StationAddress;//Station Address;
        private string stationName;
        private double latitude;
        private double longitude;

        public string STATIONADDRESS
        {
            get { return StationAddress; }
            set { StationAddress = ""; }

        }
        /// <summary>
        /// Lottery of values for longitude and latitude (within Israel)
        /// </summary>
        public double LATITUDE
        {
            get { return latitude; }
            private set
            {
                latitude = value;
            }
        }
        public double LONGITUDE
        {
            get { return longitude; }
            private set
            {
                longitude = value;
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
    }
}
