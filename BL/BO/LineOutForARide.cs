using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace BO
{
    public class LineOutForARide:BusLine
    {
        /// <summary>
        /// sets and gets
        /// </summary> 
        public new int ID { set; get; }//id for line on his way out //??
        public bool Active { set; get; }// status of a bus line whether it is active or not
        public TimeSpan TravelEndTime { set; get; }//Travel end time
        public TimeSpan FrequencyOfExit { set; get; }//Frequency of exit for the line
        public TimeSpan ExitStart { set; get; }//Travel Exit Start
        public int BusDepartureNumber { set; get; }//Exit number of the line
        public override string ToString() => this.ToStringProperty();
    }
}
