using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    class BusLineStation
    {
        private BusStation BusStop = new BusStation();
        private TimeSpan time;
        public TimeSpan TIME
        {
            set { time = value; }
            get { return time; }
        }
        /// <summary>
        /// A function that returns the distance between 2 stations that are on the line
        /// Using the Pythagorean formula
        /// </summary>
        /// <param name="previous"></param>
        /// <returns></returns>
        static decimal DistanceBetween(double latA, double longA, double latB, double longB)
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
        /// Calculation of travel time depending on the distance between them
        /// </summary>
        /// <param name="previous"></param>
        /// <returns></returns>
        public TimeSpan TravelTime(BusLineStation previous)
        {
            int time = (int)DistanceBetween((BusStop.LANDMARCK.Latitude), (BusStop.LANDMARCK.Longitude), (previous.BusStop.LANDMARCK.Latitude), (previous.BusStop.LANDMARCK.Longitude)) / 60;
            TimeSpan interval = new TimeSpan(time);
            return interval;
        }
    }
}
