using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace dotNet5781_02_3747_8971
{
    enum Zones { Gush_Dan, Gush_Etzion, Galilee, Jerusalem_Corridor, plain, General }
    class SingleBusLine : BusLineStation, IComparable
    {
        protected List<BusLineStation> Stations = new List<BusLineStation>();//Define a list of bus line stops
        int BusLine;//number line
        BusLineStation FirstStation;//Departure station
        BusLineStation LastStation;//terminus
        private Zones Area;
        public Zones AREA
        {
            set { Random r = new Random();  Area=(Zones)r.Next(-1, 6); }
            get { return Area; }
        }
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
        /// <summary>
        /// Function override 'Tostring' 
        /// The function prints the Details of bus line by using Tostring of base class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string save = "";
            foreach (BusLineStation item in Stations)
            {
                save += item.ToString();//base.ToString();
            }
            return $"Bus Line: {BusLine}, Bus Area: {Area}, List of Stations:{save}";
        }
        public bool UniqueTest(string check)
        {
            foreach (BusLineStation item in Stations)
            {
                if (item.BusStop.BUS_STATION_KEY == check)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// The user types a number where he wants to add the station number range from 0 to list size (+1, to add to the end of the list)
        /// </summary>
        public void AddStation()
        {
            BusLineStation stop= new BusLineStation();
            Console.WriteLine("enter bus station to add:");
            string _code = Console.ReadLine();
            if (!UniqueTest(_code))
                //    throw
                stop.BusStop.BUS_STATION_KEY = _code;
            Console.WriteLine($"Type a number between 0-{Stations.Count()} that you want to insert the station," +
                $" if you want the stop to be the last in the list enter {Stations.Count() + 1} ");
            int index;
            int.TryParse(Console.ReadLine(), out index);
            Stations.Insert(index, stop);
        }
        /// <summary>
        /// A function that deletes a station from a route, if the station is not found throws an exception
        /// </summary>
        public void DelStation()
        {
            Console.WriteLine("Type a code station to delete");
            string _code = Console.ReadLine();
            if (UniqueTest(_code))
                //    throw
                for (int i = 0; i < Stations.Count; i++)
                {
                    if (Stations[i].BusStop.BUS_STATION_KEY == _code)
                    {
                        Stations.Remove(Stations[i]);
                    }
                }
        }
        /// <summary>
        /// A function that checks if a station is on a particular line route
        /// The function returns true if the station is in the line route otherwise, returns a false
        /// </summary>
        /// <returns></returns>
        public bool StationInLine()
        {
            Console.WriteLine("Type a station code:");
            string _code = Console.ReadLine();
            if (!UniqueTest(_code))
                return true;
            return false;
        }
        public BusLineStation UserInput()
        {

            Console.WriteLine("enter bus station:");
            string _code = Console.ReadLine();
            if (UniqueTest(_code))
            {
                //  throw;
            }
            BusLineStation stop = new BusLineStation();
            stop.BusStop.BUS_STATION_KEY = _code;
            return stop;
        }
        /// <summary>
        /// A function that returns a distance between two stations
        /// </summary>
        /// <returns></returns>
        public decimal DistanceTwoStations()=>DistanceBetween(UserInput().BusStop.LANDMARCK.Latitude, UserInput().BusStop.LANDMARCK.Longitude, UserInput().BusStop.LANDMARCK.Latitude, UserInput().BusStop.LANDMARCK.Longitude);
        /// <summary>
        /// A function that returns travel time between 2 stations
        /// </summary>
        /// <returns></returns>
        public TimeSpan TimeTwoStations()
        {
            return UserInput().TravelTime(UserInput());
        }
        /// <summary>
        /// A function that receives 2 stations, and returns an object containing the route from station 1 to station 2
        /// </summary>
        /// <param name="stop1"></param>
        /// <param name="stop2"></param>
        /// <returns></returns>
        public SingleBusLine SubRoute(BusLineStation stop1, BusLineStation stop2)
        {
            if (!UniqueTest(stop1.BusStop.BUS_STATION_KEY) || !UniqueTest(stop2.BusStop.BUS_STATION_KEY))
            {
                //  throw;
            }
            int save1 = Stations.IndexOf(stop1);
            int save2 = Stations.IndexOf(stop2);
            int counter = 0;
            SingleBusLine Sub = new SingleBusLine();
            while (counter != save2)
            {
                Sub.Stations[counter] = this.Stations[save1];
                counter++;
                save1++;
            }
            // Sub.Stations=this.Stations.GetRange(save1, save2);
            return Sub;
        }
        /// <summary>
        /// Using the interface ICloneable , implemented the function 'coperTop' on the class SingleBusLine
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is SingleBusLine)
            {
                SingleBusLine s = (SingleBusLine)obj;
                if (s.TimeTwoStations() > this.TimeTwoStations())
                    return 1;
                if (s.TimeTwoStations() == this.TimeTwoStations())
                    return 0;
                else
                    return -1;
            }
            throw new Exception("error");
        }
        /// <summary>
        /// Checking if one line has at least two stations
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool leastTwoStations(SingleBusLine s)
        {
            if (s.Stations.Count < 2)
                return false;
            return true;

        }
       
    }
}