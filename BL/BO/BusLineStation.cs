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
       public int ID { get; set; }
       public float Distance { set; get; }//Distance between stations 
       public TimeSpan AverageTravelTime { set; get; }//Average travel time between stations
       /// <summary>
       /// override of ToString
       /// </summary>
       /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
    }
}