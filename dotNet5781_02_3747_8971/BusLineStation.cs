using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    class BusLineStation
    {
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
        public double DistanceBusLineStation(BusLineStation previous)
        {
            double a, b;
            a=this.Landmark.Latitude - previous.Landmark.Latitude;
            b=this.Landmark.Longitude - previous.Landmark.Longitude;
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
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
            int time = (int)this.DistanceBusLineStation(previous) / 60;
            TimeSpan interval = new TimeSpan(time);
            return interval;
        }
    }
}
