using DO;
using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class ConsecutiveStations: BusLineStation
    {
        /// <summary>
        /// sets and gets
        /// </summary>
        public bool Flage { set; get; }//for Deletion
        public int StationCodeOne { set; get; }//Code station one
        public int StationCodeTwo { set; get; }//Code station Two
        public float Distance { set; get; }//Distance between stations
        public TimeSpan AverageTravelTime { set; get; }//Average travel time between stations
        public override string ToString() => this.ToStringProperty();
    }
}
