
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLAPI;
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
        public IEnumerable<string> GetBussLicenseNumber()
        {
            return from Bus in DataSource.ListBuses
                   select Bus.LicensePlate;
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Bus GetBus(string id)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicensePlate == id);
            if (bus != null)
            {
                if (bus.Active == true)
                    return bus.Clone();
            }
            throw new DO.IdException(id, $"No bus have the license plate:{id}");
        }
        /// <summary>
        ///  A function that returns a list of bus that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> Buss()
        {
            return from Bus in DataSource.ListBuses
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
            var busIndex = DataSource.ListBuses.FindIndex(s => s.LicensePlate == b.LicensePlate);
            if (busIndex != -1)
                if (DataSource.ListBuses[busIndex].Active == false)
                {
                    DataSource.ListBuses[busIndex].Active = true;
                    return;
                }
                else
                    throw new IdException(b.LicensePlate, $"The bus {b.LicensePlate} already exist");
            DataSource.ListBuses.Add(b.Clone());
        }
        /// <summary>
        /// A function that receives a bus and updates its details
        /// </summary>
        /// <param name="b"></param>
        public void UpdateBus(Bus b)
        {
            var toUpdateIndex = DataSource.ListBuses.FindIndex(s => s.LicensePlate == b.LicensePlate);
            if (toUpdateIndex != -1)
                if (b.Active == true)
                    DataSource.ListBuses[toUpdateIndex] = b.Clone();
                else
                    throw new IdException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");
            else
                throw new IdException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");

        }
        /// <summary>
        /// A function that receives a bus to deletes from bus list
        /// </summary>
        /// <param name="b"></param>
        public void DeleteBus(Bus b)
        {
            var toDeleteIndex = DataSource.ListBuses.FindIndex(s => s.LicensePlate == b.LicensePlate);
            if (toDeleteIndex != -1)
                if (b.Active == true)
                    DataSource.ListBuses[toDeleteIndex].Active = false;
                else
                    throw new IdException(b.LicensePlate, $"The bus {b.LicensePlate} is already deleted");
            else
                throw new IdException(b.LicensePlate, $"The bus {b.LicensePlate} does not exist");

        }
        /// <summary>
        /// A function that returns a list of bus that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> GetAllBuss()
        {
            return from Bus in DataSource.ListBuses
                   where Bus.Active == true
                   select Bus.Clone();
        }

        #endregion
        #region BusDrive Function

        public IEnumerable<BusDrive> GetAllBusDrive()
        {
            return from busDrive in DataSource.ListBusDrives
                   where busDrive.Active == true
                   select busDrive.Clone();
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus drive object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusDrive GetBusDrive(int id)
        {
            BusDrive busDrive = DataSource.ListBusDrives.Find(b => b.ID == id);
            if (busDrive != null)
                if (busDrive.Active == true)
                    return busDrive.Clone();
            throw new IdException(id, $"No bus have the id: {id}");
        }
        /// <summary>
        ///  A function that returns a list of bus drive that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusDrive> BussDrive()
        {
            return from BusDrive in DataSource.ListBusDrives
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
            var busIndex = DataSource.ListBusDrives.FindIndex(b => b.ID == bus.ID);
            if (busIndex != -1)
                if (DataSource.ListBusDrives[busIndex].Active == false)
                {
                    DataSource.ListBusDrives[busIndex].Active = true;
                    return;
                }
                else
                    throw new DO.IdException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} already exist");
            DataSource.ListBusDrives.Add(bus.Clone());
        }
        /// <summary>
        /// A function that receives a bus drive and updates its details
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusDrive(BusDrive bus)
        {
            var toUpdateIndex = DataSource.ListBusDrives.FindIndex(b => b.ID == bus.ID);
            if (toUpdateIndex != -1)
                if (bus.Active == true)
                    DataSource.ListBusDrives[toUpdateIndex] = bus.Clone();
                else
                    throw new IdException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} does not exist");
            else
                throw new IdException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus drive is already not active we will throw a message
        /// </summary>
        /// <param name="s"></param>
        public void DeleteBusDrive(BusDrive bus)
        {
            var toDeleteIndex = DataSource.ListBusDrives.FindIndex(b => b.ID == bus.ID);
            if (toDeleteIndex != -1)
                if (bus.Active == true)
                    DataSource.BusLines[toDeleteIndex].Active = false;
                else
                    throw new IdException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} is already deleted");
            else
                throw new IdException(bus.LicensePlate, $"The bus in drive {bus.LicensePlate} does not exist");
        }

        #endregion
        #region BusStation Function
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus station object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusStation GetBusStation(int id)
        {
            BusStation busStation = DataSource.ListStations.Find(s => s.BusStationKey == id);
            if (busStation != null)
                if (busStation.Active == true)
                    return busStation.Clone();
            throw new IdException(id, $"No bus Station have the Code: {id}");
        }
        /// <summary>
        ///  A function that returns a list of bus stations that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusStation> BusStations()
        {
            return from BusStation in DataSource.ListStations
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
            var IndexStation = DataSource.ListStations.FindIndex(s => s.BusStationKey == station.BusStationKey);
            if (IndexStation != -1)
                if (DataSource.ListStations[IndexStation].Active == false)
                {
                    DataSource.ListStations[IndexStation].Active = true;
                    return;
                }
                else
                    throw new IdException(station.BusStationKey, $"The bus station {station.BusStationKey} already exist");
            DataSource.ListStations.Add(station.Clone());
        }
        /// <summary>
        /// A function that receives a bus Station and updates its details
        /// </summary>
        /// <param name="station"></param>
        public void UpdateBusStation(BusStation station)
        {
            var toUpdateIndex = DataSource.ListStations.FindIndex(s => s.BusStationKey == station.BusStationKey);
            if (toUpdateIndex != -1)
                if (station.Active == true)
                    DataSource.ListStations[toUpdateIndex] = station.Clone();
                else
                    throw new IdException(station.BusStationKey, $"The bus Station {station.BusStationKey} alredy exists");
            else
                throw new IdException(station.BusStationKey, $"The bus station {station.BusStationKey} does not exist");

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
            var toDeleteIndex = DataSource.ListStations.FindIndex(s => s.BusStationKey == station.BusStationKey);
            if (toDeleteIndex != -1)
                if (station.Active == true)
                    DataSource.ListStations[toDeleteIndex].Active = false;
                else
                    throw new IdException(station.BusStationKey, $"The bus Station {station.BusStationKey} is already deleted");
            else
                throw new IdException(station.BusStationKey, $"The bus Station {station.BusStationKey} does not exist");

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
            return from BusS in DataSource.ListStations
                   select generate(BusS.BusStationKey);
        }
        #endregion
        #region BusLine Functions
        /// <summary>
        /// A function that uses Encapsulates a method 
        /// accepts an integer and returns an object
        /// integer-bus line number
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<int> GetBusLineNumbers()
        {
            return from BusLine in DataSource.BusLines
                   select BusLine.BusLineNumber;
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
            throw new IdException(id, $"No bus line have the id: {id}");
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
        public int AddBusLine(BusLine bus)
        {

            Configuration.IdentificationNumberBusLine++;
            bus.ID = Configuration.IdentificationNumberBusLine;
            var busIndex = DataSource.BusLines.FindIndex(b => b.ID == bus.ID);
            if (busIndex != -1)
                if (DataSource.BusLines[busIndex].Active == false)
                {
                    DataSource.BusLines[busIndex].Active = true;
                    return DataSource.BusLines[busIndex].ID;
                }
                else
                    throw new DO.IdException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} already exist");
            DataSource.BusLines.Add(bus.Clone());
            return bus.ID;
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
                    throw new IdException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} does not exist");
            else
                throw new IdException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} does not exist");
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
                    throw new IdException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} is already deleted");
            else
                throw new IdException(bus.BusLineNumber, $"The bus line {bus.BusLineNumber} does not exist");
        }
        #endregion
        #region BusLineStation Functions
        /// <summary>
        ///  A function that returns a collection of bus line stations 
        ///  the function uses predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BusLineStation> GetBusLineStations(Predicate<BusLineStation> predicate)
        {
            return from sin in DataSource.BusLineStations
                   where predicate(sin)
                   select sin.Clone();
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus line station object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLineStation GetBusLineStation(int codeStation,int IDBusLine)
        {
            BusLineStation busLineStation = DataSource.BusLineStations.Find(s => s.BusStationKey == codeStation && s.ID==IDBusLine);
            if (busLineStation != null)
                if (busLineStation.Active == true)
                { 
                    return busLineStation.Clone();
                }
            throw new IdException(codeStation, $"No bus line Station have the Code: {codeStation}");
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
            var stationIndex = DataSource.BusLineStations.FindIndex(s => s.BusStationKey == station.BusStationKey && s.ID==station.ID);
            if (stationIndex != -1)
                if (DataSource.BusLineStations[stationIndex].Active == false)
                {
                    DataSource.BusLineStations[stationIndex].Active = true;
                    return;
                }
                else
                    throw new IdException(station.BusStationKey, $"The bus line station {station.BusStationKey} already exist");
            DataSource.BusLineStations.Add(station.Clone());

        }
        /// <summary>
        /// A function that receives a bus line Station and updates its details
        /// </summary>
        /// <param name="station"></param>
        public void UpdateBusLineStation(BusLineStation station)
        {
            var toUpdateIndex = DataSource.BusLineStations.FindIndex(s => s.BusStationKey == station.BusStationKey && s.ID==station.ID);
            if (toUpdateIndex != -1)
                if (station.Active == true)
                    DataSource.BusLineStations[toUpdateIndex] = station.Clone();
                else
                    throw new IdException(station.BusStationKey, $"The bus line Station {station.BusStationKey} does not exist");
            else
                throw new IdException(station.BusStationKey, $"The bus line Station {station.BusStationKey} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus line station is already not active we will throw a message
        /// </summary>
        /// <param name="station"></param>
        public void DeleteBusLineStation(BusLineStation station)
        {
            var toDeleteIndex = DataSource.BusLineStations.FindIndex(s => s.BusStationKey == station.BusStationKey);
            if (toDeleteIndex != -1)
                if (station.Active == true)
                    DataSource.BusLineStations[toDeleteIndex].Active = false;
                else
                    throw new IdException(station.BusStationKey, $"The bus line Station {station.BusStationKey} is already deleted");
            else
                throw new IdException(station.BusStationKey, $"The bus line Station {station.BusStationKey} does not exist");
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
                   select generate(BusLineStation.BusStationKey).Clone();
        }
        #endregion
        #region LineOutForARide
        /// <summary>
        /// A function that receives an ID number and returns the object with that ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LineRide GetLineWayOut(int id)
        {
            LineRide busLine = DataSource.LinesOutForARide.Find(b => b.ID == id );
           
            if (busLine != null)
                if (busLine.Active == true)
                { 
                    return busLine.Clone(); 
                }
            throw new IdException(id, $"No bus line have the id:{id}");
        }
        /// <summary>
        /// A function that returns a list of bus line  on their way out to a ride that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineRide> LinesWayOut()
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
        public void AddLineWayOut(LineRide o)
        {
            Configuration.IdentificationNumberBusLine += 1;
            o.ID = Configuration.IdentificationNumberBusLine;
            var lineIndex = DataSource.LinesOutForARide.FindIndex(b => b.ID == o.ID);
            if (lineIndex != -1)
                if (DataSource.LinesOutForARide[lineIndex].Active == false)
                {
                    DataSource.LinesOutForARide[lineIndex].Active = true;
                    return;
                }
                else
                    throw new DO.IdException(o.ID, $"The bus line {o.ID} already exist");
            DataSource.LinesOutForARide.Add(o.Clone());
        }
        /// <summary>
        /// A function that receives a bus line on his way out and updates its details
        /// </summary>
        /// <param name="outLine"></param>
        public void UpdateLineWayOut(LineRide outLine)
        {
            var toUpdateIndex = DataSource.LinesOutForARide.FindIndex(o => o.ID == outLine.ID);
            if (toUpdateIndex != -1)
                if (outLine.Active == true)
                    DataSource.LinesOutForARide[toUpdateIndex] = outLine.Clone();
                else
                    throw new IdException(outLine.ID, $"The bus line {outLine.ID} does not exist");
            else
                throw new IdException(outLine.ID, $"The bus line {outLine.ID} does not exist");
        }
        /// <summary>
        /// The function gets an object to delete
        /// in case we found the The object in the list will make its active field inactive
        /// In case that the bus is already not active we will throw a message
        /// </summary>
        /// <param name="toDeleteIndex"></param>
        public void DeleteLineWayOut(LineRide outLine)
        {
            var toDeleteIndex = DataSource.LinesOutForARide.FindIndex(o => o.ID == outLine.ID);
            if (toDeleteIndex != -1)
                if (outLine.Active == true)
                    DataSource.LinesOutForARide[toDeleteIndex].Active = false;
                else
                    throw new IdException(outLine.ID, $"The bus line {outLine.ID} is already deleted");
            else
                throw new IdException(outLine.ID, $"The bus line {outLine.ID} does not exist");
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
            throw new IdException($"No consecutive Stations have the id`s:{id1} {id2}");
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
            var stationsIndex = DataSource.ListConsecutiveStations.FindIndex(s => s.StationCodeOne == stations.StationCodeOne && s.StationCodeTwo == stations.StationCodeTwo);
            if (stationsIndex != -1)
                if (DataSource.ListConsecutiveStations[stationsIndex].Flage == false)
                {
                    DataSource.ListConsecutiveStations[stationsIndex].Flage = true;
                    return;
                }
                else
                    throw new IdException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} already exist");
            DataSource.ListConsecutiveStations.Add(stations.Clone());
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
                    throw new DO.IdException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
            else
                throw new DO.IdException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
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
                    throw new DO.IdException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
            else
                throw new DO.IdException($"The Consecutives Stations {stations.StationCodeOne} {stations.StationCodeTwo} doesn't exist");
        }
        #endregion
        #region User
        public User GetUser(string id)
        {
            var user = DataSource.Users.Find(u => u.UserName == id);
            if (user != null)
                if (user.DelUser == true)
                    return user.Clone();
            throw new IdException($"No user have the name {id}");
        }
        public IEnumerable<User> GetUsers()
        {
            return from User in DataSource.Users
                   where User.DelUser == true
                   select User.Clone();
        }
        public void AddUser(User user)
        {
            var userIndex = DataSource.Users.FindIndex(u => u.UserName == user.UserName);
            if (userIndex != -1)
                if (DataSource.Users[userIndex].DelUser == false)
                {
                    DataSource.Users[userIndex].DelUser = true;
                    return;
                }
                else
                    throw new DO.IdException(user.UserName, $"The User Name {user.UserName} already exist");
            DataSource.Users.Add(user.Clone());
        }
        public void UpdatUser(User user)
        {
            var toUpdateIndex = DataSource.Users.FindIndex(u => u.UserName == user.UserName);
            if (toUpdateIndex != -1)
                if (DataSource.Users[toUpdateIndex].DelUser == true)
                    DataSource.Users[toUpdateIndex] = user.Clone();
                else
                    throw new IdException(user.UserName, $"The user name {user.UserName} does not exist");
            else
                throw new IdException(user.UserName, $"The user name {user.UserName} does not exist");
        }
        public void DeleteUser(User user)
        {
            var toDeleteIndex = DataSource.Users.FindIndex(u => u.UserName == user.UserName);
            if (toDeleteIndex != -1)
                if (DataSource.Users[toDeleteIndex].DelUser == true)
                    DataSource.Users[toDeleteIndex].DelUser = false;
                else
                    throw new IdException(user.UserName, $"The user name {user.UserName} is already deleted");
            else
                throw new IdException(user.UserName, $"The user name {user.UserName} is already deleted");
        }
        #endregion
        #region UserJourney
        public UserJourney GetUserJourney(string id)
        {
            var userJourney = DataSource.UsersJourney.Find(u => u.UserName == id);
            if (userJourney != null)
                if (userJourney.FlageActive == true)
                    return userJourney.Clone();
            throw new IdException($"No user Journey have the name {id}");
        }
        public IEnumerable<UserJourney> GetUsersJourney()
        {
            return from UserJourney in DataSource.UsersJourney
                   where UserJourney.FlageActive == true
                   select UserJourney.Clone();
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
                    throw new DO.IdException(userJourney.UserName, $"The User Name {userJourney.UserName} already exist");
            DataSource.UsersJourney.Add(userJourney.Clone());
        }
        public void UpdatUserJourney(UserJourney userJourney)
        {
            var toUpdateIndex = DataSource.UsersJourney.FindIndex(u => u.UserName == userJourney.UserName);
            if (toUpdateIndex != -1)
                if (DataSource.UsersJourney[toUpdateIndex].FlageActive == true)
                    DataSource.UsersJourney[toUpdateIndex] = userJourney.Clone();
                else
                    throw new IdException(userJourney.UserName, $"The user name {userJourney.UserName} does not exist");
            else
                throw new IdException(userJourney.UserName, $"The user name {userJourney.UserName} does not exist");
        }
        public void DeleteUserJourney(UserJourney userJourney)
        {
            var toDeleteIndex = DataSource.UsersJourney.FindIndex(u => u.UserName == userJourney.UserName);
            if (toDeleteIndex != -1)
                if (DataSource.UsersJourney[toDeleteIndex].FlageActive == true)
                    DataSource.UsersJourney[toDeleteIndex].FlageActive = false;
                else
                    throw new IdException(userJourney.UserName, $"The user name {userJourney.UserName} is already deleted");
            else
                throw new IdException(userJourney.UserName, $"The user name {userJourney.UserName} is already deleted");
        }
        #endregion

    }

}
