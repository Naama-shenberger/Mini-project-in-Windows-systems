using System;
using System.Collections.Generic;
using System.Text;
namespace DO
{
    /// <summary>
    /// Line OutFor A Ride class-Departure for a trip
    /// </summary>
    public class LineRide
    {
        /// <summary>
        /// sets and gets
        /// </summary> 
        public bool Active { set; get; }// status of a bus line whether it is active or not
        public TimeSpan TravelStartTime { set; get; }//Travel start time of the bus 
        public TimeSpan TravelEndTime { set; get; }//Travel end time
        public TimeSpan BusDepartureNumber { set; get; }//Exit number of the line
        public int ID { set; get; }

    }
}

