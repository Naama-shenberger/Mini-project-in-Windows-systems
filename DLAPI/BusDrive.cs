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
        public int BusIdOnTheGo { get; set; }//License Number (Entity Identifying Feature, Unique)
        public string LicensePlate { get; set; }//the license plate
        public TimeSpan ExitStart { get; set; }//Actual departure time
        public int LastStasion { get; set; }//Last stop number on the line that the bus passed
        public TimeSpan TimeDrive { get; set; }//Transit time at the last stop mentioned above//
        public TimeSpan TimeNextStop { get; set; }//Time Next Stop
        public string BusDriverFirstName { get; set; }//Bus Driver First Name 
        public string BusDriverLastName { get; set; }//Bus Driver Last Name
        public string BusDriverId { get; set; }//Bus Driver Id
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bus ID number: {0}\n License Plate: {1}  Exit time:  {2}\n Time Drive:{3}\n Time to Next Stop:{4}\n Last Stasion: {5}\n  Bus Driver Id:{6}\nBus Driver Name:{7} {8}\n ", BusIdOnTheGo, LicensePlate, ExitStart, TimeDrive, TimeNextStop, LastStasion, BusDriverId, BusDriverLastName, BusDriverFirstName);
        }
    }

}
