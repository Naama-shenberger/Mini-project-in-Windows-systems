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
        /// A function that receives tracking stations and updates the distance between them
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="_distance"></param>
        public void UpdateDistanceBetweenstations(DO.ConsecutiveStations stations, float _distance)
        {
            try
            {
                DO.ConsecutiveStations consecutiveStations = dl.GetConsecutiveStations(stations.StationCodeOne, stations.StationCodeTwo);
                consecutiveStations.Distance = _distance;
                dl.UpdateConsecutiveStations(consecutiveStations);
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        ///  A function that receives tracking stations and updates the Travel Time between them
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="time"></param>
        public void UpdateTravelTimeBetweenstations(DO.ConsecutiveStations stations, TimeSpan time)
        {
            try
            {
                DO.ConsecutiveStations consecutiveStations = dl.GetConsecutiveStations(stations.StationCodeOne, stations.StationCodeTwo);
                consecutiveStations.AverageTravelTime = time;
                dl.UpdateConsecutiveStations(consecutiveStations);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function receives a bus line for updating
        /// </summary>
        /// <param name="busLine"></param>
        public void UpdateBusLine(BusLine busLine)
        {
            try
            {
                dl.UpdateBusLine(BusLineBoDoAdapter(GetBusLine(busLine.ID)));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that receives a BO type bus line object and returns a DO type bus line
        /// </summary>
        /// <param name="busLineBO"></param>
        /// <returns></returns>
        public DO.BusLine BusLineBoDoAdapter(BO.BusLine busLineBO)
        {
            DO.BusLine busLineDO = new DO.BusLine();
            DO.Bus busBO;
            int id = busLineBO.ID;
            try
            {
                busBO = dl.GetBus(id.ToString());//פונקציה שאצל אלה
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
                busDO = dl.GetBus(id.ToString());
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
        public void AddBusLine(BusLine busLine,BusLineStation stationInLineOne=null,BusLineStation stationInLineTwo=null)
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
                BusLineInStation busLineInStation = new BusLineInStation();
                busLine.CopyPropertiesTo(busLineInStation);
                stationInLineOne.ListBusLinesInStation.ToList().Add(busLineInStation);
                stationInLineTwo.ListBusLinesInStation.ToList().Add(busLineInStation);

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
        public void AddBusStationToLine(BusLine AddToLine, BusLineStation busLineStation)
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
                DO.StationInLine stationInLineDO = new DO.StationInLine();
                busLineStation.CopyPropertiesTo(stationInLineDO);
                dl.AddConsecutiveStations(stations);
                dl.AddStationInLine(stationInLineDO);
                
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
        public void DeleteBusLineStationFromeLine(BusLine DeleteFromLine, BusLineStation busLineStation)
        {
            try
            {
                dl.DeleteStationInLine(dl.GetStationInLine(busLineStation.BusStationKey));
                var IndexToDelete = DeleteFromLine.StationsInLine.ToList().FindIndex(d => d.BusStationKey == busLineStation.BusStationKey);
                DO.ConsecutiveStations stations = new DO.ConsecutiveStations
                {
                    StationCodeOne = DeleteFromLine.StationsInLine.ToList()[IndexToDelete- 1].BusStationKey,
                    StationCodeTwo = busLineStation.BusStationKey
                };
                DeleteFromLine.StationsInLine.ToList().RemoveAt(IndexToDelete);
                dl.DeleteConsecutiveStations(stations);
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
                   orderby busLine.StationsInLine.Count()
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
        /// <summary>
        /// The function receives a User for updating
        /// </summary>
        /// <param name="busLine"></param>
        public void UpdateUser(User user)
        {
            try
            {
                dl.UpdatUser(UserBoDoAdapter(GetUser(user.UserName)));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that receives a DO type bus line object and returns a BO type bus line
        /// </summary>
        /// <param name="UserDO"></param>
        /// <returns></returns>
        public BO.User UserDoBoAdapter(DO.User UserDO)
        {
            BO.User UserBO = GetUser(UserDO.UserName);
            UserDO.CopyPropertiesTo(UserBO);
            return UserBO;
        }
        /// <summary>
        /// A function that receives a BO type bus line object and returns a DO type bus line
        /// </summary>
        /// <param name="UserBO"></param>
        /// <returns></returns>
        public DO.User UserBoDoAdapter(BO.User UserBO)
        {
            DO.User UserDO = dl.GetUser(UserBO.UserName);
            UserBO.CopyPropertiesTo(UserDO);
            return UserDO;
        }
        /// <summary>
        /// The function Receives an object user and enters the database
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            try
            {
               dl.GetUser(user.UserName);
               
            }
            catch(DO.IdAlreadyExistsException)
            {
                dl.AddUser(UserBoDoAdapter(user));
            }
        }
        /// <summary>
        /// The function Receives an object user to Deletion
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            try
            {
                dl.DeleteUser(dl.GetUser(user.UserName));
            }
            catch(BO.IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function returns all user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            return from User in dl.GetUsers()
                   select UserDoBoAdapter(User);
        }
        /// <summary>
        /// The function receives an ID number and returns the corresponding object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(string id)
        {
            try
            {
                return UserDoBoAdapter(dl.GetUser(id));
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function returns a collection that is divided into two user groups with access and those that do not
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, User>> GetUsersGroupByAllowingAccess()
        {
            return from User in GetUsers()
                   group User by User.AllowingAccess into groups
                   select groups;

        }
        /// <summary>
        /// lamda Function 
        /// Accepts predicate-Which checks a condition and returns the users that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersBy(Predicate<User> predicate)
        {
            return from User in GetUsers()  
                   where predicate(User)
                   select User;
        }
        /// <summary>
        /// A function that returns a collection of usernames
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetUsersNames()
        {
            return from User in GetUsers()
                   select User.UserName;
        }
        #endregion
        #region UserJourney
        /// <summary>
        /// The function receives a User for updating
        /// </summary>
        /// <param name="busLine"></param>
        public void UpdateUserJourney(UserJourney userJourney)
        {
            try
            {
                dl.UpdatUserJourney(UserJourneyBoDoAdapter(GetUserJourney(userJourney.UserName)));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that receives a DO type bus line object and returns a BO type bus line
        /// </summary>
        /// <param name="UserJourneyDO"></param>
        /// <returns></returns>
        public BO.UserJourney UserJourneyDoBoAdapter(DO.UserJourney UserJourneyDO)
        {
            BO.UserJourney UserJourneyBO = new BO.UserJourney();
            DO.User UserDO;
            string id = UserJourneyDO.UserName;
            try
            {
                UserDO = dl.GetUser(id);
            }
            catch (DO.IdAlreadyExistsException ex) { throw new BO.IdAlreadyExistsException("User Journey ID is illegal", ex); }
            UserDO.CopyPropertiesTo(UserJourneyDO);
            UserJourneyDO.CopyPropertiesTo(UserJourneyBO);
            return UserJourneyBO;
        }
        /// <summary>
        /// A function that receives a BO type bus line object and returns a DO type bus line
        /// </summary>
        /// <param name="userJourneyBO"></param>
        /// <returns></returns>
        public DO.UserJourney UserJourneyBoDoAdapter(BO.UserJourney userJourneyBO)
        {
            DO.UserJourney userJourneyDO = new DO.UserJourney();
            BO.User UserBO;
            string id = userJourneyBO.UserName;
            try
            {
                UserBO = GetUser(id);//פונקציה שאצל אלה
            }
            catch (DO.IdAlreadyExistsException ex) { throw new BO.IdAlreadyExistsException("Bus Line ID is illegal", ex); }
            UserBO.CopyPropertiesTo(userJourneyDO);
            userJourneyBO.CopyPropertiesTo(userJourneyDO);
            return userJourneyDO;
        }
        /// <summary>
        /// The function Receives an object user Journey and enters the database
        /// </summary>
        /// <param name="userJourney"></param>
        public void AddUserJourney(UserJourney userJourney)
        {
            try
            {
                dl.AddUserJourney(UserJourneyBoDoAdapter(userJourney));
            }
            catch(BO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function Receives an object user Journey to Deletion
        /// </summary>
        /// <param name="userJourney"></param>
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
        /// <summary>
        /// The function returns all users Journey
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserJourney> GetUsersJourney()
        {
            return from UserJourney in dl.GetUsersJourney()
                select UserJourneyDoBoAdapter(UserJourney);

        }
        /// <summary>
        /// The function receives an ID number and returns the corresponding object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserJourney GetUserJourney(string id)
        {

            return UserJourneyDoBoAdapter(dl.GetUserJourney(id));
        }
        /// <summary>
        /// The function returns a collection that is divided into groups by Boarding Station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, UserJourney>> GetUsersJourneyGroupByBoardingStation()
        {
            return from UserJourney in GetUsersJourney()
                   group UserJourney by UserJourney.BoardingStation into groups
                   select groups;

        }
        /// <summary>
        /// lamda Function 
        /// Accepts predicate-Which checks a condition and returns the users Journey that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersJourneyBy(Predicate<UserJourney> predicate)
        {
            return from User in GetUsersJourney()
                   where predicate(User)
                   select User;
        }
        /// <summary>
        ///  The function returns a collection that is divided into groups by bus line number
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, UserJourney>> GetUsersJourneyGroupByBusLineJourney()
        {
            return from UserJourney in GetUsersJourney()
                   group UserJourney by UserJourney.BusLineJourney into groups
                   select groups;
        }
        /// <summary>
        /// The function retuns the Travel History of user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<object> TravelHistory(string id)
        {
            return from sin in GetUserJourney(id).Drives
                   select sin;
        }
        public BusLine LineToGo(BusStation busStationCurrent, BusStation busStationtarget)
        {
            //var buses = from BusLineInStation in busStationCurrent.BusLinesInStation
            //            let bus = GetBusLine(BusLineInStation.BusLineNumber)
            //            where bus.StationsInLine.ToList().FindIndex(p => p.BusStationKey == busStationtarget.BusStationKey) != -1
            //            where bus.StationsInLine.ToList().FindIndex(p => p.BusStationKey == busStationCurrent.BusStationKey) != -1
            //            where bus.StationsInLine.ToList().FindIndex(p => p.BusStationKey == busStationtarget.BusStationKey) < bus.StationsInLine.ToList().FindIndex(p => p.BusStationKey == busStationCurrent.BusStationKey)
            //            select new
            //            {
            //                busNumber = bus,
            //                Current = bus.StationsInLine.ToList().Find(p => p.BusStationKey == busStationtarget.BusStationKey),
            //                Target = bus.StationsInLine.ToList().Find(p => p.BusStationKey == busStationCurrent.BusStationKey),

            //            };
            //var busesOder = from BusLine in buses
            //                from  v in dl.ConsecutivesStations()
            //                where  v.StationCodeOne==BusLine.Current.BusStationKey
            //                where  BusLine.Target.BusStationKey==v.StationCodeTwo
            throw new NullReferenceException();

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


    

