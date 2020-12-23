using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class BusStation
    {
        public static int IdentificationNumber { set; get; }//Identification number
        public string BusStationKey { get; set; }//code Station
        public string StationAddress { get; set; }//Station Address;
        public string StationName { get; set; }//Station Name
        public double Latitude { get; set; }//Latitude 
        public double Longitude { get; set; }//Longitude 
        public IEnumerable<ConsecutiveStations> Consecutive_Bus_station { set; get; }//Collection of bus stations
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
        //רשימה של קווים שעוברים בתחנה פה 

    }
}
