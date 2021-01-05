using BLAPI;
using BO;
using DLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DO.Enums;
namespace BL
{
    internal class BLImp : IBL
    {
        IDal dl = DalFactory.GetDal();
        Random random = new Random();
        #region Bus
        /// <summary>
        /// The function receives a bus for updating
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBus(Bus bus)
        {
            try
            {
                dl.UpdateBus(BusBoDoAdapter(GetBus(bus.LicensePlate)));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that receives a DO type bus object and returns a BO type bus
        /// </summary>
        /// <param name="busDO"></param>
        /// <returns></returns>
        public BO.Bus BusDoBoAdapter(DO.Bus busDO)
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;

        }
        /// <summary>
        /// A function that receives a BO type bus object and returns a DO type bus 
        /// </summary>
        /// <param name="busBO"></param>
        /// <returns></returns>
        public DO.Bus BusBoDoAdapter(BO.Bus busBO)
        {
            DO.Bus busDO = new DO.Bus();
            busBO.CopyPropertiesTo(busDO);
            return busDO;
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding bus 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bus GetBus(string id)
        {
            try
            {
                return BusDoBoAdapter(dl.GetBus(id));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus ID is illegal", ex);
            }

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
                if (NumberOflicensePlate(bus) == bus.LicensePlate.Length)
                    if (dl.GetBus(bus.LicensePlate) != null)
                        throw new BO.IdException("Bus already Exists");
                    else
                        throw new BO.IdException("Invalid license number input");
                else
                    throw new BO.IdException("Invalid license number input");
            }
            catch (DO.IdException)
            {
                if (TreatmentIsNeeded(bus) || FuelCondition(bus))
                {
                    bus.Status = Enums.Status.Dangerous;
                    bus.Active = false;
                }
                else
                {
                    bus.Status = Enums.Status.Ready_to_go;
                    bus.Active = true;
                }
                dl.AddBus(BusBoDoAdapter(bus));
            }
            catch (BO.IdException)
            {
                throw new BO.IdException("Bus ID is illegal");
            }
        }
        /// <summary>
        ///  A function that deletes a bus from the list of buss
        /// </summary>
        /// <param name="bus"></param>
        public void DeleteBus(Bus bus)
        {

            try
            {
                dl.DeleteBus(dl.GetBus(bus.LicensePlate));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus ID is illegal", ex);
            }
        }
        /// <summary>
        /// refilling gas in bus
        /// </summary>
        /// <param name="bus"></param>
        public void RefillingBus(Bus bus)
        {
            bus.KilometersGas = 0;
            if(TreatmentIsNeeded(bus))
                bus.Status = Enums.Status.Ready_to_go;
            dl.UpdateBus(BusBoDoAdapter(bus));
            
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
            b.Status = Enums.Status.Ready_to_go;
            dl.UpdateBus(BusBoDoAdapter(b));
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
        ///  A function that checks if the vehicle needs to be refueled
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool FuelCondition(Bus bus)
        {
            if (bus.KilometersGas > 1200)
                return true;
            return false;
        }
        /// <summary>
        /// A function that checks if a year has passed since the last treatment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TreatmentIsNeeded(Bus bus)
        {
            if (!(bus.KilometersTreatment < 2000 && !DateCheck(bus)))
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
                b_find = dl.GetBus(bus.LicensePlate);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus ID is illegal", ex);
            }
            if (b_find.OilCondition || b_find.AirTire > 75)
                return true;
            return false;
        }
        /// <summary>
        /// A function that returns all bus
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> GetAllBus()
        {
            return from bus in dl.GetAllBuss()
                   select BusDoBoAdapter(bus);
        }
        /// <summary>
        /// A function that returns 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, Bus>> GetAllBussGroupByTreatmentIsNeeded()
        {
            return from Bus in GetAllBus()
                   group Bus by (TreatmentIsNeeded(Bus)) into groups
                   select groups;
        }
        /// <summary>
        /// a function that returns all License Plate
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetLicensePlateBuss()
        {
            return from item in dl.GetAllBuss()
                   let bus = BusDoBoAdapter(item) as BO.Bus
                   orderby bus.LicensePlate
                   select bus.LicensePlate;
        }
        /// <summary>
        /// lamda Function 
        /// Accepts predicate-Which checks a condition and returns the buses that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate) => from sic in dl.GetAllBuss()
                                                                      let bus = BusDoBoAdapter(sic) as BO.Bus
                                                                      where predicate(bus)
                                                                      select bus;

        #endregion
        #region Bus Station
        public void addTimeBetStation(BusStation s1, BusStation s2)
        {
            try
            {
                DO.ConsecutiveStations s = new DO.ConsecutiveStations() { StationCodeOne = s1.BusStationKey, StationCodeTwo = s2.BusStationKey };
                dl.AddConsecutiveStations(s);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }

        }
        public TimeSpan getTimeBetStation(BusStation s1, BusStation s2)
        {
            DO.ConsecutiveStations s;
            try
            {
                s = dl.GetConsecutiveStations(s1.BusStationKey, s2.BusStationKey);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
            return s.AverageTravelTime;
        }
        
        /// <summary>
        /// A function that receives a DO type bus station object and returns a BO type bus station
        /// </summary>
        /// <param name="stationDO"></param>
        /// <returns></returns>
        public BO.BusStation BusStationDoBoAdapter(DO.BusStation stationDO)
        {
            //BO.BusStation stationBO = new BusStation();
            //List<DO.BusLineInStation> lines=from sis in dl.BusLineStations()
            //                                from sic in dl.BusLineInStations()
            //                                where sis.BusStationKey== stationDO.BusStationKey 
            //                                where sic.BusLineNumber==
            //                                let line = dl.GetBusLine(sis.ID)
            //stationDO.CopyPropertiesTo(stationBO);
            //stationBO.ListBusLinesInStation = from sic in dl.BusLineInStations()
            //                                  where sic.BusLineNumber==dl.GetBusLineStation(sic.BusLineNumber).ID
            //                                  let line = dl.GetBusLineInStation(dl.GetBusLine(sic.BusStationKey).ID)
            //                                  select DeepCopyUtilities.CopyToLineInStation( line);
            //return stationBO;
            BO.BusStation stationBO = new BusStation();
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.ListBusLinesInStation = from sic in dl.BusLineInStations()
                                              let line = dl.GetBusLine(sic.BusLineNumber)
                                              select line.CopyToLineInStation(sic);
            return stationBO;
        }
        /// <summary>
        /// A function that receives a BO type bus station object and returns a DO type bus station
        /// </summary>
        /// <param name="busStationBO"></param>
        /// <returns></returns>
        public DO.BusStation BusStationBoDoAdapter(BO.BusStation busStationBO)
        {
            DO.BusStation busStationDO = new DO.BusStation();
            busStationBO.CopyPropertiesTo(busStationDO);
            return busStationDO;
        }
        /// <summary>
        /// A function that receives a BO type bus line in station object and returns a DO type bus line in station
        /// </summary>
        /// <param name="busLineInStationBO"></param>
        /// <returns></returns>
        public DO.BusLineInStation BusLineInStationBoDoAdapter(BO.BusLineInStation busLineInStationBO)
        {
            DO.BusLineInStation busLineInStationDO = new DO.BusLineInStation();
            busLineInStationBO.CopyPropertiesTo(busLineInStationDO);

            return busLineInStationDO;
        }
        /// <summary>
        /// adds a line to stations line list
        /// </summary>
        /// <param name="AddToStation"></param>
        /// <param name="busLineInStation"></param>
        public void AddBusLineToStation(BusStation AddToStation, BusLineInStation busLineInStation)
        {
            try
            {

                AddToStation.ListBusLinesInStation.ToList().Add(busLineInStation);
                ///?  busLineStation.ToList().ForEach(i => AddToLine.StationsInLine.Append(i));
                //number in line need to f
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.Message);
            }
        }
        /// <summary>
        /// deletes a bus line from stations list
        /// </summary>
        /// <param name="DeleteFromStation"></param>
        /// <param name="busLineInStation"></param>
        public void DeleteBusLineInStation(BusStation DeleteFromStation, BusLineInStation busLineInStation)
        {
            try
            {
                var IndexToDelete = DeleteFromStation.ListBusLinesInStation.ToList().FindIndex(d => d.BusLineNumber == busLineInStation.BusLineNumber);
                DeleteFromStation.ListBusLinesInStation.ToList().RemoveAt(IndexToDelete);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus ID is illegal", ex);
            }

            return BusStationDoBoAdapter(stationDO);
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
                dl.GetBusStation(station.BusStationKey);
            }
            catch (DO.IdException)
            {
                dl.AddBusStation(BusStationBoDoAdapter(station));
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus station dose not exists", ex);
            }
            dl.DeleteBusStation(stationDO);
            dl.DeleteBusLineStation(new DO.BusLineStation() { BusStationKey = stationDO.BusStationKey, Active = true });
        }
        /// <summary>
        ///  A function that returns all bus stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusStation> GetAllBusStation()
        {
            return from item in dl.BusStations() 
               select BusStationDoBoAdapter(item);
        }

        /// <summary>
        /// a function that returns all bus lines thet passing through the station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineInStation> GetAllBusLineInStation()
        {
            return from sic in dl.BusLineInStations()
                   let line = dl.GetBusLine(sic.BusLineNumber)
                   select line.CopyToLineInStation(sic);
        }
        /// <summary>
        /// The function receives a bus Station for updating
        /// </summary>
        /// <param name="busStation"></param>
        public void UpdateBusStation(BusStation busStation)
        {
            try
            {
                dl.UpdateBusStation(BusStationBoDoAdapter(GetBusStation(busStation.BusStationKey)));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }

        #endregion
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        ///  A function that receives tracking stations and updates the Travel Time between them
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="time"></param>
        public void UpdateTravelTimeBetweenstations(BO.BusStation station1,BO.BusStation station2, TimeSpan time)
        {
            
            try
            {
                DO.ConsecutiveStations consecutiveStations = dl.GetConsecutiveStations(station1.BusStationKey, station2.BusStationKey);
                consecutiveStations.AverageTravelTime = time;
                dl.UpdateConsecutiveStations(consecutiveStations);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
            busLineDO.CopyPropertiesTo(busLineBO);

            busLineBO.StationsInLine = from sin in dl.BusLineStations()
                                       where sin.ID == busLineDO.ID
                                       let station = dl.GetBusLineStation(sin.BusStationKey)
                                       select DeepCopyUtilities.CopyToStationInLine(station);
            return busLineBO;
        }
       
        /// <summary>
        /// Add a line to the list of bus lines
        //  The function gets a bus line to add
        /// </summary>
        /// <param name="busLine"></param>
        public void AddBusLine(BusLine busLine, IEnumerable<BusLineStation> busLineStation)
        {

            try
            {
                if (busLineStation.ToList().Count<2)
                    throw new BO.IdException("You must add at least two stations to the line");
              
                //DO.ConsecutiveStations stations = new DO.ConsecutiveStations
                //{
                //    StationCodeOne = stationInLineOne.BusStationKey,
                //    StationCodeTwo = stationInLineTwo.BusStationKey,
                //    Distance = (float)(random.NextDouble() * 1850 + 150),
                //    Flage = true,
                //    AverageTravelTime = new TimeSpan(random.Next(0, 4), random.Next(0, 60), random.Next(0, 60))
                //};
                //BusStation busLineInStation = new BusStation();
                //stationInLineOne.CopyPropertiesTo(busLineInStation);
                //BusLineInStation lineInStation = new BusLineInStation();
                //busLine.CopyPropertiesTo(lineInStation);
                //busLineInStation.ListBusLinesInStation.Append(lineInStation);
                //stationInLineTwo.CopyPropertiesTo(busLineInStation);
                //busLineInStation.ListBusLinesInStation.Append(lineInStation);

                //dl.AddConsecutiveStations(stations);
                dl.GetBusLine(busLine.ID);
            }
            catch (DO.IdException)
            {
                dl.AddBusLine(BusLineBoDoAdapter(busLine));
            }
            catch (BO.IdException)
            {
                throw new BO.IdException("Bus line ID is illegal");
            }
        }
       
        public DO.BusLineStation BusLineStationBoDoAdapter(BO.BusLineStation busLineStationBO)
        {
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            busLineStationBO.CopyPropertiesTo(busLineStationDO);

            return busLineStationDO;
        }
        /// <summary>
        /// The function gets a bus line and a stop station
        /// The function adds the station to the bus line
        /// </summary>
        /// <param name="AddToLine"></param>
        /// <param name="busLineStation"></param>
        public void AddBusStationToLine(BusLine AddToLine,IEnumerable<BusLineStation> busLineStation)
        {
            try
            {

                AddToLine.StationsInLine = AddToLine.StationsInLine.Concat(busLineStation).Distinct();


                ///?  busLineStation.ToList().ForEach(i => AddToLine.StationsInLine.Append(i));
                //busLineStation.ToList().ForEach(i => dl.AddBusLineStation(BusLineStationBoDoAdapter(i)));
                //בדיקה אם יש תחנות עוקבות
                //number in line need to fix 

            }
            catch(DO.IdException ex)
            {
                throw new BO.IdException(ex.Message);
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException("ERROR", ex);
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
                DeleteFromLine.StationsInLine = from sin in DeleteFromLine.StationsInLine
                                                where sin.BusStationKey != busLineStation.BusStationKey
                                                select sin;
                //var IndexToDelete = DeleteFromLine.StationsInLine.ToList().FindIndex(d => d.BusStationKey == busLineStation.BusStationKey);
                //dl.GetConsecutiveStations(busLineStation.BusStationKey, DeleteFromLine.StationsInLine.ToList()[IndexToDelete - 1].BusStationKey);
                //dl.GetConsecutiveStations(DeleteFromLine.StationsInLine.ToList()[IndexToDelete - 1].BusStationKey, busLineStation.BusStationKey);
                //DO.ConsecutiveStations stations = new DO.ConsecutiveStations
                //{
                //    StationCodeOne = DeleteFromLine.StationsInLine.ToList()[IndexToDelete - 1].BusStationKey,
                //    StationCodeTwo = busLineStation.BusStationKey
                //};
                //DeleteFromLine.StationsInLine.ToList().RemoveAt(IndexToDelete);
                //dl.DeleteConsecutiveStations(stations);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that returns all bus lines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLine> GetAllBusLines()
        {
            return from BusLine in dl.BusLines() 
                   select BusLineDoBoAdapter(BusLine);
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
            return from item in dl.BusLines()
                   let busLine = BusLineDoBoAdapter(item) as BO.BusLine
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// lamda Function 
        /// Accepts predicate-Which checks a condition and returns the lines that Sustainers the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate) => from sic in dl.BusLines()
                                                                                  let busLine = BusLineDoBoAdapter(sic) as BO.BusLine
                                                                                  where predicate(busLine)
                                                                                  select busLine;
        /// <summary>
        /// A  Function that returns a Collection with all the bus lines number Sorting
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetNumberLines()
        {
            return from item in dl.BusLines()
                   let busLine = BusLineDoBoAdapter(item) as BO.BusLine
                   orderby busLine.BusLineNumber
                   select busLine.BusLineNumber;
        }
        #endregion
        #region Bus Line In Station
        /// <summary>
        /// A function that receives a DO type bus line in statin object and returns a BO type bus bus line in statin
        /// </summary>
        /// <param name="lineDO"></param>
        /// <returns></returns>
        public BO.BusLineInStation LineInStationDoBoAdapter(DO.BusLineInStation lineDO)
        {
            BO.BusLineInStation lineBO = null;
            lineDO.CopyPropertiesTo(lineBO);
            return lineBO;

        }
        /// <summary>
        /// A function that receives a BO type bus line in statin object and returns a DO type bus bus line in statin
        /// </summary>
        /// <param name="lineBO"></param>
        /// <returns></returns>
        public DO.BusLineInStation LineStationBoDoAdapter(BO.BusLineInStation lineBO)
        {
            DO.BusLineInStation lineDO = null;
            lineBO.CopyPropertiesTo(lineDO);
            return lineDO;
        }
        /// <summary>
        /// A function that receives an ID number and returns the corresponding bus line in station 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusLineInStation GetLineInStation(int id)
        {
            DO.BusLineInStation lineInStationDO;
            try
            {
                lineInStationDO = dl.GetBusLineInStation(id);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus ID is illegal", ex);
            }
            return LineInStationDoBoAdapter(lineInStationDO);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineInStation> GetAllBusLineInStations()
        {
            return from BusLineInStation in dl.BusLineInStations()
                   select LineInStationDoBoAdapter(BusLineInStation);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="busLineInStations"></param>
        /// <returns></returns>
        public IEnumerable<object> BusLineDetails(IEnumerable<BusLineInStation> busLineInStations)
        {
            return from BusLine in dl.BusLines()
                   from BusLineInStation in busLineInStations
                   where BusLine.BusLineNumber == BusLineInStation.BusLineNumber
                   select new
                   {
                       BusLineNumber= BusLineInStation.BusLineNumber,
                       Area= BusLine.Area,
                       FirstStopNumber= BusLine.FirstStopNumber,
                       LastStopNumber = BusLine.LastStopNumber,
                   };
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
            catch (DO.IdException)
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
            catch (BO.IdException ex)
            {
                throw new IdException(ex.ToString());
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
            catch (DO.IdException ex) { throw new BO.IdException("User Journey ID is illegal", ex); }
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
            catch (DO.IdException ex) { throw new BO.IdException("Bus Line ID is illegal", ex); }
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
            catch (BO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
        #region Bus Line Station
        public void AddBusLinesStation(IEnumerable<BusLineStation> busLineStations)
        {
            try
            {
                busLineStations.ToList().ForEach(item => dl.AddBusLineStation(BusLineStationBoDoAdapter(item)));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.Message);
            }
        }
        public BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation busLineStation)
        {
            BO.BusLineStation busLineStationBO = new BusLineStation();
            busLineStation.CopyPropertiesTo(busLineStationBO);
            return busLineStationBO;
        }
        public IEnumerable<BusLineStation> GetAllBusLineStations()
        {
            return from BusLineStation in dl.BusLineStations()
                   select BusLineStationDoBoAdapter(BusLineStation);
        }
        public IEnumerable<object> StationDetails(IEnumerable<BusLineStation> busLineStations)
        {
            
                 return  from BusStation in dl.BusStations()
                         from BusLineStation in busLineStations
                         where BusStation.BusStationKey == BusLineStation.BusStationKey
                         orderby BusLineStation.NumberStationInLine
                         select new
                         {
                             BusStationKey = BusLineStation.BusStationKey,
                             NumberStationInLine = BusLineStation.NumberStationInLine,
                             Active = BusLineStation.Active,
                             StationName = BusStation.StationName,
                             StationAddress = BusStation.StationAddress,
                             Latitude=  BusStation.Latitude,
                             Longitude= BusStation.Longitude,
                         }; 
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

