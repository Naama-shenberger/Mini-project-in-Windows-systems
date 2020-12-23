using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class BusLineStation: BusStation
    {
        // <summary>
        /// sets and gets
        /// </summary>
        public bool Active { get; set; }// status of a bus line whether it is active or not
        public int CodeStation { get; set; }// bus station code
        public int NumberStationInLine { get; set; }//The station number in the line
        public IEnumerable<ConsecutiveStations> Consecutive_Stations { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
