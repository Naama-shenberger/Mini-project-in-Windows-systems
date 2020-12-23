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
        /// Add a line to the list of bus lines
        //  The function gets a bus line to add
        /// </summary>
        /// <param name="busLine"></param>
        public void AddABusLine(BusLine busLine)
        {
            try
            {
                dl.GetBusLine(busLine.ID);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line ID is illegal", ex);
            }
            DO.BusLine busLineDO=null;
            busLine.CopyPropertiesTo(busLineDO);
            dl.AddBusLine(busLineDO);
          
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
                if (busLineStation.CodeStation.ToString().Length < 6)
                    throw new IdAlreadyExistsException(busLineStation.CodeStation.ToString());
                dl.GetBusLineStation(busLineStation.CodeStation);
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("ERROR", ex);
            }
            DO.BusLineStation busLineStationDO = null;
            busLineStation.CopyPropertiesTo(busLineStationDO);
            dl.AddBusLineStation(busLineStationDO);
            AddToLine.LineStations.ToList().Add(busLineStation);
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
            return (IEnumerable<BusLine>)(from item in dl.BusLines() select item);
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
            BO.BusLine BusLineBO = new BO.BusLine();
            DO.Bus BusDO;
            try
            {
                BusDO = dl.GetBus(id);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus line ID is illegal", ex);
            }
            BusDO.CopyPropertiesTo(BusLineBO);
            DO.BusLine BusLineDO = dl.GetBusLine(id);
            BusLineDO.CopyPropertiesTo(BusLineBO);
            return BusLineBO;
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
        #endregion
        #region  User
        public void AddUser(User user)
        {
            //try
            //{
            //    dl.AddUser(user)
            //}
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

        }
        public  IEnumerable<IGrouping<bool, User>> GetUsersGroupByAllowingAccess()
        {

        }
        public  IEnumerable<User> GetUsersBy(Predicate<User> predicate)
        {

        }
        public  IEnumerable<string> GetUsersNames()
        {

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


    

