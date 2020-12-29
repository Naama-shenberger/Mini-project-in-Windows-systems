using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Bus
    {
        public string LicensePlate { get; set; }//the license plate 
        public DateTime DateActivity { get; set; }// the bus date activity
        public DateTime DateTreatment { get; set; }// the bus next date of treatment
        public float KilometersTreatment { get; set; }// the buss kilometer to treatment
        public float KilometersGas { get; set; }//the bus gas kilmeters
        public float Totalkilometers { get; set; }//the bus total kilometers
        public float AirTire { get; set; }// Percentage of tire air
        public bool OilCondition { get; set; }//Says if you need to fill the oil tank
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

    }
}