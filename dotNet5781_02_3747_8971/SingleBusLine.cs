using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    //enum 
    class SingleBusLine : BusLineStation
    {
        protected List<BusLineStation> Stations = new List<BusLineStation>();//Define a list of bus line stops
        int BusLine;//number line
        BusLineStation FirstStation;//Departure station
        BusLineStation LastStation;//terminus
        public override string ToString()
        {

            return base.ToString();
        }


    }
}
