using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DLAPI
{
    /// <summary>
    /// Statement of functions for all class in DO
    /// CRUD Logic
    /// </summary>
    public interface IDal
    {
       
        #region Bus
        IEnumerable<Bus> GetAllBuss();
        Bus GetBus(string id);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(Bus bus);
        #endregion
        #region BusStation
        BusStation GetBusStation(int code);
        IEnumerable<BusStation> BusStations();
        void AddBusStation(BusStation b);
        void UpdateBusStation(BusStation b);
        void DeleteBusStation(BusStation b);
        #endregion
        #region BusLine
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> BusLines();
        int AddBusLine(BusLine b);
        void UpdateBusLine(BusLine b);
        void DeleteBusLine(BusLine b);
        #endregion
        #region BusLineStation
        BusLineStation GetBusLineStation(int codeStation,int IDBusLine);
        IEnumerable<BusLineStation> BusLineStations();
        void AddBusLineStation(BusLineStation s);
        void UpdateBusLineStation(BusLineStation s);
        void DeleteBusLineStation(BusLineStation s);
        #endregion
        #region LineRide
        LineRide GetLineWayOut(int id);
        IEnumerable<LineRide> LinesWayOut();
        void AddLineWayOut(LineRide o);
        void UpdateLineWayOut(LineRide o);
        void DeleteLineWayOut(LineRide o);
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

    }
}
