using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.BLAPI;
using BLAPI;
using BO;
using DalApi;
using static DO.Enums;

namespace BL
{
    class BLlmpNaama :IBLNaama
    {
        IDal dl = DalFactory.GetDal();
        Random random = new Random();
        #region Bus Line
        /// <summary>
        /// A function that receives a BO type bus line object and returns a DO type bus line
        /// </summary>
        /// <param name="busLineBO"></param>
        /// <returns></returns>
        public DO.BusLine BusLineBoDoAdapter(BO.BusLine busLineBO)
        {
            DO.BusLine busLineDO = new DO.BusLine();
            BO.Bus busBO;
            int id = busLineBO.ID;
            try
            {
              busBO = GetBus(id);//פונקציה שאצל אלה
            }
            catch (DO.IdAlreadyExistsException ex) { throw new BO.IdAlreadyExistsException("Bus Line ID is illegal", ex); }
            busBO.CopyPropertiesTo(busLineDO);
            busLineBO.CopyPropertiesTo(busLineDO);
            return busLineDO;
        }
        /// <summary>
        /// A function that receives a DO type bus line object and returns a BO type bus line
        /// </summary>
        /// <param name="busLineDO"></param>
        /// <returns></returns>
        public BO.BusLine BusLineDoBoAdapter(DO.BusLine busLineDO)
        {
            BO.BusLine busLineBO = new BO.BusLine();
            DO.Bus busDO;
            int id = busLineDO.ID;
            try
            {
                busDO = dl.GetBus(id);
            }
            catch(DO.IdAlreadyExistsException ex) { throw new BO.IdAlreadyExistsException("Bus Line ID is illegal", ex); }
            busDO.CopyPropertiesTo(busLineDO);
            busLineDO.CopyPropertiesTo(busLineBO);
            busLineBO.StationsInLine = from sin in dl.GetStationInLines()
                                       where (sin.IDBusLine==id)
                                       let StationsInLine = dl.GetStationInLine(sin.BusStationKey)
                                       select StationsInLine.CopyToStationInLine(sin);
            return busLineBO;
        }
    
        /// <summary>
        /// Add a line to the list of bus lines
        //  The function gets a bus line to add
        /// </summary>
        /// <param name="busLine"></param>
        public void AddBusLine(BusLine busLine,StationInLine stationInLineOne=null,StationInLine stationInLineTwo=null)
        {
           
            try
            {
                if(stationInLineOne==null && stationInLineTwo==null)
                    throw new BO.IdAlreadyExistsException("You must add at least two stations to the line");
                busLine.StationsInLine.ToList().Add(stationInLineOne);
                busLine.StationsInLine.ToList().Add(stationInLineTwo);
                DO.ConsecutiveStations stations = new DO.ConsecutiveStations { StationCodeOne = stationInLineOne.BusStationKey, StationCodeTwo = stationInLineTwo.BusStationKey
                , Distance = (float)(random.NextDouble() * 1850 + 150), Flage = true,
                AverageTravelTime = new TimeSpan(random.Next(0, 4), random.Next(0, 60), random.Next(0, 60))};
                dl.AddConsecutiveStations(stations);
                dl.GetBusLine(busLine.ID);
            }
            catch (DO.IdAlreadyExistsException)
            {
                dl.AddBusLine(BusLineBoDoAdapter(busLine));
            }
            catch (BO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line ID is illegal", ex);
            }
        }
        /// <summary>
        /// The function gets a bus line and a stop station
        /// The function adds the station to the bus line
        /// </summary>
        /// <param name="AddToLine"></param>
        /// <param name="busLineStation"></param>
        public void AddBusStationToLine(BusLine AddToLine, StationInLine busLineStation)
        {
            try
            {
                AddToLine.StationsInLine.ToList().Add(busLineStation);
                DO.ConsecutiveStations stations = new DO.ConsecutiveStations
                {
                    StationCodeOne = busLineStation.BusStationKey,
                    StationCodeTwo = AddToLine.StationsInLine.ToList()[AddToLine.StationsInLine.ToList().Count-1].BusStationKey,
                    Distance = (float)(random.NextDouble() * 1850 + 150),
                    Flage = true,
                    AverageTravelTime = new TimeSpan(random.Next(0, 4), random.Next(0, 60), random.Next(0, 60))
                };
                dl.AddConsecutiveStations(stations);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function receives a bus line for deletion
        /// </summary>
        /// <param name="busLine"></param>
        public void DeleteBusLine(BusLine busLine)
        {
            try
            {
                dl.DeleteBusLine(dl.GetBusLine(busLine.ID));
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("ERROR", ex);
            }
        }
        /// <summary>
        /// The function gets a bus line and the bus stop for deletion
        /// The function deletes the station from the received line
        /// </summary>
        /// <param name="DeleteFromLine"></param>
        /// <param name="busLineStation"></param>
        public void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, StationInLine busLineStation)
        {
            try
            {
                
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that returns all bus lines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> GetAllBusLines()
        {
            return from item in dl.BusLines() select BusLineDoBoAdapter(item);
        }
        /// <summary>
        ///A function that returns all bus lines in groups according to their areas 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea()
        {
            return from BusLine in GetAllBusLines()
                   group BusLine by ((Zones)BusLine.Area).ToString() into groups
                   select groups;
        }

        /// <summary>
        /// A function that returns all bus lines
        /// Are appointed according to their stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations()
        {
            return from item in dl.GetBusLineNumbers((id) => { return GetBusLine(id); })
                   let busLine = item as BO.BusLine
                   orderby busLine.LineStations
                   select busLine;
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding bus line
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLine GetBusLine(int id)
        {
            try
            {
                return BusLineDoBoAdapter(dl.GetBusLine(id));
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// lamda Function 
        /// Accepts predicate-Which checks a condition and returns the lines that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate) => from sic in dl.GetBusLineNumbers((id) => { return GetBusLine(id); })
                                                                                  let busLine = sic as BO.BusLine
                                                                                  where predicate(busLine)
                                                                                  select busLine;
        /// <summary>
        /// A  Function that returns a Collection with all the bus lines number Sorting
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetNumberLines()
        {
            return from item in dl.GetBusLineNumbers((id) => { return GetBusLine(id); })
                   let busLine = item as BO.BusLine
                   orderby busLine.BusLineNumber
                   select busLine.BusLineNumber;
        }
        #endregion
        #region  User

        public void AddUser(User user)
        {
            try
            {
               DO.User userdDO =dl.GetUser(user.UserName);
               dl.AddUser(userdDO);
            }
            catch(BO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        public void DeleteUser(User user)
        {
            dl.DeleteUser(dl.GetUser(user.UserName));
        }
        public IEnumerable<User> GetUsers()
        {
            return (IEnumerable<User>)dl.GetUsers();
        }
        public User GetUser(string id)
        {
            BO.User user = null;
            dl.GetUser(id).CopyPropertiesTo(user);
            return user;
        }
        public  IEnumerable<IGrouping<bool, User>> GetUsersGroupByAllowingAccess()
        {
            return from User in GetUsers()
                   group User by User.AllowingAccess into groups
                   select groups;

        }
        public  IEnumerable<User> GetUsersBy(Predicate<User> predicate)
        {
            return from User in GetUsers()  
                   where predicate(User)
                   select User;
        }
        public IEnumerable<string> GetUsersNames()
        {
            return from User in GetUsers()
                   select User.UserName;
        }
        #endregion
        #region UserJourney
        public void AddUserJourney(UserJourney userJourney)
        {
            try
            {
                DO.UserJourney userJourneydDO = dl.GetUserJourney(userJourney.UserName);
                dl.AddUserJourney(userJourneydDO);
            }
            catch(BO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        public void DeleteUserJourney(UserJourney userJourney)
        {
            try
            {
                dl.DeleteUserJourney(dl.GetUserJourney(userJourney.UserName));
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        public IEnumerable<UserJourney> GetUsersJourney()
        {
            return (IEnumerable<UserJourney>)dl.GetUsersJourney();
        }
        public UserJourney GetUserJourney(string id)
        {
            DO.UserJourney userJourneyDO= dl.GetUserJourney(id);
            BO.UserJourney userJourneyBO=null;
            userJourneyDO.CopyPropertiesTo(userJourneyBO);
            return userJourneyBO;
        }
        public IEnumerable<IGrouping<string, UserJourney>> GetUsersJourneyGroupByBoardingStation()
        {
            return from UserJourney in GetUsersJourney()
                   group UserJourney by UserJourney.BoardingStation into groups
                   select groups;

        }
        public IEnumerable<User> GetUsersJourneyBy(Predicate<UserJourney> predicate)
        {
            return from User in GetUsersJourney()
                   where predicate(User)
                   select User;
        }
        public IEnumerable<IGrouping<int, UserJourney>> GetUsersJourneyGroupByBusLineJourney()
        {
            return from UserJourney in GetUsersJourney()
                   group UserJourney by UserJourney.BusLineJourney into groups
                   select groups;
        }
        #endregion
    }
}
//יכולים לכלול אוספים גנריים (<>IEnumerable)
//יכולים לכלול תכונות מטיפוס של ישות BO אחרות
//יכולים לרשת מישות BO אחרת (יש להיזהר מאד עם שימוש בירושה ב-BO!)
//חובה לכלול לפחות 4 שאילתות LINQtoObject
//חובה לכלול לפחות 4 ביטויי למבדה-------צריך עוד 3 
//בשאילתות LINQ חובה להשתמש לפחות פעם אחת
//ב-let ------we used
//ב-select new
//בקיבוץ(grouping)----יש
//במיון----יש


    

