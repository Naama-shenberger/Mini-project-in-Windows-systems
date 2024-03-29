﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
namespace BLAPI
{
    public interface IBL
    {
        #region Bus
        Bus GetBus(string id);
        IEnumerable<Bus> GetAllBus();
        IEnumerable<IGrouping<bool, Bus>> GetAllBussGroupByTreatmentIsNeeded();
        void AddABus(Bus bus);
        void DeleteBus(Bus bus);
        void UpdateBus(Bus bus);
        void BusInTreatment(Bus b);
        void RefillingBus(Bus bus);
        #endregion
        #region Bus Station
        BO.BusStation BusStationDoBoAdapter(DO.BusStation stationDO);
        DO.BusStation BusStationBoDoAdapter(BO.BusStation busStationBO);
        BusStation GetBusStation(int id);
        IEnumerable<BusStation> GetAllBusStation();
        void AddBusStation(BusStation station);
        void DeleteBusStation(BusStation station);
        void UpdateBusStation(BusStation busStation);
        void AddBusLineToStation(BusStation busStation, BusLine busLine, TimeSpan Time, float Disten);
        void DeleteBusLineFromStation(BusStation busStation, BusLineInStation busLine);
        #endregion
        #region stations
        void UpdateDistanceBetweenstations(int id1, int id2, float _distance);
        void UpdateTravelTimeBetweenstations(int id1, int id2, TimeSpan time);
        #endregion
        #region Bus Line 
        BO.BusLine BusLineDoBoAdapter(DO.BusLine busLineDO);
        DO.BusLine BusLineBoDoAdapter(BO.BusLine busLineBO);
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        void AddBusLine(BusLine busLine, IEnumerable<BusLineStation> busLineStation);
        void AddBusStationToLine(BusLine AddToLine, BO.BusLineStation busLineStation);
        void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, BusLineStation busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();
        void UpdateBusLine(BusLine busLine);
        void UpdateBusLineStation(int id,int IDBusLine,BO.BusLine busLine,int index);
        void DeleteBusLineStation(BusLineStation busLineStation);
        #endregion
        #region User
        BO.User UserDoBoAdapter(DO.User UserDO);
        DO.User UserBoDoAdapter(BO.User UserBO);
        void AddUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(string id);
        void UpdateUser(User user);
        #endregion
        #region Bus Line Station
        IEnumerable<BusLineStation> GetAllBusLineStations();
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation busLineStation);
        IEnumerable<object> StationDetails(IEnumerable<BusLineStation> busLineStations);
        void AddBusLinesStation(IEnumerable<BusLineStation> busLineStations);
        DO.BusLineStation BusLineStationBoDoAdapter(BO.BusLineStation busLineStationBO);
        #endregion
        #region Line Ride
        void AddLineRides(BO.LineRides lineRides);
        DO.LineRide LineRideStationDoBoAdapter(BO.LineRides lineRides);
        BO.LineRides LineRideStationBoDoAdapter(DO.LineRide lineRides);
        void DeleteLineRide(BO.LineRides line);
        TimeSpan GetTimeDrive(int IdBusLine, int codeStation);
        IEnumerable<LineRides> GetLineTimingPerStation(BusStation CurbusStation, TimeSpan tsCurTime);
      
        #endregion
    }
}
