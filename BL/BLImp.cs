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
        BO.BusStation busStationDOBOAdapter(DO.BusStation stationDO)
        {
            BO.BusStation stationBO = new BO.BusStation();
            stationDO.CopyPropertiesTo(stationBO);

        }
        public void RefillingBus(Bus bus)
        {
            bus.KilometersGas = 0;
        }
        /// <summary>
        /// Treatment function
        /// The function calls a Refueling function because every bus that goes to treatment also comes out refueled 
        /// </summary>
        /// <param name="b"></param>
        public void BusInTreatment(Bus b)
        {
            b.KilometersTreatment = 0;
            RefillingBus(b);
            b.DateTreatment = DateTime.Now;
        }
        /// <summary>
        /// A function that checks how many numbers the user needs to type for the number plate
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int NumberOflicensePlate(Bus b)
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
        public bool DateCheck(Bus b)
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
            return from item in dl.GetAllBuss()
                   select; 
            //return (IEnumerable<Bus>)(from item in dl.() select item);
        }
        /// <summary>
        /// A function that returns 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, Bus>> GetAllBusslicensePlate()
        {
            return from Bus in GetAllBusLines()
                   group Bus by (TreatmentIsNeeded(int.Parse(Bus.LicensePlate))) into groups
                   select groups;
        }
        /// <summary>
        /// a function that returns all License Plate
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetNumberbuss()
        {
            return from item in dl.GetBusNum((id) => { return GetBus(id); })
                   let bus = item as BO.Bus
                   orderby bus.LicensePlate
                   select int.Parse(bus.LicensePlate);
        }
        /// <summary>
        /// A function that returns all buss thet need treatment
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, Bus>> GetAllBusNeedTreatment()
        {
            return from Bus in GetAllBusLines()
                   group Bus by ((NumberOflicensePlate(Bus) == Bus.LicensePlate.Length)) into groups
                   select groups;
        }
        /// <summary>
        /// lamda Function 
        /// Accepts predicate-Which checks a condition and returns the buses that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate) => from sic in dl.GetBusNum((id) => { return GetBus(id); })
                                                                      let bus = sic as BO.Bus
                                                                      where predicate(bus)
                                                                      select bus;
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
            if (!(b_find.KilometersTreatment < 2000 && !DateCheck(busBO)))
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
        #region Bus Station
        /// <summary>
        /// A function that receives an ID number of a station and returns the corresponding station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusStation GetBusStation(int id)
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
        /// <summary>
        ///  A function that returns all bus stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusStation> GetAllBusStation()
        {
            return (IEnumerable<BusStation>)(from item in dl.BusStations() select item);
        }
        /// <summary>
        ///  A function that returns all bus station numbers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetNumberStation()
        {
            return from item in dl.GetBusStationNumbers((id) => { return GetBusLine(id); })
                   let busStation = item as BO.BusStation
                   orderby busStation.BusStationKey
                   select busStation.BusStationKey;

        }
        /// <summary>
        /// Add a bus station to the list of bus stations
        /// The function gets a bus station to add  
        /// </summary>
        /// <param name="station"></param>
        public void AddBusStation(BusStation station)
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
        public void DeleteBusStation(BusStation station)
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
        /// <summary>
        /// a function that returns all bus lines thet passing through the station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> GetAllBusLineInStation()
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

