using System;
using System.Collections.Generic;
using System.Text;
namespace DO
{
    /// <summary>
    /// Line way out class-Departure for a trip
    /// </summary>
    public class LineWayOut
    {
       
        private static string identificationNumber;//Identification number
        private TimeSpan travelStartTime;//Travel start time of the bus 
        private TimeSpan travelEndTime;//Travel end time
        static int frequencyOfExit;//Frequency of exit for the line 
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
            get { return identificationNumber; }
            set { identificationNumber = value; }
        }
        public TimeSpan TravelStartTime
        {
            get { return travelStartTime; }
            set { travelStartTime = value; }
        }
        public TimeSpan TravelEndTime
        {
            get { return travelEndTime; }
            set { travelEndTime = value; }
        }
        public int FrequencyOfExit
        {
            get { return frequencyOfExit; }
            set { frequencyOfExit = value; }
        }

    }
}
