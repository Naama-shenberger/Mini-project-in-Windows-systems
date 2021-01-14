using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DLAPI;
using DO;
using static DO.Enums;

namespace DL
{
    sealed class DalXml /*: IDal*/
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion
        #region DS XML Files
        string busStationPath = @"BusStation.xml"; //XElement
        string BusPath= @"BusFile.xml";
        string BusLinePath = @"BusLine.xml"; 
        string BusLineStationPath = @"BusLineStation.xml"; 
        string LineRidePath = @"LineRide.xml"; 
        string ConsecutiveStationsPath = @"ConsecutiveStations.xml"; 
        string serialsPath = @"serials.xml"; //XMLSerializer
        #endregion
        #region Bus
        public DO.Bus GetBus(string id)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            Bus p = (from bus in busRootElem.Elements()
                            where bus.Element("LicensePlate").Value == id
                            select new Bus()
                            {
                                Active = Boolean.Parse(bus.Element("Active").Value),
                                LicensePlate = bus.Element("LicensePlate").Value,
                                DateActivity= DateTime.Parse(bus.Element("DateActivity").Value),
                                DateTreatment = DateTime.Parse(bus.Element("DateTreatment").Value),
                                Totalkilometers=float.Parse(bus.Element("Totalkilometers").Value),
                                KilometersGas= float.Parse(bus.Element("KilometersGas").Value),
                                KilometersTreatment= float.Parse(bus.Element("KilometersTreatment").Value),
                                //Status= (Status)Enum.Parse(typeof(Bus), bus.Element("PersonalStatus").Value),
                                AirTire = float.Parse(bus.Element("AirTire").Value),
                                OilCondition = Boolean.Parse(bus.Element("OilCondition").Value)
                            }).FirstOrDefault();

            if (p == null)
                throw new DO.IdException(id, $"there is no bus with the id: {id}");

            return p;
        }
        public IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            return (from bus in busRootElem.Elements()
                    select new Bus()
                    {
                        Active = Boolean.Parse(bus.Element("Active").Value),
                        LicensePlate = bus.Element("LicensePlate").Value,
                        DateActivity = DateTime.Parse(bus.Element("DateActivity").Value),
                        DateTreatment = DateTime.Parse(bus.Element("DateTreatment").Value),
                        Totalkilometers = float.Parse(bus.Element("Totalkilometers").Value),
                        KilometersGas = float.Parse(bus.Element("KilometersGas").Value),
                        KilometersTreatment = float.Parse(bus.Element("KilometersTreatment").Value),
                        //Status= (Status)Enum.Parse(typeof(Bus), bus.Element("PersonalStatus").Value),
                        AirTire = float.Parse(bus.Element("AirTire").Value),
                        OilCondition = Boolean.Parse(bus.Element("OilCondition").Value)
                    }
                   );
        }
        public void AddBus(DO.Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement bus1 = (from b in busRootElem.Elements()
                                 where b.Element("BusStationKey").Value == bus.LicensePlate
                                 select b).FirstOrDefault();

            if (bus1 != null)
                throw new DO.IdException(bus.LicensePlate, "Duplicate bus id");

            XElement busElem = new XElement("Bus",
                                   new XElement("Active", bus.Active),
                                   new XElement("LicensePlate", bus.LicensePlate),
                                   new XElement("DateActivity", bus.DateActivity.ToString()),
                                   new XElement("DateTreatment", bus.DateTreatment.ToString()),
                                   new XElement("Totalkilometers", bus.Totalkilometers.ToString()),
                                   new XElement("KilometersGas", bus.KilometersGas.ToString()),
                                   new XElement("KilometersTreatment", bus.KilometersTreatment.ToString()),
                                   new XElement("Status", bus.Status.ToString()),
                                   new XElement("AirTire", bus.AirTire.ToString()),
                                   new XElement("OilCondition", bus.OilCondition.ToString())
                                   );
            busRootElem.Add(busElem);

            XMLTools.SaveListToXMLElement(busRootElem, BusPath);
        }
        public void DeleteBus(DO.Bus delBus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement bus = (from b in busRootElem.Elements()
                                where b.Element("LicensePlate").Value == delBus.LicensePlate
                                select b).FirstOrDefault();

            if (bus != null)
            {
                bus.Remove();
                XMLTools.SaveListToXMLElement(busRootElem, BusPath);
            }
            else
                throw new DO.IdException(delBus.LicensePlate, $"station with key {delBus.LicensePlate} dosn't exists ");
        }
        public void UpdateBus(DO.Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement bus1 = (from b in busRootElem.Elements()
                          where b.Element("LicensePlate").Value ==bus.LicensePlate
                          select b).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Element("Active").Value = bus.Active.ToString();
                bus1.Element("LicensePlate").Value = bus.LicensePlate;
                bus1.Element("DateActivity").Value = bus.DateActivity.ToString();
                bus1.Element("DateTreatment").Value = bus.DateTreatment.ToString();
                bus1.Element("Totalkilometers").Value = bus.Totalkilometers.ToString();
                bus1.Element("KilometersGas").Value = bus.KilometersGas.ToString();
                bus1.Element("KilometersTreatment").Value = bus.KilometersTreatment.ToString();
                bus1.Element("Status").Value = bus.Status.ToString();
                bus1.Element("AirTire").Value = bus.AirTire.ToString();
                bus1.Element("OilCondition").Value =bus.OilCondition.ToString();

                XMLTools.SaveListToXMLElement(busRootElem, BusPath);
            }
            else
                throw new DO.IdException(bus.LicensePlate, $"station with key {bus.LicensePlate} dosn't exists ");
        }
        #endregion
        #region Bus Station
        public DO.BusStation GetBusStation(int key)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            BusStation p = (from station in stationRootElem.Elements()
                            where int.Parse(station.Element("BusStationKey").Value) == key
                            select new BusStation()
                            {
                                BusStationKey = Int32.Parse(station.Element("BusStationKey").Value),
                                Active = Boolean.Parse(station.Element("Active").Value),
                                StationName = station.Element("Name").Value,
                                StationAddress = station.Element("StationAddress").Value,
                                Latitude = Int32.Parse(station.Element("Latitude").Value),
                                Longitude = Int32.Parse(station.Element("Longitude").Value)
                            }
                        ).FirstOrDefault();

            if (p == null)
                throw new DO.IdException(key, $"there is no station with the key: {key}");

            return p;
        }
        public IEnumerable<DO.BusStation> BusStations()
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            return (from s in stationRootElem.Elements()
                    select new BusStation()
                    {
                        BusStationKey = Int32.Parse(s.Element("BusStationKey").Value),
                        Active = Boolean.Parse(s.Element("Active").Value),
                        StationName = s.Element("StationName").Value,
                        StationAddress = s.Element("StationAddress").Value,
                        Latitude = Int32.Parse(s.Element("Latitude").Value),
                        Longitude = Int32.Parse(s.Element("Longitude").Value)
                    }
                   );
        }
        public void AddBusStation(DO.BusStation station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            XElement station1 = (from s in stationRootElem.Elements()
                                 where int.Parse(s.Element("BusStationKey").Value) == station.BusStationKey
                                 select s).FirstOrDefault();

            if (station1 != null)
                throw new DO.IdException(station.BusStationKey, "Duplicate station key");

            XElement stationElem = new XElement("BusStation",
                                   new XElement("BusStationKey", station.BusStationKey),
                                   new XElement("Active", station.Active.ToString()),
                                   new XElement("StationName", station.StationName),
                                   new XElement("StationAddress", station.StationAddress),
                                   new XElement("Latitude", station.Latitude.ToString()),
                                   new XElement("Longitude", station.Longitude.ToString())
                                   );
            stationRootElem.Add(stationElem);

            XMLTools.SaveListToXMLElement(stationRootElem, busStationPath);
        }
        public void DeleteBusStation(DO.BusStation delStation)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            XElement station = (from s in stationRootElem.Elements()
                                where int.Parse(s.Element("BusStationKey").Value) == delStation.BusStationKey
                                select s).FirstOrDefault();

            if (station != null)
            {
                station.Remove();
                XMLTools.SaveListToXMLElement(stationRootElem, busStationPath);
            }
            else
                throw new DO.IdException(delStation.BusStationKey, $"station with key {delStation.BusStationKey} dosn't exists ");
        }
        public void UpdateBusStation(DO.BusStation station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            XElement bs = (from s in stationRootElem.Elements()
                           where int.Parse(s.Element("BusStationKey").Value) == station.BusStationKey
                           select s).FirstOrDefault();

            if (bs != null)
            {
                bs.Element("Active").Value = station.Active.ToString();
                bs.Element("BusStationKey").Value = station.BusStationKey.ToString();
                bs.Element("StationName").Value = station.StationName;
                bs.Element("StationAddress").Value = station.StationAddress;
                bs.Element("Latitude").Value = station.Latitude.ToString();
                bs.Element("Longitude").Value = station.Longitude.ToString();

                XMLTools.SaveListToXMLElement(stationRootElem, busStationPath);
            }
            else
                throw new DO.IdException(station.BusStationKey, $"station with key {station.BusStationKey} dosn't exists ");
        }

        #endregion
        #region BusLine 
        public DO.BusLine GetBusLine(int id)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            BusLine b = (from per in BusLinesRootElem.Elements()
                         where int.Parse(per.Element("ID").Value) == id
                         select new BusLine()
                         {
                             ID = Int32.Parse(per.Element("ID").Value),
                             Active = Boolean.Parse(per.Element("Active").Value),
                             BusLineNumber = Int32.Parse(per.Element("BusLineNumber").Value),
                             FirstStopNumber = Int32.Parse(per.Element("FirstStopNumber").Value),
                             LastStopNumber = Int32.Parse(per.Element("LastStopNumber").Value),
                             Area = (Zones)Enum.Parse(typeof(Zones), per.Element("Area").Value)
                         }
                        ).FirstOrDefault();

            if (b == null)
                throw new DO.IdException(id, $"bad person id: {id}");

            return b;
        }
        public IEnumerable<DO.BusLine> BusLines()
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            return (from p in BusLinesRootElem.Elements()
                    select new BusLine
                    {
                        ID = Int32.Parse(p.Element("ID").Value),
                        Active = Boolean.Parse(p.Element("Active").Value),
                        BusLineNumber = Int32.Parse(p.Element("BusLineNumber").Value),
                        FirstStopNumber = Int32.Parse(p.Element("FirstStopNumber").Value),
                        LastStopNumber = Int32.Parse(p.Element("LastStopNumber").Value),
                        Area = (Zones)Enum.Parse(typeof(Zones), p.Element("Area").Value)
                    }
                   );
        }
        public int AddBusLine(DO.BusLine bus)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);
            XElement serialsRootElem = XMLTools.LoadListFromXMLElement(serialsPath);
            XElement bus1 = (from p in BusLinesRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == bus.ID
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new DO.IdException(bus.ID, "Duplicate Bus Line ID");
            serialsRootElem.Element("dentificationNumberBusLine").Value = serialsRootElem.Element("dentificationNumberBusLine").Value + 1;
            XMLTools.SaveListToXMLElement(serialsRootElem, serialsPath);
            bus.ID = Int32.Parse(serialsRootElem.Element("dentificationNumberBusLine").Value);
            XElement BusLineElem = new XElement("BusLine",
                                   new XElement("ID", bus.ID),
                                   new XElement("BusLineNumber", bus.BusLineNumber),
                                   new XElement("FirstStopNumber", bus.FirstStopNumber),
                                   new XElement("LastStopNumber", bus.LastStopNumber),
                                   new XElement("Active", bus.Active),
                                   new XElement("Area", bus.Area.ToString()));
                                
            BusLinesRootElem.Add(BusLineElem);
            XMLTools.SaveListToXMLElement(BusLinesRootElem, BusLinePath);
            return bus.ID;
        }
        public void DeleteBusLine(BusLine bus)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            XElement per = (from p in BusLinesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == bus.ID
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("Active").Value = false.ToString();
                XMLTools.SaveListToXMLElement(BusLinesRootElem, BusLinePath);
            }
            else
                throw new DO.IdException(bus.ID, $"bad Bus Line id: {bus.ID}");
        }
        public void UpdateBusLine(BusLine bus)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            XElement per = (from p in BusLinesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == bus.ID
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("ID").Value = bus.ID.ToString();
                per.Element("BusLineNumber").Value = bus.BusLineNumber.ToString();
                per.Element("FirstStopNumber").Value = bus.FirstStopNumber.ToString();
                per.Element("LastStopNumber").Value = bus.LastStopNumber.ToString();
                per.Element("Active").Value = bus.Active.ToString();
                per.Element("Area").Value =bus.Area.ToString();
                XMLTools.SaveListToXMLElement(BusLinesRootElem, BusLinePath);
            }
            else
                throw new DO.IdException(bus.ID, $"bad Bus Line id: {bus.ID}");
        }
        public IEnumerable<int> GetBusLineNumbers()
        {
            List<BusLine> ListBusLineStation = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinePath);

            return from BusLine in ListBusLineStation
                   select BusLine.BusLineNumber;
        }
        #endregion
        #region BusLineStation
        public BusLineStation GetBusLineStation(int codeStation, int IDBusLine)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            BusLineStation s = (from per in BusLineStationsRootElem.Elements()
                         where int.Parse(per.Element("ID").Value) == IDBusLine && int.Parse(per.Element("BusStationKey").Value)==codeStation
                         select new BusLineStation()
                         {
                             ID = Int32.Parse(per.Element("ID").Value),
                             Active = Boolean.Parse(per.Element("Active").Value),
                             BusStationKey = Int32.Parse(per.Element("BusStationKey").Value),
                             NumberStationInLine = Int32.Parse(per.Element("  NumberStationInLine").Value)
                         }
                        ).FirstOrDefault();

            if (s == null)
                throw new DO.IdException(codeStation, $"bad Bus Line Station  id: {codeStation}");

            return s;
        }
        public IEnumerable<BusLineStation> BusLineStations()
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            return (from s in BusLineStationsRootElem.Elements()
                    select new BusLineStation
                    {
                        ID = Int32.Parse(s.Element("ID").Value),
                        Active = Boolean.Parse(s.Element("Active").Value),
                        BusStationKey= Int32.Parse(s.Element("BusStationKey").Value),
                        NumberStationInLine=Int32.Parse(s.Element("NumberStationInLine").Value)
                    }
                   );
        }
        public IEnumerable<BusLineStation> GetBusLineStations(Predicate<BusLineStation> predicate)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            return from s in BusLineStationsRootElem.Elements()
                   let s1 = new BusLineStation()
                   {
                       ID = Int32.Parse(s.Element("ID").Value),
                       Active = Boolean.Parse(s.Element("Active").Value),
                       BusStationKey = Int32.Parse(s.Element("BusStationKey").Value),
                       NumberStationInLine = Int32.Parse(s.Element("NumberStationInLine").Value)
                   }
                   where predicate(s1)
                   select s1;
        }
        public void AddBusLineStation(BusLineStation station)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            XElement BusLineStation1 = (from p in BusLineStationsRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == station.ID && station.BusStationKey== int.Parse(p.Element("BusStationKey").Value)
                             select p).FirstOrDefault();

            if (BusLineStation1 != null)
                throw new DO.IdException(station.BusStationKey, "Duplicate Bus Line ID");
            XElement BusLineStationElem = new XElement("BusLineStation",
                                   new XElement("ID", station.ID),
                                   new XElement("BusStationKey", station.BusStationKey),
                                   new XElement("Active", station.Active),
                                   new XElement("NumberStationInLine", station.NumberStationInLine));

            BusLineStationsRootElem.Add(BusLineStationElem);
            XMLTools.SaveListToXMLElement(BusLineStationsRootElem, BusLineStationPath);
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            XElement busLineStation = (from b in BusLineStationsRootElem.Elements()
                            where int.Parse(b.Element("ID").Value) == station.ID && station.BusStationKey== int.Parse(b.Element("BusStationKey").Value)
                            select b).FirstOrDefault();

            if (busLineStation != null)
            {
                busLineStation.Element("ID").Value = station.ID.ToString();
                busLineStation.Element("BusStationKey").Value = station.BusStationKey.ToString();
                busLineStation.Element("NumberStationInLine").Value = station.NumberStationInLine.ToString();
                busLineStation.Element("Active").Value = station.Active.ToString();
                XMLTools.SaveListToXMLElement(BusLineStationsRootElem, BusLineStationPath);
            }
            else
                throw new DO.IdException(station.ID, $"bad person id: {station.ID}");
        }
        public void DeleteBusLineStation(BusLineStation station)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            XElement s1 = (from s in BusLineStationsRootElem.Elements()
                            where int.Parse(s.Element("ID").Value) == station.ID && station.BusStationKey== int.Parse(s.Element("BusStationKey").Value)
                          select s).FirstOrDefault();

            if (s1 != null)
            {
                s1.Element("Active").Value = false.ToString();
                XMLTools.SaveListToXMLElement(BusLineStationsRootElem, BusLineStationPath);
            }
            else
                throw new DO.IdException(station.BusStationKey, $"bad Bus Line Station id: {station.ID}");
        }
        public IEnumerable<object> GetBusLineStationCode(Func<int, object> generate)
        {
            List<BusLineStation> ListBusLineStation = XMLTools.LoadListFromXMLSerializer<BusLineStation>(BusLineStationPath);

            return from BusLineStation in ListBusLineStation
                   select generate(BusLineStation.BusStationKey);
        }
        #endregion
        #region LineRide
        public LineRide GetLineWayOut(int id)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            LineRide b = (from per in LineRidesRootElem.Elements()
                         where int.Parse(per.Element("ID").Value) == id
                         select new LineRide()
                         {
                             ID = Int32.Parse(per.Element("ID").Value),
                             Active = Boolean.Parse(per.Element("Active").Value),
                             BusDepartureNumber= XmlConvert.ToTimeSpan(per.Element("BusDepartureNumber").Value),
                             TravelEndTime = XmlConvert.ToTimeSpan(per.Element("TravelEndTime").Value),
                             TravelStartTime = XmlConvert.ToTimeSpan(per.Element("TravelStartTime").Value)
                         }
                        ).FirstOrDefault();

            if (b == null)
                throw new DO.IdException(id, $"bad person id: {id}");

            return b;
        }
        public IEnumerable<LineRide> LinesWayOut()
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            return (from s in LineRidesRootElem.Elements()
                    select new LineRide
                    {
                        ID = Int32.Parse(s.Element("ID").Value),
                        Active = Boolean.Parse(s.Element("Active").Value),
                        BusDepartureNumber = XmlConvert.ToTimeSpan(s.Element("BusDepartureNumber").Value),
                        TravelEndTime = XmlConvert.ToTimeSpan(s.Element("TravelEndTime").Value),
                        TravelStartTime = XmlConvert.ToTimeSpan(s.Element("TravelStartTime").Value)
                    }
                   );
        }
        public void AddLineWayOut(LineRide o)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            XElement bus1 = (from p in LineRidesRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == o.ID
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new DO.IdException(o.ID, "Duplicate Bus Line ID");
            XElement BusLineElem = new XElement("LineRide",
                                   new XElement("ID", o.ID),
                                   new XElement("TravelStartTime", o.TravelStartTime),
                                   new XElement("TravelEndTime", o.TravelEndTime),
                                   new XElement("BusDepartureNumber", o.BusDepartureNumber),
                                   new XElement("Active", o.Active)
                                   );

            LineRidesRootElem.Add(BusLineElem);
            XMLTools.SaveListToXMLElement(LineRidesRootElem, LineRidePath);
        }
        public void UpdateLineWayOut(LineRide outLine)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            XElement ride = (from p in LineRidesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == outLine.ID
                            select p).FirstOrDefault();

            if (ride != null)
            {
                ride.Element("ID").Value = outLine.ID.ToString();
                ride.Element("BusDepartureNumber").Value = outLine.BusDepartureNumber.ToString();
                ride.Element("TravelStartTime").Value = outLine.TravelStartTime.ToString();
                ride.Element("TravelEndTime").Value =outLine.TravelEndTime.ToString();
                ride.Element("Active").Value = outLine.Active.ToString();
                XMLTools.SaveListToXMLElement(LineRidesRootElem, LineRidePath);
            }
            else
                throw new DO.IdException(outLine.ID, $"bad Line Ride id: {outLine.ID}");
        }
        public void DeleteLineWayOut(LineRide outLine)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            XElement line = (from p in LineRidesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == outLine.ID
                            select p).FirstOrDefault();

            if (line != null)
            {
                line.Element("Active").Value =false.ToString();
                XMLTools.SaveListToXMLElement(LineRidesRootElem, LineRidePath);
            }
            else
                throw new DO.IdException(outLine.ID, $"bad  Line id: {outLine.ID}");
        }
        #endregion
        #region ConsecutiveStations
        public ConsecutiveStations GetConsecutiveStations(int id1, int id2)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            ConsecutiveStations s = (from S in ConsecutiveStationsRootElem.Elements()
                          where int.Parse(S.Element("StationCodeOne").Value) == id1 && id2== int.Parse(S.Element("StationCodeTwo").Value)
                          select new ConsecutiveStations
                          {
                              StationCodeOne= Int32.Parse(S.Element("StationCodeOne").Value),
                              StationCodeTwo= Int32.Parse(S.Element("StationCodeTwo").Value),
                              Flage = Boolean.Parse(S.Element("Active").Value),
                              AverageTravelTime = XmlConvert.ToTimeSpan(S.Element("AverageTravelTime").Value),
                              Distance=float.Parse(S.Element("Distance").Value)
                          }
                        ).FirstOrDefault();

            if (s == null)
                throw new DO.IdException($"No consecutive Stations have the id`s:{id1} {id2}");

            return s;
        }
        public IEnumerable<ConsecutiveStations> ConsecutivesStations()
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            return (from s in ConsecutiveStationsRootElem.Elements()
                    select new ConsecutiveStations
                    {
                        StationCodeOne = Int32.Parse(s.Element("StationCodeOne").Value),
                        StationCodeTwo = Int32.Parse(s.Element("StationCodeTwo").Value),
                        Flage = Boolean.Parse(s.Element("Active").Value),
                        AverageTravelTime = XmlConvert.ToTimeSpan(s.Element("AverageTravelTime").Value),
                        Distance = float.Parse(s.Element("Distance").Value)
                    }
                   );
        }
        public void AddConsecutiveStations(ConsecutiveStations s)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement Stations = (from st in ConsecutiveStationsRootElem.Elements()
                             where int.Parse(st.Element("StationCodeOne").Value) == s.StationCodeOne && s.StationCodeTwo== int.Parse(st.Element("StationCodeTwo").Value)
                             select st).FirstOrDefault();

            if (Stations != null) 
                throw new DO.IdException($"Duplicate consecutive Stations:{s.StationCodeOne} {s.StationCodeTwo}");
            XElement ConsecutiveStationsElem = new XElement("ConsecutiveStations",
                                   new XElement("StationCodeOne", s.StationCodeOne),
                                   new XElement("StationCodeTwo", s.StationCodeTwo),
                                   new XElement("Flage", s.Flage),
                                   new XElement("AverageTravelTime", s.AverageTravelTime),
                                   new XElement("Distance", s.Distance)
                                   );

            ConsecutiveStationsRootElem.Add(ConsecutiveStationsElem);
            XMLTools.SaveListToXMLElement(ConsecutiveStationsRootElem, ConsecutiveStationsPath);
        }
        public  void UpdateConsecutiveStations(ConsecutiveStations c)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement stations = (from s in ConsecutiveStationsRootElem.Elements()
                             where int.Parse(s.Element("StationCodeOne").Value) == c.StationCodeOne && c.StationCodeTwo== int.Parse(s.Element("StationCodeTwo").Value)
                                 select s).FirstOrDefault();

            if (stations != null)
            {
                stations.Element("StationCodeOne").Value = c.StationCodeOne.ToString();
                stations.Element("StationCodeTwo").Value = c.StationCodeTwo.ToString();
                stations.Element("Flage").Value = c.Flage.ToString();
                stations.Element("Distance").Value = c.Distance.ToString();
                stations.Element("AverageTravelTime").Value = c.AverageTravelTime.ToString();
                XMLTools.SaveListToXMLElement(ConsecutiveStationsRootElem, ConsecutiveStationsPath);
            }
            else
                throw new DO.IdException($"No consecutive Stations have the id`s:{c.StationCodeOne} {c.StationCodeTwo}");
        }
        public void DeleteConsecutiveStations(ConsecutiveStations c)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement stations = (from s in ConsecutiveStationsRootElem.Elements()
                                 where int.Parse(s.Element("StationCodeOne").Value) == c.StationCodeOne && c.StationCodeTwo == int.Parse(s.Element("StationCodeTwo").Value)
                                 select s).FirstOrDefault();

            if (stations != null)
            {
                stations.Element("Flage").Value = false.ToString();
                XMLTools.SaveListToXMLElement(ConsecutiveStationsRootElem, ConsecutiveStationsPath);
            }
            else
                throw new DO.IdException($"No consecutive Stations have the id`s:{c.StationCodeOne} {c.StationCodeTwo}");
        }
        #endregion
    }
}  
