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
        public TimeSpan BusDepartureNumber { set; get; }//Exit number of the line
        public  int BusLineNumber { get; set; }//bus line number
        public int CodeLastStasion { get; set; }//code last stasion
        public string NameLastStasion { get; set; }//Last Stasion name
        public TimeSpan CurTimeStasion { get; set; }// current Time Drive
        public int ID { set; get; }//id of bus line ride
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

    }
}
