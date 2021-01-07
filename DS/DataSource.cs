using System;
using System.Collections.Generic;
using DO;
using static DO.Enums;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBuses;
        public static List<BusStation> ListStations;
        public static List<BusLine> BusLines;
        public static List<BusDrive> ListBusDrives;
        public static List<BusLineStation> BusLineStations;
        public static List<LineRide> LinesOutForARide;
        public static List<ConsecutiveStations> ListConsecutiveStations;
        public static List<User> Users;
        public static List<UserJourney> UsersJourney;
       

        static DataSource()
        {
            InitAllLists();
        }
        /// <summary>
        /// initialization function fo bus lines
        /// </summary>
        public static void InitAllLists()
        {
           
            ListBuses = new List<Bus>
            {
               
                new Bus{Active=true,LicensePlate="12345678",DateActivity=new DateTime(2018,12,3),DateTreatment=new DateTime(2019,10,3),Totalkilometers=111,KilometersGas=338,KilometersTreatment=1211,AirTire=15,OilCondition=true,Status=Status.Ready_to_go},
                new Bus{Active=true,LicensePlate="22345678",DateActivity=new DateTime(2018,11,3),DateTreatment=new DateTime(2019,11,3),Totalkilometers=112,KilometersGas=339,KilometersTreatment=1131,AirTire=75,OilCondition=true,Status=Status.Ready_to_go},
                new Bus{Active=true,LicensePlate="32345678",DateActivity=new DateTime(2018,10,3),DateTreatment=new DateTime(2019,10,3),Totalkilometers=1113,KilometersGas=303,KilometersTreatment=1141,AirTire=165,OilCondition=false,Status=Status.Ready_to_go},
                new Bus{Active=true,LicensePlate="42345678",DateActivity=new DateTime(2018,9,3),DateTreatment=new DateTime(2018,2,3),Totalkilometers=114,KilometersGas=3333,KilometersTreatment=1115,AirTire=157,OilCondition=true,Status=Status.Dangerous},
                new Bus{Active=true,LicensePlate="52345678",DateActivity=new DateTime(2018,1,3),DateTreatment=new DateTime(2019,2,23),Totalkilometers=115,KilometersGas=3343,KilometersTreatment=1116,AirTire=150,OilCondition=true,Status=Status.Dangerous},
                new Bus{Active=true,LicensePlate="6234678",DateActivity=new DateTime(2017,6,3),DateTreatment=new DateTime(2017,12,23),Totalkilometers=116,KilometersGas=3336,KilometersTreatment=1116,AirTire=15,OilCondition=false,Status=Status.Dangerous},
                new Bus{Active=true,LicensePlate="7245678",DateActivity=new DateTime(2015,7,3),DateTreatment=new DateTime(2019,12,29),Totalkilometers=117,KilometersGas=3383,KilometersTreatment=1171,AirTire=145,OilCondition=true,Status=Status.Dangerous},
                new Bus{Active=true,LicensePlate="8235678",DateActivity=new DateTime(2016,3,3),DateTreatment=new DateTime(2018,12,22),Totalkilometers=118,KilometersGas=339,KilometersTreatment=1118,AirTire=154,OilCondition=true,Status=Status.Ready_to_go},
                new Bus{Active=true,LicensePlate="9234678",DateActivity=new DateTime(2011,1,3),DateTreatment=new DateTime(2019,12,14),Totalkilometers=119,KilometersGas=3330,KilometersTreatment=1191,AirTire=15,OilCondition=true,Status=Status.Dangerous},
                new Bus{Active=true,LicensePlate="0234567",DateActivity=new DateTime(2012,2,3),DateTreatment=new DateTime(2020,12,15),Totalkilometers=110,KilometersGas=363,KilometersTreatment=1112,AirTire=20,OilCondition=false,Status=Status.Ready_to_go}
            };
            ListStations = new List<BusStation>()
            {
                new BusStation{Active=true, BusStationKey  = 388311, StationName  = "בי'ס בר לב/בןיהודה", StationAddress = "בן יהודה 76",  Latitude = 32.183921,   Longitude = 34.917806   },
                new BusStation{Active=true, BusStationKey  = 857321, StationName  = "ממילא/אגרון", StationAddress = " גרשון אגרון",  Latitude = 31.777846, Longitude = 35.222215},
                new BusStation{Active=true, BusStationKey  = 873031, StationName  = "ככר צרפת/אגרון",StationAddress = " גרשון אגרון 3",  Latitude = 31.775868, Longitude = 35.218523 },
                new BusStation{Active=true, BusStationKey  = 873141, StationName  = "   הרוזמרין/כביש המנהרות ", StationAddress = "  הרוזמרין 57", Latitude = 31.732231,Longitude = 35.202069 },
                new BusStation{Active=true, BusStationKey  = 873261, StationName  = " דב יוסף/יערי ", StationAddress = " דב יוסף ", Latitude = 31.734036, Longitude = 35.194675 },
                new BusStation{Active=true, BusStationKey  = 873371, StationName  = "אצטדיון טדי/א''ס ביתר",  StationAddress = " דרך אגודת ספורט בית''ר", Latitude = 31.751193,   Longitude = 35.18933    },
                new BusStation{Active=true, BusStationKey  = 873401, StationName  = "חניון הלאום",  StationAddress = " שדרות הנשיא השישי 2",   Latitude = 31.783061,   Longitude = 35.203237   },
                new BusStation{Active=true, BusStationKey  = 873702, StationName  = " שכונת בזבז",   StationAddress = " דרך בית לחם הישנה",   Latitude =   31.770693,   Longitude = 35.243402   },
                new BusStation{Active=true, BusStationKey  = 874003, StationName  = "שמואל הנביא/תדהר",  StationAddress = "שמואל הנביא 71",   Latitude =   31.793299,   Longitude = 35.222176   },
                new BusStation{Active=true, BusStationKey  = 874104, StationName  = "שמואל הנביא/יקים",  StationAddress = " שמואל הנביא 47",   Latitude =  31.791394,   Longitude = 35.22416    },
                new BusStation{Active=true, BusStationKey  = 874305, StationName  = "שרי ישראל/יפו",   StationAddress = "שדרות שרי ישראל 15",    Latitude = 31.789128,   Longitude =35.206146},
                new BusStation{Active=true, BusStationKey  = 874405, StationName  = "שמואל הנביא/יקים",  StationAddress = "שמואל הנביא 48",   Latitude =  31.791505,   Longitude = 35.224204   },
                new BusStation{Active=true, BusStationKey  = 874507, StationName  = "שמואל הנביא/תדהר",  StationAddress = "שמואל הנביא 70",   Latitude =   31.792975,   Longitude = 35.222797   },
                new BusStation{Active=true, BusStationKey  = 874808, StationName  = "ירמיהו/אלקנה",  StationAddress = " ירמיהו",  Latitude = 31.792925,   Longitude = 35.213412   },
                new BusStation{Active=true, BusStationKey  = 874909, StationName  = "משרד החוץ/שד' רבין",    StationAddress = " שדרות יצחק רבין",   Latitude =   31.782889, Longitude = 35.202207   },
                new BusStation{Active=true, BusStationKey  = 875010, StationName  = "אצטדיון טדי/א''ס ביתר", StationAddress = " דרך אגודת ספורט בית''ר",  Latitude = 31.751345,   Longitude = 35.188474   },
                new BusStation{Active=true, BusStationKey  = 875511, StationName  = "אנג'ל/כנפי נשרים",  StationAddress = " כנפי נשרים 6", Latitude = 31.787712,   Longitude = 35.192306   },
                new BusStation{Active=true, BusStationKey  = 875612, StationName  = "מרכז שטנר/כנפי נשרים",  StationAddress = " כנפי נשרים 20",    Latitude = 31.787703,   Longitude = 35.188761   },
                new BusStation{Active=true, BusStationKey  = 855813, StationName  = "יוסף קארו/בן איש חי",   StationAddress = " יוסף קארו",  Latitude = 31.744547,   Longitude = 35.000493    },
                new BusStation{Active=true, BusStationKey  = 875814, StationName  = "רוזנטל/כנפי נשרים", StationAddress = " הרב אברהם דוד רוזנטל",    Latitude = 31.787754,   Longitude = 35.179644   },
                new BusStation{Active=true, BusStationKey  = 876015, StationName  = "קצנלבוגן/מעלות בוסטון", StationAddress = " הרב רפאל קצנלבוגן 16", Latitude = 31.788077,   Longitude = 35.173651   },
                new BusStation{Active=true, BusStationKey  = 876216, StationName  = "דבר ירושלים/קצנלבוגן",  StationAddress = " הרב רפאל קצנלבוגן 58", Latitude = 31.783625,   Longitude = 35.174993   },
                new BusStation{Active=true, BusStationKey  = 876317, StationName  = "ישיבת ויזניץ/קצנלבוגן", StationAddress = " הרב רפאל קצנלבוגן 74", Latitude = 31.782684,   Longitude = 35.177453   },
                new BusStation{Active=true, BusStationKey  = 876418, StationName  = "קצנלבוגן/הפלאה",    StationAddress = " הרב רפאל קצנלבוגן 88", Latitude = 31.784417,   Longitude = 35.177826   },
                new BusStation{Active=true, BusStationKey  = 876519, StationName  = "כפר שאול/קצנלבוגן", StationAddress = " הרב רפאל קצנלבוגן 94", Latitude = 31.785614,   Longitude = 35.178884   },
                new BusStation{Active=true, BusStationKey  = 900320, StationName  = "נחל שורק/לכיש", StationAddress = "נחל שורק 3",    Latitude =   31.713796,  Longitude = 34.996277 },
                new BusStation{Active=true, BusStationKey  = 878721, StationName  = "דרך שכם/טובלר", StationAddress = " דרך שכם 45",   Latitude = 31.792304,   Longitude = 35.229086   },
                new BusStation{Active=true, BusStationKey  = 878822, StationName  = "בית ספר א סכאכיני", StationAddress = " דרך שכם 17",   Latitude = 31.793122,   Longitude = 35.230291   },
                new BusStation{Active=true, BusStationKey  = 879223, StationName  = "שאולזון/רוזנטל",    StationAddress = "הרב ש. שאולזון 3",  Latitude = 31.790632,   Longitude = 35.17594    },
                new BusStation{Active=true, BusStationKey  = 879324, StationName  = "בית ענבר/כנפי נשרים",  StationAddress = "כנפי נשרים 21",   Latitude = 31.787528,   Longitude = 35.184601   },
                new BusStation{Active=true, BusStationKey  = 879425, StationName  = "מרכז שטנר/כנפי נשרים",  StationAddress = " כנפי נשרים 11",    Latitude = 31.78751,    Longitude = 35.188178   },
                new BusStation{Active=true, BusStationKey  = 879526, StationName  = "אנג'ל/כנפי נשרים",  StationAddress = " כנפי נשרים",  Latitude = 31.787504,   Longitude = 35.192121   },
                new BusStation{Active=true, BusStationKey  = 388332, StationName  = "הנחשול/הדייגים",    StationAddress = " הנחשול 30 ",  Latitude =31.984553 ,   Longitude = 34.782828   },
                new BusStation{Active=true, BusStationKey  = 388342, StationName  = "פריד/ששתהימים", StationAddress = "רחוב:משה פריד 9 :",   Latitude = 31.88855,    Longitude = 34.790904   },
                new BusStation{Active=true, BusStationKey  = 388372, StationName  = "חנה אברך/וולקני",    StationAddress = " חנה אברך 9",  Latitude = 31.892166,   Longitude = 34.796071   },
                new BusStation{Active=true, BusStationKey  = 388393, StationName  = "הבנים/אליכהן",  StationAddress = "הבנים 4",  Latitude = 31.862305,   Longitude = 34.821857   },
                new BusStation{Active=true, BusStationKey  = 388403, StationName  = "ויצמן/הבנים",   StationAddress = "וייצמן 11",    Latitude = 31.865085,   Longitude = 34.822237   },
                new BusStation{Active=true, BusStationKey  = 388413, StationName  = "האירוס/הכלנית", StationAddress = "  האירוס 13",   Latitude = 31.865222,   Longitude = 34.818957   },
                new BusStation{Active=true, BusStationKey  = 388443, StationName  = "אלי כהן/לוחמי הגטאות",    StationAddress = " אלי כהן 62",   Latitude = 31.86244,    Longitude = 34.827023   },
                new BusStation{Active=true, BusStationKey  = 388453, StationName  = "שבזי/שבת אחים", StationAddress = " שבזי 51", Latitude = 31.863501,   Longitude = 34.828702   },
                new BusStation{Active=true, BusStationKey  = 388473, StationName  = "חייםברלב/שדרותיצחקרבין",    StationAddress = " חייםברלב",  Latitude = 31.977409,   Longitude = 34.763896   },
                new BusStation{Active=true, BusStationKey  = 388483, StationName  = " מרכז לבריאות הנפש לב השרון ",  StationAddress = " עיר:צור משה",  Latitude = 32.300345,   Longitude = 34.912708   },
                new BusStation{Active=true, BusStationKey  = 388493, StationName  = "נחל דולב/קטלב",    StationAddress = " דולב 3",  Latitude =31.715667,   Longitude = 34.998752   },
                new BusStation{Active=true, BusStationKey  = 388523, StationName  = "נחל דולב/נחל תמנע",   StationAddress = "דולב 76",  Latitude =31.713339,    Longitude = 34.999308   },
                new BusStation{Active=true, BusStationKey  = 388563, StationName  = "הרותם/הדגניות", StationAddress = "  הרותם 3 ",    Latitude = 31.874963,   Longitude = 34.81249    },
                new BusStation{Active=true, BusStationKey  = 388604, StationName  = "מבוא הגפן/מורד התאנה",    StationAddress = " מבואה גפן ",    Latitude =32.305234,   Longitude = 34.948647   },
                new BusStation{Active=true, BusStationKey  = 388614, StationName  = "מתנ''ס/נחל דולב",   StationAddress = " נחל דולב 18 ",    Latitude = 31.715221,   Longitude = 34.996684},
                new BusStation{Active=true, BusStationKey  = 388624, StationName  = "נחל שורק/נחל לכיש",   StationAddress = "נחל שורק 6", Latitude = 31.714054,   Longitude =34.995407 },
                new BusStation{Active=true, BusStationKey  = 388634, StationName  = "ההרחבהב",   StationAddress = " רחוב:ההרחבהעיר:גאוליםרציף:קומה:",    Latitude = 32.300264,   Longitude = 34.939512   },
                new BusStation{Active=true, BusStationKey  = 388644, StationName  = "ההרחבה/הותיקים",    StationAddress = "ההרחבה", Latitude = 32.298171,   Longitude = 34.938705   },
                new BusStation{Active=true, BusStationKey  = 388704, StationName  = " הראשונים/כביש 5700",    StationAddress = " המגדל 13 ",  Latitude = 32.352953,   Longitude = 34.899465   },
                new BusStation{Active=true, BusStationKey  = 388724, StationName  = "נחל איילון/נחל הקישון",    StationAddress = " נחל איילון",  Latitude =31.711476,   Longitude = 34.991179},
                new BusStation{Active=true, BusStationKey  = 388734, StationName  = "עוקשי/לוי אשכול",    StationAddress = " ישראל עוקשי 4",   Latitude = 31.883941,   Longitude = 34.807039   },
                new BusStation{Active=true, BusStationKey  = 388764, StationName  = "גורודסקי/יחיאלפלדי",    StationAddress = " יהודה גורודיסקי  35",  Latitude = 31.898463,   Longitude = 34.823461   },
                new BusStation{Active=true, BusStationKey  = 388774, StationName  = "דרך מנחם בגין/יעקבחזן",   StationAddress = "דרך מנחם בגין 30 ",    Latitude = 32.076535,   Longitude = 34.904907   },
                new BusStation{Active=true, BusStationKey  = 388795, StationName  = " התאנה/הגפן",   StationAddress = " התאנה",   Latitude = 31.865457,   Longitude = 34.859437   },
                new BusStation{Active=true, BusStationKey  = 388805, StationName  = "בן צבי/רופין",   StationAddress = "שדרות בן צבי 39",    Latitude = 31.777844,   Longitude = 35.20928},
                new BusStation{Active=true, BusStationKey  = 388815, StationName  = "דרך הפרחים/יסמין",   StationAddress = " דרך הפרחים 46", Latitude = 31.809325,   Longitude = 34.784347   },
                new BusStation{Active=true, BusStationKey  = 388845, StationName  = "מנחם בגין/יצחק רבין", StationAddress = "שדרות מנחם בגין4 ",   Latitude = 31.799224,   Longitude = 34.782985   },
                new BusStation{Active=true, BusStationKey  = 388855, StationName  = "חיים הרצוג/דולב",    StationAddress = " חיים הרצוג 12", Latitude = 31.800334,   Longitude = 34.785069   },
            };
            ListBusDrives = new List<BusDrive>
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
            {  new BusLine{ID=0,Active=false,BusLineNumber = 134,FirstStopNumber =388624,LastStopNumber=875612,Area=Zones.General},
               new BusLine{ID=1,Active=true,BusLineNumber = 14,FirstStopNumber = 388624,LastStopNumber=875612,Area=Zones.Zefat},
               new BusLine{ID=2,Active=true,BusLineNumber = 112,FirstStopNumber =388624,LastStopNumber=875612,Area=Zones.Alon_Shvut},
               new BusLine{ID=3,Active=true,BusLineNumber = 9,FirstStopNumber = 873401,LastStopNumber=875612,Area=Zones.Beer_Sheva},
               new BusLine{ID=4,Active=true,BusLineNumber = 22,FirstStopNumber =873401,LastStopNumber=875612,Area=Zones.Zeev_hill},
               new BusLine{ID=5,Active=true,BusLineNumber = 19,FirstStopNumber = 873401,LastStopNumber=388634,Area=Zones.Alon_Shvut},
               new BusLine{ID=6,Active=true,BusLineNumber = 10,FirstStopNumber = 388614,LastStopNumber=388634,Area=Zones.Itamar},
               new BusLine{ID=7,Active=true,BusLineNumber = 233,FirstStopNumber = 388614,LastStopNumber=388634,Area=Zones.Gush_Dan},
               new BusLine{ID=8,Active=true,BusLineNumber = 7,FirstStopNumber = 388614,LastStopNumber=388624,Area=Zones.Jerusalem_Corridor},
               new BusLine{ID=9,Active=true,BusLineNumber = 80,FirstStopNumber = 874104,LastStopNumber=388624,Area=Zones.General},
               new BusLine{ID=10,Active=true,BusLineNumber =4,FirstStopNumber = 874104,LastStopNumber=388624,Area=Zones.Ramat_Gan},
               new BusLine{ID=11,Active=true,BusLineNumber = 119,FirstStopNumber = 874104,LastStopNumber=388624,Area=Zones.Beer_Sheva},
               new BusLine{ID=12,Active=true,BusLineNumber = 68,FirstStopNumber = 873702,LastStopNumber=388624,Area=Zones.Gush_Etzion},
               new BusLine{ID=13,Active=true,BusLineNumber = 89,FirstStopNumber = 873702,LastStopNumber=388614,Area=Zones.Alon_Shvut},
               new BusLine{ID=14,Active=true,BusLineNumber = 1,FirstStopNumber = 876015,LastStopNumber=388614,Area=Zones.Jerusalem_Corridor},
               new BusLine{ID=15,Active=true,BusLineNumber = 5,FirstStopNumber = 876015,LastStopNumber=388614,Area=Zones.General},
               new BusLine{ID=16,Active=true,BusLineNumber = 65,FirstStopNumber = 876015,LastStopNumber=388614,Area=Zones.Itamar},
               new BusLine{ID=17,Active=true,BusLineNumber = 55,FirstStopNumber = 857321,LastStopNumber=873401,Area=Zones.Ramat_Gan},
               new BusLine{ID=18,Active=true,BusLineNumber = 117,FirstStopNumber = 857321,LastStopNumber=873401,Area=Zones.Zeev_hill},
               new BusLine{ID=19,Active=true,BusLineNumber = 115,FirstStopNumber = 857321,LastStopNumber=873401,Area=Zones.Galilee}
            };
            LinesOutForARide = new List<LineRide>
            {
               new LineRide{ID=0, Active=true, BusDepartureNumber=new TimeSpan(30,0,0), TravelStartTime=new TimeSpan(9,0,0), TravelEndTime=new TimeSpan(12,30,0)},
               new LineRide{ID=0, Active=true, BusDepartureNumber=new TimeSpan(10,0,0), TravelStartTime=new TimeSpan(19,0,0), TravelEndTime=new TimeSpan(22,30,0)},
               new LineRide{ID=1, Active=true, BusDepartureNumber=new TimeSpan(12,0,0), TravelStartTime=new TimeSpan(7,0,0), TravelEndTime=new TimeSpan(11,30,0)},
               new LineRide{ID=1, Active=true, BusDepartureNumber=new TimeSpan(3,0,0), TravelStartTime=new TimeSpan(19,0,0), TravelEndTime=new TimeSpan(22,30,0)},
               new LineRide{ID=2, Active=true, BusDepartureNumber=new TimeSpan(3,0,0), TravelStartTime=new TimeSpan(8,0,0), TravelEndTime=new TimeSpan(13,30,0)},
               new LineRide{ID=3, Active=true, BusDepartureNumber=new TimeSpan(4,0,0), TravelStartTime=new TimeSpan(23,0,0), TravelEndTime=new TimeSpan(1,30,0)},
               new LineRide{ID=4, Active=true, BusDepartureNumber=new TimeSpan(19,0,0), TravelStartTime=new TimeSpan(14,0,0), TravelEndTime=new TimeSpan(16,30,0)},
               new LineRide{ID=5, Active=true, BusDepartureNumber=new TimeSpan(5,0,0), TravelStartTime=new TimeSpan(22,0,0), TravelEndTime=new TimeSpan(1,30,0)},
               new LineRide{ID=6, Active=true, BusDepartureNumber=new TimeSpan(8,0,0), TravelStartTime=new TimeSpan(20,0,0), TravelEndTime=new TimeSpan(22,30,0)},
               new LineRide{ID=7, Active=true, BusDepartureNumber=new TimeSpan(3,0,0), TravelStartTime=new TimeSpan(10,0,0), TravelEndTime=new TimeSpan(13,30,0)},
               new LineRide{ID=8, Active=true, BusDepartureNumber=new TimeSpan(9,0,0), TravelStartTime=new TimeSpan(19,0,0), TravelEndTime=new TimeSpan(20,30,0)},
               new LineRide{ID=9, Active=true, BusDepartureNumber=new TimeSpan(6,0,0), TravelStartTime=new TimeSpan(9,0,0), TravelEndTime=new TimeSpan(16,30,0)},
               new LineRide{ID=10,Active=true, BusDepartureNumber=new TimeSpan(10,0,0), TravelStartTime=new TimeSpan(5,0,0), TravelEndTime=new TimeSpan(12,30,0)},
               new LineRide{ID=11, Active=true, BusDepartureNumber=new TimeSpan(4,0,0), TravelStartTime=new TimeSpan(20,0,0), TravelEndTime=new TimeSpan(1,30,0)},
               new LineRide{ID=12, Active=true, BusDepartureNumber=new TimeSpan(12,0,0), TravelStartTime=new TimeSpan(22,0,0), TravelEndTime=new TimeSpan(2,30,0)},
               new LineRide{ID=13, Active=true, BusDepartureNumber=new TimeSpan(22,0,0), TravelStartTime=new TimeSpan(8,35,0), TravelEndTime=new TimeSpan(10,30,0)},
               new LineRide{ID=14, Active=true, BusDepartureNumber=new TimeSpan(14,30,0), TravelStartTime=new TimeSpan(10,0,0), TravelEndTime=new TimeSpan(14,30,0)},
               new LineRide{ID=15, Active=true, BusDepartureNumber=new TimeSpan(9,30,0), TravelStartTime=new TimeSpan(10,0,0), TravelEndTime=new TimeSpan(11,30,0)},
               new LineRide{ID=16, Active=true, BusDepartureNumber=new TimeSpan(9,0,0), TravelStartTime=new TimeSpan(12,0,0), TravelEndTime=new TimeSpan(14,30,0)},
               new LineRide{ID=17, Active=true, BusDepartureNumber=new TimeSpan(14,0,0), TravelStartTime=new TimeSpan(11,0,0), TravelEndTime=new TimeSpan(15,40,0)},
               new LineRide{ID=18, Active=true, BusDepartureNumber=new TimeSpan(15,0,0), TravelStartTime=new TimeSpan(17,0,0), TravelEndTime=new TimeSpan(19,30,0)},
               new LineRide{ID=19, Active=true, BusDepartureNumber=new TimeSpan(4,0,0), TravelStartTime=new TimeSpan(23,0,0), TravelEndTime=new TimeSpan(1,30,0)},

            };
            BusLineStations = new List<BusLineStation>
            {
                new BusLineStation{Active=true,BusStationKey=857321,NumberStationInLine=1,ID=17},
                new BusLineStation{Active=true,BusStationKey=876015,NumberStationInLine=2,ID=16},
                new BusLineStation{Active=true,BusStationKey=873401,NumberStationInLine=3,ID=19},
                new BusLineStation{Active=true,BusStationKey=873702,NumberStationInLine=4,ID=13},
                new BusLineStation{Active=true,BusStationKey=874104,NumberStationInLine=5,ID=11},
                new BusLineStation{Active=true,BusStationKey=873401,NumberStationInLine=6,ID=5},
                new BusLineStation{Active=true,BusStationKey=388614,NumberStationInLine=7,ID=16},
                new BusLineStation{Active=true,BusStationKey=388624,NumberStationInLine=8,ID=11},
                new BusLineStation{Active=true,BusStationKey=388634,NumberStationInLine=9,ID=7},
                new BusLineStation{Active=true,BusStationKey=875612,NumberStationInLine=10,ID=0}

                //new BusLineStation{Active=true,BusStationKey=388473,NumberStationInLine=11},
                //new BusLineStation{Active=true,BusStationKey=388453,NumberStationInLine=12},
                //new BusLineStation{Active=true,BusStationKey=388483,NumberStationInLine=13},
                //new BusLineStation{Active=true,BusStationKey=388493,NumberStationInLine=14},
                //new BusLineStation{Active=true,BusStationKey=388724,NumberStationInLine=15},
                //new BusLineStation{Active=true,BusStationKey=879425,NumberStationInLine=16},
                //new BusLineStation{Active=true,BusStationKey=388795,NumberStationInLine=17},
                //new BusLineStation{Active=true,BusStationKey=388815,NumberStationInLine=18},
                //new BusLineStation{Active=true,BusStationKey=388845,NumberStationInLine=19},
                //new BusLineStation{Active=true,BusStationKey=388704,NumberStationInLine=20},
               
              
               

               };
            ListConsecutiveStations = new List<ConsecutiveStations>
            {
                new ConsecutiveStations{StationCodeOne=857321,StationCodeTwo=876015,Distance=(float)1.12,AverageTravelTime=new TimeSpan(00,02,30)},
                new ConsecutiveStations{StationCodeOne=876015,StationCodeTwo=388523,Distance=(float)3,AverageTravelTime=new TimeSpan(00,04,30)},
                new ConsecutiveStations{StationCodeOne=388523,StationCodeTwo=873702,Distance=(float)1,AverageTravelTime=new TimeSpan(00,02,00)},
                new ConsecutiveStations{StationCodeOne=873702,StationCodeTwo=874104,Distance=(float)7,AverageTravelTime=new TimeSpan(00,05,00)},
                new ConsecutiveStations{StationCodeOne=874104,StationCodeTwo=873401,Distance=(float)2,AverageTravelTime=new TimeSpan(00,02,50)},
                new ConsecutiveStations{StationCodeOne=873401,StationCodeTwo=388614,Distance=(float)4.67,AverageTravelTime=new TimeSpan(00,04,45)},
                new ConsecutiveStations{StationCodeOne=388614,StationCodeTwo=388624,Distance=(float)5.3,AverageTravelTime=new TimeSpan(00,04,30)},
                new ConsecutiveStations{StationCodeOne=388624,StationCodeTwo=388634,Distance=(float)6.98,AverageTravelTime=new TimeSpan(00,04,50)},
                new ConsecutiveStations{StationCodeOne=388634,StationCodeTwo=875612,Distance=(float)2.4,AverageTravelTime=new TimeSpan(00,03,00)},
               
            };

        }

    }
}



