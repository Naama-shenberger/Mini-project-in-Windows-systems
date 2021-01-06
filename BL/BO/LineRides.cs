using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineRides
    {
        /// <summary>
        /// sets and gets
        /// </summary> 
        public bool Active { set; get; }// status of a bus line whether it is active or not
        public TimeSpan TravelStartTime { set; get; }//Travel start time of the bus 
        public TimeSpan TravelEndTime { set; get; }//Travel end time
        public static int FrequencyOfExit { set; get; }//Frequency of exit for the line 
        public int BusDepartureNumber { set; get; }//Exit number of the line
        public int ID { set; get; }
    }
}
