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
        Random r = new Random();
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }//static ctor
        BLImp() { }//default ctor
        public static BLImp Instance { get => instance; }//The public Instance property to use
        #endregion
        #region Bus
        /// <summary>
        /// The function receives a bus for updating
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBus(Bus bus)
        {
            try
            {
                dl.UpdateBus(BusBoDoAdapter(bus));
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
                    if (TreatmentIsNeeded(bus) || FuelCondition(bus))
                        bus.Status = Enums.Status.Dangerous;
                    else
                        bus.Status = Enums.Status.Ready_to_go;
                else
                    throw new BO.IdException("Invalid license number input");
                dl.AddBus(BusBoDoAdapter(bus));
            }
            catch (DO.IdException)
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
                dl.DeleteBus(BusBoDoAdapter(bus));
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
            if (!TreatmentIsNeeded(bus))
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
        private bool DateCheck(Bus b)
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
        private bool FuelCondition(Bus bus)
        {
            if (bus.KilometersGas > 1200)
                return true;
            return false;
        }
        /// <summary>
        /// A function that checks if a year has passed since the last treatment
        /// returns true if Treatment Is Needed else returns false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TreatmentIsNeeded(Bus bus)
        {
            if ((bus.KilometersTreatment >= 20000 || DateCheck(bus)))
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
        #endregion
        #region Bus Station
        /// <summary>
        /// The function gets a bus line and a bus stop
        ///The function deletes a line from station 
        /// </summary>
        /// <param name="busStation"></param>
        /// <param name="busLine"></param>
        public void DeleteBusLineFromStation(BusStation busStation, BusLineInStation busLine)
        {
            DO.BusLineStation bls;
            try
            {
                bls = dl.GetBusLineStation(busStation.BusStationKey, busLine.ID);
                dl.DeleteBusLineStation(bls);
                busStation.ListBusLinesInStation.ToList().Remove(busLine);
                BusLine busline = GetBusLine(busLine.ID);
                busline.StationsInLine = busline.StationsInLine.Where(c => c.BusStationKey != busStation.BusStationKey);

            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// The function gets a bus line and a bus stop 
        /// And time and distance between stations
        /// adds a line to station 
        /// </summary>
        /// <param name="busStation"></param>
        /// <param name="busLine"></param>
        public void AddBusLineToStation(BusStation busStation, BusLine busLine, TimeSpan Time, float _Distance)
        {
            BO.BusLineStation bls;
            try
            {
                var b = busStation.ListBusLinesInStation.ToList().FindIndex(s => s.ID == busLine.ID);
                if (b == -1)
                {
                    busStation.ListBusLinesInStation.Append(new BusLineInStation() { BusLineNumber = busLine.BusLineNumber, ID = busLine.ID, Area = (int)busLine.Area, Active = true });
                    int count = busLine.StationsInLine.Count() + 1;
                    bls = new BO.BusLineStation()
                    {
                        BusStationKey = busStation.BusStationKey,
                        Active = true,
                        ID = busLine.ID,
                        NumberStationInLine = count,
                        AverageTravelTime = Time,
                        Distance = _Distance
                    };
                    dl.AddBusLineStation(BusLineStationBoDoAdapter(bls));
                    busLine.StationsInLine.Append(bls);
                    try
                    {
                        DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations
                        {
                            Distance = _Distance,
                            AverageTravelTime = Time,
                            Flage = true,
                            StationCodeTwo = busLine.StationsInLine.ToList()[busLine.StationsInLine.Count() - 1].BusStationKey,
                            StationCodeOne = busLine.StationsInLine.ToList()[busLine.StationsInLine.Count() - 2].BusStationKey
                        };
                        dl.AddConsecutiveStations(consecutiveStations);
                    }
                    catch (DO.IdException ex)
                    {
                        throw new BO.IdException("Consecutive Stations already exists");

                    }
                }
                else
                    busStation.ListBusLinesInStation.ToList()[b].Active = true;
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }

        }
        /// <summary>
        /// A function that receives a DO type bus station object and returns a BO type bus station
        /// </summary>
        /// <param name="stationDO"></param>
        /// <returns></returns>
        public BO.BusStation BusStationDoBoAdapter(DO.BusStation stationDO)
        {
            BO.BusStation stationBO = new BusStation();
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.ListBusLinesInStation = GetAllBusLines().SelectMany(line => line.StationsInLine.Where(station => station.BusStationKey == stationDO.BusStationKey)
                                                .Select(lineInStation => new BO.BusLineInStation() { BusLineNumber = line.BusLineNumber, Area = (int)line.Area, ID = line.ID }));
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
        /// A function that receives an ID number of a station and returns the corresponding station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusStation GetBusStation(int id)
        {
            try
            {
                return BusStationDoBoAdapter(dl.GetBusStation(id));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus  station ID is illegal", ex);
            }
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
                if (station.BusStationKey.ToString().Length<6)
                    throw new BO.IdException("Bus station ID is illegal, needs at least 6 digits");
                dl.AddBusStation(BusStationBoDoAdapter(station));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());

            }

        }
        /// <summary>
        /// A function that deletes a bus station from the list of buss that are in drives
        /// the function resives the bis station to delete
        /// </summary>
        /// <param name="station"></param>
        public void DeleteBusStation(BusStation station)
        {
            try
            {
                dl.DeleteBusStation(BusStationBoDoAdapter(station));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException("Bus station dose not exists", ex);
            }
        }
        /// <summary>
        ///  A function that returns all bus stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusStation> GetAllBusStation()
        {
            return from item in dl.BusStations()
                   where item.Active == true
                   select BusStationDoBoAdapter(item);
        }
        /// <summary>
        /// The function receives a bus Station for updating
        /// </summary>
        /// <param name="busStation"></param>
        public void UpdateBusStation(BusStation busStation)
        {
            try
            {
                dl.UpdateBusStation(BusStationBoDoAdapter(busStation));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        #endregion
        #region Bus Line
        /// <summary>
        /// A function that receives a bus line station
        /// The function sends to the deletion function of dal
        /// </summary>
        /// <param name="busLineStation"></param>
        public void DeleteBusLineStation(BusLineStation busLineStation)
        {
            try
            {
                dl.DeleteBusLineStation(BusLineStationBoDoAdapter(busLineStation));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// The receiving function
        ///Line runner number-IDBusLine
        ///Station number-Station
        ///The line itself
        ///Index for updating-inedx
        ///The function handles a case update of a station line
        ///The function handles a case of ConsecutiveStations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IDBusLine"></param>
        /// <param name="busLine"></param>
        /// <param name="index"></param>
        public void UpdateBusLineStation(int idStation, int IDBusLine, BO.BusLine busLine, int index)
        {
            try
            {
                BO.BusLineStation busLineStation = new BusLineStation
                {
                    ID = IDBusLine,
                    BusStationKey = idStation,
                    NumberStationInLine = index,
                    Active = true
                };
                busLine.StationsInLine = from sin in busLine.StationsInLine
                                         where sin.BusStationKey != idStation
                                         select sin;
                dl.DeleteBusLineStation(dl.GetBusLineStation(idStation, IDBusLine));
                var list = busLine.StationsInLine.ToList();
                for (int i = 0; i < busLine.StationsInLine.Count(); i++)
                {
                    if (busLine.StationsInLine.ElementAt(i).NumberStationInLine >= busLineStation.NumberStationInLine)
                        list[i].NumberStationInLine = busLine.StationsInLine.ElementAt(i).NumberStationInLine + 1;
                }
                busLine.StationsInLine = list;
                busLine.StationsInLine = busLine.StationsInLine.Append(busLineStation);
                dl.AddBusLineStation(BusLineStationBoDoAdapter(busLineStation));
                list = busLine.StationsInLine.ToList();
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].Distance == 0 && list[j].NumberStationInLine != 1)
                    {
                        try
                        {
                            list[j].Distance = dl.GetConsecutiveStations(list[j].BusStationKey, list[j - 1].BusStationKey).Distance;
                            list[j].AverageTravelTime = dl.GetConsecutiveStations(list[j].BusStationKey, list[j - 1].BusStationKey).AverageTravelTime;
                        }
                        catch (DO.IdException)
                        {
                            DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations
                            {
                                StationCodeOne = list[j - 1].BusStationKey,
                                StationCodeTwo = list[j].BusStationKey,
                                Flage = true,
                                AverageTravelTime = new TimeSpan((long)(((dl.GetBusStation(list[j].BusStationKey).Latitude * dl.GetBusStation(list[j - 1].BusStationKey).Latitude * dl.GetBusStation(list[j].BusStationKey).Longitude * dl.GetBusStation(list[j - 1].BusStationKey).Longitude) * r.NextDouble() * 1 + 0.5) / 66 * 100.0)),
                                Distance = (float)((dl.GetBusStation(list[j].BusStationKey).Latitude * dl.GetBusStation(list[j - 1].BusStationKey).Latitude * dl.GetBusStation(list[j].BusStationKey).Longitude * dl.GetBusStation(list[j - 1].BusStationKey).Longitude) * r.NextDouble() * 1 + 0.5) / 66,

                            };
                            dl.AddConsecutiveStations(consecutiveStations);
                            list[j].Distance = dl.GetConsecutiveStations(consecutiveStations.StationCodeOne, consecutiveStations.StationCodeTwo).Distance;
                            list[j].AverageTravelTime = dl.GetConsecutiveStations(consecutiveStations.StationCodeOne, consecutiveStations.StationCodeTwo).AverageTravelTime;
                        }
                    }
                }
                busLine.StationsInLine = list;

            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that receives tracking stations and updates the distance between them
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="_distance"></param>
        public void UpdateDistanceBetweenstations(int id1, int id2, float _distance)
        {
            var UpdateItem = dl.GetConsecutiveStations(id1, id2);
            UpdateItem.Distance = _distance;
            dl.UpdateConsecutiveStations(UpdateItem);
        }
        /// <summary>
        ///  A function that receives tracking stations and updates the Travel Time between them
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="time"></param>
        public void UpdateTravelTimeBetweenstations(int id1, int id2, TimeSpan time)
        {
            var UpdateItem = dl.GetConsecutiveStations(id1, id2);
            UpdateItem.AverageTravelTime = time;
            dl.UpdateConsecutiveStations(UpdateItem);
        }
        /// <summary>
        /// The function receives a bus line for updating
        /// </summary>
        /// <param name="busLine"></param>
        public void UpdateBusLine(BusLine busLine)
        {
            try
            {
                dl.UpdateBusLine(BusLineBoDoAdapter(busLine));
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
                                       select DeepCopyUtilities.CopyToStationInLine(sin, dl);
            busLineBO.lineRides = from sin in dl.LinesWayOut()
                                  where sin.ID == busLineBO.ID
                                  select DeepCopyUtilities.CopyToLineRide(sin);
            return busLineBO;
        }
        public DO.LineRide LineRideStationDoBoAdapter(BO.LineRides lineRides)
        {
            DO.LineRide lineRide = new DO.LineRide();
            lineRides.CopyPropertiesTo(lineRide);
            return lineRide;
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
                if (busLineStation.ToList().Count < 2)
                    throw new BO.IdException("You must add at least two stations to the line");
                var RunNumber = dl.AddBusLine(BusLineBoDoAdapter(busLine));
                busLine.StationsInLine = busLine.StationsInLine.Concat(busLineStation).Distinct();
                busLine.StationsInLine.AsParallel().ForAll(id => id.ID = RunNumber);
                busLine.StationsInLine.ToList().ForEach(s => dl.AddBusLineStation(BusLineStationBoDoAdapter(s)));
                busLine.StationsInLine = from sin in busLine.StationsInLine
                                         select DeepCopyUtilities.CopyToStationInLine(BusLineStationBoDoAdapter(sin), dl);
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// The function receives a Bo bus type station and copies the details to a bo bus type station
        /// The function returns  DO.BusLineStation
        /// </summary>
        /// <param name="busLineStationBO"></param>
        /// <returns></returns>
        public DO.BusLineStation BusLineStationBoDoAdapter(BO.BusLineStation busLineStationBO)
        {
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            busLineStationBO.CopyPropertiesTo(busLineStationDO);
            return busLineStationDO;
        }
        /// <summary>
        /// The function receives a do bus type station and copies the details to a bo bus type station
        /// The function returns  BO.BusLineStation
        /// </summary>
        /// <param name="busLineStation"></param>
        /// <returns></returns>
        public BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation busLineStation)
        {
            BO.BusLineStation busLineStationBO = new BusLineStation();
            busLineStation.CopyPropertiesTo(busLineStationBO);
            return busLineStationBO;
        }
        /// <summary>
        /// The function gets a bus line and a stop station
        /// The function adds the station to the bus line
        /// Check that there are indeed consecutive stations
        /// </summary>
        /// <param name="AddToLine"></param>
        /// <param name="busLineStation"></param>
        public void AddBusStationToLine(BusLine AddToLine, BO.BusLineStation busLineStation)
        { 
            try
            {

                AddToLine.StationsInLine = AddToLine.StationsInLine.Append(busLineStation);
                busLineStation.ID = AddToLine.ID;
                dl.AddBusLineStation(BusLineStationBoDoAdapter(busLineStation));
                AddToLine.StationsInLine = from sin in AddToLine.StationsInLine
                                           orderby sin.NumberStationInLine
                                           select sin;
                int id1 = AddToLine.StationsInLine.ToList()[AddToLine.StationsInLine.Count() - 1].BusStationKey;
                int id2 = AddToLine.StationsInLine.ToList()[AddToLine.StationsInLine.Count() - 2].BusStationKey;
                try
                {
                    dl.GetConsecutiveStations(id1, id2);
                }
                catch (DO.IdException)
                {
                    DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations
                    {
                        AverageTravelTime = new TimeSpan((long)(((dl.GetBusStation(id1).Latitude * dl.GetBusStation(id2).Latitude * dl.GetBusStation(id1).Longitude * dl.GetBusStation(id2).Longitude) * r.NextDouble() * 1 + 0.5) / 66 * 100.0)),
                        Distance = (float)((dl.GetBusStation(id1).Latitude * dl.GetBusStation(id2).Latitude * dl.GetBusStation(id1).Longitude * dl.GetBusStation(id2).Longitude) * r.NextDouble() * 1 + 0.5) / 66,
                        Flage = true,
                        StationCodeTwo = id2,
                        StationCodeOne = id1
                    };
                    dl.AddConsecutiveStations(consecutiveStations);
                }
                AddToLine.StationsInLine = from sin in AddToLine.StationsInLine
                                           select DeepCopyUtilities.CopyToStationInLine(BusLineStationBoDoAdapter(sin), dl);

            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
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
                dl.DeleteBusLine(BusLineBoDoAdapter(busLine));
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
                if (DeleteFromLine.StationsInLine.FirstOrDefault(s => s == busLineStation) != null)
                {
                    DeleteFromLine.StationsInLine = from sin in DeleteFromLine.StationsInLine
                                                    where sin.BusStationKey != busLineStation.BusStationKey
                                                    select sin;
                    var list = DeleteFromLine.StationsInLine.ToList();
                    for (int i = 0; i < DeleteFromLine.StationsInLine.Count(); i++)
                    {
                        if (list.ElementAt(i).NumberStationInLine > busLineStation.NumberStationInLine)
                            list[i].NumberStationInLine = DeleteFromLine.StationsInLine.ElementAt(i).NumberStationInLine - 1;
                    }
                    DeleteFromLine.StationsInLine = list;
                }
                dl.DeleteBusLineStation(BusLineStationBoDoAdapter(busLineStation));
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
                dl.UpdatUser(UserBoDoAdapter(user));
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
            BO.User UserBO = new User();
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
            DO.User UserDO = new DO.User();
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
                dl.AddUser(UserBoDoAdapter(user));

            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
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
                dl.DeleteUser(UserBoDoAdapter(user));
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
        #endregion
        #region Bus Line Station
        /// <summary>
        /// A function that receives a collection of bus stops and adds them to the bus line list
        /// </summary>
        /// <param name="busLineStations"></param>
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
        /// <summary>
        /// The function that returns a collection of bus line stops
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineStation> GetAllBusLineStations()
        {
            return from BusLineStation in dl.BusLineStations()
                   select BusLineStationDoBoAdapter(BusLineStation);
        }
        /// <summary>
        /// The function receives a collection of bus line stops
        /// The function returns a collection of objects containing details of line stations and stations
        /// </summary>
        /// <param name="busLineStations"></param>
        /// <returns></returns>
        public IEnumerable<object> StationDetails(IEnumerable<BusLineStation> busLineStations)
        {

            return from BusStation in dl.BusStations()
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
                       Latitude = BusStation.Latitude,
                       Longitude = BusStation.Longitude,
                       Distance = BusLineStation.Distance,
                       AverageTravelTime = BusLineStation.AverageTravelTime

                   };
        }
        #endregion
        #region LineRide
        /// <summary>
        /// Add a line to the exit
        /// </summary>
        /// <param name="lineRides"></param>
        public void AddLineRides(BO.LineRides lineRides)
        {
            try
            {
                dl.AddLineWayOut(LineRideStationDoBoAdapter(lineRides));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that accepts a line ride object for deletion
        /// </summary>
        /// <param name="line"></param>
        public void DeleteLineRide(BO.LineRides line)
        {
            try
            {
                dl.DeleteLineWayOut(LineRideStationDoBoAdapter(line));
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.ToString());
            }
        }
        /// <summary>
        /// A function that receives a running number of the line and a station number 
        /// returns the time from the next station
        /// </summary>
        /// <param name="IdBusLine"></param>
        /// <param name="codeStation"></param>
        /// <returns></returns>
        public TimeSpan GetTimeDrive(int IdBusLine, int codeStation)
        {
            TimeSpan SaveTimeDrive = new TimeSpan(0, 0, 0);
            for (int i = 0; i < GetBusLine(IdBusLine).StationsInLine.Count(); i++)
            {
                if (GetBusLine(IdBusLine).StationsInLine.ToList()[i].BusStationKey != codeStation)
                {
                    SaveTimeDrive += GetBusLine(IdBusLine).StationsInLine.ToList()[i].AverageTravelTime;
                }
                else { break; }
            }
            return SaveTimeDrive;
        }
        /// <summary>
        /// A function that receives a DO type Line Ride object and returns a BO type Line Ride
        /// </summary>
        /// <param name="lineRides"></param>
        /// <returns></returns>
        public BO.LineRides LineRideStationBoDoAdapter(DO.LineRide lineRides)
        {
            BO.LineRides lineBO = new LineRides();
            lineRides.CopyPropertiesTo(lineBO);
            return lineBO;
        }
        /// <summary>
        /// A function gets a current station and a current time
        ///  The function returns a collection of all lines that Travel Start Time is the cur time or befor
        /// </summary>
        /// <param name="CurbusStation"></param>
        /// <param name="tsCurTime"></param>
        /// <returns></returns>

        public IEnumerable<LineRides> GetLineTimingPerStation(BusStation CurbusStation, TimeSpan tsCurTime)
        {
            return from LineRides in dl.LinesWayOut()
                   from Line in CurbusStation.ListBusLinesInStation
                   where LineRides.ID == Line.ID
                   let line=GetBusLine(LineRides.ID)
                   where tsCurTime >= LineRides.TravelStartTime 
                   select new LineRides
                   {
                       BusDepartureNumber = LineRides.BusDepartureNumber,
                       BusLineNumber = Line.BusLineNumber,
                       Active = LineRides.Active,
                       TravelStartTime = LineRides.TravelStartTime,
                       TravelEndTime = LineRides.TravelEndTime,
                       CodeLastStasion = line.StationsInLine.Last().BusStationKey,
                       NameLastStasion = dl.GetBusStation(line.StationsInLine.Last().BusStationKey).StationName,
                       CurTimeStasion = GetTimeDrive(Line.ID, CurbusStation.BusStationKey)

                   };
        } 
        #endregion
    }
}

