using System;
using System.Collections.Generic;
using System.Text;
using DO;
using static DO.Enums;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> Buses;
        public static List<BusStation> Stations;
        public static List<BusLine> BusLines;
        public static List<BusDrive> BusDrives;
        public static List<BusLineStation> BusLineStations;
        public static List<LineOutForARide> LinesOutForARide; 
        public static List<ConsecutiveStations> ListConsecutiveStations;
        static DataSource()
        {
            InitAllLists();
        }
        /// <summary>
        /// initialization function fo bus lines
        /// </summary>
        public static void InitAllLists()
        {
            List<string> adaress = new List<string> {"דרך הגפן 24", "" };
            Buses = new List<Bus> 
            { 
                new Bus{Active=true,LicensePlate="12345678",DateActivity=new DateTime(2018,12,3),DateTreatment=new DateTime(2019,10,3),Totalkilometers=111,KilometersGas=338,KilometersTreatment=1211,AirTire=15,OilCondition=true},
                new Bus{Active=true,LicensePlate="22345678",DateActivity=new DateTime(2018,11,3),DateTreatment=new DateTime(2019,11,3),Totalkilometers=112,KilometersGas=339,KilometersTreatment=1131,AirTire=75,OilCondition=true},
                new Bus{Active=true,LicensePlate="32345678",DateActivity=new DateTime(2018,10,3),DateTreatment=new DateTime(2019,10,3),Totalkilometers=1113,KilometersGas=303,KilometersTreatment=1141,AirTire=165,OilCondition=true},
                new Bus{Active=true,LicensePlate="42345678",DateActivity=new DateTime(2018,9,3),DateTreatment=new DateTime(2018,2,3),Totalkilometers=114,KilometersGas=3333,KilometersTreatment=1115,AirTire=157,OilCondition=true},
                new Bus{Active=true,LicensePlate="52345678",DateActivity=new DateTime(2018,1,3),DateTreatment=new DateTime(2019,2,23),Totalkilometers=115,KilometersGas=3343,KilometersTreatment=1116,AirTire=150,OilCondition=true},
                new Bus{Active=true,LicensePlate="6234678",DateActivity=new DateTime(2017,6,3),DateTreatment=new DateTime(2017,12,23),Totalkilometers=116,KilometersGas=3336,KilometersTreatment=1116,AirTire=15,OilCondition=true},
                new Bus{Active=true,LicensePlate="7245678",DateActivity=new DateTime(2015,7,3),DateTreatment=new DateTime(2019,12,29),Totalkilometers=117,KilometersGas=3383,KilometersTreatment=1171,AirTire=145,OilCondition=true},
                new Bus{Active=true,LicensePlate="8235678",DateActivity=new DateTime(2016,3,3),DateTreatment=new DateTime(2018,12,22),Totalkilometers=118,KilometersGas=3339,KilometersTreatment=1118,AirTire=154,OilCondition=true},
                new Bus{Active=true,LicensePlate="9234678",DateActivity=new DateTime(2011,1,3),DateTreatment=new DateTime(2019,12,14),Totalkilometers=119,KilometersGas=3330,KilometersTreatment=1191,AirTire=15,OilCondition=true},
                new Bus{Active=true,LicensePlate="0234567",DateActivity=new DateTime(2012,2,3),DateTreatment=new DateTime(2020,12,15),Totalkilometers=110,KilometersGas=3363,KilometersTreatment=1112,AirTire=20,OilCondition=true},
            };
            //50 תחנות
            Stations = new List<BusStation>()
            {

 new BusStation { Active = true, BusStationKey = " 183933", StationAddress = "ישראלי 5", StationName = "תחנה מרכזית ירושלים", Latitude = 3 * (33.3 - 31) + 31, Longitude = 1 * (35.5 - 34.3) + 34.3 },
 new BusStation { Active = true, BusStationKey = "63816" ,StationAddress = "נרקיס 4", StationName = "אלה/בן מימון", Latitude = 6* (33.3 - 31) + 31, Longitude =1* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "263460" ,StationAddress = "נשיא השישי 2", StationName = "שדרות גולדה", Latitude = 18* (33.3 - 31) + 31, Longitude =2* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "190883" ,StationAddress = "הרצוג 5", StationName = "בן סימון", Latitude = 1* (33.3 - 31) + 31, Longitude =16* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "156777" ,StationAddress = "שלום עליכם 8", StationName = "נעמה/שיינברגר", Latitude = 6* (33.3 - 31) + 31, Longitude =2* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "114742" ,StationAddress = "אריה לוי 7", StationName = "נחל חבר/צדוק", Latitude = 2* (33.3 - 31) + 31, Longitude =16* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "291754" ,StationAddress = "נמרוד 6", StationName = "דוד המלך/קקל", Latitude = 2* (33.3 - 31) + 31, Longitude =39* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "268033",StationAddress = "ניקולאי 60", StationName = "האגסי/מנחם", Latitude = 8* (33.3 - 31) + 31, Longitude =48* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "243303",StationAddress = "שבע שבתות תמימות 7", StationName = "ירמיהו/ישעיהו", Latitude = 6* (33.3 - 31) + 31, Longitude =2* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "57589",StationAddress = " רבקה אימנו 55", StationName = "צומאת פת", Latitude = 5* (33.3 - 31) + 31, Longitude =48* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "279745",StationAddress = "בנט 12", StationName = "תחנה מרכזית בית שמש", Latitude = 5* (33.3 - 31) + 31, Longitude =6* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "231591",StationAddress = "שזר 12", StationName = "תחנה מרכזית תל אביב", Latitude = 9* (33.3 - 31) + 31, Longitude =45* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "167697",StationAddress = "שש בש 4", StationName = "תחנת אוניברסיטה העברית", Latitude = 3* (33.3 - 31) + 31, Longitude =39* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "99723",StationAddress = "גולדה מאיר 5", StationName = "מנחם בגין", Latitude = 9* (33.3 - 31) + 31, Longitude =45* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "5970",StationAddress = "מארי קירי 101", StationName = "הרצל/בן גוריון", Latitude = 9* (33.3 - 31) + 31, Longitude =48* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "30760",StationAddress = "שרה אהרונסון 2", StationName = "שדרות רוטשילד", Latitude = 5* (33.3 - 31) + 31, Longitude =2* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "119335",StationAddress = "שזר 12", StationName = "בית לחם/רחל אימינו", Latitude = 8* (33.3 - 31) + 31, Longitude =48* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "48897",StationAddress = "סידני 67", StationName = "יוני נתניהו", Latitude = 3* (33.3 - 31) + 31, Longitude =39* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "54926",StationAddress = "קורונה 19", StationName = "מרכז האקדמי לב", Latitude = 4* (33.3 - 31) + 31, Longitude =27* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "321780",StationAddress = "דובאי 7", StationName = "בן יהודה/שטראוס", Latitude = 9* (33.3 - 31) + 31, Longitude =45* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey =" 145689",StationAddress = "דוב 7", StationName = "גרשון אגרון/קינג גורג", Latitude = 6* (33.3 - 31) + 31, Longitude =27* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "39739",StationAddress = "דנמרק 9", StationName = "מקסיקו/צילה", Latitude = 8* (33.3 - 31) + 31, Longitude =45* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey =" 2624",StationAddress = "המהרל 4", StationName = "כט בנובמבר/שיירת ל ה", Latitude = 7* (33.3 - 31) + 31, Longitude =6* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "4000",StationAddress = "יהודה 4", StationName = "שי אגנון/רחל המשוררת", Latitude = 8* (33.3 - 31) + 31, Longitude =16* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "123691",StationAddress = "אסירי ציון 2", StationName = "החלוץ/אסירי ציון", Latitude = 8* (33.3 - 31) + 31, Longitude =6* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "173577",StationAddress = "מרדכי אנילוויץ 4", StationName = "אנה פראנק/מרדכי אנילוויץ", Latitude = 9* (33.3 - 31) + 31, Longitude =39* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "186398",StationAddress = "ההגנה 50", StationName = "ההגנה/האצלה", Latitude = 4* (33.3 - 31) + 31, Longitude =27* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true,BusStationKey = " 196318",StationAddress = "חבד 770", StationName = "חבד/חיים סבאתו", Latitude = 2* (33.3 - 31) + 31, Longitude =24* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "198070",StationAddress = "חנקין 5", StationName = "אחד העם/חנקין", Latitude = 5* (33.3 - 31) + 31,Longitude =27* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "55035",StationAddress = "קנפה נשרים 44", StationName = "קנפה נשרים/אנגל", Latitude = 3* (33.3 - 31) + 31, Longitude =16* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "216197",StationAddress = "רפפורט 3", StationName = "אהבת ישראל/רפפורט", Latitude = 4*(33.3 - 31) + 31,Longitude =6* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "147342",StationAddress = "מעלות 1", StationName = "שמואל הנגיד/מעלות", Latitude = 9* (33.3 - 31) + 31, Longitude =39* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "115336",StationAddress = "אוסישקין 32", StationName = "בצלאל/אוסישקין", Latitude = 2* (33.3 - 31) + 31, Longitude =39* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = " 19613",StationAddress = "5 הדבידקה", StationName = "הדקל/הדבידקה", Latitude =1* (33.3 - 31) + 31, Longitude =27* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true,BusStationKey = " 198238",StationAddress = "יפו", StationName = "יפו מרכז", Latitude =8* (33.3 - 31) + 31,Longitude =24* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "255213",StationAddress = "הזז 2", StationName = "שדרות בן צבי/הזז", Latitude =3* (33.3 - 31) + 31, Longitude =24* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "261470",StationAddress = "ארתור 77", StationName = "יאנוש קורצק/ארתור", Latitude =1* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = " 1697565",StationAddress = "הרב קוק 12", StationName = "רבי יוסי.הרב קוק", Latitude =22* (33.3 - 31) + 31, Longitude =16* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true,BusStationKey = " 99060",StationAddress = "טבריה 6", StationName = "הרצוג/טבריה", Latitude =6* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "285269",StationAddress = "בן גוריון 2", StationName = "בן גוריון/זאב זבוטינסקי", Latitude =7* (33.3 - 31) + 31, Longitude =24* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "236838",StationAddress = "הירקון 9", StationName = "אבא הלל/הירקון", Latitude =9* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "311197",StationAddress = "מנורה 6", StationName = "מנורה/חיל האוויר", Latitude =8* (33.3 - 31) + 31, Longitude =45* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey =" 301406",StationAddress = "פורים 77", StationName = "חנוכה/פורים", Latitude =7* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "176072",StationAddress = "גדעון 11", StationName = "איילון/גדעון", Latitude =7* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "47283",StationAddress = "אמיר 44", StationName = "שיקמה/אמיר", Latitude =6* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "151005",StationAddress = "שכם 12", StationName = "קקל", Latitude =9* (33.3 - 31) + 31, Longitude =12* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "77448",StationAddress = "טיימ סקוואר 3", StationName = "טיימ סקוואר", Latitude =9* (33.3 - 31) + 31, Longitude =6* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "280814",StationAddress = "גורג 3 ", StationName = "קניג גורג", Latitude =7* (33.3 - 31) + 31, Longitude =6* (35.5 - 34.3) + 34.3},
 new BusStation { Active = true, BusStationKey = "69103",StationAddress = "רייגן 78", StationName = "וויסטן צורצל/רייגין", Latitude =2* (33.3 - 31) + 31, Longitude =24* (35.5 - 34.3) + 34.3},



            };
            BusDrives = new List<BusDrive>
            {
                new BusDrive{Active=true,LicensePlate="12345611",ExitStart=new TimeSpan(11,23,4),TimeDrive=new TimeSpan(1,662,33),TimeNextStop=new TimeSpan(12,2,3),LastStasion=06101,BusDriverFirstName="mark",BusDriverLastName="smith",BusDriverId="017909168"},
                new BusDrive{Active=true,LicensePlate="12345612",ExitStart=new TimeSpan(06,11,2),TimeDrive=new TimeSpan(1,23,3),TimeNextStop=new TimeSpan(1,2,33),LastStasion=06101,BusDriverFirstName="james",BusDriverLastName="lay",BusDriverId="017909168"},
                new BusDrive{Active=true,LicensePlate="12345613",ExitStart=new TimeSpan(20,24,3),TimeDrive=new TimeSpan(1,32,3),TimeNextStop=new TimeSpan(1,2,34),LastStasion=064101,BusDriverFirstName="jessi",BusDriverLastName="white",BusDriverId="01786543"},
                new BusDrive{Active=true,LicensePlate="12345614",ExitStart=new TimeSpan(33,4,5),TimeDrive=new TimeSpan(13,2,3),TimeNextStop=new TimeSpan(1,2,83),LastStasion=0611601,BusDriverFirstName="joul",BusDriverLastName="night",BusDriverId="01790111"},
                new BusDrive{Active=true,LicensePlate="12345615",ExitStart=new TimeSpan(7,8,9),TimeDrive=new TimeSpan(13,2,3),TimeNextStop=new TimeSpan(19,2,3),LastStasion=061107,BusDriverFirstName="ellen",BusDriverLastName="poal",BusDriverId="017900068"},
                new BusDrive{Active=true,LicensePlate="12345616",ExitStart=new TimeSpan(3,5,6),TimeDrive=new TimeSpan(1,2,33),TimeNextStop=new TimeSpan(1,82,3),LastStasion=069101,BusDriverFirstName="jacson",BusDriverLastName="noal",BusDriverId="01999168"},
                new BusDrive{Active=true,LicensePlate="12345876",ExitStart=new TimeSpan(3,5,6),TimeDrive=new TimeSpan(1,23,3),TimeNextStop=new TimeSpan(1,24,3),LastStasion=0687101,BusDriverFirstName="jacson",BusDriverLastName="wester",BusDriverId="033309168"},
                new BusDrive{Active=true,LicensePlate="12345617",ExitStart=new TimeSpan(3,4,69),TimeDrive=new TimeSpan(1,2,34),TimeNextStop=new TimeSpan(1,21,3),LastStasion=066101,BusDriverFirstName="percy",BusDriverLastName="jacson",BusDriverId="07779168"},
                new BusDrive{Active=true,LicensePlate="12345618",ExitStart=new TimeSpan(11,2,4),TimeDrive=new TimeSpan(2,3,45),TimeNextStop=new TimeSpan(1,2,23),LastStasion=057101,BusDriverFirstName="harry",BusDriverLastName="potter",BusDriverId="066609168"},
                new BusDrive{Active=true,LicensePlate="12345619",ExitStart=new TimeSpan(13,2,2),TimeDrive=new TimeSpan(10,02,9),TimeNextStop=new TimeSpan(1,2,63),LastStasion=821101,BusDriverFirstName="ron",BusDriverLastName="wez",BusDriverId="017999998"},
                new BusDrive{Active=true,LicensePlate="12345610",ExitStart=new TimeSpan(33,4,5),TimeDrive=new TimeSpan(11,0,21),TimeNextStop=new TimeSpan(1,2,36),LastStasion=901101,BusDriverFirstName="america",BusDriverLastName="singer",BusDriverId="04449168"},
            };

            BusLines = new List<BusLine>
            {  new BusLine{Active=false,BusLineNumber = 134,FirstStopNumber = "123456",LastStopNumber="987463",Area=(int)Zones.General},
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
                new BusLineStation{Active=true,CodeStation="178530",NumberStationInLine=1},
                new BusLineStation{Active=true,CodeStation="178531",NumberStationInLine=2},
                new BusLineStation{Active=true,CodeStation="178532",NumberStationInLine=3},
                new BusLineStation{Active=true,CodeStation="178533",NumberStationInLine=4},
                new BusLineStation{Active=true,CodeStation="178534",NumberStationInLine=5},
                new BusLineStation{Active=true,CodeStation="178535",NumberStationInLine=6},
                new BusLineStation{Active=true,CodeStation="178536",NumberStationInLine=7},
                new BusLineStation{Active=true,CodeStation="178537",NumberStationInLine=8},
                new BusLineStation{Active=true,CodeStation="178538",NumberStationInLine=9},
                new BusLineStation{Active=true,CodeStation="178539",NumberStationInLine=10},

            };
            ListConsecutiveStations = new List<ConsecutiveStations>
            {
                new ConsecutiveStations{StationCodeOne="178530",StationCodeTwo="178531",Distance=(float)1.12,AverageTravelTime=new TimeSpan(00,02,30)},
                new ConsecutiveStations{StationCodeOne="178531",StationCodeTwo="178532",Distance=(float)3,AverageTravelTime=new TimeSpan(00,04,30)},
                new ConsecutiveStations{StationCodeOne="178532",StationCodeTwo="178533",Distance=(float)1,AverageTravelTime=new TimeSpan(00,02,00)},
                new ConsecutiveStations{StationCodeOne="178533",StationCodeTwo="178534",Distance=(float)7,AverageTravelTime=new TimeSpan(00,05,00)},
                new ConsecutiveStations{StationCodeOne="178534",StationCodeTwo="178535",Distance=(float)2,AverageTravelTime=new TimeSpan(00,02,50)},
                new ConsecutiveStations{StationCodeOne="178535",StationCodeTwo="178536",Distance=(float)4.67,AverageTravelTime=new TimeSpan(00,04,45)},
                new ConsecutiveStations{StationCodeOne="178536",StationCodeTwo="178537",Distance=(float)5.3,AverageTravelTime=new TimeSpan(00,04,30)},
                new ConsecutiveStations{StationCodeOne="178537",StationCodeTwo="178538",Distance=(float)6.98,AverageTravelTime=new TimeSpan(00,04,50)},
                new ConsecutiveStations{StationCodeOne="178538",StationCodeTwo="178539",Distance=(float)2.4,AverageTravelTime=new TimeSpan(00,03,00)},
            };

        }
       
    }
   
  
}
