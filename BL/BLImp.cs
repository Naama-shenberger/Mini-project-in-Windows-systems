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
        #region Bus
        /// <summary>
        /// Refueling function
        /// </summary>
        /// <param name="bus"></param>
        private void refillingBus(Bus bus)
        {
            bus.KilometersGas = 0;
        }
        /// <summary>
        /// Treatment function
        /// The function calls a Refueling function because every bus that goes to treatment also comes out refueled 
        /// </summary>
        /// <param name="b"></param>
        private void BusInTreatment(Bus b)
        {
            b.KilometersTreatment = 0;
            refillingBus(b);
            b.DateTreatment = DateTime.Now;
        }
        /// <summary>
        /// A function that checks how many numbers the user needs to type for the number plate
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private int NumberOflicensePlate(Bus b)
        {
            int year;
            int.TryParse(b.DateActivity.Year.ToString(), out year);
            return year < 2018 ? 7 : 8;

        }
        /// <summary>
        ///  A function that checks if a year has passed since the last treatment
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool dateCheck(Bus b)
        {
            int day;
            int month;
            int year;
            int.TryParse(b.DateTreatment.Day.ToString(), out day);
            int.TryParse(b.DateTreatment.Month.ToString(), out month);
            int.TryParse(b.DateTreatment.Year.ToString(), out year);
            DateTime currentDate = DateTime.Now;
            if (int.Parse(currentDate.Day.ToString()) == day && int.Parse(currentDate.Month.ToString()) == month && int.Parse(currentDate.Year.ToString()) < year || int.Parse(currentDate.Year.ToString()) < year)
                return true;
            return false;

        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding bus 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bus GetBus(int id)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(id);
            }
            catch(DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
            BO.Bus busBO = null;
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        /// <summary>
        /// Add a bus to the list of buss
        /// The function gets a bus to add 
        /// </summary>
        /// <param name="bus"></param>
        public void AddABus(Bus bus)
        {
            try
            {
                if (NumberOflicensePlate(bus)==bus.LicensePlate.Length)
                    dl.AddBus(dl.GetBus(int.Parse(bus.LicensePlate)));
                else
                    throw new InvalidOperationException("Invalid license number input");
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
        }
        /// <summary>
        ///  A function that checks if the vehicle needs to be refueled
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool FuelCondition(Bus b)
        {
            DO.Bus b_find = dl.GetBus(int.Parse(b.LicensePlate));
            if (b_find.KilometersGas > 1200)
                return true;
            return false;
        }
        /// <summary>
        ///  A function that deletes a bus from the list of buss
        /// </summary>
        /// <param name="bus"></param>
        public void DeleteBus(Bus bus)
        {
            DO.Bus b_find;
            try
            {
                b_find = dl.GetBus(int.Parse(bus.LicensePlate));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
            dl.DeleteBus(b_find);

        }
        /// <summary>
        /// A function that returns all bus
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> GetAllBus()
        {
            return (IEnumerable<Bus>)(from item in dl.Buss() select item);
        }
        public IEnumerable<IGrouping<string, Bus>> GetAllBusslicensePlate()
        {
        }
        public IEnumerable<int> GetNumberbuss()
        {
        }
        public IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate) { }
        /// <summary>
        /// A function that checks if a year has passed since the last treatment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TreatmentIsNeeded(int id)
        {
            DO.Bus b_find;
            try
            {
                b_find = dl.GetBus(id);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
            BO.Bus busBO = null;
            b_find.CopyPropertiesTo(busBO);
            if (!(b_find.KilometersTreatment < 2000 && !dateCheck(busBO)))
                return true;
            return false;

        }
        /// <summary>
        /// A function that checks if the vehicle needs to fill oil or 
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool BusCondition(Bus bus)
        {
            DO.Bus b_find;
            try
            {
                b_find = dl.GetBus(int.Parse(bus.LicensePlate));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
            if (b_find.OilCondition||b_find.AirTire>75)
                return true;
            return false;
        }
        #endregion
        #region Bus in Drive
        /// <summary>
        /// A function that receives an ID number and returns the corresponding bus in drive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BusDrive GetBusDrive(int id)
        {
            DO.BusDrive driveDO;
            try
            {
                driveDO = dl.GetBusDrive(id);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
            BO.BusDrive DriveBO = null;
            driveDO.CopyPropertiesTo(DriveBO);
            return DriveBO;
        }
        IEnumerable<BusDrive> GetAllBusInDrive()
        {


        }
        /// <summary>
        /// Add a bus drive to the list of buss
        /// The function gets a bus drive to add 
        /// </summary>
        /// <param name="DriveBO"></param>
        void AddBusDrive(BusDrive DriveBO)
        {
            DO.BusDrive DriveDO = null;
            DriveBO.CopyPropertiesTo(DriveDO);
            try
            {
                if (!(TreatmentIsNeeded(int.Parse(DriveBO.LicensePlate)))&& BusCondition(DriveBO))
                    dl.AddBusDrive(dl.GetBusDrive(int.Parse(DriveDO.LicensePlate)));
                else
                    throw new InvalidOperationException("Bus Can't go for a ride do to its condition ");
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
        }
        /// <summary>
        /// A function that deletes a bus drive from the list of buss that are in drives
        /// </summary>
        /// <param name="busDrive"></param>
        void DeleteBusDrive(BusDrive busDrive)
        {
            DO.BusDrive busDrive_find;
            try
            {
                busDrive_find = dl.GetBusDrive(int.Parse(busDrive.LicensePlate));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus in drive ID is illegal", ex);
            }

            dl.DeleteBusDrive(busDrive_find);
        }
        #endregion
        #region Bus Station
        /// <summary>
        /// A function that receives an ID number of a station and returns the corresponding station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BusStation GetBusStation(string id)
        {
            DO.BusStation stationDO;
            try
            {
                stationDO = dl.GetBusStation(id);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus ID is illegal", ex);
            }
            BO.BusStation stationBO = null;
            stationDO.CopyPropertiesTo(stationBO);
            return stationBO;
        }
        IEnumerable<int> GetNumberStation()
        { 
        }
        /// <summary>
        /// Add a bus station to the list of bus stations
        /// The function gets a bus station to add  
        /// </summary>
        /// <param name="station"></param>
        void AddBusStation(BusStation station)
        {
            try
            {
                dl.AddBusStation(dl.GetBusStation(station.BusStationKey));
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus station is illegal", ex);
            }

        }
        /// <summary>
        /// A function that deletes a bus station from the list of buss that are in drives
        /// the function resives the bis station to delete
        /// </summary>
        /// <param name="station"></param>
        void DeleteBusStation(BusStation station)
        {
            DO.BusStation stationDO;
            try
            {
                stationDO = dl.GetBusStation(station.BusStationKey);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new BO.IdAlreadyExistsException("Bus station dose not exists", ex);
            }
            dl.DeleteBusStation(stationDO);
        }
        #endregion
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
        public void AddBusStationToLine(BusLine AddToLine, BusLineStation busLineStation)
        {
            AddToLine.LineStations.ToList().Add(busLineStation);
        }
        /// <summary>
        /// The function receives a bus line for deletion
        /// </summary>
        /// <param name="busLine"></param>
        public void DeleteBusLine(BusLine busLine)
        { 
            dl.DeleteBusLine(dl.GetBusLine(busLine.ID).C);
        }
        /// <summary>
        /// The function gets a bus line and the bus stop for deletion
        /// The function deletes the station from the received line
        /// </summary>
        /// <param name="DeleteFromLine"></param>
        /// <param name="busLineStation"></param>
        public void DeleteBusLineStationFromeLine(BusLine DeleteFromLine,BusLineStation busLineStation)
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

