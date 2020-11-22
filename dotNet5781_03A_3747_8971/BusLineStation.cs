using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_02_3747_8971
{
   
    /// <summary>
    /// BusLineStation class that Containing the class Bus statio
    /// </summary>
    class BusLineStation
    {
        /// <summary>
        /// A field that saves its time from a previous station
        /// </summary>
        TimeSpan TimebeforeStation;
        /// <summary>
        /// The class contains an object from a class BusLineStation
        /// </summary>
        public BusStation BusStationObject = new BusStation();
        public override string ToString()
        {
            return $"Bus Station Code: {BusStationObject.BUS_STATION_KEY} Landmarck: {BusStationObject.LANDMARCK.Latitude}°N  {BusStationObject.LANDMARCK.Longitude}°E {TimebeforeStation}";
        }
        /// <summary>
        /// A constructor that initializes the object BusStationObject
        ///The constructor Receiving a station code and entering the appropriate field BUS_STATION_KEY 
        /// </summary>
        /// <param name="key"></param>
        public BusLineStation(string key)
        {
            BusStationObject.BUS_STATION_KEY = key;
        }
        /// <summary>
        /// set and get for the field TimebeforeStation
        /// </summary>
        public TimeSpan TIMEBEFORESTATIOS
        {
            get { return TimebeforeStation; }
            set { TimebeforeStation = value; }
        }
        /// <summary>
        /// A function that returns the distance between 2 stations that are on the line
        /// </summary>
        /// <param name="latA"></param>
        /// <param name="longA"></param>
        /// <param name="latB"></param>
        /// <param name="longB"></param>
        /// <returns></returns>
        public static decimal DistanceBetween(double latA, double longA, double latB, double longB)
        {
            var RadianLatA = Math.PI * latA / 180;
            var RadianLatb = Math.PI * latB / 180;
            var RadianLongA = Math.PI * longA / 180;
            var RadianLongB = Math.PI * longB / 180;

            double theDistance = (Math.Sin(RadianLatA)) *
                    Math.Sin(RadianLatb) +
                    Math.Cos(RadianLatA) *
                    Math.Cos(RadianLatb) *
                    Math.Cos(RadianLongA - RadianLongB);

            return Convert.ToDecimal(((Math.Acos(theDistance) * (180.0 / Math.PI)))) * 69.09M * 1.6093M;
        }
        /// <summary>
        /// A function that returns travel time between 2 stations that are on the line
        /// Use of structure TimeSpan
        /// Fixed time between two stations 2 minutes and 30 seconds
        /// </summary>
        /// <returns></returns>
        public static TimeSpan TravelTime()
        {
            TimeSpan interval = new TimeSpan(0,2,30);
            return interval;
        }
       

    }
}



