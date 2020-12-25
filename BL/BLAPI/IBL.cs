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
        IEnumerable<IGrouping<bool,Bus>> GetAllBusNeedTreatment();
        IEnumerable<IGrouping<bool,Bus>> GetAllBusslicensePlate();
        IEnumerable<int> GetNumberbuss();
        IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate);
        void AddABus(Bus bus);
        void DeleteBus(Bus bus);
        bool TreatmentIsNeeded(int id);
        void BusInTreatment(Bus b);
        bool FuelCondition(Bus b);
        void RefillingBus(Bus bus);
        int NumberOflicensePlate(Bus b);
        bool DateCheck(Bus b);
        bool BusCondition(Bus bus);
        #endregion
        #region Bus Station
        BusStation GetBusStation(int id);
        IEnumerable<BusStation> GetAllBusStation();
        IEnumerable<BusLineInStation> GetAllBusLineInStation();//A bus line passing through the station
        void AddBusStation(BusStation station);
        void DeleteBusStation(BusStation station);

        #endregion
        #region Bus Line In Station
        BusLineInStation GetLineInStation(int id);
        void AddBusLineInStation(BusLineInStation lineInStation);
        void DeleteBusLineInStation(BusLineInStation lineInStation);

        #endregion


    }
}
