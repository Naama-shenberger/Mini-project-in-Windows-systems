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
        public TimeSpan TimeDrive { get; set; }//Transit time at the last stop mentioned above//????
        public TimeSpan TimeNextStop { get; set; }
        public string BusDriverFirstName { get; set; }
        public string BusDriverLastName { get; set; }
        public string BusDriverId { get; set; }
    }

}
