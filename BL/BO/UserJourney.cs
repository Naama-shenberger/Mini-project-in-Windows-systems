using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class UserJourney : User
    { 
        /// <summary>
        /// sets and gets
        /// </summary>
        public int BusLineJourney { set; get; }//bus line number of the ride
        public string BoardingStation { set; get; }//Id Boarding Station
        public string DropStation { get; set; }//id Drop Station
        public TimeSpan StartJourneyTime { get; set; }//Time Start Journey
        public TimeSpan EndJourneyTime { get; set; }//Time end Journey
        public override string ToString() => this.ToStringProperty();
    }
}
