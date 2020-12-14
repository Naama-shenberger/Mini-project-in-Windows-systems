using System;
using System.Collections.Generic;
using System.Text;
using DALApi;
using DO;
using DS;

namespace DAL
{
    internal sealed class DalObject : IDal
    {
        //בגרסה הסופית אין להשתמש בלולאת foreach במקום שניתן להשתמש ב-LINQ ---נשתמש עכשיו בפור ואז נשנה 
        #region singelton
        //create instance
        static DalObject instance;
        public static DalObject Instance
        {
            get
            {
                if (instance == null)
                    instance = new DalObject();
                return instance;
            }
        }
        /// <summary>
        /// static ctor
        /// </summary>
        static DalObject() { }
        /// <summary>
        /// private ctor
        /// </summary>
        private DalObject() { }
        #endregion
        #region BusLine Functions
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus line object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLine getBusLine(int id)
        {
            foreach(BusLine item in DataSource.BusLines)
            {
                if (item.IdentificationNumber == id.ToString())
                    return item;
            }
            throw new ArgumentException("The bus line does not exist");
           
        }
        /// <summary>
        /// A function that returns a list of bus lines that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> BusLines()
        {
            List<BusLine> temp = new List<BusLine>();
            if (DataSource.BusLines != null)
                foreach (BusLine item in DataSource.BusLines)
                    if (item.Active == true)
                        temp.Add(item);
                if (temp != null)
                   return temp;
            throw new NullReferenceException("There is no Bus lines");
        }
        /// <summary>
        /// A function that receives an bus line object and adds it to the list 
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="b"></param>
        public void addBusLine(BusLine b)
        {
            foreach (BusLine item in DataSource.BusLines)
                if (item.IdentificationNumber == b.IdentificationNumber)
                    if (item.Active == false)
                        item.Active = true;
                    else
                        throw new ArgumentException("The bus line already exist");
            DataSource.BusLines.Add(b);
        }
        /// <summary>
        /// A function that receives a bus line and updates its details
        /// </summary>
        /// <param name="b"></param>
        public void updateBusLine(BusLine b)
        {
            for (int i=0;i<DataSource.BusLines.Count;i++)
            {
                if (DataSource.BusLines[i].IdentificationNumber == b.IdentificationNumber)
                    DataSource.BusLines[i] = b;
            }
            throw new ArgumentException("The bus line does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus line is already not active we will throw a message
        /// </summary>
        /// <param name="b"></param>
        public void deleteBusLine(BusLine b)
        {
            foreach (BusLine item in DataSource.BusLines)
            {
                if (item.IdentificationNumber == b.IdentificationNumber)
                    if (item.Active == true)
                        item.Active = false;
                    else
                        throw new ArithmeticException("The bus is already not active");
            }
            throw new ArgumentException("The bus line does not exist");
        }
        #endregion 
        #region BusLineStation Functions
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus line station object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLineStation getBusLineStation(int id)
        {
            foreach (BusLineStation item in DataSource.BusLineStations)
            {
                if (item.IdentificationNumber == id.ToString())
                    return item;
            }
            throw new ArgumentException("The bus line station does not exist");

        }
        /// <summary>
        ///  A function that returns a list of bus line stations that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineStation> BusLineStations()
        {
            List<BusLineStation> temp = new List<BusLineStation>();
            if (DataSource.BusLineStations != null)
                foreach (BusLineStation item in DataSource.BusLineStations)
                    if (item.Active == true)
                        temp.Add(item);
                if (temp != null)
                return temp;
            throw new NullReferenceException("There is no Bus lines");
        }
        /// <summary>
        /// A function that receives an bus line station object and adds it to the list
        /// In case the bus station is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="s"></param>
        public void addBusLineStation(BusLineStation s)
        {
            foreach (BusLineStation item in DataSource.BusLineStations)
                if (item.IdentificationNumber == s.IdentificationNumber)
                    if (item.Active == false)
                        item.Active = true;
                    else
                        throw new ArgumentException("The bus line already exist");
            DataSource.BusLineStations.Add(s);
        }
        /// <summary>
        /// A function that receives a bus line Station and updates its details
        /// </summary>
        /// <param name="s"></param>
        public void updateBusLineStation(BusLineStation s)
        {
            for (int i = 0; i < DataSource.BusLineStations.Count; i++)
            {
                if (DataSource.BusLineStations[i].IdentificationNumber == s.IdentificationNumber)
                    DataSource.BusLineStations[i] = s;
            }
            throw new ArgumentException("The bus line does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus line station is already not active we will throw a message
        /// </summary>
        /// <param name="s"></param>
        public void deleteBusLineStation(BusLineStation s)
        {
            foreach (BusLineStation item in DataSource.BusLineStations)
            {
                if (item.IdentificationNumber == s.IdentificationNumber)
                    if (item.Active == true)
                        item.Active = false;
                    else
                        throw new ArithmeticException("The bus is already not active");
            }
            throw new ArgumentException("The bus line does not exist");
        }
        #endregion
        #region LineWayOut
        /// <summary>
        /// A function that receives an ID number and returns the object with that ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LineWayOut getLineWayOut(int id)
        {
            foreach (LineWayOut item in DataSource.LinesWayOut)
            {
                if (item.IdentificationNumber == id.ToString())
                    return item;
            }
            throw new ArgumentException("The  line does not exist");
        }
        /// <summary>
        /// A function that returns a list of bus line  on their way out to a ride that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineWayOut> LinesWayOut()
        {
            List<LineWayOut> temp = new List<LineWayOut>();
            if (DataSource.LinesWayOut != null)
                foreach (LineWayOut item in DataSource.LinesWayOut)
                    if (item.Active == true)
                        temp.Add(item);
                if (temp != null)
                     return temp;
            throw new NullReferenceException("There is no Bus lines");
        }
        /// <summary>
        ///  A function that receives an bus line on is way out  object and adds it to the list
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="o"></param>
        public void addLineWayOut(LineWayOut o)
        {
            foreach (LineWayOut item in DataSource.LinesWayOut)
                if (item.IdentificationNumber == o.IdentificationNumber)
                    if (item.Active == false)
                        item.Active = true;
                    else
                        throw new ArgumentException("The line already exist");
            DataSource.LinesWayOut.Add(o);
        }
        /// <summary>
        /// A function that receives a bus line on his way out and updates its details
        /// </summary>
        /// <param name="o"></param>
        public void updateLineWayOut(LineWayOut o)
        {
            for (int i = 0; i < DataSource.LinesWayOut.Count; i++)
            {
                if (DataSource.LinesWayOut[i].IdentificationNumber == o.IdentificationNumber)
                    DataSource.LinesWayOut[i] = o;
            }
            throw new ArgumentException("The  line does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="o"></param>
        public void deleteLineWayOut(LineWayOut o)
        {
            foreach (LineWayOut item in DataSource.LinesWayOut)
            {
                if (item.IdentificationNumber == o.IdentificationNumber)
                    if (item.Active == true)
                        item.Active = false;
                    else
                        throw new ArithmeticException("The line is already not active");
            }
            throw new ArgumentException("The line does not exist");
        }
        /// <summary>
        /// function that Receive An exit object for travel
        /// the function returns the total travel time of this port
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public TimeSpan TravelTime(LineWayOut o)
        {
            return o.TravelStartTime - o.TravelEndTime;
        }
        #endregion
        #region ConsecutiveStations
        /// <summary>
        /// Receives station codes and returns the appropriate object
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public ConsecutiveStations getConsecutiveStations(string id1,string id2)
        {
            foreach (ConsecutiveStations item in DataSource.ListConsecutiveStations)
                if (item.StationCodeOne == id1 && item.StationCodeTwo == id2)
                    return item;
            throw new ArgumentException("There is no Consecutive Stations in the list");
        }
        /// <summary>
        /// A function that returns a list of two nearby stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsecutiveStations> ConsecutivesStations()
        {
            List<ConsecutiveStations> temp = new List<ConsecutiveStations>();
               if(DataSource.ListConsecutiveStations != null)
                 foreach (ConsecutiveStations item in DataSource.ListConsecutiveStations)
                    if (item.Flage == true)
                        temp.Add(item);
                 if(temp!=null)
                  return temp;
            throw new NullReferenceException("There is Consecutives Stations");
        }
        /// <summary>
        /// A function that Getting an object puts it on the list
        //  Provided he does not exist in the list
        /// </summary>
        /// <param name="c"></param>
        public void addConsecutiveStations(ConsecutiveStations c)
        {
            foreach (ConsecutiveStations item in DataSource.ListConsecutiveStations)
                if (item == c)
                    if (item.Flage == false)
                        item.Flage = true;
                    else
                        throw new ArgumentException("The Consecutives Stations already exist");
            DataSource.ListConsecutiveStations.Add(c);
        }
        /// <summary>
        ///  A function that receives a ConsecutiveStations object updates its details
        /// </summary>
        /// <param name="c"></param>
        public void updateConsecutiveStations(ConsecutiveStations c)
        {
            for (int i = 0; i < DataSource.ListConsecutiveStations.Count; i++)
            {
                if (DataSource.ListConsecutiveStations[i] == c)
                    DataSource.ListConsecutiveStations[i] = c;
            }
            throw new ArgumentException("The Consecutives Stations does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="c"></param>
        public void deleteConsecutiveStations(ConsecutiveStations c)
        {
            foreach (ConsecutiveStations item in DataSource.ListConsecutiveStations)
            {
                if (item== c)
                    if (item.Flage == true)
                        item.Flage = false;
                    else
                        throw new ArithmeticException("The Consecutive Stationsis already not active");
            }
            throw new ArgumentException("The Consecutive Stations does not exist");
        }
        #endregion

    }
}