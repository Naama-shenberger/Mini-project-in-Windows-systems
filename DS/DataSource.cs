using System;
using System.Collections.Generic;
using System.Text;
using DO;
using static DO.Enums;

namespace DS
{
    public class DataSource
    {
        public static List<BusLine> BusLines;
        public static List<BusLineStation> BusLineStations;
        public static List<LineOutForARide> LinesOutForARide; 
        public static List<ConsecutiveStations> ListConsecutiveStations;
        /// <summary>
        /// initialization function fo bus lines
        /// </summary>
        public void createLists()
        {
            BusLines = new List<BusLine>
            {  new BusLine{Active=true,BusLineNumber = 134,FirstStopNumber = "123456",LastStopNumber="987463",Area=(int)Zones.General},
               new BusLine{Active=true,BusLineNumber = 14,FirstStopNumber = "128256",LastStopNumber="917453",Area=(int)Zones.Zefat},
               new BusLine{Active=true,BusLineNumber = 112,FirstStopNumber = "120486",LastStopNumber="680463",Area=(int)Zones.Alon_Shvut},
               new BusLine{Active=true,BusLineNumber = 9,FirstStopNumber = "188456",LastStopNumber="907453",Area=(int)Zones.Beer_Sheva},
               new BusLine{Active=true,BusLineNumber = 22,FirstStopNumber = "120457",LastStopNumber="917563",Area=(int)Zones.Zeev_hill},
               new BusLine{Active=true,BusLineNumber = 19,FirstStopNumber = "193406",LastStopNumber="967061",Area=(int)Zones.Alon_Shvut},
               new BusLine{Active=true,BusLineNumber = 10,FirstStopNumber = "103456",LastStopNumber="937163",Area=(int)Zones.Itamar},
               new BusLine{Active=true,BusLineNumber = 233,FirstStopNumber = "123497",LastStopNumber="387563",Area=(int)Zones.Gush_Dan},
               new BusLine{Active=true,BusLineNumber = 7,FirstStopNumber = "120896",LastStopNumber="989453",Area=(int)Zones.Jerusalem_Corridor},
               new BusLine{Active=true,BusLineNumber = 80,FirstStopNumber = "684729",LastStopNumber="573625",Area=(int)Zones.General},
               new BusLine{Active=true,BusLineNumber =4,FirstStopNumber = "137563",LastStopNumber="958473",Area=(int)Zones.Ramat_Gan},
               new BusLine{Active=true,BusLineNumber = 119,FirstStopNumber = "198574",LastStopNumber="564738",Area=(int)Zones.Beer_Sheva},
               new BusLine{Active=true,BusLineNumber = 68,FirstStopNumber = "124209",LastStopNumber="980973",Area=(int)Zones.Gush_Etzion},
               new BusLine{Active=true,BusLineNumber = 89,FirstStopNumber = "249583",LastStopNumber="867490",Area=(int)Zones.Alon_Shvut},
               new BusLine{Active=true,BusLineNumber = 1,FirstStopNumber = "984763",LastStopNumber="123432",Area=(int)Zones.Jerusalem_Corridor},
               new BusLine{Active=true,BusLineNumber = 5,FirstStopNumber = "098453",LastStopNumber="109089",Area=(int)Zones.General},
               new BusLine{Active=true,BusLineNumber = 65,FirstStopNumber = "158749",LastStopNumber="198674",Area=(int)Zones.Itamar},
               new BusLine{Active=true,BusLineNumber = 55,FirstStopNumber = "678594",LastStopNumber="123543",Area=(int)Zones.Ramat_Gan},
               new BusLine{Active=true,BusLineNumber = 117,FirstStopNumber = "234564",LastStopNumber="689043",Area=(int)Zones.Zeev_hill},
               new BusLine{Active=true,BusLineNumber = 115,FirstStopNumber = "098532",LastStopNumber="574839",Area=(int)Zones.Galilee}
            };
            BusLineStations = new List<BusLineStation>
            {

            };

            

           

        }
        /// <summary>
        /// initialization function fo bus line Consecutive Stations
        /// </summary>
        public void AddConsecutiveStations()
        {
            for (int i = 0; i < 20; i++)
            {
                ConsecutiveStations newConsecutiveStations = new ConsecutiveStations();
                ListConsecutiveStations.Add(newConsecutiveStations);
            }
        }
    }
   
  
}
