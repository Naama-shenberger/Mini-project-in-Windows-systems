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
    /// <summary>
    /// class DalXml 
    /// singelton
    /// </summary>
    sealed class DalXml : IDal
    {
       
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion
        #region DS XML Files
        string busStationPath = @"BusStation.xml";
        string BusPath= @"BusFile.xml";
        string BusLinePath = @"BusLine.xml"; 
        string BusLineStationPath = @"BusLineStation.xml"; 
        string LineRidePath = @"LineRide.xml"; 
        string ConsecutiveStationsPath = @"ConsecutiveStations.xml"; 
        string serialsPath = @"serials.xml"; 
        string userPath = @"User.xml";
        #endregion
        #region Bus
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Bus GetBus(string id)
        {
            
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);
            Bus b = (from bus in busRootElem.Elements()
                         where bus.Element("LicensePlate").Value == id
                         select new Bus()
                         {
                             Active = Boolean.Parse(bus.Element("Active").Value),
                             LicensePlate = bus.Element("LicensePlate").Value,
                             DateActivity = DateTime.Parse(bus.Element("DateActivity").Value),
                             DateTreatment = DateTime.Parse(bus.Element("DateTreatment").Value),
                             Totalkilometers = float.Parse(bus.Element("Totalkilometers").Value),
                             KilometersGas = float.Parse(bus.Element("KilometersGas").Value),
                             KilometersTreatment = float.Parse(bus.Element("KilometersTreatment").Value),
                             Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                             AirTire = float.Parse(bus.Element("AirTire").Value),
                             OilCondition = Boolean.Parse(bus.Element("OilCondition").Value)
                         }).FirstOrDefault();

                if (b != null)
                    if (b.Active == true)
                        return b;
                throw new DO.IdException(id, $"there is no bus with the id: {id}");
           
        }
        /// <summary>
        ///  A function that returns a list of bus that are active 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            return (from bus in busRootElem.Elements()
                    where Boolean.Parse(bus.Element("Active").Value) == true
                    select new Bus()
                    {
                        Active = Boolean.Parse(bus.Element("Active").Value),
                        LicensePlate = bus.Element("LicensePlate").Value,
                        DateActivity = DateTime.Parse(bus.Element("DateActivity").Value),
                        DateTreatment = DateTime.Parse(bus.Element("DateTreatment").Value),
                        Totalkilometers = float.Parse(bus.Element("Totalkilometers").Value),
                        KilometersGas = float.Parse(bus.Element("KilometersGas").Value),
                        KilometersTreatment = float.Parse(bus.Element("KilometersTreatment").Value),
                        Status= (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                        AirTire = float.Parse(bus.Element("AirTire").Value),
                        OilCondition = Boolean.Parse(bus.Element("OilCondition").Value)
                    }
                   );
        }
        /// <summary>
        /// A function that receives an bus  object and adds it to the xml array
        /// There is a comparison between unique parameters of the bus to see if the bus exists
        /// If the bus has the same bus id code is the same bus,
        /// In case the bus is inactive we will make it active
        /// In case it is active and exists in the system an exception will be thrown
        /// </summary>
        /// <param name="bus"></param>
        public void AddBus(DO.Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement bus1 = (from b in busRootElem.Elements()
                                 where b.Element("LicensePlate").Value == bus.LicensePlate
                                 select b).FirstOrDefault();

            if (bus1 != null)
                if (Boolean.Parse(bus1.Element("Active").Value) == false)
                {
                    bus1.Element("Active").Value = true.ToString();
                    XMLTools.SaveListToXMLElement(busRootElem, BusPath);
                    return;
                }
                else
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
        /// <summary>
        /// A function that receives a bus to deletes from bus xml list
        /// </summary>
        /// <param name="delBus"></param>
        public void DeleteBus(DO.Bus delBus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement bus = (from b in busRootElem.Elements()
                                where b.Element("LicensePlate").Value == delBus.LicensePlate 
                            select b).FirstOrDefault();

            if (bus != null)
            {
                if (Boolean.Parse(bus.Element("Active").Value) == true)
                {
                    bus.Element("Active").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(busRootElem, BusPath);
                   
                }
                else
                    throw new DO.IdException(delBus.LicensePlate, $"bus license plate {delBus.LicensePlate} is already deleted ");
            }
            else
                throw new DO.IdException(delBus.LicensePlate, $"bus license plate  {delBus.LicensePlate} dosn't exists ");
        }
        /// <summary>
        /// A function that receives a bus and updates its details
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBus(DO.Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(BusPath);

            XElement bus1 = (from b in busRootElem.Elements()
                          where b.Element("LicensePlate").Value ==bus.LicensePlate 
                          select b).FirstOrDefault();

            if (bus1 != null)
            {
                if (Boolean.Parse(bus1.Element("Active").Value) == true)
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
                    bus1.Element("OilCondition").Value = bus.OilCondition.ToString();

                    XMLTools.SaveListToXMLElement(busRootElem, BusPath);
                }
                else
                    throw new DO.IdException(bus.LicensePlate, $"bus License Plate {bus.LicensePlate} dosn't exists ");
            }
            else
                throw new DO.IdException(bus.LicensePlate, $"bus License Plate {bus.LicensePlate} dosn't exists ");
        }
        #endregion
        #region Bus Station
        /// <summary>
        /// A function that receives an ID number and returns the corresponding Bus station object
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DO.BusStation GetBusStation(int key)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            BusStation s = (from station in stationRootElem.Elements()
                            where int.Parse(station.Element("BusStationKey").Value) == key 
                            select new BusStation()
                            {
                                BusStationKey = Int32.Parse(station.Element("BusStationKey").Value),
                                Active = Boolean.Parse(station.Element("Active").Value),
                                StationName = station.Element("StationName").Value,
                                StationAddress = station.Element("StationAddress").Value,
                                Latitude = Double.Parse(station.Element("Latitude").Value),
                                Longitude = Double.Parse(station.Element("Longitude").Value)
                            }
                        ).FirstOrDefault();

            if (s != null)
                if (s.Active == true)
                    return s;
            throw new DO.IdException(key, $"there is no station with the key: {key}");
        }
        /// <summary>
        ///  A function that returns a list of bus stations that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.BusStation> BusStations()
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            return (from s in stationRootElem.Elements()
                    where Boolean.Parse(s.Element("Active").Value) == true
                    select new BusStation()
                    {
                        BusStationKey = Int32.Parse(s.Element("BusStationKey").Value),
                        Active = Boolean.Parse(s.Element("Active").Value),
                        StationName = s.Element("StationName").Value,
                        StationAddress = s.Element("StationAddress").Value,
                        Latitude = Double.Parse(s.Element("Latitude").Value),
                        Longitude = Double.Parse(s.Element("Longitude").Value)
                    }
                   );
        }
        /// <summary>
        /// A function that receives an bus station object and adds it to the xml file 
        /// There is a comparison between unique parameters of the bus to see if the bus station exists
        /// If the bus has the same bus station number and the same name and address it is the same line,
        /// because if it was the Reverse  line the stop codes would be reversed
        /// In case it is active and exists in the system an exception will be thrown 
        /// </summary>
        /// <param name="station"></param>
        public void AddBusStation(DO.BusStation station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            XElement station1 = (from s in stationRootElem.Elements()
                                 where int.Parse(s.Element("BusStationKey").Value) == station.BusStationKey 
                                 select s).FirstOrDefault();

            if (station1 != null)
            {
                if (Boolean.Parse(station1.Element("Active").Value) == false)
                {
                    station1.Element("Active").Value = true.ToString();
                    XMLTools.SaveListToXMLElement(stationRootElem, busStationPath);
                    return;
                }
                else
                    throw new DO.IdException(station.BusStationKey, "Duplicate station key");
            }

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
        /// <summary>
        /// The function gets an object to delete
        /// In case that the  bus station  is already not active we will throw a message
        /// </summary>
        /// <param name="delStation"></param>
        public void DeleteBusStation(DO.BusStation delStation)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            XElement station = (from s in stationRootElem.Elements()
                                where int.Parse(s.Element("BusStationKey").Value) == delStation.BusStationKey
                                select s).FirstOrDefault();

            if (station != null)
            {
                if (Boolean.Parse(station.Element("Active").Value) == true)
                {
                    station.Element("Active").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(stationRootElem, busStationPath);
                }
                else
                    throw new DO.IdException(delStation.BusStationKey, $"station with key {delStation.BusStationKey} is already deleted ");
            }
            else
                throw new DO.IdException(delStation.BusStationKey, $"station with key {delStation.BusStationKey} dosn't exists ");
        }
        /// <summary>
        /// A function that receives a bus Station and updates its details
        /// </summary>
        /// <param name="station"></param>
        public void UpdateBusStation(DO.BusStation station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(busStationPath);

            XElement bs = (from s in stationRootElem.Elements()
                           where int.Parse(s.Element("BusStationKey").Value) == station.BusStationKey
                           select s).FirstOrDefault();

            if (bs != null)
            {
                if (Boolean.Parse(bs.Element("Active").Value) == true)
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
            else
                throw new DO.IdException(station.BusStationKey, $"station with key {station.BusStationKey} dosn't exists ");
        }

        #endregion
        #region BusLine
        /// <summary>
        /// returns a bus line with the id you want
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.BusLine GetBusLine(int id)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            BusLine b = (from per in BusLinesRootElem.Elements()
                         where int.Parse(per.Element("ID").Value) == id && Boolean.Parse(per.Element("Active").Value) == true
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
        /// <summary>
        /// returns list of all bus lines that are active in xml file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.BusLine> BusLines()
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            return (from p in BusLinesRootElem.Elements()
                   where  Boolean.Parse(p.Element("Active").Value) == true
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
        /// <summary>
        /// adds a bus line to xml file
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public int AddBusLine(DO.BusLine bus)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);
            XElement serialsRootElem = XMLTools.LoadListFromXMLElement(serialsPath);
            XElement bus1 = (from p in BusLinesRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == bus.ID && Boolean.Parse(p.Element("Active").Value) == true
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new DO.IdException(bus.ID, "Duplicate Bus Line ID");
            serialsRootElem.Element("IdentificationNumberBusLine").Value = serialsRootElem.Element("IdentificationNumberBusLine").Value + 1;
            XMLTools.SaveListToXMLElement(serialsRootElem, serialsPath);
            bus.ID = Int32.Parse(serialsRootElem.Element("IdentificationNumberBusLine").Value);
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
        /// <summary>
        /// deletes a bus line from the system 
        /// </summary>
        /// <param name="bus"></param>
        public void DeleteBusLine(BusLine bus)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            XElement per = (from p in BusLinesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == bus.ID && Boolean.Parse(p.Element("Active").Value) == true
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("Active").Value = false.ToString();
                XMLTools.SaveListToXMLElement(BusLinesRootElem, BusLinePath);
            }
            else
                throw new DO.IdException(bus.ID, $"bad Bus Line id: {bus.ID}");
        }
        /// <summary>
        /// updates bus line info in the system
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusLine(BusLine bus)
        {
            XElement BusLinesRootElem = XMLTools.LoadListFromXMLElement(BusLinePath);

            XElement per = (from p in BusLinesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == bus.ID && Boolean.Parse(p.Element("Active").Value) == true
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
        #endregion
        #region BusLineStation
        /// <summary>
        /// returns a bus line object with the codeStation and  IDBusLine in the system
        /// </summary>
        /// <param name="codeStation"></param>
        /// <param name="IDBusLine"></param>
        /// <returns></returns>
        public BusLineStation GetBusLineStation(int codeStation, int IDBusLine)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            BusLineStation s = (from per in BusLineStationsRootElem.Elements()
                                where int.Parse(per.Element("ID").Value) == IDBusLine && int.Parse(per.Element("BusStationKey").Value) == codeStation && Boolean.Parse(per.Element("Active").Value) == true
                                select new BusLineStation()
                                {
                                    ID = Int32.Parse(per.Element("ID").Value),
                                    Active = Boolean.Parse(per.Element("Active").Value),
                                    BusStationKey = Int32.Parse(per.Element("BusStationKey").Value),
                                    NumberStationInLine = Int32.Parse(per.Element("NumberStationInLine").Value)
                                }
                        ).FirstOrDefault();

            if (s == null)
                throw new DO.IdException(codeStation, $"bad Bus Line Station  id: {codeStation}");

            return s;
        }
        /// <summary>
        /// returns a list of all bus line object in system 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineStation> BusLineStations()
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            return (from s in BusLineStationsRootElem.Elements()
                    where Boolean.Parse(s.Element("Active").Value)==true
                    select new BusLineStation
                    {
                        ID = Int32.Parse(s.Element("ID").Value),
                        Active = Boolean.Parse(s.Element("Active").Value),
                        BusStationKey= Int32.Parse(s.Element("BusStationKey").Value),
                        NumberStationInLine=Int32.Parse(s.Element("NumberStationInLine").Value)
                    }
                   );
        }
        /// <summary>
        /// adds a bus line station to system
        /// </summary>
        /// <param name="station"></param>
        public void AddBusLineStation(BusLineStation station)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            XElement BusLineStation1 = (from p in BusLineStationsRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == station.ID && station.BusStationKey== int.Parse(p.Element("BusStationKey").Value) 
                                        select p).FirstOrDefault();

            if (BusLineStation1 != null)
                if (Boolean.Parse(BusLineStation1.Element("Active").Value) == false)
                {
                    BusLineStation1.Element("Active").Value = true.ToString();
                    XMLTools.SaveListToXMLElement(BusLineStationsRootElem, BusLineStationPath);
                    return;
                }
               else
                    throw new DO.IdException(station.BusStationKey, "Duplicate Bus Line ID");
            XElement BusLineStationElem = new XElement("BusLineStation",
                                   new XElement("ID", station.ID),
                                   new XElement("BusStationKey", station.BusStationKey),
                                   new XElement("Active", station.Active),
                                   new XElement("NumberStationInLine", station.NumberStationInLine));

            BusLineStationsRootElem.Add(BusLineStationElem);
            XMLTools.SaveListToXMLElement(BusLineStationsRootElem, BusLineStationPath);
        }
        /// <summary>
        /// updates a bus line station object from system
        /// </summary>
        /// <param name="station"></param>
        public void UpdateBusLineStation(BusLineStation station)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            XElement busLineStation = (from b in BusLineStationsRootElem.Elements()
                            where int.Parse(b.Element("ID").Value) == station.ID && station.BusStationKey== int.Parse(b.Element("BusStationKey").Value) && Boolean.Parse(b.Element("Active").Value) == true
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
        /// <summary>
        /// deletes a bus line station object from system
        /// </summary>
        /// <param name="station"></param>
        public void DeleteBusLineStation(BusLineStation station)
        {
            XElement BusLineStationsRootElem = XMLTools.LoadListFromXMLElement(BusLineStationPath);

            XElement s1 = (from s in BusLineStationsRootElem.Elements()
                            where int.Parse(s.Element("ID").Value) == station.ID && station.BusStationKey== int.Parse(s.Element("BusStationKey").Value) 
                           select s).FirstOrDefault();

            if (s1 != null)
            {
                if (Boolean.Parse(s1.Element("Active").Value) == true)
                {
                    s1.Element("Active").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(BusLineStationsRootElem, BusLineStationPath);
                }
                else
                    throw new DO.IdException(station.BusStationKey, $"bad Bus Line Station id: {station.ID}");
            }
            else
                throw new DO.IdException(station.BusStationKey, $"bad Bus Line Station id: {station.ID}");
        }
        #endregion
        #region LineRide
        /// <summary>
        /// returns a line on the way object with the id that exists in system
        /// if id dosn't exists =>throw
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LineRide GetLineWayOut(int id)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            LineRide b = (from per in LineRidesRootElem.Elements()
                         where int.Parse(per.Element("ID").Value) == id && Boolean.Parse(per.Element("Active").Value) == true
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
        /// <summary>
        /// returns list of all Lines Way Out in system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineRide> LinesWayOut()
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            return (from s in LineRidesRootElem.Elements()
                    where Boolean.Parse(s.Element("Active").Value) == true
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
        /// <summary>
        /// adds a Lines Way Out object to system
        /// </summary>
        /// <param name="o"></param>
        public void AddLineWayOut(LineRide o)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            XElement bus1 = (from p in LineRidesRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == o.ID && XmlConvert.ToTimeSpan(p.Element("TravelStartTime").Value) == o.TravelStartTime
                             select p).FirstOrDefault();

            if (bus1 != null)
                if(Boolean.Parse(bus1.Element("Active").Value) == false)
                {
                    bus1.Element("Active").Value = true.ToString();
                    XMLTools.SaveListToXMLElement(LineRidesRootElem, LineRidePath);
                    return;
                }
                else
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
        /// <summary>
        /// updates a Lines Way Out object from system
        /// </summary>
        /// <param name="outLine"></param>
        public void UpdateLineWayOut(LineRide outLine)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            XElement ride = (from p in LineRidesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == outLine.ID && Boolean.Parse(p.Element("Active").Value) == true
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
        /// <summary>
        /// deletes  a Lines Way Out object from system
        /// </summary>
        /// <param name="outLine"></param>
        public void DeleteLineWayOut(LineRide outLine)
        {
            XElement LineRidesRootElem = XMLTools.LoadListFromXMLElement(LineRidePath);

            XElement line = (from p in LineRidesRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == outLine.ID && XmlConvert.ToTimeSpan(p.Element("TravelStartTime").Value) == outLine.TravelStartTime
                             select p).FirstOrDefault();

            if (line != null)
            {
                if (Boolean.Parse(line.Element("Active").Value) == true)
                {
                    line.Element("Active").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(LineRidesRootElem, LineRidePath);
                }
                else
                    throw new DO.IdException(outLine.ID, $"bad  Line id: {outLine.ID}");
            }
            else
                throw new DO.IdException(outLine.ID, $"bad  Line id: {outLine.ID}");
        }
        #endregion
        #region ConsecutiveStations
        /// <summary>
        /// returns a Consecutive Stations object with the two ids that exists in system
        /// if ids do not exists =>throw
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public ConsecutiveStations GetConsecutiveStations(int id1, int id2)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            ConsecutiveStations s = (from S in ConsecutiveStationsRootElem.Elements()
                          where int.Parse(S.Element("StationCodeOne").Value) == id1 && id2== int.Parse(S.Element("StationCodeTwo").Value) && Boolean.Parse(S.Element("Flage").Value) == true
                                     select new ConsecutiveStations
                          {
                              StationCodeOne= Int32.Parse(S.Element("StationCodeOne").Value),
                              StationCodeTwo= Int32.Parse(S.Element("StationCodeTwo").Value),
                              Flage = Boolean.Parse(S.Element("Flage").Value),
                              AverageTravelTime = XmlConvert.ToTimeSpan(S.Element("AverageTravelTime").Value),
                              Distance=float.Parse(S.Element("Distance").Value)
                          }
                        ).FirstOrDefault();

            if (s == null)
                throw new DO.IdException($"No consecutive Stations have the id`s:{id1} {id2}");

            return s;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsecutiveStations> ConsecutivesStations()
        {
           
                XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            return (from s in ConsecutiveStationsRootElem.Elements()
                    where Boolean.Parse(s.Element("Flage").Value) == true
                    select new ConsecutiveStations
                    {
                        StationCodeOne = Int32.Parse(s.Element("StationCodeOne").Value),
                        StationCodeTwo = Int32.Parse(s.Element("StationCodeTwo").Value),
                        Flage = Boolean.Parse(s.Element("Flage").Value),
                        AverageTravelTime = XmlConvert.ToTimeSpan(s.Element("AverageTravelTime").Value),
                        Distance = float.Parse(s.Element("Distance").Value)
                    }
                   );
        }
        /// <summary>
        /// adds a Consecutive Stations object to system
        /// </summary>
        /// <param name="s"></param>
        public void AddConsecutiveStations(ConsecutiveStations s)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement Stations = (from st in ConsecutiveStationsRootElem.Elements()
                             where int.Parse(st.Element("StationCodeOne").Value) == s.StationCodeOne && s.StationCodeTwo== int.Parse(st.Element("StationCodeTwo").Value)
                                 select st).FirstOrDefault();
            if (Stations != null) 
                if(Stations.Element("Flage").Value ==false.ToString())
                {
                    Stations.Element("Flage").Value = true.ToString();
                    XMLTools.SaveListToXMLElement(ConsecutiveStationsRootElem, ConsecutiveStationsPath);
                }
                else
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
        /// <summary>
        /// updates a Consecutive Stations object from system
        /// </summary>
        /// <param name="c"></param>
        public void UpdateConsecutiveStations(ConsecutiveStations c)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement stations = (from s in ConsecutiveStationsRootElem.Elements()
                             where int.Parse(s.Element("StationCodeOne").Value) == c.StationCodeOne && c.StationCodeTwo== int.Parse(s.Element("StationCodeTwo").Value) && Boolean.Parse(s.Element("Flage").Value) == true
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
        /// <summary>
        /// deletes a Consecutive Stations object from system
        /// </summary>
        /// <param name="c"></param>
        public void DeleteConsecutiveStations(ConsecutiveStations c)
        {
            XElement ConsecutiveStationsRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement stations = (from s in ConsecutiveStationsRootElem.Elements()
                                 where int.Parse(s.Element("StationCodeOne").Value) == c.StationCodeOne && c.StationCodeTwo == int.Parse(s.Element("StationCodeTwo").Value) 
                                 select s).FirstOrDefault();

            if (stations != null)
            {
                if (Boolean.Parse(stations.Element("Flage").Value) == true)
                {
                    stations.Element("Flage").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(ConsecutiveStationsRootElem, ConsecutiveStationsPath);
                }
                else
                    throw new DO.IdException($"No consecutive Stations have the id`s:{c.StationCodeOne} {c.StationCodeTwo}");
            }
            else
                throw new DO.IdException($"No consecutive Stations have the id`s:{c.StationCodeOne} {c.StationCodeTwo}");
        }
        #endregion
        #region User
        /// <summary>
        /// returns a user with the name(id) that we are looking for
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(string id)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(userPath);
            DO.User user = (from u in UserRootElem.Elements()
                             where u.Element("UserName").Value == id && Boolean.Parse(u.Element("DelUser").Value) == false
                            select new User
                             {
                                      UserName = u.Element("UserName").Value,
                                      Salt= int.Parse(u.Element("Salt").Value),
                                      HashedPassword= Tools.hashPassword(u.Element("HashedPassword").Value),
                                      AllowingAccess= Boolean.Parse(u.Element("AllowingAccess").Value),
                                      password= u.Element("password").Value,
                                      DelUser= Boolean.Parse(u.Element("DelUser").Value)
                             }
           ).FirstOrDefault();

            if (user != null)
                    return user;
            throw new DO.IdException($"The user name {id} does not exist");

        }
        /// <summary>
        /// returns a list of all users in the xml file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.User> GetUsers()
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(userPath);

            return (from u in UserRootElem.Elements()
                    where Boolean.Parse(u.Element("DelUser").Value) == false
                    select new User
                    {
                        UserName = u.Element("UserName").Value,
                        Salt = int.Parse(u.Element("Salt").Value),
                        HashedPassword = u.Element("HashedPassword").Value,
                        AllowingAccess = Boolean.Parse(u.Element("AllowingAccess").Value),
                        password = u.Element("password").Value,
                        DelUser = Boolean.Parse(u.Element("DelUser").Value)
                    }
                   );

        }
        /// <summary>
        /// adds a new user
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(DO.User user)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(userPath);
            XElement user1 = (from u in UserRootElem.Elements()
                             where u.Element("UserName").Value == user.UserName 
                              select u).FirstOrDefault();

            if (user1 != null)
            {
                if (Boolean.Parse(user1.Element("DelUser").Value) == true)
                {
                    user1.Element("DelUser").Value = false.ToString();
                    XMLTools.SaveListToXMLElement(UserRootElem, userPath);

                    return;
                }
                else
                    throw new DO.IdException($"The user name {user.UserName} does not exist");
            }
            XElement UserElem = new XElement("User",
                                   new XElement("UserName", user.UserName),
                                   new XElement("Salt", user.Salt),
                                   new XElement("HashedPassword", user.HashedPassword),
                                   new XElement("AllowingAccess", user.AllowingAccess),
                                   new XElement("password", user.password),
                                   new XElement("DelUser", user.DelUser)
                                   
                                   );

            UserRootElem.Add(UserElem);
            XMLTools.SaveListToXMLElement(UserRootElem, userPath);

        }
        /// <summary>
        /// updates a user
        /// </summary>
        /// <param name="user"></param>
        public void UpdatUser(DO.User user)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(userPath);
            XElement user1 = (from u in UserRootElem.Elements()
                             where u.Element("UserName").Value == user.UserName
                             select u).FirstOrDefault();

            if (user1 != null)
            {
                if (Boolean.Parse(user1.Element("DelUser").Value) == false)
                {
                    user1.Element("UserName").Value = user.UserName;
                    user1.Element("Salt").Value = user.Salt.ToString();
                    user1.Element("HashedPassword").Value = user.HashedPassword;
                    user1.Element("AllowingAccess").Value = user.AllowingAccess.ToString();
                    user1.Element("password").Value = user.password;
                    user1.Element("DelUser").Value = user.DelUser.ToString();
                    XMLTools.SaveListToXMLElement(UserRootElem, userPath);
                }
                else
                    throw new DO.IdException($"The user name {user.UserName} does not exist");
            }
            else
                throw new DO.IdException($"The user name {user.UserName} does not exist");
        }
        /// <summary>
        /// deletes a user
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(DO.User user)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(userPath);
            XElement user1 = (from u in UserRootElem.Elements()
                             where u.Element("UserName").Value == user.UserName 
                             select u).FirstOrDefault();

            if (user1 != null)
            {
                if (Boolean.Parse(user1.Element("DelUser").Value) == false)
                {
                    user1.Element("DelUser").Value = true.ToString();
                    XMLTools.SaveListToXMLElement(UserRootElem, userPath);
                }
                else
                    throw new DO.IdException($"The user name {user.UserName} is already deleted");
            }
            else
                throw new DO.IdException($"The user name {user.UserName} does not exist");
        }
        #endregion
    }
}  
