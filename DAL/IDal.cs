using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DALApi
{
    /// <summary>
    /// Statement of functions for all class in DO
    /// CRUD Logic
    /// </summary>
    public interface IDal
    {
        #region BusLine
        BusLine getBusLine(int id);
        IEnumerable<BusLine> BusLines();
        void addBusLine(BusLine b);
        void updateBusLine(BusLine b);
        void deleteBusLine(BusLine b);
        #endregion
        #region  BusLineStation
        BusLineStation getBusLineStation(int id);
        IEnumerable<BusLineStation> BusLineStations();
        void addBusLineStation(BusLineStation s);
        void updateBusLineStation(BusLineStation s);
        void deleteBusLineStation(BusLineStation s);
        #endregion
        #region LineWayOut
        LineWayOut getLineWayOut(int id);
        IEnumerable<LineWayOut> LinesWayOut();
        void addLineWayOut(LineWayOut o);
        void updateLineWayOut(LineWayOut o);
        void deleteLineWayOut(LineWayOut o);
        TimeSpan TravelTime(LineWayOut o);
        #endregion
        #region ConsecutiveStations
        ConsecutiveStations getConsecutiveStations(string id1, string id2);
        IEnumerable<ConsecutiveStations> ConsecutivesStations();
        void addConsecutiveStations(ConsecutiveStations s);
        void updateConsecutiveStations(ConsecutiveStations c);
        void deleteConsecutiveStations(ConsecutiveStations c);
        #endregion
    }
}
