using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static DO.Enums;

namespace DO
{
    /// <summary>
    /// Bus Class
    /// This class constitutes a data contract of the dl layer
    /// </summary>
    public class Bus
    {
        public string LicensePlate { get; set; }// License plate of the bus
        public DateTime DateActivity { get; set; }// The date activity of bus
        public DateTime DateTreatment { get; set; }// the bus next date of treatment
        public float KilometersTreatment { get; set; }// the buss kilometer since last treatment
        public float KilometersGas { get; set; }//the bus gas kilmeters
        public float Totalkilometers { get; set; }//the bus total kilometers
        public float AirTire { get; set; }// Percentage of tire air
        public bool OilCondition { get; set; }//Says if you need to fill the oil tank
        public bool Active { set; get; }//if the bus exists or not
        public Status Status { get; set; }//status of the bus
    }
}