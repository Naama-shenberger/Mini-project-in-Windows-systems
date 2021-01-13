using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
namespace DL
{
    sealed class DalXml : IDal
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
        //string studentsPath = @"StudentsXml.xml"; //XMLSerializer
        //string coursesPath = @"CoursesXml.xml"; //XMLSerializer
        //string lecturersPath = @"LecturersXml.xml"; //XMLSerializer
        //string lectInCoursesPath = @"LecturerInCourseXml.xml"; //XMLSerializer
        //string studInCoursesPath = @"StudentInCoureseXml.xml"; //XMLSerializer
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

    }
}  
