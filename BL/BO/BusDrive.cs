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
        public TimeSpan TimeDrive { get; set; }//Transit time at the last stop mentioned above
        public TimeSpan TimeNextStop { get; set; }//Time to Next Stop 
        public string BusDriverFirstName { get; set; }//Bus Driver First Name 
        public string BusDriverLastName { get; set; }//Bus Drive rLast Name
        public string BusDriverId { get; set; }//Bus Driver ID
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

    }
}
