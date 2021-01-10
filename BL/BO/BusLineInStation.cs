using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class BusLineInStation
    {
        /// <summary>
        /// sets and gets 
        /// </summary> 
        public int BusLineNumber { set; get; }//Bus line number 
        public int Area { set; get; }//Area of the bus line
        public int ID { get; set; }//Unique ID number
        public bool Active { set; get; }
        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

    }
}