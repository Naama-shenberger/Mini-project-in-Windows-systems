using System;
using System.Collections.Generic;
using System.Text;
namespace DO
{
    /// <summary>
    /// Line OutFor A Ride class-Departure for a trip
    /// </summary>
    public class LineOutForARide
    {
        /// <summary>
        /// sets and gets
        /// </summary> 
        public int ID { set; get; }//id for line on his way out 
        public bool Active {set; get; }// status of a bus line whether it is active or not
        public TimeSpan TravelEndTime { set; get; }//Travel end time
        public TimeSpan FrequencyOfExit { set; get; }//Frequency of exit for the line 
        public int BusDepartureNumber { set; get; }//Exit number of the line
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bus line number: {0}\n Travel End Time{1} Frequency Of Exit line{2}", TravelEndTime, FrequencyOfExit);
        }
    }
}
