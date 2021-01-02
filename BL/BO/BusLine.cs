using System;
using System.Collections.Generic;
using System.Text;
using DO;
using static BO.Enums;

namespace BO
{
    public class BusLine 
    {
        /// <summary>
        /// sets and gets 
        /// </summary>
        public int BusLineNumber { set; get; }//Bus line number 
        public Zones Area { set; get; }//Area of the bus line
        public int ID { get; set; }//Unique ID number
        public int FirstStopNumber { set; get; }//First stop number
        public int LastStopNumber { set; get; }//Last stop number
        public bool Active { set; get; }
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
        public IEnumerable<BusLineStation> StationsInLine { set; get; }//Collection of bus line station
        public IEnumerable<LineRide> lineRides { set; get; }

    }
}