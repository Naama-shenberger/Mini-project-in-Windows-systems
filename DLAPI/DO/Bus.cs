using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static DO.Enums;

namespace DO
{
    public  class Bus
    {
        public string LicensePlate { get; set; }//the license plate 
        public DateTime DateActivity { get; set; }// the bus date activity
        public DateTime DateTreatment { get; set; }// the bus next date of treatment
        public float KilometersTreatment { get; set; }// the buss kilometer to treatment
        public float KilometersGas { get; set; }//the bus gas kilmeters
        public float Totalkilometers { get; set; }//the bus total kilometers
        public float AirTire { get; set; }// Percentage of tire air
        public bool OilCondition { get; set; }//Says if you need to fill the oil tank
        public bool Active { set; get; }
        public Status Status { get; set; }
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>


    }
}