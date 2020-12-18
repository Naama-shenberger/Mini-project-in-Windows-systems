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
        
            mydal.addBusLine(new BusLine { Active = true, BusLineNumber = 134, FirstStopNumber = "123456", LastStopNumber = "987463", Area = (int)Zones.General }) ;
            mydal.addBusLine(new BusLine { Active = true, BusLineNumber = 14, FirstStopNumber = "128256", LastStopNumber = "917453", Area = (int)Zones.Zefat });
            mydal.addBusLine(new BusLine { Active = true, BusLineNumber = 112, FirstStopNumber = "120486", LastStopNumber = "680463", Area = (int)Zones.Alon_Shvut });
            Console.WriteLine(mydal.getBusLine(1).ToString());
            Console.ReadKey();
              
        }
    }
}
