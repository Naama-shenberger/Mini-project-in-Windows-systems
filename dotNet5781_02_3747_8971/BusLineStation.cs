using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    class BusLineStation: BusStation
    {
       public double DistanceBusLineStation(BusLineStation previous)
       {
            double a, b;
            a=this.Landmark.Latitude - previous.Landmark.Latitude;///קו רוחב
            b=this.Landmark.Longitude - previous.Landmark.Longitude;//קו אורך
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
       public DateTime TravelTime(BusLineStation previous)
       {
            double distance = DistanceBusLineStation(previous);

       }
    }
}
