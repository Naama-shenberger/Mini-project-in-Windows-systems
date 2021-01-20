using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// A class that has information on consecutive stations
    /// This class constitutes a data contract of the dl layer
    /// </summary>
    public class ConsecutiveStations
    {
        /// <summary>
        /// sets and gets
        /// </summary>
        public bool Flage { set; get; }//for Deletion,true if the ststions are active
        public int StationCodeOne { set; get; }//Code station one
        public int StationCodeTwo { set; get; }//Code station Two
        public float Distance { set; get; }//Distance between stations
        public TimeSpan AverageTravelTime { set; get; }//Average travel time between stations
    }
}
