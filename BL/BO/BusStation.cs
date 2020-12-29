using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class BusStation
    {
        /// <summary>
        /// get and set
        /// </summary>
        public bool Active { get; set; }//status of a bus line whether it is active or not
        public int BusStationKey { get; set; }//code Station
        public string StationAddress { get; set; }//Station Address;
        public string StationName { get; set; }//Station Name
        public IEnumerable<BusLineInStation> ListBusLinesInStation { set; get; }//Collection of lines in bus stations
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();


    }
}