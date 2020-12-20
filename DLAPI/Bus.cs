using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Basic bus department
    /// </summary>
    public class Bus
    {
        /// <summary>
        /// get and set
        /// </summary>
        public bool Active { get; set; }//status of a bus whether it is active or not
        public string LicensePlate { get; set; }//the license plate 
        public DateTime DateActivity { get; set; }// the bus date activity
        public DateTime DateTreatment { get; set; }// the bus next date of treatment
        public float KilometersTreatment { get; set; }// the buss kilometer to treatment
        public float KilometersGas { get; set; }//the bus gas kilmeters
        public float Totalkilometers { get; set; }//the bus total kilometers
        public  float AirTire { get; set; }// Percentage of tire air
        public  bool OilCondition { get; set; }//Says if you need to fill the oil tank
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("\nBUS License Plate:{0}\n Date Activity:{1}\nDate Treatment:{2}\nKilometers Treatment:{3}\nKilometers Gas:{4}\nTotal kilometers:{5}\nAir in Tire:{6}\ndo we need to fill Oil Condition?{7}\n",LicensePlate, DateActivity, DateTreatment, KilometersTreatment, KilometersGas, Totalkilometers, AirTire, OilCondition);
        }
    }
}
