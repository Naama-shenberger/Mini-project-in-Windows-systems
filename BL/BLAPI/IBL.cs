using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
namespace BLAPI
{
    public interface IBL
    {
        #region Bus Line 
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations();
        IEnumerable<int> GetNumberLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddABusLine(BusLine busLine);
        void AddBusStationToLine(BusLine AddToLine,BusLineStation busLineStation);
        void DeleteBusLineStationFromeLine(BusLine DeleteFromLine,BusLineStation busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();
        #endregion
        #region BusLineStation
        void AddBusLineStation(BusLineStation busLineStation);
        void DeleteBusLineStation(BusLineStation busLineStation);
        IEnumerable<BusLineStation> GetAllStudents(Func<BusLineStation, bool> predicat = null);
        #endregion
        #region LineOutForARide
        void AddLineOutForARide(LineOutForARide lineOutForARide);
        void DeleteLineOutForARide(LineOutForARide lineOutForARide);
        IEnumerable<IGrouping<TimeSpan, LineOutForARide>> GetAllBusLinesGroupByExitStartTime();
        IEnumerable<IGrouping<TimeSpan, LineOutForARide>> GetAllBusLinesGroupByTravelEndTime();
        IEnumerable<LineOutForARide> GetLineOutForARides();
        IEnumerable<LineOutForARide> GetLineOutForARidesBy(Predicate<LineOutForARide> predicate);
        #endregion
        #region ConsecutiveStations
        void AddConsecutiveStations(ConsecutiveStations one, ConsecutiveStations two);
        void DeleteConsecutiveStations(ConsecutiveStations one, ConsecutiveStations two);
        IEnumerable<ConsecutiveStations> GetConsecutiveStations();
        IEnumerable<ConsecutiveStations> GetConsecutiveStationsBy(Predicate<ConsecutiveStations> predicate);
        #endregion
    }
}
