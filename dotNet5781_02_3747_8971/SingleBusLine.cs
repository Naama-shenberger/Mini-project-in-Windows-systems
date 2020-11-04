using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    enum 
    class SingleBusLine : BusLineStation
    {
        protected List<BusLineStation> Stations = new List<BusLineStation>();//Define a list of bus line stops
        int BusLine;//number line
        BusLineStation FirstStation;//Departure station
        BusLineStation LastStation;//terminus
        public int BUSLINE
        {
            set { BusLine = value; }
            get { return BusLine; }
        }
        public BusLineStation LASTSTATION
        {
            get { return LastStation; }
            set { LastStation = value; }
        }
        public BusLineStation FIRSTSTATION
        {
            get { return FirstStation; }
            set { FirstStation = value; }
        }
        public override string ToString()
        {
            return $"Bus Line: {BusLine}

            return base.ToString();
        }
        public void AddStation(BusLineStation stop)
        {
            if(Stations==null)
            {
                Stations.Add(stop);
            }
            else
            {
                Stations.
            }
        }
        /// <summary>
        /// A function that deletes a station from a route, if the station is not found throws an exception
        /// </summary>
        public void DelStation()
        {
            Console.WriteLine("Type a code station to delete");
            string _code = Console.ReadLine();
            bool flage = false;
            for(int i=0;i<Stations.Count;i++)
            {
                if(Stations[i].BUS_STATION_KEY == _code)
                {
                    flage = true;
                    Stations.Remove(Stations[i]);
                }
            }
            if(!flage)
            {
                //throw new Exception("code station Does not exist");
            }
        }
        /// <summary>
        /// A function that checks if a station is on a particular line route
        /// The function returns true if the station is in the line route otherwise, returns a false
        /// </summary>
        /// <returns></returns>
        public bool StationInLine()
        {
            Console.WriteLine("Type a station code to delete");
            string _code = Console.ReadLine();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BUS_STATION_KEY == _code)
                    return true;
            }
            return false;
        }
        public double DistanceTwoStations()
        {

        }

    }
}
