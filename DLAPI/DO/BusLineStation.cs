using System;
using System.Collections.Generic;
using System.Text;
namespace DO
{
    /// <summary>
    /// Bus Line Station calss
    /// This class constitutes a data contract of the dl layer
    /// </summary>
    public class BusLineStation
    {
        /// <summary>
        /// sets and gets
        /// </summary>
        public bool Active { get; set; }// status of a bus line whether it is active or not
        /// <summary>
        /// A unique key of the department consists of two fields:
        /// Station code and bus line Id
        /// </summary>
        public int BusStationKey { get; set; }// bus station code
        public int ID { get; set; }//bus line id 
        public int NumberStationInLine { get; set; }//The station number in the line
      
    }
}
