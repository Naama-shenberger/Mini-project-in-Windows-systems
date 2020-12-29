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
        public static List<LineOutForARide> LinesOutForARide;
        public static List<ConsecutiveStations> ListConsecutiveStations;
        public static List<User> Users;
        public static List<UserJourney> UsersJourney;
        public static List<BusLineInStation> ListLineInStations;

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
            ListStations = new List<BusStation>()
            {
                new BusStation{ BusStationKey  = 388311, StationName  = "בי''סברלב/בןיהודה", StationAddress = "רחוב:בןיהודה76עיר:כפרסבארציף:קומה:",  Latitude = 32.183921,   Longitude = 34.917806   },
                new BusStation{ BusStationKey  = 857321, StationName  = "אלמדינה אלמנורה/זעקוקה", StationAddress = " רחוב:אל מדינה אל מונאוורה  עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.737062, Longitude = 35.23667},
                new BusStation{  BusStationKey  = 873031, StationName  = "הפסגה/הרשד''ם",StationAddress = " רחוב:הפסגה 13 עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.769614, Longitude = 35.182558 },
                new BusStation{ BusStationKey  = 873141,StationName  = " הרוזמרין/כביש ", StationAddress = " המנהרות רחוב:הרוזמרין 57 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.732231,Longitude = 35.202069 },
                new BusStation{ BusStationKey  = 873261, StationName  = " דב יוסף/יערי ", StationAddress = " רחוב:דב יוסף  עיר: ירושלים רציף:   קומה:  ", Latitude = 31.734036, Longitude = 35.194675 },
                new BusStation{ BusStationKey  = 873371,  StationName  = "",  StationAddress = "אצטדיון טדי/א''ס ביתר רחוב:דרך אגודת ספורט בית''ר  עיר: ירושלים רציף:   קומה:  ", Latitude = 31.751193,   Longitude = 35.18933    },
                new BusStation{ BusStationKey  = 873401,  StationName  = "חניון הלאום",  StationAddress = " רחוב:שדרות הנשיא השישי 2 עיר: ירושלים רציף:   קומה:  ",   Latitude = 31.783061,   Longitude = 35.203237   },
                new BusStation{ BusStationKey  = 873702,  StationName  = " שכונת בזבז",   StationAddress = " רחוב:דרך בית לחם הישנה  עיר: ירושלים רציף:   קומה:",   Latitude =   31.770693,   Longitude = 35.243402   },
                new BusStation{ BusStationKey  = 874003,  StationName  = "שמואל הנביא/תדהר",  StationAddress = "רחוב:שמואל הנביא 71 עיר: ירושלים רציף:   קומה: ",   Latitude =   31.793299,   Longitude = 35.222176   },
                new BusStation{ BusStationKey  = 874104,  StationName  = "שמואל הנביא/יקים",  StationAddress = " רחוב:שמואל הנביא 47 עיר: ירושלים רציף:   קומה: ",   Latitude =  31.791394,   Longitude = 35.22416    },
                new BusStation{ BusStationKey  = 874305,  StationName  = "זקס",   StationAddress = " רחוב:משה זקס 5 עיר: ירושלים רציף:   קומה:  ",    Latitude = 31.789828,   Longitude = 35.225822   },
                new BusStation{ BusStationKey  = 874405,  StationName  = "שמואל הנביא/יקים",  StationAddress = "רחוב:שמואל הנביא 48 עיר: ירושלים רציף:   קומה:  ",   Latitude =  31.791505,   Longitude = 35.224204   },
                new BusStation{ BusStationKey  = 874507,  StationName  = "שמואל הנביא/תדהר",  StationAddress = "רחוב:שמואל הנביא 70 עיר: ירושלים רציף:   קומה: ",   Latitude =   31.792975,   Longitude = 35.222797   },
                new BusStation{ BusStationKey  = 874808,  StationName  = "ירמיהו/אלקנה",  StationAddress = " רחוב:ירמיהו  עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.792925,   Longitude = 35.213412   },
                new BusStation{ BusStationKey  = 874909,  StationName  = "משרד החוץ/שד' רבין",    StationAddress = " רחוב:שדרות יצחק רבין  עיר: ירושלים רציף:   קומה:",   Latitude =   31.782889, Longitude = 35.202207   },
                new BusStation{ BusStationKey  = 875010,  StationName  = "אצטדיון טדי/א''ס ביתר", StationAddress = " רחוב:דרך אגודת ספורט בית''ר  עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.751345,   Longitude = 35.188474   },
                new BusStation{ BusStationKey  = 875511,  StationName  = "אנג'ל/כנפי נשרים",  StationAddress = " רחוב:כנפי נשרים 6 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.787712,   Longitude = 35.192306   },
                new BusStation{ BusStationKey  = 875612,  StationName  = "מרכז שטנר/כנפי נשרים",  StationAddress = " רחוב:כנפי נשרים 20 עיר: ירושלים רציף:   קומה:  ",    Latitude = 31.787703,   Longitude = 35.188761   },
                new BusStation{ BusStationKey  = 875713,  StationName  = "בית ענבר/כנפי נשרים",   StationAddress = " רחוב:כנפי נשרים  עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.787728,   Longitude = 35.185498    },
                new BusStation{ BusStationKey  = 875814,  StationName  = "רוזנטל/כנפי נשרים", StationAddress = " רחוב:הרב אברהם דוד רוזנטל  עיר: ירושלים רציף:   קומה:  ",    Latitude = 31.787754,   Longitude = 35.179644   },
                new BusStation{ BusStationKey  = 876015,  StationName  = "קצנלבוגן/מעלות בוסטון", StationAddress = " רחוב:הרב רפאל קצנלבוגן 16 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.788077,   Longitude = 35.173651   },
                new BusStation{ BusStationKey  = 876216,  StationName  = "דבר ירושלים/קצנלבוגן",  StationAddress = " רחוב:הרב רפאל קצנלבוגן 58 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.783625,   Longitude = 35.174993   },
                new BusStation{ BusStationKey  = 876317,  StationName  = "ישיבת ויזניץ/קצנלבוגן", StationAddress = " רחוב:הרב רפאל קצנלבוגן 74 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.782684,   Longitude = 35.177453   },
                new BusStation{ BusStationKey  = 876418,  StationName  = "קצנלבוגן/הפלאה",    StationAddress = " רחוב:הרב רפאל קצנלבוגן 88 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.784417,   Longitude = 35.177826   },
                new BusStation{ BusStationKey  = 876519,  StationName  = "כפר שאול/קצנלבוגן", StationAddress = " רחוב:הרב רפאל קצנלבוגן 94 עיר: ירושלים רציף:   קומה:  ", Latitude = 31.785614,   Longitude = 35.178884   },
                new BusStation{ BusStationKey  = 878620,  StationName  = "דרך שכם/טובלר", StationAddress = " רחוב:דרך שכם 45 עיר: ירושלים רציף:   קומה:",    Latitude =   31.792394,  Longitude = 35.22901    },
                new BusStation{ BusStationKey  = 878721,  StationName  = "דרך שכם/טובלר", StationAddress = " רחוב:דרך שכם 45 עיר: ירושלים רציף:   קומה:  ",   Latitude = 31.792304,   Longitude = 35.229086   },
                new BusStation{ BusStationKey  = 878822,  StationName  = "בית ספר א סכאכיני", StationAddress = " רחוב:דרך שכם 17 עיר: ירושלים רציף:   קומה:  ",   Latitude = 31.793122,   Longitude = 35.230291   },
                new BusStation{ BusStationKey  = 879223,  StationName  = "שאולזון/רוזנטל",    StationAddress = "רחוב:הרב ש. שאולזון 3 עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.790632,   Longitude = 35.17594    },
                new BusStation{ BusStationKey  = 879324,  StationName  = "בית ענבר/כנפי נשרים",  StationAddress = "רחוב:כנפי נשרים 21 עיר: ירושלים רציף:   קומה:  ",   Latitude = 31.787528,   Longitude = 35.184601   },
                new BusStation{ BusStationKey  = 879425,  StationName  = "מרכז שטנר/כנפי נשרים",  StationAddress = " רחוב:כנפי נשרים 11 עיר: ירושלים רציף:   קומה:  ",    Latitude = 31.78751,    Longitude = 35.188178   },
                new BusStation{ BusStationKey  = 879526,  StationName  = "אנג'ל/כנפי נשרים",  StationAddress = " רחוב:כנפי נשרים  עיר: ירושלים רציף:   קומה:  ",  Latitude = 31.787504,   Longitude = 35.192121   },
                new BusStation{ BusStationKey  = 388332, StationName  = "הנחשול/הדייגים",    StationAddress = " :רחוב:הנחשול30עיר:ראשוןלציוןרציף:קומה ",  Latitude =31.984553 ,   Longitude = 34.782828   },
                new BusStation{ BusStationKey  = 388342, StationName  = "פריד/ששתהימים", StationAddress = "רחוב:משה פריד 9  עיר: רחוב ותרציף :קומה:",   Latitude = 31.88855,    Longitude = 34.790904   },
                new BusStation{ BusStationKey  = 388372, StationName  = "חנהאברך/וולקני",    StationAddress = " רחוב:חנהאברך9עיר:רחובותרציף:קומה:",  Latitude = 31.892166,   Longitude = 34.796071   },
                new BusStation{ BusStationKey  = 388393, StationName  = "הבנים/אליכהן",  StationAddress = "רחוב:הבנים4עיר:קריתעקרוןרציף:קומה:",  Latitude = 31.862305,   Longitude = 34.821857   },
                new BusStation{ BusStationKey  = 388403, StationName  = "ויצמן/הבנים",   StationAddress = "רחוב:וייצמן11עיר:קריתעקרוןרציף:קומה:",    Latitude = 31.865085,   Longitude = 34.822237   },
                new BusStation{ BusStationKey  = 388413, StationName  = "האירוס/הכלנית", StationAddress = " רחוב:האירוס13עיר:קריתעקרוןרציף:קומה:",   Latitude = 31.865222,   Longitude = 34.818957   },
                new BusStation{ BusStationKey  = 388443, StationName  = "אליכהן/לוחמיהגטאות",    StationAddress = " רחוב:אליכהן62עיר:קריתעקרוןרציף:קומה:",   Latitude = 31.86244,    Longitude = 34.827023   },
                new BusStation{ BusStationKey  = 388453, StationName  = "שבזי/שבת אחים", StationAddress = " רחוב:שבזי51עיר:קריתעקרוןרציף:קומה:", Latitude = 31.863501,   Longitude = 34.828702   },
                new BusStation{ BusStationKey  = 388473, StationName  = "חייםברלב/שדרותיצחקרבין",    StationAddress = " רחוב:חייםברלבעיר:ראשוןלציוןרציף:קומה:",  Latitude = 31.977409,   Longitude = 34.763896   },
                new BusStation{ BusStationKey  = 388483, StationName  = " מרכז לבריאות הנפש לב השרון ",  StationAddress = " רחוב:עיר:צורמשהרציף:קומה:",  Latitude = 32.300345,   Longitude = 34.912708   },
                new BusStation{ BusStationKey  = 388493, StationName  = "מרכז לבריאות הנפש לב השרון",    StationAddress = " רחוב:עיר:צורמשהרציף:קומה:",  Latitude = 32.301347,   Longitude = 34.912602   },
                new BusStation{ BusStationKey  = 388523, StationName  = "הולצמן/המדע",   StationAddress = " רחוב:חייםהולצמן2עיר:רחובותרציף:קומה: ",  Latitude =31.914255,    Longitude = 34.807944   },
                new BusStation{ BusStationKey  = 388563, StationName  = "הרותם/הדגניות", StationAddress = " רחוב:הרותם3עיר:רחובותרציף:קומה:",    Latitude = 31.874963,   Longitude = 34.81249    },
                new BusStation{ BusStationKey  = 388604, StationName  = "מבואהגפן/מורדהתאנה",    StationAddress = " רחוב:מבואהגפןעיר:ינוברציף:קומה ",    Latitude =32.305234,   Longitude = 34.948647   },
                new BusStation{ BusStationKey  = 388614, StationName  = "מבואהגפן/ההרחבה",   StationAddress = " רחוב:מבואהגפןעיר:ינוברציף:קומה:",    Latitude = 32.304022,   Longitude = 34.943393   },
                new BusStation{ BusStationKey  = 388624, StationName  = "ההרחבהא",   StationAddress = "רחוב:ההרחבהעיר:גאוליםרציף:קומה:", Latitude = 32.302957,   Longitude = 34.940529   },
                new BusStation{ BusStationKey  = 388634, StationName  = "ההרחבהב",   StationAddress = " רחוב:ההרחבהעיר:גאוליםרציף:קומה:",    Latitude = 32.300264,   Longitude = 34.939512   },
                new BusStation{ BusStationKey  = 388644, StationName  = "ההרחבה/הותיקים",    StationAddress = "רחוב:ההרחבהעיר:גאוליםרציף:קומה:", Latitude = 32.298171,   Longitude = 34.938705   },
                new BusStation{ BusStationKey  = 388704, StationName  = " הראשונים/כביש5700",    StationAddress = " רחוב:המגדל13עיר:כפרחייםרציף:קומה:",  Latitude = 32.352953,   Longitude = 34.899465   },
                new BusStation{ BusStationKey  = 388724, StationName  = "הגאוןבןאישחי/צאלון",    StationAddress = " רחוב:עיר:רחובותרציף:קומה:",  Latitude = 31.897286,   Longitude = 34.775083   },
                new BusStation{ BusStationKey  = 388734, StationName  = "עוקשי/לויאשכול",    StationAddress = " רחוב:ישראלעוקשי4עיר:רחובותרציף:קומה:",   Latitude = 31.883941,   Longitude = 34.807039   },
                new BusStation{ BusStationKey  = 388764, StationName  = "גורודסקי/יחיאלפלדי",    StationAddress = " רחוב:יהודהגורודיסקי35עיר:רחובותרציף:קומה:",  Latitude = 31.898463,   Longitude = 34.823461   },
                new BusStation{ BusStationKey  = 388774, StationName  = "דרךמנחםבגין/יעקבחזן",   StationAddress = "רחוב:דרךמנחםבגין30עיר:פתחתקווהרציף:קומה:",    Latitude = 32.076535,   Longitude = 34.904907   },
                new BusStation{ BusStationKey  = 388795, StationName  = " התאנה/הגפן",   StationAddress = " רחוב:התאנהעיר:יציץרציף:קומה:",   Latitude = 31.865457,   Longitude = 34.859437   },
                new BusStation{ BusStationKey  = 388805, StationName  = "התאנה/האלון",   StationAddress = "רחוב:התאנהעיר:יציץרציף:קומה:",    Latitude = 31.866772,   Longitude = 34.864555   },
                new BusStation{ BusStationKey  = 388815, StationName  = "דרךהפרחים/יסמין",   StationAddress = " רחוב:דרךהפרחים46עיר:גדרהרציף:קומה:", Latitude = 31.809325,   Longitude = 34.784347   },
                new BusStation{ BusStationKey  = 388845, StationName  = "מנחםבגין/יצחקרבין", StationAddress = "רחוב:שדרותמנחםבגין4עיר:גדרהרציף:קומה:",   Latitude = 31.799224,   Longitude = 34.782985   },
                new BusStation{ BusStationKey  = 388855, StationName  = "חייםהרצוג/דולב",    StationAddress = " רחוב:חייםהרצוג12עיר:גדרהרציף:קומה:", Latitude = 31.800334,   Longitude = 34.785069   },
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
                new BusLineStation{Active=true,CodeStation=178530,NumberStationInLine=1},
                new BusLineStation{Active=true,CodeStation=178531,NumberStationInLine=2},
                new BusLineStation{Active=true,CodeStation=178532,NumberStationInLine=3},
                new BusLineStation{Active=true,CodeStation=178533,NumberStationInLine=4},
                new BusLineStation{Active=true,CodeStation=178534,NumberStationInLine=5},
                new BusLineStation{Active=true,CodeStation=178535,NumberStationInLine=6},
                new BusLineStation{Active=true,CodeStation=178536,NumberStationInLine=7},
                new BusLineStation{Active=true,CodeStation=178537,NumberStationInLine=8},
                new BusLineStation{Active=true,CodeStation=178538,NumberStationInLine=9},
                new BusLineStation{Active=true,CodeStation=178539,NumberStationInLine=10},

            };
            ListConsecutiveStations = new List<ConsecutiveStations>
            {
                new ConsecutiveStations{StationCodeOne=178530,StationCodeTwo=178531,Distance=(float)1.12,AverageTravelTime=new TimeSpan(00,02,30)},
                new ConsecutiveStations{StationCodeOne=178531,StationCodeTwo=178532,Distance=(float)3,AverageTravelTime=new TimeSpan(00,04,30)},
                new ConsecutiveStations{StationCodeOne=178532,StationCodeTwo=178533,Distance=(float)1,AverageTravelTime=new TimeSpan(00,02,00)},
                new ConsecutiveStations{StationCodeOne=178533,StationCodeTwo=178534,Distance=(float)7,AverageTravelTime=new TimeSpan(00,05,00)},
                new ConsecutiveStations{StationCodeOne=178534,StationCodeTwo=178535,Distance=(float)2,AverageTravelTime=new TimeSpan(00,02,50)},
                new ConsecutiveStations{StationCodeOne=178535,StationCodeTwo=178536,Distance=(float)4.67,AverageTravelTime=new TimeSpan(00,04,45)},
                new ConsecutiveStations{StationCodeOne=178536,StationCodeTwo=178537,Distance=(float)5.3,AverageTravelTime=new TimeSpan(00,04,30)},
                new ConsecutiveStations{StationCodeOne=178537,StationCodeTwo=178538,Distance=(float)6.98,AverageTravelTime=new TimeSpan(00,04,50)},
                new ConsecutiveStations{StationCodeOne=178538,StationCodeTwo=178539,Distance=(float)2.4,AverageTravelTime=new TimeSpan(00,03,00)},
            };

        }

    }
}



