using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using DO;
using DalApi;
using static DO.Enums;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDal mydal = DalFactory.GetDal();

            mydal.addBusLine(new BusLine { Active = true, BusLineNumber = 134, FirstStopNumber = "123456", LastStopNumber = "987463", Area = (int)Zones.General });
            mydal.addBusLine(new BusLine { Active = true, BusLineNumber = 14, FirstStopNumber = "128256", LastStopNumber = "917453", Area = (int)Zones.Zefat });
            mydal.addBusLine(new BusLine { Active = true, BusLineNumber = 112, FirstStopNumber = "120486", LastStopNumber = "680463", Area = (int)Zones.Alon_Shvut });
            mydal.addBus(new Bus { Active = true, LicensePlate = "12345677", DateActivity = new DateTime(2018, 12, 3), DateTreatment = new DateTime(2019, 10, 3), Totalkilometers = 111, KilometersGas = 338, KilometersTreatment = 1211, AirTire = 15, OilCondition = true });
            mydal.addBus(new Bus { Active = true, LicensePlate = "22345679", DateActivity = new DateTime(2018, 11, 3), DateTreatment = new DateTime(2019, 11, 3), Totalkilometers = 112, KilometersGas = 339, KilometersTreatment = 1131, AirTire = 75, OilCondition = true });
            mydal.addBus(new Bus { Active = true, LicensePlate = "32345673", DateActivity = new DateTime(2018, 10, 3), DateTreatment = new DateTime(2019, 10, 3), Totalkilometers = 1113, KilometersGas = 303, KilometersTreatment = 1141, AirTire = 165, OilCondition = true });
            Console.WriteLine(mydal.getBusLine(1).ToString());
            Console.WriteLine(mydal.getBus(12345678).ToString());
            mydal.addBusDrive(new BusDrive { Active = true, LicensePlate = "12345611", ExitStart = new TimeSpan(11, 23, 4), TimeDrive = new TimeSpan(1, 662, 33), TimeNextStop = new TimeSpan(12, 2, 3), LastStasion = 06101, BusDriverFirstName = "mark", BusDriverLastName = "smith", BusDriverId = "017909168" });
            mydal.addBusDrive(new BusDrive { Active = true, LicensePlate = "12345612", ExitStart = new TimeSpan(06, 11, 2), TimeDrive = new TimeSpan(1, 23, 3), TimeNextStop = new TimeSpan(1, 2, 33), LastStasion = 06101, BusDriverFirstName = "james", BusDriverLastName = "lay", BusDriverId = "017909168" });
            mydal.addBusDrive(new BusDrive { Active = true, LicensePlate = "12345613", ExitStart = new TimeSpan(20, 24, 3), TimeDrive = new TimeSpan(1, 32, 3), TimeNextStop = new TimeSpan(1, 2, 34), LastStasion = 064101, BusDriverFirstName = "jessi", BusDriverLastName = "white", BusDriverId = "01786543" });
            Console.WriteLine(mydal.getBusDrive(12345612).ToString());
            mydal.addBusStation(new BusStation { Active = true, BusStationKey = "06311", StationAddress = "עמק בית שאן 2", StationName = "בית שאן", Latitude = 1 * (33.3 - 31) + 31, Longitude = 1 * (35.5 - 34.3) + 34.3 });
            mydal.addBusStation(new BusStation { Active = true, BusStationKey = "06143", StationAddress = "אליהו הנביא 2", StationName = "אליהו", Latitude = 2 * (33.3 - 31) + 31, Longitude = 2 * (35.5 - 34.3) + 34.3 });
            mydal.addBusStation(new BusStation { Active = true, BusStationKey = "06153", StationAddress = "עמק האלה 23", StationName = "אלה", Latitude = 3 * (33.3 - 31) + 31, Longitude = 3 * (35.5 - 34.3) + 34.3 });
            Console.WriteLine(mydal.getBusStation(1).ToString());
            foreach (var item in mydal.BusLines())
                Console.WriteLine(item.ToString());
            Console.ReadKey();
              
        }
    }
}
