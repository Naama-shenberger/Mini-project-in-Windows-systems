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
            busLineBO.LineStations = from sin in dl.GetBusLineStations(sin => sin.CodeStation == id)
                                     let BusLineStation = dl.GetBusLineStation(sin.CodeStation)
                                     select BusLineStation.CopyTobusLineStation(sin);
            return busLineBO;
        }
        /// <summary>
        /// The function gets an object to add nearby stations
        /// </summary>
        /// <param name="stations"></param>
        public void Addatleasttwostations(ConsecutiveStations stations)
        {
            try
            {
                if (stations.StationCodeOne.ToString().Length < 6 || stations.StationCodeTwo.ToString().Length < 6)
                    throw new IdAlreadyExistsException("Code Station number Must have at least 6 numbers");
                DO.ConsecutiveStations ConsecutiveStationsDO = new DO.ConsecutiveStations();
                stations.CopyPropertiesTo(ConsecutiveStationsDO);
                BusLine.busLineTwoStations.ToList().Add(stations);
                BusLine.busLineTwoStations=ConsecutiveStationsOrderByDistance(BusLine.busLineTwoStations);
            }
            catch(IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// Add a line to the list of bus lines
        //  The function gets a bus line to add
        /// </summary>
        /// <param name="busLine"></param>
        public void AddABusLine(BusLine busLine)
        {
            try
            {
                dl.GetBusLine(busLine.ID);
                if(BusLine.busLineTwoStations.ToList().Count==0)
                    throw new BO.IdAlreadyExistsException("You must add at least two stations to the line");
                busLine.LineStations.ToList().Add(BusLine.busLineTwoStations.ToList()[0]);
                BusLine.busLineTwoStations.ToList().Clear();
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line ID is illegal", ex);
            }
            catch (BO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line ID is illegal", ex);
            }
            dl.AddBusLine(BusLineBoDoAdapter(busLine));
          
        }
        /// <summary>
        /// The function gets a bus line and a stop station
        /// The function adds the station to the bus line
        /// </summary>
        /// <param name="AddToLine"></param>
        /// <param name="busLineStation"></param>
        public void AddBusStationToLine(BusLine AddToLine, BusLineStation busLineStation,float _Distance,TimeSpan _AverageTravelTime)
        {
            try
            {
                if (busLineStation.CodeStation.ToString().Length < 6)
                    throw new IdAlreadyExistsException("Code Station number Must have at least 6 numbers");
                dl.GetBusLineStation(busLineStation.CodeStation);
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("ERROR", ex);
            }
            dl.AddBusLineStation(BusLineStationBoDoAdapter(busLineStation));
            BO.ConsecutiveStations stations = new ConsecutiveStations();
            stations.StationCodeOne = AddToLine.ConsecutiveStations.ToList()[AddToLine.ConsecutiveStations.ToList().Count - 1].StationCodeTwo;
            stations.StationCodeTwo = busLineStation.CodeStation;
            stations.Flage = true;
            stations.Distance = _Distance;
            stations.AverageTravelTime = _AverageTravelTime;
            AddToLine.ConsecutiveStations.ToList().Add(stations);
            AddToLine.ConsecutiveStations= ConsecutiveStationsOrderByDistance(AddToLine.ConsecutiveStations);
            AddToLine.LineStations.ToList().Add(busLineStation);
            AddToLine.LineStations = busLineStationsStationsOrderByDistance(AddToLine.ConsecutiveStations,AddToLine.LineStations);
        }
        /// <summary>
        /// Function that receives a collection of successive stations and stations of lines 
        /// and sorted the collection stations of lines in the order of the collection of successive stations
        /// </summary>
        /// <param name="consecutiveStations"></param>
        /// <param name="busLineStations"></param>
        /// <returns></returns>
        public IEnumerable<BusLineStation> busLineStationsStationsOrderByDistance(IEnumerable<ConsecutiveStations> consecutiveStations, IEnumerable<BusLineStation> busLineStations)
        {
            return from ConsecutiveStations in consecutiveStations
                   join BusLineStation in busLineStations
                   on ConsecutiveStations.StationCodeOne equals BusLineStation.CodeStation
                   orderby ConsecutiveStations.CodeStation
                   select BusLineStation;
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
                DO.BusLineStation busLineStationDO = null;
                busLineStationDO.CopyPropertiesTo(busLineStation);
                dl.DeleteBusLineStation(busLineStationDO);
                DeleteFromLine.ConsecutiveStations.Where(p => p.CodeStation == busLineStation.CodeStation).Select(a => a.Active = false);
                DeleteFromLine.ConsecutiveStations = ConsecutiveStationsOrderByDistance(DeleteFromLine.ConsecutiveStations);
                DeleteFromLine.LineStations = (IEnumerable<BusLineStation>)DeleteFromLine.LineStations.Where(p => p.CodeStation == busLineStation.CodeStation).Select(a => a.Active = false);
                
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
          return BusLineDoBoAdapter(dl.GetBusLine(id));
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
        #region BusLineStation
        /// <summary>
        ///  A function that receives a DO type bus line object and returns a BO type bus line
        /// </summary>
        /// <param name="busLineStationDO"></param>
        /// <returns></returns>
        public BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation busLineStationDO)
        {
            BO.BusLineStation busLineStationBO = new BO.BusLineStation();
            DO.BusStation busStationDO;
            int id = busLineStationDO.CodeStation;
            try
            {
                busStationDO = dl.GetBusStation(id);//של אלה להחליף הכל לint 
            }
            catch (DO.IdAlreadyExistsException ex) { throw new BO.IdAlreadyExistsException("Bus Line ID is illegal", ex); }
            busStationDO.CopyPropertiesTo(busLineStationDO);
            busLineStationDO.CopyPropertiesTo(busLineStationBO);
            return busLineStationBO;
        }
        /// <summary>
        ///  A function that receives a BO type bus line object and returns a DO type bus line
        /// </summary>
        /// <param name="busLineStationBO"></param>
        /// <returns></returns>
        public DO.BusLineStation BusLineStationBoDoAdapter(BO.BusLineStation busLineStationBO)
        {
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            BO.BusStation busStationBO;
            int id = busLineStationBO.CodeStation;
            try
            {
                busStationBO = GetBusStation(id);//פונקציה שאצל אלה
            }
            catch (DO.IdAlreadyExistsException ex) { throw new BO.IdAlreadyExistsException("Bus Line ID is illegal", ex); }
            busStationBO.CopyPropertiesTo(busLineStationDO);
            busLineStationBO.CopyPropertiesTo(busLineStationDO);
            return busLineStationDO;
            
        }
        /// <summary>
        /// A function that receives a bus line station and adds it to the collection
        /// Provided it does not exist
        /// </summary>
        /// <param name="busLineStation"></param>
        public void AddBusLineStation(BusLineStation busLineStation)
        {
            try 
            {
                if (busLineStation.CodeStation.ToString().Length < 6)
                    throw new IdAlreadyExistsException(busLineStation.CodeStation.ToString());
                dl.GetBusLineStation(busLineStation.CodeStation);
            }
            catch(IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line Station ID is illegal", ex);
            }
            DO.BusLineStation busLineStationDO = null;
            busLineStation.CopyPropertiesTo(busLineStationDO);
            dl.AddBusLineStation(busLineStationDO);

        }
        /// <summary>
        /// A function that receives a bus line station for deletion
        /// </summary>
        /// <param name="busLineStation"></param>
        public void DeleteBusLineStation(BusLineStation busLineStation)
        {
            try
            {
                dl.DeleteBusLineStation(dl.GetBusLineStation(busLineStation.CodeStation));
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
           
        }
        /// <summary>
        /// The function Accepts predicate-Which checks a condition and returns the bus line stations that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BusLineStation> GetAllBusLineStations(Func<BusLineStation, bool> predicate = null)
        {
          return  from sic in dl.GetBusLineStationCode((id) => { return GetBusLineStation(id);})
                      let busLineStation = sic as BO.BusLineStation
                      where predicate(busLineStation)
                      select busLineStation;
        }
        /// <summary>
        /// The function receives an ID number and returns the corresponding object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLineStation GetBusLineStation(int id)
        {
            BO.BusLineStation BusLineStationBO = new BO.BusLineStation();
            DO.BusStation BusStationDO;
            try
            {
                BusStationDO = dl.GetBusStation(id.ToString());
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line Station ID is illegal", ex);
            }
            BusStationDO.CopyPropertiesTo(BusLineStationBO);
            DO.BusLine BusLineDO = dl.GetBusLine(id);
            BusLineDO.CopyPropertiesTo(BusLineStationBO);
            return BusLineStationBO;
        }
        #endregion
        #region LineOutForARide
        /// <summary>
        /// A function that receives a bus to the exit and adds it to the collection
        /// Provided it does not exist
        /// </summary>
        /// <param name="lineOutForARide"></param>
        public void AddLineOutForARide(LineOutForARide lineOutForARide)
        {
            try
            {
                dl.GetLineWayOut(lineOutForARide.ID);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("line Out For A Ride ID is illegal", ex);
            }
            DO.LineOutForARide LineOutForARideDO = null;
            lineOutForARide.CopyPropertiesTo(LineOutForARideDO);
            dl.AddLineWayOut(LineOutForARideDO);
        }
        /// <summary>
        /// The function receives a bus to exit for deletion
        /// </summary>
        /// <param name="lineOutForARide"></param>
        public void DeleteLineOutForARide(LineOutForARide lineOutForARide)
        {
            try 
            {
                DO.LineOutForARide lineOutForARideDO = null;
                lineOutForARide.CopyPropertiesTo(lineOutForARideDO);
                dl.DeleteLineWayOut(lineOutForARideDO);
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function returns a collection divided into groups according by the start time of the buses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<TimeSpan, LineOutForARide>> GetAllBusLinesGroupByExitStartTime()
        {
            return from LineOutForARide in GetLineOutForARides()
                   group LineOutForARide by LineOutForARide.ExitStart into groups
                   select groups;

        }
        /// <summary>
        /// The function returns a collection divided into groups according by the End time of the buses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<TimeSpan, LineOutForARide>> GetAllBusLinesGroupByTravelEndTime()
        {
            return from LineOutForARide in GetLineOutForARides()
                   group LineOutForARide by LineOutForARide.TravelEndTime into groups
                   select groups;
        }
        /// <summary>
        /// returns collection of all Lines Out For A Rides
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineOutForARide> GetLineOutForARides()
        {
            return (IEnumerable<LineOutForARide>)dl.LinesWayOut();
        }
        /// <summary>
        /// A function that returns a collection of all bus lines to the exit by condition-Predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<LineOutForARide> GetLineOutForARidesBy(Predicate<LineOutForARide> predicate)
        {
           return from sic in dl.GetBusLineNumbers((id) => { return GetLineOutForARide(id);})
                     let LineOutForARide = sic as BO.LineOutForARide
            where predicate(LineOutForARide)
                     select LineOutForARide;
        }
        /// <summary>
        /// The function receives an ID number and returns the corresponding object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LineOutForARide GetLineOutForARide(int id)
        {
            BO.LineOutForARide LineOutForARideBO = new BO.LineOutForARide();
            DO.Bus BusDO;
            try
            {
                BusDO = dl.GetBus(id);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line Station ID is illegal", ex);
            }
            BusDO.CopyPropertiesTo(LineOutForARideBO);
            DO.BusLine BusLineDO = dl.GetBusLine(id);
            BusLineDO.CopyPropertiesTo(LineOutForARideBO);
            return LineOutForARideBO;
        }
        #endregion
        #region ConsecutiveStations
        /// <summary>
        /// A function that accepts an object of the Consecutive stations type and adds it to the list
        /// </summary>
        /// <param name="consecutiveStations"></param>
        public void AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            try
            {
                dl.GetConsecutiveStations(consecutiveStations.StationCodeOne, consecutiveStations.StationCodeTwo);
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
            DO.ConsecutiveStations consecutiveStationsDO = null;
            consecutiveStationsDO.CopyPropertiesTo(consecutiveStations);
            dl.AddConsecutiveStations(consecutiveStationsDO);
        }
        /// <summary>
        /// A function that accepts Consecutive stations to deletion
        /// </summary>
        /// <param name="consecutiveStations"></param>
        public void DeleteConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            try
            {
                dl.DeleteConsecutiveStations(dl.GetConsecutiveStations(consecutiveStations.StationCodeOne, consecutiveStations.StationCodeTwo));
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException(ex.ToString());
            }
        }
        /// <summary>
        /// The function returns a collection of Consecutive Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsecutiveStations> GetConsecutiveStations()
        {
            return (IEnumerable<ConsecutiveStations>)dl.LinesWayOut();
        }
        /// <summary>
        /// The Function 
        /// Accepts predicate-Which checks a condition and returns the Consecutive Stations that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<ConsecutiveStations> GetConsecutiveStationsBy(Predicate<ConsecutiveStations> predicate)
        {
            return from sic in dl.GetBusLineStationCode((id) => { return GetBusLine(id); })
                   let ConsecutiveStations = sic as BO.ConsecutiveStations
                   where predicate(ConsecutiveStations)
                   select ConsecutiveStations;
        }
        /// <summary>
        /// The function receives two ID numbers and returns the corresponding object
        /// two ID- code Consecutive Stations
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public ConsecutiveStations GetConsecutiveStations(int id1, int id2)
        {
            BO.ConsecutiveStations consecutiveStations = new ConsecutiveStations();
            dl.GetConsecutiveStations(id1, id2).CopyPropertiesTo(consecutiveStations);
            return consecutiveStations;
        }
        public IEnumerable<ConsecutiveStations> ConsecutiveStationsOrderByDistance(IEnumerable<ConsecutiveStations> busLineStations)
        {
            return from ConsecutiveStations in busLineStations
                   orderby ConsecutiveStations.Distance
                   select ConsecutiveStations;
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


    

