using dotNet5781_02_3747_8971;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_02_3747_8971
{
    /// <summary>
    /// The class containing a list of SingkeBusLine
    /// The class implements the interface IEnumerable
    /// </summary>
    class ListBusLines :IEnumerable<SingleBusLine>
    {
        public List<SingleBusLine> LineBuses = new List<SingleBusLine>();
        /// <summary>
        /// implements the interface IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerator<SingleBusLine> GetEnumerator()
        {
            return this.LineBuses.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)LineBuses).GetEnumerator();
        }
        /// <summary>
        /// A function that adds a line to a list of lines
        /// The function adds two stations to the line, to make sure that every bus line has at least 2 stops
        // The function checks that there is no other line with the same number,that they dont have an Reverse origin and destination stations
        /// </summary>
        /// <param name="id"></param>
        public void AddBusLine(string id)
        {
            SingleBusLine AddLine = new SingleBusLine(id);
            if (!AddLine.LeastTwoStations())
            {
                AddLine.AddStation(BusStation.r.Next(1, 1000001).ToString());
                AddLine.AddStation(BusStation.r.Next(1, 1000001).ToString());
            }
            if (LineBuses.Count != 0)
                if (LineBuses[LineBuses.Count - 1].BUSLINE == AddLine.BUSLINE && !LineBuses[LineBuses.Count - 1].SameBusLine(AddLine))
                throw new MyException("Two bus lines can not have the same line number if the lines dont have the Reverse end station and start station");
            LineBuses.Add(AddLine);
        }
        /// <summary>
        /// A function that receives a string as a line number and removes the line 
        /// in case the line does not exist will inject an exception
        /// </summary>
        /// <param name="id"></param>
        public void DeletBusLine(string id)
        {
            bool flage = false;
            foreach (SingleBusLine Bus in LineBuses)
                if (Bus.BUSLINE == id)
                {
                    flage = true;
                    LineBuses.Remove(Bus);
                    break;
                }
            if(!flage)
                throw new MyException("The line does not exist so it cannot be removed");
        }
        /// <summary>
        /// A function that accepts a string as a station number
        /// the function returns a list of lines that pass through the station
        /// if there are no lines passing through the station an exception will be thrown
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SingleBusLine> FinedID(string id)
        {
            List<SingleBusLine> LinesStation = new List<SingleBusLine>();
            foreach (SingleBusLine Bus in LineBuses)
                if (Bus.StationInLine(id))
                    LinesStation.Add(Bus);
            if(LinesStation.Count!=0)
                return LinesStation;
           throw new MyException("There are no lines passing through the station");
        }
        /// <summary>
        /// the Indexer- recipient numbers a line and returns the show. If there is no such line an anomaly will be thrown
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SingleBusLine this[string busLine]
        {
            get
            {
                foreach (SingleBusLine Bus in LineBuses)
                    if (Bus.BUSLINE == busLine)
                              return Bus;
                throw new MyException("The line does not exist");
            }
            set
            {
                foreach (SingleBusLine Bus in LineBuses)
                    if (Bus.BUSLINE == busLine)
                        LineBuses[LineBuses.IndexOf(Bus)] = value;
            }

        }
        /// <summary>
        /// A method that returns a list of all lines sorted by total travel time, from short to long
        /// The function returns a list of lines
        /// </summary>
        /// <returns></returns>
        public List<SingleBusLine> SortedLines()
        {
            LineBuses.Sort();
            return LineBuses;
        }
        /// <summary>
        /// A method that returns true if there are 2 stations with the same station number and the same location
        /// else the method will return false
        /// The test is done on each line in his list of stations compared to the last line with his stations
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SameStation(string code)
        {
            foreach (SingleBusLine BusOne in LineBuses)
            {  
                    foreach (BusLineStation StationOne in BusOne.Stations)
                    {
                        foreach (BusLineStation StationTwo in LineBuses[LineBuses.Count - 1].Stations)
                        {
                            if (BusOne.StationInLine(code) && LineBuses[LineBuses.Count - 1].StationInLine(code))
                            {
                                if (StationOne.BusStationObject.BUS_STATION_KEY == code && StationTwo.BusStationObject.BUS_STATION_KEY == code)
                                {
                                    if (StationOne.BusStationObject.LANDMARCK.Latitude == StationTwo.BusStationObject.LANDMARCK.Latitude && StationOne.BusStationObject.LANDMARCK.Longitude == StationTwo.BusStationObject.LANDMARCK.Longitude)
                                        return true;
                                    else
                                        return false;
                                }

                            }
                        }
                    }
                
            }
            return true;   
        }
        public ListBusLines GetAllBusLines()
        {
            ListBusLines Anew = new ListBusLines();
            Anew.LineBuses= LineBuses;
            return Anew;
        }
    }
}







