using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalApi;
using DO;
using DS;

namespace DL
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
        #region Bus Function
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bus getBus(int id)
        {
            Bus bus= DataSource.Buses.Find(b => b.LicensePlate == id.ToString());
            if (bus != null)
            {
                if (bus.Active == true)
                    return bus  /*.Clone()*/;
            }
            throw new IdAlreadyExistsException(id, $"No bus line Station have the Code:{id}");
        }
        /// <summary>
        ///  A function that returns a list of bus that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> Bus()
        {
            List<Bus> temp = new List<Bus>();
            if (DataSource.Buses != null)
                foreach (Bus item in DataSource.Buses)
                    if (item.Active == true)
                        temp.Add(item);
            if (temp != null)
                return temp;
            throw new NullReferenceException("There is no Bus");
        }
        /// <summary>
        /// A function that receives an bus  object and adds it to the list
        /// There is a comparison between unique parameters of the bus to see if the bus exists
        /// If the bus has the same bus id code is the same bus,
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="b"></param>
        public void addBus(Bus b)
        {
            Bus newBus = DataSource.Buses.FirstOrDefault(x => x.LicensePlate == b.LicensePlate);
            if (newBus != null)
            {
                if (newBus.Active == false)
                {
                    newBus.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(b.LicensePlate, $"The bus line station {b.LicensePlate} already exist");
            }
            DataSource.Buses.Add(newBus/*.Clone()*/);//נצטרך לממש בDl
        }
        /// <summary>
        /// A function that receives a bus and updates its details
        /// </summary>
        /// <param name="b"></param>
        public void updateBus(Bus b)
        {
            var toUpdate = DataSource.Buses.SingleOrDefault(x => x.LicensePlate == b.LicensePlate);
            if (toUpdate != null)
            {
                if (toUpdate.Active == true)
                    toUpdate = b;
                else
                    throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");
            }
            else
                throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public void deleteBus(Bus b)
        {
            var toDelete = DataSource.Buses.SingleOrDefault(x => x.LicensePlate == b.LicensePlate);
            if (toDelete != null)
            {
                if (toDelete.Active == true)
                    toDelete.Active = false;
                else
                    throw new IdAlreadyExistsException(b.LicensePlate, $"The bus in drive {b.LicensePlate} is already deleted");
            }
            else
                throw new IdAlreadyExistsException(b.LicensePlate, $"The bus in drive {b.LicensePlate} does not exist");
        }
        /// <summary>
        /// A function that checks if the vehicle needs to be refueled
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool FuelCondition(Bus b)
        {
            Bus find = DataSource.Buses.FirstOrDefault(x => x.LicensePlate == b.LicensePlate);
            if (find.KilometersGas > 1200)
                return true;
            return false;
        }
        /// <summary>
        /// Function that checks if the bus needs treatment - 
        /// treatment bus defined if the bus has traveled more than 20,000 kilometers or a year has passed since the last treatment
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool TreatmentIsNeeded(Bus b)
        {
            Bus find = DataSource.Buses.FirstOrDefault(x => x.LicensePlate == b.LicensePlate);
            if (!(find.KilometersTreatment < 2000 && !dateCheck(b)))
                return true;
            return false;
        }
        /// <summary>
        /// A function that checks if a year has passed since the last treatment
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool dateCheck(Bus b)
        {
            Bus find = DataSource.Buses.FirstOrDefault(x => x.LicensePlate == b.LicensePlate);
            int day;
            int month;
            int year;
            int.TryParse(find.DateTreatment.Day.ToString(), out day);
            int.TryParse(find.DateTreatment.Month.ToString(), out month);
            int.TryParse(find.DateTreatment.Year.ToString(), out year);
            DateTime currentDate = DateTime.Now;
            if (int.Parse(currentDate.Day.ToString()) == day && int.Parse(currentDate.Month.ToString()) == month && int.Parse(currentDate.Year.ToString()) < year || int.Parse(currentDate.Year.ToString()) < year)
                return true;
            return false;
        }
        /// <summary>
        /// A function that checks how many numbers the user needs to type for the number plate
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int NumberOflicensePlate(Bus b)
        {
            Bus find = DataSource.Buses.FirstOrDefault(x => x.LicensePlate == b.LicensePlate);
            int year;
            int.TryParse(find.DateActivity.Year.ToString(), out year);
            return year < 2018 ? 7 : 8;
        }
        #endregion
        #region BusDrive Function
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus drive object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusDrive getBusDrive(int id)
        {
            BusDrive busDrive = DataSource.BusDrives.Find(b => b.BusIdOnTheGo == id);
            if (busDrive != null)
            {
                if (busDrive.Active == true)
                    return busDrive  /*.Clone()*/;
            }
            throw new IdAlreadyExistsException(id, $"No bus in drive have the Code:{id}");
        }
        /// <summary>
        ///  A function that returns a list of bus drive that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusDrive> BusDrives()
        {
            List<BusDrive> temp = new List<BusDrive>();
            if (DataSource.BusDrives != null)
                foreach (BusDrive item in DataSource.BusDrives)
                    if (item.Active == true)
                        temp.Add(item);
            if (temp != null)
                return temp;
            throw new NullReferenceException("There is no Bus in drive");
        }
        /// <summary>
        /// A function that receives an bus drive object and adds it to the list
        /// In case the bus drive is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="s"></param>
        public void addBusDrive(BusDrive s)
        {

            Configuration.IdentificationNumberBusDrive+= 1;
            s.BusIdOnTheGo = Configuration.IdentificationNumberBusDrive;
            if (DataSource.BusLines == null)
            {
                DataSource.createLists();
            }
            BusDrive newBusDrive = DataSource.BusDrives.FirstOrDefault(b => b.BusIdOnTheGo == s.BusIdOnTheGo);
            if (newBusDrive != null)
            {
                if (newBusDrive.Active == false)
                {
                    newBusDrive.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(s.LicensePlate, $"The bus line {s.LicensePlate} already exist");
            }
            DataSource.BusDrives.Add(s/*.Clone()*/);//נצטרך לממש בDl
        }
        /// <summary>
        /// A function that receives a bus drive and updates its details
        /// </summary>
        /// <param name="s"></param>
        public void updateBusDrive(BusDrive s)
        {
            var toUpdate = DataSource.BusDrives.SingleOrDefault(x => x.BusIdOnTheGo == s.BusIdOnTheGo);
            if (toUpdate != null)
            {
                if (toUpdate.Active == true)
                    toUpdate = s;
                else
                    throw new IdAlreadyExistsException(s.BusIdOnTheGo, $"The bus in drive{s.BusIdOnTheGo} does not exist");
            }
            else
                throw new IdAlreadyExistsException(s.BusIdOnTheGo, $"The bus in drive {s.BusIdOnTheGo} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus drive is already not active we will throw a message
        /// </summary>
        /// <param name="s"></param>
        public void deleteBusDrive(BusDrive s)
        {
            var toDelete = DataSource.BusDrives.SingleOrDefault(x => x.BusIdOnTheGo == s.BusIdOnTheGo);
            if (toDelete != null)
                if (toDelete.Active == true)
                    toDelete.Active = false;
                else
                    throw new IdAlreadyExistsException(s.BusIdOnTheGo, $"The bus in drive {s.BusIdOnTheGo} is already deleted");
            else
                throw new IdAlreadyExistsException(s.BusIdOnTheGo, $"The bus in drive {s.BusIdOnTheGo} does not exist");
        }

        #endregion
        #region BusStation Function
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus station object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusStation getBusStation(int id)
        {
            BusStation busStation = DataSource.Stations.Find(b => b.BusStationKey == id.ToString());
            if (busStation != null)
                if (busStation.Active == true)
                    return busStation  /*.Clone()*/;
            throw new IdAlreadyExistsException(id, $"No bus station have the key:{id}");
        }
        /// <summary>
        ///  A function that returns a list of bus stations that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusStation> BusStations()
        {
            List<BusStation> temp = new List<BusStation>();
            if (DataSource.Stations != null)
                foreach (BusStation item in DataSource.Stations)
                    if (item.Active == true)
                        temp.Add(item);
            if (temp != null)
                return temp;
            throw new NullReferenceException("There is no Bus station");
        }
        /// <summary>
        /// A function that receives an bus station object and adds it to the list 
        /// There is a comparison between unique parameters of the bus to see if the bus station exists
        /// If the bus has the same bus station number and the same name and address it is the same line,
        /// because if it was the Reverse  line the stop codes would be reversed
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="b"></param>
        public void addBusStation(BusStation b)
        {
            BusStation newBusStation = DataSource.Stations.FirstOrDefault(x => x.BusStationKey == b.BusStationKey);
            if (newBusStation != null)
                if (newBusStation.Active == false)
                {
                    newBusStation.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(b.BusStationKey, $"The bus station {b.BusStationKey} already exist");
            DataSource.Stations.Add(newBusStation/*.Clone()*/);//נצטרך לממש בDl
        }
        public void updateBusStation(BusStation b)
        {
            var toUpdate = DataSource.Stations.SingleOrDefault(x => x.BusStationKey == b.BusStationKey);
            if (toUpdate != null)
            {
                if (toUpdate.Active == true)
                    toUpdate = b;
                else
                    throw new IdAlreadyExistsException(b.BusStationKey, $"The bus station{b.BusStationKey} does not exist");
            }
            else
                throw new IdAlreadyExistsException(b.BusStationKey, $"The bus station {b.BusStationKey} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// If the bus has the same bus station number and the same name and address it is the same line,
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the  bus station  is already not active we will throw a message
        /// </summary>
        /// <param name="b"></param>
        public void deleteBusStation(BusStation b)
        {
            var toDelete = DataSource.Stations.SingleOrDefault(x => x.BusStationKey == b.BusStationKey);
            if (toDelete != null)
                if (toDelete.Active == true)
                    toDelete.Active = false;
                else
                    throw new IdAlreadyExistsException(b.BusStationKey, $"The bus station {b.BusStationKey} is already deleted");
            else
                throw new IdAlreadyExistsException(b.BusStationKey, $"The bus station {b.BusStationKey} does not exist");
        }
        #endregion
        #region BusLine Functions
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus line object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLine getBusLine(int id)
        {
            BusLine busLine = DataSource.BusLines.Find(b => b.ID == id);
            if (busLine != null)
                if(busLine.Active==true)
                    return busLine/*.Clone()*/;
            throw new IdAlreadyExistsException(id, $"No bus line have the id:{id}");
        }
        /// <summary>
        /// A function that returns a list of bus lines that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> BusLines()
        {
            //return from busLine in DataSource.BusLines;
            //       select busLine/*.Clone()*/;
            IEnumerable<BusLine> temp = DataSource.BusLines;
            foreach (BusLine item in DataSource.BusLines)
                if (item.Active != true)
                    DataSource.BusLines.Remove(item);
            IEnumerable<BusLine> ActiveBues = DataSource.BusLines;
            DataSource.BusLines = (List<BusLine>)temp;
            if (ActiveBues != null)
                return ActiveBues;
            throw new NullReferenceException("There is no Bus lines");
        }
        /// <summary>
        /// A function that receives an bus line object and adds it to the list 
        /// There is a comparison between unique parameters of the bus to see if the bus line exists
        /// If the bus has the same line number and the first code stop and last stop code it is the same line,
        /// because if it was the Reverse  line the stop codes would be reversed
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="b"></param>
        public void addBusLine(BusLine bus)
        {
            

            Configuration.IdentificationNumberBusLine += 1;
            bus.ID = Configuration.IdentificationNumberBusLine;
            if(DataSource.BusLines==null)
            {
                DataSource.createLists();
            }
            BusLine newBus = DataSource.BusLines.FirstOrDefault(b => b.ID == bus.ID);
            if (newBus != null)
                if (newBus.Active == false)
                {
                    newBus.Active = true;
                    return;
                }
               else
                throw new DO.IdAlreadyExistsException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} already exist");
           DataSource.BusLines.Add(bus/*.Clone()*/);//נצטרך לממש בDl
        }
        /// <summary>
        /// A function that receives a bus line and updates its details
        /// </summary>
        /// <param name="b"></param>
        public void updateBusLine(BusLine bus)
        {
            var toUpdate = DataSource.BusLines.SingleOrDefault(x => x.ID == bus.ID);
            if (toUpdate != null)
                if(toUpdate.Active==true)
                     toUpdate = bus;
                else
                    throw new IdAlreadyExistsException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} does not exist");
            else
                throw new IdAlreadyExistsException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus line is already not active we will throw a message
        /// </summary>
        /// <param name="b"></param>
        public void deleteBusLine(BusLine b)
        {
            var toDelete = DataSource.BusLines.SingleOrDefault(x => x.ID == b.ID);
            if (toDelete != null)
                if (toDelete.Active == true)
                    toDelete.Active = false;
                else
                    throw new IdAlreadyExistsException(b.BusLineNumber, $"The bus line {b.BusLineNumber} is already deleted");
            else
                throw new IdAlreadyExistsException(b.BusLineNumber, $"The bus line {b.BusLineNumber} does not exist");
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
            BusLineStation busLineStation = DataSource.BusLineStations.Find(b => b.CodeStation == id.ToString());
            if (busLineStation != null)
                if(busLineStation.Active==true)
                    return busLineStation /*.Clone()*/;
            throw new IdAlreadyExistsException(id, $"No bus line Station have the Code:{id}");
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
            BusLineStation newBusStation = DataSource.BusLineStations.FirstOrDefault(b => b.CodeStation == s.CodeStation);
            if (newBusStation != null)
                if (newBusStation.Active == false)
                {
                    newBusStation.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(s.CodeStation, $"The bus line station {s.CodeStation} already exist");
            DataSource.BusLineStations.Add(newBusStation/*.Clone()*/);//נצטרך לממש בDl
        }
        /// <summary>
        /// A function that receives a bus line Station and updates its details
        /// </summary>
        /// <param name="s"></param>
        public void updateBusLineStation(BusLineStation s)
        {
            var toUpdate = DataSource.BusLineStations.SingleOrDefault(x => x.CodeStation == s.CodeStation);
            if (toUpdate != null)
            {
                if(toUpdate.Active==true)
                      toUpdate = s;
                else
                    throw new IdAlreadyExistsException(s.CodeStation, $"The bus line Station {s.CodeStation} does not exist");
            }
            else
                throw new IdAlreadyExistsException(s.CodeStation, $"The bus line Station {s.CodeStation} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus line station is already not active we will throw a message
        /// </summary>
        /// <param name="s"></param>
        public void deleteBusLineStation(BusLineStation s)
        {
            var toDelete = DataSource.BusLineStations.SingleOrDefault(x => x.CodeStation == s.CodeStation);
            if (toDelete != null)
                if (toDelete.Active == true)
                    toDelete.Active = false;
                else
                    throw new IdAlreadyExistsException(s.CodeStation, $"The bus line Station {s.CodeStation} is already deleted");
            else
                throw new IdAlreadyExistsException(s.CodeStation, $"The bus line Station {s.CodeStation} does not exist");
        }
        #endregion
        #region LineOutForARide
        /// <summary>
        /// A function that receives an ID number and returns the object with that ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LineOutForARide getLineWayOut(int id)
        {
            LineOutForARide busLine = DataSource.LinesOutForARide.Find(b => b.ID == id);
            if (busLine != null)
                if (busLine.Active == true)
                    return busLine/*.Clone()*/;
            throw new IdAlreadyExistsException(id, $"No bus line have the id:{id}");
        }
        /// <summary>
        /// A function that returns a list of bus line  on their way out to a ride that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineOutForARide> LinesWayOut()
        {
            List<LineOutForARide> temp = new List<LineOutForARide>();
            if (DataSource.LinesOutForARide != null)
                foreach (LineOutForARide item in DataSource.LinesOutForARide)
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
        public void addLineWayOut(LineOutForARide o)
        {
            Configuration.IdentificationNumberBusLine += 1;
            o.ID = Configuration.IdentificationNumberBusLine;
            LineOutForARide newBusWayOut = DataSource.LinesOutForARide.FirstOrDefault(b => b.ID == o.ID);
            if (newBusWayOut != null)
                if (newBusWayOut.Active == false)
                {
                    newBusWayOut.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(o.ID, $"The bus line {o.ID} already exist");
            DataSource.LinesOutForARide.Add(newBusWayOut/*.Clone()*/);//נצטרך לממש בDl
        }
        /// <summary>
        /// A function that receives a bus line on his way out and updates its details
        /// </summary>
        /// <param name="o"></param>
        public void updateLineWayOut(LineOutForARide o)
        {
            var toUpdate = DataSource.LinesOutForARide.SingleOrDefault(x => x.ID == o.ID);
            if (toUpdate != null)
                if (toUpdate.Active == true)
                    toUpdate = o;
                else
                    throw new IdAlreadyExistsException(o.ID, $"The bus line {o.ID} does not exist");
            else
                throw new IdAlreadyExistsException(o.ID, $"The bus line {o.ID} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="o"></param>
        public void deleteLineWayOut(LineOutForARide o)
        {
            var toDelete = DataSource.LinesOutForARide.SingleOrDefault(x => x.ID == o.ID);
            if (toDelete != null)
                if (toDelete.Active == true)
                    toDelete.Active = false;
                else
                    throw new IdAlreadyExistsException(o.ID, $"The bus line {o.ID} is already deleted");
            else
                throw new IdAlreadyExistsException(o.ID, $"The bus line {o.ID} does not exist");
        }
        /// <summary>
        /// function that Receive An exit object for travel
        /// the function returns the total travel time of this port
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public TimeSpan TravelTime(LineOutForARide o)
        {
            return /*ExitStart -*/ o.TravelEndTime;
        }
        #endregion
        #region ConsecutiveStations
        /// <summary>
        /// Receives station codes and returns the appropriate object
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public ConsecutiveStations getConsecutiveStations(string id1, string id2)
        {
            var consecutiveStations = DataSource.ListConsecutiveStations.Find(b => b.StationCodeOne == id1 && b.StationCodeTwo==id2);
            if (consecutiveStations != null)
                if (consecutiveStations.Flage == true)
                    return consecutiveStations/*.Clone()*/;
            throw new IdAlreadyExistsException($"No consecutive Stations have the id`s:{id1} {id2}");
        }
        /// <summary>
        /// A function that returns a list of two nearby stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsecutiveStations> ConsecutivesStations()
        {
            List<ConsecutiveStations> temp = new List<ConsecutiveStations>();
            if (DataSource.ListConsecutiveStations != null)
                foreach (ConsecutiveStations item in DataSource.ListConsecutiveStations)
                    if (item.Flage == true)
                        temp.Add(item);
            if (temp != null)
                return temp;
            throw new NullReferenceException("There is Consecutives Stations");
        }
        /// <summary>
        /// A function that Getting an object puts it on the list
        //  Provided he does not exist in the list
        /// </summary>
        /// <param name="c"></param>
        public void addConsecutiveStations(ConsecutiveStations s)
        {
            var consecutiveStations = DataSource.ListConsecutiveStations.FirstOrDefault(b=>b.StationCodeOne == s.StationCodeOne && b.StationCodeTwo==s.StationCodeTwo);
            if (consecutiveStations != null)
                if (consecutiveStations.Flage== false)
                {
                    consecutiveStations.Flage= true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException($"The Consecutives Stations {s.StationCodeOne} {s.StationCodeTwo} already exist");
            DataSource.ListConsecutiveStations.Add(consecutiveStations /*.Clone()*/);//נצטרך לממש בDl
        }
        /// <summary>
        ///  A function that receives a ConsecutiveStations object updates its details
        /// </summary>
        /// <param name="c"></param>
        public void updateConsecutiveStations(ConsecutiveStations c)
        {
            var toUpdate = DataSource.ListConsecutiveStations.SingleOrDefault(x => x.StationCodeOne == c.StationCodeOne && x.StationCodeTwo==c.StationCodeTwo);
            if (toUpdate != null)
                if (toUpdate.Flage == true)
                    toUpdate = c;
                else
                    throw new DO.IdAlreadyExistsException($"The Consecutives Stations {c.StationCodeOne} {c.StationCodeTwo} doesn't exist");
            else
                throw new DO.IdAlreadyExistsException($"The Consecutives Stations {c.StationCodeOne} {c.StationCodeTwo} doesn't exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="c"></param>
        public void deleteConsecutiveStations(ConsecutiveStations c)
        {
            var toDelete = DataSource.ListConsecutiveStations.SingleOrDefault(x => x.StationCodeOne==c.StationCodeOne && x.StationCodeTwo==c.StationCodeTwo);
            if (toDelete != null)
                if (toDelete.Flage == true)
                    toDelete.Flage = false;
                else
                    throw new DO.IdAlreadyExistsException($"The Consecutives Stations {c.StationCodeOne} {c.StationCodeTwo} doesn't exist");
            else
                throw new DO.IdAlreadyExistsException($"The Consecutives Stations {c.StationCodeOne} {c.StationCodeTwo} doesn't exist");
        }

        public int lastBusStation()
        {
            throw new NotImplementedException();
        }

        public TimeSpan TimeDrive()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}