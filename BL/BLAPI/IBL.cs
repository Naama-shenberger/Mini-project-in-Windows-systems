using System;
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
        IEnumerable<IGrouping<bool,Bus>> GetAllBussGroupByTreatmentIsNeeded();
        IEnumerable<string> GetLicensePlateBuss();
        IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate);
        void AddABus(Bus bus);
        void DeleteBus(Bus bus);
        void UpdateBus(Bus bus);
        bool TreatmentIsNeeded(string id);
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
        void UpdateBusStation(BusStation busStation);
        #endregion
        #region Bus Line 
        BO.BusLine BusLineDoBoAdapter(DO.BusLine busLineDO);
        DO.BusLine BusLineBoDoAdapter(BO.BusLine busLineBO);
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations();
        IEnumerable<int> GetNumberLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddBusLine(BusLine busLine, BusLineStation stationInLineOne = null, BusLineStation stationInLineTwo = null);
        void AddBusStationToLine(BusLine AddToLine, BusLineStation busLineStation);
        void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, BusLineStation busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();
        void UpdateBusLine(BusLine busLine);
        void UpdateDistanceBetweenstations(DO.ConsecutiveStations stations, float _distance);
        void UpdateTravelTimeBetweenstations(DO.ConsecutiveStations stations, TimeSpan time);
        #endregion
        #region Bus Line In Station
        BusLineInStation GetLineInStation(int id);
        void AddBusLineInStation(BusLineInStation lineInStation);
        void DeleteBusLineInStation(BusLineInStation lineInStation);
        void UpdateBusLineInStation(BusLineInStation busStation);
        #endregion
        #region User
        BO.User UserDoBoAdapter(DO.User UserDO);
        DO.User UserBoDoAdapter(BO.User UserBO);
        void AddUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(string id);
        IEnumerable<IGrouping<bool, User>> GetUsersGroupByAllowingAccess();
        IEnumerable<User> GetUsersBy(Predicate<User> predicate);
        IEnumerable<string> GetUsersNames();
        void UpdateUser(User user);
        #endregion
        #region UserJourney
        void UpdateUserJourney(UserJourney UserJourney);
        BO.UserJourney UserJourneyDoBoAdapter(DO.UserJourney UserJourneyDO);
        DO.UserJourney UserJourneyBoDoAdapter(BO.UserJourney UserJourneyBO);
        void AddUserJourney(UserJourney userJourney);
        void DeleteUserJourney(UserJourney userJourney);
        IEnumerable<UserJourney> GetUsersJourney();
        IEnumerable<object> TravelHistory(string id);
        UserJourney GetUserJourney(string id);
        IEnumerable<IGrouping<string, UserJourney>> GetUsersJourneyGroupByBoardingStation();
        IEnumerable<User> GetUsersJourneyBy(Predicate<UserJourney> predicate);
        IEnumerable<IGrouping<int, UserJourney>> GetUsersJourneyGroupByBusLineJourney();
        BusLine LineToGo(BusStation busStationCurrent, BusStation busStationtarget);
        #endregion


    }
}
