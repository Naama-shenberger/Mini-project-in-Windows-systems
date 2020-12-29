using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace BO
{
    public class BusLine : Bus
    {
        /// <summary>
        /// sets and gets 
        /// </summary>
        public int BusLineNumber { set; get; }//Bus line number 
        public int Area { set; get; }//Area of the bus line
        public int ID { get; set; }//Unique ID number
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
        public IEnumerable<BusLineStation> StationsInLine { set; get; }//Collection of bus line station

    }
}