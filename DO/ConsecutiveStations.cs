using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// A class that has information on consecutive stations
    /// </summary>
    public class ConsecutiveStations
    {
        private string stationCodeOne;//Code station one
        private string stationCodeTwo;//Code station Two
        private float distance;//Distance between stations
        private float averageTravelTime;//Average travel time between stations
        private bool flage;//for Deletion
        /// <summary>
        /// sets and gets Functions
        /// </summary>
        public bool Flage
        {
            set { flage = value; }
            get { return flage; }
        }
        public string StationCodeOne
        {
            get { return stationCodeOne; }
            set { stationCodeOne = value; }
        }
        public string StationCodeTwo
        {
            get { return stationCodeTwo; }
            set { stationCodeTwo = value; }
        }
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public float AverageTravelTime
        {
            get { return averageTravelTime; }
            set { averageTravelTime = value; }
        }
    }
}
