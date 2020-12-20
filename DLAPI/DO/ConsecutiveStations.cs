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
        /// <summary>
        /// sets and gets
        /// </summary>
        public bool Flage { set; get; }//for Deletion
        public string StationCodeOne { set; get; }//Code station one
        public string StationCodeTwo { set; get; }//Code station Two
        public float Distance { set; get; }//Distance between stations
        public float AverageTravelTime { set; get; }//Average travel time between stations
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Station Code One:{0} Station Code Two:{1} Distance:{2} Average Travel Time:{3}", StationCodeOne,StationCodeTwo,Distance, AverageTravelTime);
        }
    }
}
