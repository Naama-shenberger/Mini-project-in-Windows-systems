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
        /// enum of Zones\\????
        /// </summary>
        public enum Zones { Itamar,Zeev_hill, Alon_Shvut, Gush_Dan, Gush_Etzion, Galilee, Jerusalem_Corridor, Beer_Sheva, General, Zefat,Ramat_Gan }//?
        private static string identificationNumber;//Identification number
        private int busLineNumber;//Bus line number 
        private string firstStopNumber;//First stop number
        private string lastStopNumber;//Last stop number
        private int area;//zone of the bus line
        private bool active;//A field that brings me the status of a bus line whether it is active or not
        /// <summary>
        /// sets and gets Functions
        /// </summary> 
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public string IdentificationNumber
        {
            set { identificationNumber = value; }
            get { return identificationNumber; }
        }
        public int BusLineNumber
        {
            set { busLineNumber = value; }
            get { return busLineNumber; }
        }
        public string FirstStopNumber
        {
            set { firstStopNumber = value; }
            get { return firstStopNumber; }
        }
        public string LastStopNumber
        {
            set { lastStopNumber = value; }
            get { return lastStopNumber; }
        }
        public int Area
        {
            set { area = value; }
            get { return area; }
        }
    }
}
