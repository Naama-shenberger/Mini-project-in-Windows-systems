using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

 namespace dotNet5781_02_3747_8971
 {
    /// <summary>
    /// Creating an Exception Class
    /// </summary>
    [Serializable]
    public class MyException : Exception
    {
        public MyException()
        { }
        public MyException(string message, int intmessage)
        { }

        public MyException(string message)
            : base(message)
        { Console.WriteLine(message); }

        public MyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
    /// <summary>
    /// enum of Zones
    /// </summary>
    enum Zones { Gush_Dan, Gush_Etzion, Galilee, Jerusalem_Corridor, Beer_Sheva, General }
    /// <summary>
    /// SingleBusLine class that heiress class BusLineStation and Implements the interface IComparable
    /// </summary>
    class SingleBusLine : IComparable<SingleBusLine>
    {
        public List<BusLineStation> Stations = new List<BusLineStation>();//Define a list of bus line stops
        string BusLine;//number line
        BusLineStation FirstStation;//Departure station
        BusLineStation LastStation;//terminus
        /// <summary>
        /// constructor that receive a line number and grills area for the line
        /// </summary>
        /// <param name="BusLineNumber"></param>
        public SingleBusLine(string BusLineNumber){ BUSLINE = BusLineNumber; Area = (Zones)BusStation.r.Next(0,6); }
        private Zones Area;
        public Zones AREA
        {
            set { Area = value; }
            get { return Area; }
        }
        public string BUSLINE
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

        public SingleBusLine Value { get; }

        /// <summary>
        /// Function override 'Tostring' 
        /// The function prints the Details of bus line by using Tostring of base class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string save = "";
            foreach (BusLineStation Station in Stations)
            {

                save += Station.BusStationObject.ToString();
                
            }
            return $"Bus Line: {BusLine}\n"+ $"Bus Area: {Area}\n"+ $"List of Stations:{save}\n";
        }
        /// <summary>
        /// The function checks if the station code is unique
        /// if the station code is unique then returns true if not returns false
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public bool UniqueTest(string check)
        {
            foreach (BusLineStation Station in Stations)
            {
                if (Station.BusStationObject.BUS_STATION_KEY == check)
                       return false;
            }
            return true;
        }
        /// <summary>
        /// The user types a number where he wants to add the station number range from 0 to size +1 list
        /// </summary>
        public void AddStation(string _code)
        {
            if (!UniqueTest(_code))
                throw new MyException("Station code already exists");
            if (_code.Length > 6)
                throw new MyException("Station number should be less than 6 digits");
            int index = BusStation.r.Next(0, this.Stations.Count() + 1);
            if (index > 1 + Stations.Count || index < 0)
                throw new ArgumentOutOfRangeException($"index should more then -1 and" + $"index should be less then or equal to {Stations.Count - 1}");
            BusLineStation AddStation = new BusLineStation(_code);
            if (Stations.Count == 0)
            {
                //If the list is empty the station is the last and first station
                FIRSTSTATION = AddStation;
                LASTSTATION = AddStation;
                AddStation.TIMEBEFORESTATIOS = BusLineStation.TravelTime();
                Stations.Add(AddStation);
            }
            if (index == Stations.Count() && Stations.Count != 0)
            {
                //Adds a station to the end of the list and updates his time from a station before
                //updates the last bus station Object
                LASTSTATION = AddStation;
                AddStation.TIMEBEFORESTATIOS = Stations[index - 1].TIMEBEFORESTATIOS + BusLineStation.TravelTime();
                Stations.Add(AddStation);
            }
            else
            {
                if (index == 0)
                {
                    AddStation.TIMEBEFORESTATIOS = BusLineStation.TravelTime();
                    FIRSTSTATION = AddStation;
                    Stations.Add(AddStation);
                    for(int i=1; i<Stations.Count; i++)
                    {
                        // updates travel time from previous station
                        Stations[i].TIMEBEFORESTATIOS+= BusLineStation.TravelTime();
                    }

                }
                else
                {
                   
                    //Adds a station to the list in a index that Lottery and updates his time from a station before
                    AddStation.TIMEBEFORESTATIOS = Stations[index - 1].TIMEBEFORESTATIOS + BusLineStation.TravelTime();
                    Stations.Insert(index, AddStation);
                    for (int i = index + 1; i < Stations.Count; i++)
                    {
                        Stations[i].TIMEBEFORESTATIOS += BusLineStation.TravelTime();
                    }

                }
            }
        }
        /// <summary>
        /// A function that deletes a station from a route, if the station is not found throws an exception
        /// And updates travel time from previous station
        /// </summary>
        public void DelStation(string _code)
        { 
            if (UniqueTest(_code))
                throw new MyException("There is no such station code");
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationObject.BUS_STATION_KEY == _code)
                {
                    Stations.Remove(Stations[i]);
                    for (int j =i; j < Stations.Count; j++)
                    {
                        Stations[j].TIMEBEFORESTATIOS -= BusLineStation.TravelTime();
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// A function that checks if a station is on a particular line route
        /// The function returns true if the station is in the line route otherwise, returns a false
        /// </summary>
        /// <returns></returns>
        public bool StationInLine(string _code)
        {
            if (!UniqueTest(_code))
                return true;
            return false;
        }
        /// <summary>
        /// Request a station code from the user and returns the item with that code station
        /// if the code is not find will throw an excption
        /// </summary>
        /// <returns></returns>
        public BusLineStation UserInput()
        {
            Console.WriteLine("enter bus station:");
            string _code = Console.ReadLine();
            foreach (BusLineStation Station in Stations)
                if (Station.BusStationObject.BUS_STATION_KEY== _code)
                    return Station;
            throw new MyException("There is no such station code");
        }
        /// <summary>
        /// A function that returns a distance between two stations
        /// </summary>
        /// <returns></returns>
        public decimal DistanceTwoStations()
        { try { return BusLineStation.DistanceBetween(UserInput().BusStationObject.LANDMARCK.Longitude, UserInput().BusStationObject.LANDMARCK.Latitude, UserInput().BusStationObject.LANDMARCK.Longitude, UserInput().BusStationObject.LANDMARCK.Latitude); } catch (MyException e) { Console.WriteLine($"ERROR:{e}");return -1; } }
        /// <summary>
        /// A function that returns travel time between 2 stations
        /// </summary>
        /// <returns></returns>
        public TimeSpan TimeTwoStations()
        {
            return UserInput().TIMEBEFORESTATIOS - UserInput().TIMEBEFORESTATIOS;

        }
        /// <summary>
        /// A function that receives 2 stations, and returns an object containing the route from station 1 to station 2
        /// </summary>
        /// <param name="stop1"></param>
        /// <param name="stop2"></param>
        /// <returns></returns>
        public SingleBusLine SubRoute(BusLineStation stop1, BusLineStation stop2)
        {
            if (UniqueTest(stop1.BusStationObject.BUS_STATION_KEY) || UniqueTest(stop2.BusStationObject.BUS_STATION_KEY))
                throw new MyException("There is no such station codes");
            int index1 = Stations.IndexOf(stop1);
            int index2 = Stations.IndexOf(stop2);
            if (index2 > Stations.Count)
                throw new ArgumentOutOfRangeException("index", "index should be less then or equal to" + Stations.Count);
            SingleBusLine Sub = new SingleBusLine("");
            for(int i=index1;i<index2;i++)
                Sub.Stations.Add(this.Stations[i]);
            return Sub;
        }  
        public double TotalTravelTime()
        {
            TimeSpan TimeTotal = new TimeSpan();
            foreach (BusLineStation item in Stations)
                TimeTotal += item.TIMEBEFORESTATIOS;
            return TimeTotal.TotalMinutes;

        }
        /// <summary>
        /// Using the interface ICloneable , implemented the function 'coperTop' on the class SingleBusLine
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(SingleBusLine obj)
        {
            if (obj is SingleBusLine)
            {
                SingleBusLine s = (SingleBusLine)obj;
                if (s.TotalTravelTime()> this.TotalTravelTime())
                    return 1;
                if (s.TotalTravelTime() == this.TotalTravelTime())
                    return 0;
                else
                    return -1;
            }
            throw new MyException("obj is not SingleBusLine");
        }
        /// <summary>
        /// Checking if one line has at least two stations
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool LeastTwoStations()
        {
            if (this.Stations.Count < 2)
                return false;
            return true;

        }
        /// <summary>
        /// A function that checks two lines with the same bus number,if they have the same stations only in reverse order
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public bool SameBusLine(SingleBusLine Bus)
        {
            if (this.LASTSTATION == Bus.FIRSTSTATION && this.FIRSTSTATION == Bus.LASTSTATION)
                return true;
            return false;
        }
        /// <summary>
        /// The function receives a string as station number and returns the station with the station number
        /// if the station does not exist will be thrown an exception
        /// </summary>
        /// <param name="_code"></param>
        /// <returns></returns>
        public BusLineStation StationSearch(string _code)
        {
            foreach(BusLineStation Station in Stations)
            {
                if (Station.BusStationObject.BUS_STATION_KEY == _code)
                    return Station;
            }
            throw new MyException("Station does not exist");
        }
    }
 }
