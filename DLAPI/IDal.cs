using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DalApi
{
    /// <summary>
    /// Statement of functions for all class in DO
    /// CRUD Logic
    /// </summary>
    public interface IDal
    {
        #region Bus
        IEnumerable<Bus> GetAllBuss();
        IEnumerable<Bus> GetAllBuses(Predicate<Bus> predicate);
        Bus GetBus(int id);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void UpdateBus(int id, Action<Bus> update);//method that knows to update a specific object
        void DeleteBus(Bus bus);
        IEnumerable<object> GetBusNum(Func<int, object> generate);

        #endregion
        #region BusDrive
        BusDrive GetBusDrive(int id);
        IEnumerable<DO.BusDrive> GetAllBusDrive();
        void AddBusDrive(BusDrive bus);
        void UpdateBusDrive(BusDrive bus);
        void UpdateBusDrive(int id, Action<Bus> update);//method that knows to update a specific object
        void DeleteBusDrive(BusDrive bus);
        #endregion
        #region BusStation
        BusStation GetBusStation(int code);
        IEnumerable<BusStation> BusStations();
        IEnumerable<object> GetBusStationNumbers(Func<int, object> generate);
        void AddBusStation(BusStation b);
        void UpdateBusStation(BusStation b);
        void UpdateBusStation(string id, Action<Bus> update);//method that knows to update a specific object
        void DeleteBusStation(BusStation b);
        #endregion
        #region BusLine
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> BusLines();
        void AddBusLine(BusLine b);
        void UpdateBusLine(BusLine b);
        void DeleteBusLine(BusLine b);
        IEnumerable<object> GetBusLineNumbers(Func<int, string, object> generate);
        IEnumerable<object> GetBusLineNumbers(Func<int, object> generate);
        #endregion
        #region BusLineStation
        IEnumerable<object> GetBusLineStationCode(Func<int, object> generate);
        BusLineStation GetBusLineStation(int id);
        IEnumerable<BusLineStation> BusLineStations();
        void AddBusLineStation(BusLineStation s);
        void UpdateBusLineStation(BusLineStation s);
        void DeleteBusLineStation(BusLineStation s);
        IEnumerable<BusLineStation> GetBusLineStations(Predicate<BusLineStation> predicate);
        #endregion
        #region LineWayOut
        LineOutForARide GetLineWayOut(int id);
        IEnumerable<LineOutForARide> LinesWayOut();
        void AddLineWayOut(LineOutForARide o);
        void UpdateLineWayOut(LineOutForARide o);
        void DeleteLineWayOut(LineOutForARide o);
        #endregion
        #region ConsecutiveStations
        ConsecutiveStations GetConsecutiveStations(int id1, int id2);
        IEnumerable<ConsecutiveStations> ConsecutivesStations();
        void AddConsecutiveStations(ConsecutiveStations s);
        void UpdateConsecutiveStations(ConsecutiveStations c);
        void DeleteConsecutiveStations(ConsecutiveStations c);
        #endregion
        #region User
        User GetUser(string id);
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        void UpdatUser(User user);
        void DeleteUser(User user);
        #endregion
        #region UserJourney
        UserJourney GetUserJourney(string id);
        IEnumerable<UserJourney> GetUsersJourney();
        void AddUserJourney(UserJourney user);
        void UpdatUserJourney(UserJourney user);
        void DeleteUserJourney(UserJourney user);
        #endregion
        #region StationInLine
        StationInLine GetStationInLine(int id);
        void AddStationInLine(StationInLine stationInLine);
        void DeleteStationInLine(StationInLine stationInLine);
        void UpdateStationInLine(StationInLine stationInLine);
        IEnumerable<StationInLine> GetStationInLines();
        #region
    }
}
