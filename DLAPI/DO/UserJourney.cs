using System;
namespace DO
{
    /// <summary>
    ///  User Journey class
    /// </summary>
    public class UserJourney
    {
        /// <summary>
        /// sets and gets
        /// </summary>
        public string UserName { set; get; }//user name
        public int BusLineJourney { set; get; }//bus line number of the ride
        public string BoardingStation { set; get; }//Id Boarding Station
        public string DropStation { get; set; }//id Drop Station
        public TimeSpan StartJourneyTime { get; set; }//Time Start Journey
        public TimeSpan EndJourneyTime { get; set; }//Time end Journey
        public bool FlageActive { set; get; }//A flage for Deletion
    }
}