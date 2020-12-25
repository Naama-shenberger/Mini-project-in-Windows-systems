using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class StationInLine
    {
        public int IDBusLine { get; set; }//number line
        public int BusStationKey { get; set; }//code Station
        public string StationAddress { get; set; }//Station Address;
        public string StationName { get; set; }//Station Name
        public bool FlageActive { get; set; }
    }
}
