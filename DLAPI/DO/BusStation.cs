using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{

    /// <summary>
    /// bus station class
    /// This class constitutes a data contract of the dl layer
    /// </summary>
    public class BusStation
    {
        /// <summary>
        /// get and set
        /// </summary>
        public bool Active { get; set; }//status of a bus station whether it is active or not
        public int BusStationKey { get; set; }//code Station unique key
        public string StationAddress { get; set; }//Station Address
        public string StationName { get; set; }//Station Name
        public double Latitude { get; set; }//Latitude 
        public double Longitude { get; set; }//Longitude 

    }
}

