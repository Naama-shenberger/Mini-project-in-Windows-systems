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
            this.Latitude = previous.Latitude - this.Latitude; ///קו רוחב 
            this.Longitude = previous.Longitude - this.Longitude;//קו אורך
       }
       public DateTime TravelTime(BusLineStation previous)
       {

       }
    }
}
