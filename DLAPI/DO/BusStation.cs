using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// bus station class
    /// </summary>
    public class BusStation
    {
        /// <summary>
        /// get and set
        /// </summary>
        public bool Active { get; set; }//status of a bus line whether it is active or not
        public static int IdentificationNumber { set; get; }//Identification number
        public string BusStationKey { get; set; }//code Station
        public string StationAddress { get; set; }//Station Address;
        public string StationName { get; set; }//Station Name
        public double Latitude { get; set; }//Latitude 
        public double Longitude { get; set; }//Longitude 
                                             //tostring ציך

    }
}
