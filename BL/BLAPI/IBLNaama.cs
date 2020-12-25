using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
namespace BL.BLAPI
{
    interface IBLNaama
    {
        #region Bus Line 
        BO.BusLine BusLineDoBoAdapter(DO.BusLine busLineDO);
        DO.BusLine BusLineBoDoAdapter(BO.BusLine busLineBO);
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations();
        IEnumerable<int> GetNumberLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddBusLine(BusLine busLine, StationInLine stationInLineOne=null, StationInLine stationInLineTwo =null);
        void AddBusStationToLine(BusLine AddToLine, StationInLine busLineStation);
        void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, StationInLine busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();
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

    

