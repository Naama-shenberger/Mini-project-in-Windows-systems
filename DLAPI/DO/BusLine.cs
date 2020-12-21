using System;
using System.Collections.Generic;
using static DO.Enums;

namespace DO
{
    /// <summary>
    /// Bus line class
    /// </summary>
    public class BusLine
    {
        /// <summary>
        /// sets and gets 
        /// </summary> 
        public bool Active { get; set; }//status of a bus line whether it is active or not
        public int BusLineNumber{ set; get; }//Bus line number 
        public string FirstStopNumber {set; get; }//First stop number
        public string LastStopNumber { set; get; }//Last stop number
        public int Area{set;  get;}//Area of the bus line
        public int ID { get; set; }//Unique ID number
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bus line number: {0}\nFirst Stop: {1}  Last Stop: {2}\nArea of the bus: {3}", BusLineNumber, FirstStopNumber, LastStopNumber, (Zones)Area);
        }
    }
}
