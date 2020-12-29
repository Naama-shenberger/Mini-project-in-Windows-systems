using System;
using System.Collections.Generic;
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
        public static int IdentificationNumber { set; get; }//Identification number
        public int BusLineNumber { set; get; }//Bus line number 
        public string FirstStopNumber { set; get; }//First stop number
        public string LastStopNumber { set; get; }//Last stop number
        public int Area { set; get; }//Area of the bus line
        public int ID { set; get; }
    }
}
