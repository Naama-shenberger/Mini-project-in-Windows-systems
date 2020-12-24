using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class thet is for a bus is traveling
    /// </summary>
    public class BusDrive
    {
        /// <summary>
        /// get and set
        /// </summary>
        public bool Active { get; set; }//status of a bus whether it is active or not
        public int ID { get; set; }//License Number (Entity Identifying Feature, Unique)
        public string LicensePlate { get; set; }//the license plate
        public TimeSpan ExitStart { get; set; }//Actual departure time
        public int LastStasion { get; set; }//Last stop number on the line that the bus passed
        public TimeSpan TimeDrive { get; set; }//Actual time at the last stop mentioned above//????
        public TimeSpan TimeNextStop { get; set; }//time to next stop
        public string BusDriverFirstName { get; set; }//Bus Driver First Name
        public string BusDriverLastName { get; set; }//Bus Driver Last Name
        public string BusDriverId { get; set; }// Bus Driver ID
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("\nLicensePlate: {0}\nExit Start: {1}\nLastStasion: {2}\nTime Drive: {3}\nTime to Next Stop:{4}\n bus driver info:\nID number-{5}\nfull name-{7} {6}\n", LicensePlate, ExitStart, LastStasion, TimeDrive, TimeNextStop, BusDriverId, BusDriverLastName, BusDriverFirstName);
        }
    }

}
