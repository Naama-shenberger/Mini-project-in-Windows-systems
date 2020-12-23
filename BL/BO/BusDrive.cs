using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
     public class BusDrive:Bus
    {
        public int ID { get; set; }//License Number (Entity Identifying Feature, Unique)
        public TimeSpan ExitStart { get; set; }//Actual departure time
        public int LastStasion { get; set; }//Last stop number on the line that the bus passed
        public TimeSpan TimeDrive { get; set; }//Transit time at the last stop mentioned above//????
        public TimeSpan TimeNextStop { get; set; }
        public string BusDriverFirstName { get; set; }
        public string BusDriverLastName { get; set; }
        public string BusDriverId { get; set; }
        public IEnumerable<ConsecutiveStations> Consecutive_Bus_Drive { set; get; }//Collection of bus in drive
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

    }
}
