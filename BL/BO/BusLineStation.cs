using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class BusLineStation 
    {
       public int NumberStationInLine { get; set; }
       public bool Active { get; set; }//status of a bus line whether it is active or not
       public int BusStationKey { get; set; }//code Station
       public string StationAddress { get; set; }//Station Address;
       public string StationName { get; set; }//Station Name

    }
}