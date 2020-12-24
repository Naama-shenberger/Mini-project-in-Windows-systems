using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
namespace BL.BLAPI
{
    interface IBLNaama
    {
        #region Bus Station

        #endregion
        #region Bus Line 
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations();
        IEnumerable<int> GetNumberLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddABusLine(BusLine busLine);
        void AddBusStationToLine(BusLine AddToLine, BusLineStation busLineStation,float _Distance, TimeSpan _AverageTravelTime);
        void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, BusLineStation busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();
        void Addatleasttwostations(ConsecutiveStations stations);
        IEnumerable<ConsecutiveStations> ConsecutiveStationsOrderByDistance(IEnumerable<ConsecutiveStations> busLineStations);
        #endregion
        #region BusLineStation
        void AddBusLineStation(BusLineStation busLineStation);
        void DeleteBusLineStation(BusLineStation busLineStation);
        IEnumerable<BusLineStation> GetAllBusLineStations(Func<BusLineStation, bool> predicat = null);
        BusLineStation GetBusLineStation(int id);
        #endregion
        #region LineOutForARide
        void AddLineOutForARide(LineOutForARide lineOutForARide);
        void DeleteLineOutForARide(LineOutForARide lineOutForARide);
        IEnumerable<IGrouping<TimeSpan, LineOutForARide>> GetAllBusLinesGroupByExitStartTime();
        IEnumerable<IGrouping<TimeSpan, LineOutForARide>> GetAllBusLinesGroupByTravelEndTime();
        IEnumerable<LineOutForARide> GetLineOutForARides();
        IEnumerable<LineOutForARide> GetLineOutForARidesBy(Predicate<LineOutForARide> predicate);
        LineOutForARide GetLineOutForARide(int id);
        #endregion
        #region ConsecutiveStations
        void AddConsecutiveStations(ConsecutiveStations consecutiveStations);
        void DeleteConsecutiveStations(ConsecutiveStations consecutiveStations);
        IEnumerable<ConsecutiveStations> GetConsecutiveStations();
        IEnumerable<ConsecutiveStations> GetConsecutiveStationsBy(Predicate<ConsecutiveStations> predicate);
        ConsecutiveStations GetConsecutiveStations(int id1, int id2);
        #endregion
        #region User
        void AddUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(string id);
        IEnumerable<IGrouping<bool, User>> GetUsersGroupByAllowingAccess();
        IEnumerable<User> GetUsersBy(Predicate<User> predicate);
        IEnumerable<string> GetUsersNames();
        #endregion
        #region UserJourney
        void AddUserJourney(UserJourney userJourney);
        void DeleteUserJourney(UserJourney userJourney);
        IEnumerable<UserJourney> GetUsersJourney();
        UserJourney GetUserJourney(string id);
        IEnumerable<IGrouping<string, UserJourney>> GetUsersJourneyGroupByBoardingStation();
        IEnumerable<User> GetUsersJourneyBy(Predicate<UserJourney> predicate);
        IEnumerable<IGrouping<int, UserJourney>> GetUsersJourneyGroupByBusLineJourney();
        #endregion
//        נסיעת משתמש  - מישהו נמצא בתחנה מסוימת הוא יודע שם אילו אוטובוסים עוברים, מחפש ביניהם רק את האוטובוסים שיש לו את התחנת ירידה איפה שהוא רוצה ויבדוק מה הכי מהר.זו שאילתא שמבוצעת בBL על סמך כל הנתונים

//אם רוצים ישות משתמש אז שומרים את היסטוריית כל הבקשות שלו
    }
}

    

