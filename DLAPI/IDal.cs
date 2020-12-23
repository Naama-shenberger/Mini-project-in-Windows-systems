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
        Bus GetBus(int id);
        IEnumerable<Bus> Buss();
        void AddBus(Bus b);
        void UpdateBus(Bus b);
        void DeleteBus(Bus b);
        #endregion
        #region BusDrive
        BusDrive GetBusDrive(int id);
        IEnumerable<BusDrive> BussDrive();
        void AddBusDrive(BusDrive b);
        void UpdateBusDrive(BusDrive b);
        void DeleteBusDrive(BusDrive b);
        #endregion
        #region BusStation
        BusStation GetBusStation(string code);
        IEnumerable<BusStation> BusStations();
        void AddBusStation(BusStation b);
        void UpdateBusStation(BusStation b);
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
        User GetUserJourney(string id);
        IEnumerable<UserJourney> GetUsersJourney();
        void AddUserJourney(UserJourney user);
        void UpdatUser(UserJourney user);
        void DeleteUserJourney(UserJourney user);
        #endregion
    }
}
