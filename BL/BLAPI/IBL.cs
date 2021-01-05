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
        IEnumerable<IGrouping<bool, Bus>> GetAllBussGroupByTreatmentIsNeeded();
        IEnumerable<string> GetLicensePlateBuss();
        IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate);
        void AddABus(Bus bus);
        void DeleteBus(Bus bus);
        void UpdateBus(Bus bus);
        bool TreatmentIsNeeded(Bus bus);
        void BusInTreatment(Bus b);
        bool FuelCondition(Bus b);
        void RefillingBus(Bus bus);
        int NumberOflicensePlate(Bus b);
        bool DateCheck(Bus b);
        bool BusCondition(Bus bus);
        #endregion
        #region Bus Station
        BO.BusStation BusStationDoBoAdapter(DO.BusStation stationDO);
        DO.BusStation BusStationBoDoAdapter(BO.BusStation busStationBO);
        DO.BusLineInStation BusLineInStationBoDoAdapter(BO.BusLineInStation busLineInStation);
        BusStation GetBusStation(int id);
        IEnumerable<BusStation> GetAllBusStation();
        IEnumerable<BusLineInStation> GetAllBusLineInStation();//A bus line passing through the station
        void AddBusStation(BusStation station);
        void AddBusLineToStation(BusStation AddToStation, BusLineInStation busLineInStation);
        void DeleteBusLineInStation(BusStation DeleteFromStation, BusLineInStation busLineInStation);
        void DeleteBusStation(BusStation station);
        void UpdateBusStation(BusStation busStation);
        TimeSpan getTimeBetStation(BusStation s1, BusStation s2);
        void addTimeBetStation(BusStation s1, BusStation s2);

        #endregion
        #region Bus Line 
        DO.BusLineStation BusLineStationBoDoAdapter(BO.BusLineStation busLineStationBO);
        BO.BusLine BusLineDoBoAdapter(DO.BusLine busLineDO);
        DO.BusLine BusLineBoDoAdapter(BO.BusLine busLineBO);
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations();
        IEnumerable<int> GetNumberLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddBusLine(BusLine busLine, IEnumerable<BusLineStation> busLineStation);
        void AddBusStationToLine(BusLine AddToLine,IEnumerable<BusLineStation> busLineStation);
        void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, BusLineStation busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();
        void UpdateBusLine(BusLine busLine);
        void UpdateDistanceBetweenstations(DO.ConsecutiveStations stations, float _distance);
        void UpdateTravelTimeBetweenstations(BO.BusStation stations1, BO.BusStation stations2, TimeSpan time);
       
        #endregion
        #region Bus Line In Station
        BusLineInStation GetLineInStation(int id);
        BO.BusLineInStation LineInStationDoBoAdapter(DO.BusLineInStation lineDO);
        IEnumerable<object> BusLineDetails(IEnumerable<BusLineInStation> busLineInStations);
        IEnumerable<BusLineInStation> GetAllBusLineInStations();
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
        #region Bus Line Station
        IEnumerable<BusLineStation> GetAllBusLineStations();
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation busLineStation);
        IEnumerable<object> StationDetails(IEnumerable<BusLineStation> busLineStations);
        void AddBusLinesStation(IEnumerable<BusLineStation> busLineStations);
        #endregion


    }
}
