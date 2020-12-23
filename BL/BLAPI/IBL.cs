using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
namespace BLAPI
{
    public interface IBL
    {
        #region BUS
        Bus GetBus(int id);
        IEnumerable<Bus> GetAllBus();
        IEnumerable<Bus> GetAllBusNeedTreatment();
        IEnumerable<IGrouping<string,Bus>> GetAllBusslicensePlate();
        IEnumerable<int> GetNumberbuss();
        IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate);
        void AddABus(Bus bus);
        void DeleteBus(Bus bus);
        bool TreatmentIsNeeded(int id);
        void BusInTreatment(Bus b);
        bool FuelCondition(Bus b);
        void refillingBus(Bus bus);
        int NumberOflicensePlate(Bus b);
        bool dateCheck(Bus b);
        bool BusCondition(Bus bus);
        #endregion
        #region Bus Drive
        BusDrive GetBusDrive(int id);
        IEnumerable<BusDrive> GetAllBusInDrive();
        void AddBusDrive(BusDrive busDrive);
        void DeleteBusDrive(BusDrive busDrive);
        #endregion
        #region Bus Station
        BusStation GetBusStation(string id);
        IEnumerable<int> GetNumberStation();
        void AddBusStation(BusStation station);
        void DeleteBusStation(BusStation station);

        #endregion
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
