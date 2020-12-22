using BLAPI;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DO.Enums;

namespace BL
{
    internal class BLImp : IBL
    {
        IDal dl = DalFactory.GetDal();
        #region Bus Line
        /// <summary>
        /// Add a line to the list of bus lines
        //The function gets a bus line to add
        /// </summary>
        /// <param name="busLine"></param>
        public void AddABusLine(BusLine busLine)
        {
            dl.AddBusLine(dl.GetBusLine(busLine.ID));
        }
        /// <summary>
        /// The function gets a bus line and a stop station
        /// The function adds the station to the bus line
        /// </summary>
        /// <param name="AddToLine"></param>
        /// <param name="busLineStation"></param>
        public void AddBusStation(BusLine AddToLine, BusLineStation busLineStation)
        {
            AddToLine.LineStations.ToList().Add(busLineStation);
        }
        /// <summary>
        /// The function receives a bus line for deletion
        /// </summary>
        /// <param name="busLine"></param>
        public void DeleteBusLine(BusLine busLine)
        { 
            dl.DeleteBusLine(dl.GetBusLine(busLine.ID));
        }
        /// <summary>
        /// The function gets a bus line and the bus stop for deletion
        /// The function deletes the station from the received line
        /// </summary>
        /// <param name="DeleteFromLine"></param>
        /// <param name="busLineStation"></param>
        public void DeleteBusLineStation(BusLine DeleteFromLine,BusLineStation busLineStation)
        {

            DeleteFromLine.LineStations= (IEnumerable<BusLineStation>)DeleteFromLine.LineStations.Where(p => p.CodeStation == busLineStation.CodeStation).Select(a => a.Active = false);
        }
        /// <summary>
        /// A function that returns all bus lines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> GetAllBusLines()
        {
            return (IEnumerable<BusLine>)(from item in dl.BusLines()select item);
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

