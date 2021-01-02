using System;
using System.Collections.Generic;
using System.Text;
namespace DO
{
    /// <summary>
    /// Bus Line Station calss
    /// </summary>
    public class BusLineStation
    {
        /// <summary>
        /// sets and gets
        /// </summary>
        public bool Active { get; set; }// status of a bus line whether it is active or not
        public static int IdentificationNumber { get; set; }//Identification number
        public int BusStationKey { get; set; }// bus station code
        public int NumberStationInLine { get; set; }//The station number in the line
    }
}
