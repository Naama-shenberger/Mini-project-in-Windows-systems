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
        /// A function that uses Encapsulates a method 
        /// accepts an integer and returns an object
        /// integer-bus License Plate
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusNum(Func<int, object> generate)
        {
            return from Bus in DataSource.Buses
                   select generate(int.Parse(Bus.LicensePlate));
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bus GetBus(int id)
        {
            Bus bus = DataSource.Buses.Find(b => b.LicensePlate == id.ToString());
            if (bus != null)
            {
                if (bus.Active == true)
                    return bus.Clone();
            }
            throw new IdAlreadyExistsException(id, $"No bus line Station have the Code:{id}");
        }
        /// <summary>
        ///  A function that returns a list of bus that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> Buss()
        {
            return from Bus in DataSource.Buses
                   where Bus.Active == true
                   select Bus.Clone();
        }
        /// <summary>
        /// A function that receives an bus  object and adds it to the list
        /// There is a comparison between unique parameters of the bus to see if the bus exists
        /// If the bus has the same bus id code is the same bus,
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="b"></param>
        public void AddBus(Bus b)
        {
            Bus newBus = DataSource.Buses.SingleOrDefault(s => s.LicensePlate== b.LicensePlate);
            if (newBus!= null)
                if (newBus.Active == false)
                {
                    newBus.Active = true;
                    return;
                }
                else
                    throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} already exist");
            DataSource.Buses.Add(b.Clone());
        }
        /// <summary>
        /// A function that receives a bus and updates its details
        /// </summary>
        /// <param name="b"></param>
        public void UpdateBus(Bus b)
        {
            var toUpdateIndex = DataSource.Buses.FindIndex(s => s.LicensePlate == b.LicensePlate);
            if (toUpdateIndex != -1)
                if (b.Active == true)
                    DataSource.Buses[toUpdateIndex] = b.Clone();
                else
                    throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");
            else
                throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public void DeleteBus(Bus b)
        {
            var toDeleteIndex = DataSource.Buses.FindIndex(s => s.LicensePlate == b.LicensePlate);
            if (toDeleteIndex != -1)
                if (b.Active == true)
                    DataSource.Buses[toDeleteIndex].Active = false;
                else
                    throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} is already deleted");
            else
                throw new IdAlreadyExistsException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");
            
        }
       
        #endregion
        #region BusDrive Function
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus drive object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusDrive GetBusDrive(int id)
        {
            BusDrive busDrive = DataSource.BusDrives.Find(b => b.ID == id);
            if (busDrive != null)
                if (busDrive.Active == true)
                    return busDrive.Clone();
            throw new IdAlreadyExistsException(id, $"No bus have the id: {id}");
        }
        /// <summary>
        ///  A function that returns a list of bus drive that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusDrive> BussDrive()
        {
            return from BusDrive in DataSource.BusDrives
                   where BusDrive.Active == true
                   select BusDrive.Clone();
        }
        /// <summary>
        /// A function that receives an bus drive object and adds it to the list
        /// In case the bus drive is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="bus"></param>
        public void AddBusDrive(BusDrive bus)
        {
            
            Configuration.IdentificationNumberBusDrive += 1;
           bus.ID = Configuration.IdentificationNumberBusDrive;
            BusDrive newBusDrive = DataSource.BusDrives.FirstOrDefault(b => b.ID== bus.ID);
            if (newBusDrive != null)
                if (newBusDrive.Active == false)
                {
                    newBusDrive.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} already exist");
            DataSource.BusDrives.Add(bus.Clone());
        }
        /// <summary>
        /// A function that receives a bus drive and updates its details
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusDrive(BusDrive bus)
        {
            var toUpdateIndex = DataSource.BusDrives.FindIndex(b => b.ID == bus.ID);
            if (toUpdateIndex != -1)
                if (bus.Active == true)
                    DataSource.BusDrives[toUpdateIndex] = bus.Clone();
                else
                    throw new IdAlreadyExistsException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} does not exist");
            else
                throw new IdAlreadyExistsException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus drive is already not active we will throw a message
        /// </summary>
        /// <param name="s"></param>
        public void DeleteBusDrive(BusDrive bus)
        {
            var toDeleteIndex = DataSource.BusDrives.FindIndex(b => b.ID == bus.ID);
            if (toDeleteIndex != -1)
                if (bus.Active == true)
                    DataSource.BusLines[toDeleteIndex].Active = false;
                else
                    throw new IdAlreadyExistsException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} is already deleted");
            else
                throw new IdAlreadyExistsException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} does not exist");
        }

        #endregion
        #region BusStation Function
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus station object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusStation GetBusStation(string id)
        {
            BusStation busStation = DataSource.Stations.Find(s => s.BusStationKey == id);
            if (busStation != null)
                if (busStation.Active == true)
                    return busStation.Clone();
            throw new IdAlreadyExistsException(id, $"No bus Station have the Code: {id}");
        }
        /// <summary>
        ///  A function that returns a list of bus stations that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusStation> BusStations()
        {
            return from BusStation in DataSource.Stations
                   where BusStation.Active == true
                   select BusStation.Clone();
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
        public void AddBusStation(BusStation station)
        {
            BusStation newBusStation = DataSource.Stations.SingleOrDefault(s => s.BusStationKey == station.BusStationKey);
            if (newBusStation != null)
                if (newBusStation.Active == false)
                {
                    newBusStation.Active = true;
                    return;
                }
                else
                    throw new IdAlreadyExistsException(station.BusStationKey, $"The bus station {station.BusStationKey} already exist");
            DataSource.Stations.Add(station.Clone());

        }
        /// <summary>
        /// A function that receives a bus Station and updates its details
        /// </summary>
        /// <param name="station"></param>
        public void UpdateBusStation(BusStation station)
        {
            var toUpdateIndex = DataSource.Stations.FindIndex(s => s.BusStationKey == station.BusStationKey);
            if (toUpdateIndex != -1)
                if (station.Active == true)
                    DataSource.Stations[toUpdateIndex] = station.Clone();
                else
                    throw new IdAlreadyExistsException(station.BusStationKey, $"The bus Station {station.BusStationKey} does not exist");
            else
                throw new IdAlreadyExistsException(station.BusStationKey, $"The bus Station {station.BusStationKey} does not exist");

        }
        /// <summary>
        /// The function gets an object to delete
        /// If the bus has the same bus station number and the same name and address it is the same line,
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the  bus station  is already not active we will throw a message
        /// </summary>
        /// <param name="b"></param>
        public void DeleteBusStation(BusStation station)
        {
            var toDeleteIndex = DataSource.Stations.FindIndex(s => s.BusStationKey == station.BusStationKey);
            if (toDeleteIndex != -1)
                if (station.Active == true)
                    DataSource.BusLineStations[toDeleteIndex].Active = false;
                else
                    throw new IdAlreadyExistsException(station.BusStationKey, $"The bus Station {station.BusStationKey} is already deleted");
            else
                throw new IdAlreadyExistsException(station.BusStationKey, $"The bus Station {station.BusStationKey} does not exist");

        }
        /// <summary>
        /// A function that uses Encapsulates a method 
        /// accepts an integer and returns an object
        /// integer-bus station number
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusStatinNumbers(Func<int, object> generate)
        {
            return from BusS in DataSource.Stations
                   select generate(int.Parse(BusS.BusStationKey));
        }
        #endregion
        #region BusLine Functions
        /// <summary>
        /// A function that uses Encapsulates a method 
        /// accepts an integer and a string and returns an object
        /// integer-bus line number
        /// string-License Plate
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusLineNumbers(Func<int, string, object> generate)
        {
            return from BusLine in DataSource.BusLines
                   select generate(BusLine.BusLineNumber, GetBus(BusLine.ID).LicensePlate);
        }
        /// <summary>
        /// A function that uses Encapsulates a method 
        /// accepts an integer and returns an object
        /// integer-bus line number
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusLineNumbers(Func<int, object> generate)
        {
            return from BusLine in DataSource.BusLines
                   select generate(BusLine.BusLineNumber);
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus line object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLine GetBusLine(int id)
        {
            BusLine busLine = DataSource.BusLines.Find(b => b.ID == id);
            if (busLine != null)
                if (busLine.Active == true)
                    return busLine.Clone();
            throw new IdAlreadyExistsException(id, $"No bus line have the id: {id}");
        }
        /// <summary>
        /// A function that returns a list of bus lines that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> BusLines()
        {
            return from BusLine in DataSource.BusLines
                   where BusLine.Active == true
                   select BusLine.Clone();
        }
        /// <summary>
        /// A function that receives an bus line object and adds it to the list 
        /// There is a comparison between unique parameters of the bus to see if the bus line exists
        /// If the bus has the same line number and the first code stop and last stop code it is the same line,
        /// because if it was the Reverse  line the stop codes would be reversed
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="bus"></param>
        public void AddBusLine(BusLine bus)
        {
            Configuration.IdentificationNumberBusLine += 1;
            bus.ID = Configuration.IdentificationNumberBusLine;
            BusLine newBus = DataSource.BusLines.FirstOrDefault(b => b.ID == bus.ID);
            if (newBus != null)
                if (newBus.Active == false)
                {
                    newBus.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} already exist");
            DataSource.BusLines.Add(bus.Clone());
        }
        /// <summary>
        /// A function that receives a bus line and updates its details
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusLine(BusLine bus)
        {
            var toUpdateIndex = DataSource.BusLines.FindIndex(b => b.ID == bus.ID);
            if (toUpdateIndex != -1)
                if (bus.Active == true)
                    DataSource.BusLines[toUpdateIndex] = bus.Clone();
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
        /// <param name="bus"></param>
        public void DeleteBusLine(BusLine bus)
        {
            var toDeleteIndex = DataSource.BusLines.FindIndex(b => b.ID == bus.ID);
            if (toDeleteIndex != -1)
                if (bus.Active == true)
                    DataSource.BusLines[toDeleteIndex].Active = false;
                else
                    throw new IdAlreadyExistsException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} is already deleted");
            else
                throw new IdAlreadyExistsException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} does not exist");
        }
        #endregion 
        #region BusLineStation Functions
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus line station object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLineStation GetBusLineStation(int id)
        {
            BusLineStation busLineStation = DataSource.BusLineStations.Find(s => s.CodeStation == id);
            if (busLineStation != null)
                if (busLineStation.Active == true)
                    return busLineStation.Clone();
            throw new IdAlreadyExistsException(id, $"No bus line Station have the Code: {id}");
        }
        /// <summary>
        ///  A function that returns a list of bus line stations that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineStation> BusLineStations()
        {
            return from BusLineStation in DataSource.BusLineStations
                   where BusLineStation.Active == true
                   select BusLineStation.Clone();
        }
        /// <summary>
        /// A function that receives an bus line station object and adds it to the list
        /// In case the bus station is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="station"></param>
        public void AddBusLineStation(BusLineStation station)
        {
            BusLineStation newBusStation = DataSource.BusLineStations.SingleOrDefault(s => s.CodeStation == station.CodeStation);
            if (newBusStation != null)
                if (newBusStation.Active == false)
                {
                    newBusStation.Active = true;
                    return;
                }
                else
                    throw new IdAlreadyExistsException(station.CodeStation, $"The bus line station {station.CodeStation} already exist");
            DataSource.BusLineStations.Add(station.Clone());
        }
        /// <summary>
        /// A function that receives a bus line Station and updates its details
        /// </summary>
        /// <param name="station"></param>
        public void UpdateBusLineStation(BusLineStation station)
        {
            var toUpdateIndex = DataSource.BusLineStations.FindIndex(s => s.CodeStation == station.CodeStation);
            if (toUpdateIndex != -1)
                if (station.Active == true)
                    DataSource.BusLineStations[toUpdateIndex] = station.Clone();
                else
                    throw new IdAlreadyExistsException(station.CodeStation, $"The bus line Station {station.CodeStation} does not exist");
            else
                throw new IdAlreadyExistsException(station.CodeStation, $"The bus line Station {station.CodeStation} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus line station is already not active we will throw a message
        /// </summary>
        /// <param name="station"></param>
        public void DeleteBusLineStation(BusLineStation station)
        {
            var toDeleteIndex = DataSource.BusLineStations.FindIndex(s => s.CodeStation == station.CodeStation);
            if (toDeleteIndex != -1)
                if (station.Active == true)
                    DataSource.BusLineStations[toDeleteIndex].Active = false;
                else
                    throw new IdAlreadyExistsException(station.CodeStation, $"The bus line Station {station.CodeStation} is already deleted");
            else
                throw new IdAlreadyExistsException(station.CodeStation, $"The bus line Station {station.CodeStation} does not exist");
        }
        /// <summary>
        /// A function that uses Encapsulates a method 
        /// accepts an integer and returns an object
        /// integer-bus line Code Station
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusLineStationCode(Func<int, object> generate)
        {
            return from BusLineStation in DataSource.BusLineStations
                   select generate(BusLineStation.CodeStation);
        }
        #endregion
        #region LineOutForARide
        /// <summary>
        /// A function that receives an ID number and returns the object with that ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LineOutForARide GetLineWayOut(int id)
        {
            LineOutForARide busLine = DataSource.LinesOutForARide.Find(b => b.ID == id);
            if (busLine != null)
                if (busLine.Active == true)
                    return busLine.Clone();
            throw new IdAlreadyExistsException(id, $"No bus line have the id:{id}");
        }
        /// <summary>
        /// A function that returns a list of bus line  on their way out to a ride that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineOutForARide> LinesWayOut()
        {
            return from LinesOutForARide in DataSource.LinesOutForARide
                   where LinesOutForARide.Active == true
                   select LinesOutForARide.Clone();
        }
        /// <summary>
        ///  A function that receives an bus line on is way out  object and adds it to the list
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="o"></param>
        public void AddLineWayOut(LineOutForARide o)
        {
            Configuration.IdentificationNumberBusLine += 1;
            o.ID = Configuration.IdentificationNumberBusLine;
            LineOutForARide newBusWayOut = DataSource.LinesOutForARide.SingleOrDefault(b => b.ID == o.ID);
            if (newBusWayOut != null)
                if (newBusWayOut.Active == false)
                {
                    newBusWayOut.Active = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(o.ID, $"The bus line {o.ID} already exist");
            DataSource.LinesOutForARide.Add(newBusWayOut.Clone());
        }
        /// <summary>
        /// A function that receives a bus line on his way out and updates its details
        /// </summary>
        /// <param name="outLine"></param>
        public void UpdateLineWayOut(LineOutForARide outLine)
        {
            var toUpdateIndex = DataSource.LinesOutForARide.FindIndex(o => o.ID == outLine.ID);
            if (toUpdateIndex != -1)
                if (outLine.Active == true)
                    DataSource.LinesOutForARide[toUpdateIndex] = outLine.Clone();
                else
                    throw new IdAlreadyExistsException(outLine.ID, $"The bus line {outLine.ID} does not exist");
            else
                throw new IdAlreadyExistsException(outLine.ID, $"The bus line {outLine.ID} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="toDeleteIndex"></param>
        public void DeleteLineWayOut(LineOutForARide outLine)
        {
            var toDeleteIndex = DataSource.LinesOutForARide.FindIndex(o => o.ID == outLine.ID);
            if (toDeleteIndex != -1)
                if (outLine.Active == true)
                    DataSource.LinesOutForARide[toDeleteIndex].Active = false;
                else
                    throw new IdAlreadyExistsException(outLine.ID, $"The bus line {outLine.ID} is already deleted");
            else
                throw new IdAlreadyExistsException(outLine.ID, $"The bus line {outLine.ID} does not exist");
        }
        #endregion
        #region ConsecutiveStations
        /// <summary>
        /// Receives station codes and returns the appropriate object
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public ConsecutiveStations GetConsecutiveStations(int id1, int id2)
        {
            var consecutiveStations = DataSource.ListConsecutiveStations.Find(b => b.StationCodeOne == id1 && b.StationCodeTwo == id2);
            if (consecutiveStations != null)
                if (consecutiveStations.Flage == true)
                    return consecutiveStations.Clone();
            throw new IdAlreadyExistsException($"No consecutive Stations have the id`s:{id1} {id2}");
        }
        /// <summary>
        /// A function that returns a list of two nearby stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsecutiveStations> ConsecutivesStations()
        {
            return from ConsecutiveStations in DataSource.ListConsecutiveStations
                   where ConsecutiveStations.Flage == true
                   select ConsecutiveStations.Clone();
        }
        /// <summary>
        /// A function that Getting an object puts it on the list
        //  Provided he does not exist in the list
        /// </summary>
        /// <param name="stations"></param>
        public void AddConsecutiveStations(ConsecutiveStations stations)
        {
            var consecutiveStations = DataSource.ListConsecutiveStations.FirstOrDefault(s => s.StationCodeOne == stations.StationCodeOne && s.StationCodeTwo == stations.StationCodeTwo);
            if (consecutiveStations != null)
                if (consecutiveStations.Flage == false)
                {
                    consecutiveStations.Flage = true;
                    return;
                }
                else
                    throw new IdAlreadyExistsException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} already exist");
            DataSource.ListConsecutiveStations.Add(consecutiveStations.Clone());
        }
        /// <summary>
        ///  A function that receives a ConsecutiveStations object updates its details
        /// </summary>
        /// <param name="stations"></param>
        public void UpdateConsecutiveStations(ConsecutiveStations stations)
        {
            var toUpdateIndex = DataSource.ListConsecutiveStations.FindIndex(s => s.StationCodeOne == stations.StationCodeOne && s.StationCodeTwo == stations.StationCodeTwo);
            if (toUpdateIndex != -1)
                if (stations.Flage == true)
                    DataSource.ListConsecutiveStations[toUpdateIndex] = stations.Clone();
                else
                    throw new DO.IdAlreadyExistsException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
            else
                throw new DO.IdAlreadyExistsException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="stations"></param>
        public void DeleteConsecutiveStations(ConsecutiveStations stations)
        {
            var toDeleteIndex = DataSource.ListConsecutiveStations.FindIndex(s => s.StationCodeOne == stations.StationCodeOne && s.StationCodeTwo == stations.StationCodeTwo);
            if (toDeleteIndex != -1)
                if (stations.Flage == true)
                    DataSource.ListConsecutiveStations[toDeleteIndex].Flage = false;
                else
                    throw new DO.IdAlreadyExistsException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
            else
                throw new DO.IdAlreadyExistsException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
        }
        #endregion
        #region User
        public User GetUser(string id)
        {
            var user = DataSource.Users.Find(u=>u.UserName == id);
            if (user != null)
                if (user.DelUser == true)
                    return user.Clone();
            throw new IdAlreadyExistsException($"No user have the name {id}");
        }
        public IEnumerable<User> GetUsers()
        {
            return from User in DataSource.Users
                   where User.DelUser == true
                   select User;
        }
        public void AddUser(User user)
        {
            var userIndex = DataSource.Users.FindIndex(u => u.UserName== user.UserName);
            if (userIndex != -1)
                if (DataSource.Users[userIndex].DelUser == false)
                {
                    DataSource.Users[userIndex].DelUser = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(user.UserName, $"The User Name {user.UserName} already exist");
            DataSource.Users.Add(user.Clone());
        }
        public void UpdatUser(User user)
        {
            var toUpdateIndex = DataSource.Users.FindIndex(u => u.UserName== user.UserName);
            if (toUpdateIndex != -1)
                if (DataSource.Users[toUpdateIndex].DelUser == true)
                    DataSource.Users[toUpdateIndex] = user.Clone();
                else
                    throw new IdAlreadyExistsException(user.UserName, $"The user name {user.UserName} does not exist");
            else
                throw new IdAlreadyExistsException(user.UserName, $"The user name {user.UserName} does not exist");
        }
        public void DeleteUser(User user)
        {
            var toDeleteIndex = DataSource.Users.FindIndex(u=>u.UserName==user.UserName);
            if (toDeleteIndex != -1)
                if (DataSource.Users[toDeleteIndex].DelUser == true)
                    DataSource.Users[toDeleteIndex].DelUser = false;
                else
                    throw new IdAlreadyExistsException(user.UserName, $"The user name {user.UserName} is already deleted");
            else
                throw new IdAlreadyExistsException(user.UserName, $"The user name {user.UserName} is already deleted");
        }
        #endregion
        #region UserJourney
        public UserJourney GetUserJourney(string id)
        {
            var userJourney = DataSource.UsersJourney.Find(u => u.UserName == id);
            if (userJourney != null)
                if (userJourney.FlageActive == true)
                    return userJourney.Clone();
            throw new IdAlreadyExistsException($"No user Journey have the name {id}");
        }
        public IEnumerable<UserJourney> GetUsersJourney()
        {
            return from UserJourney in DataSource.UsersJourney
                   where UserJourney.FlageActive == true
                   select UserJourney;
        }
        public void AddUserJourney(UserJourney userJourney)
        {
            var userJourneyIndex = DataSource.UsersJourney.FindIndex(u => u.UserName == userJourney.UserName);
            if (userJourneyIndex != -1)
                if (DataSource.UsersJourney[userJourneyIndex].FlageActive == false)
                {
                    DataSource.UsersJourney[userJourneyIndex].FlageActive = true;
                    return;
                }
                else
                    throw new DO.IdAlreadyExistsException(userJourney.UserName, $"The User Name {userJourney.UserName} already exist");
            DataSource.UsersJourney.Add(userJourney.Clone());
        }
        public void UpdatUserJourney(UserJourney userJourney)
        {
            var toUpdateIndex = DataSource.UsersJourney.FindIndex(u => u.UserName == userJourney.UserName);
            if (toUpdateIndex != -1)
                if (DataSource.UsersJourney[toUpdateIndex].FlageActive == true)
                    DataSource.UsersJourney[toUpdateIndex] = userJourney.Clone();
                else
                    throw new IdAlreadyExistsException(userJourney.UserName, $"The user name {userJourney.UserName} does not exist");
            else
                throw new IdAlreadyExistsException(userJourney.UserName, $"The user name {userJourney.UserName} does not exist");
        }
        public void DeleteUserJourney(UserJourney userJourney)
        {
            var toDeleteIndex = DataSource.UsersJourney.FindIndex(u => u.UserName == userJourney.UserName);
            if (toDeleteIndex != -1)
                if (DataSource.UsersJourney[toDeleteIndex].FlageActive == true)
                    DataSource.UsersJourney[toDeleteIndex].FlageActive = false;
                else
                    throw new IdAlreadyExistsException(userJourney.UserName, $"The user name {userJourney.UserName} is already deleted");
            else
                throw new IdAlreadyExistsException(userJourney.UserName, $"The user name {userJourney.UserName} is already deleted");
        }
        #endregion
    }
}