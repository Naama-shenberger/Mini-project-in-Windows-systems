using System;
using System.Collections.Generic;
using System.Text;
using static DO.Enums;

namespace DO
{
    public class BusLineInStation
    {

        /// <summary>
        /// sets and gets 
        /// </summary> 
        public bool Active { get; set; }//status of a bus line whether it is active or not
        public int BusLineNumber { set; get; }//Bus line number 
        public int Area { set; get; }//Area of the bus line

        /// <summary>
        /// override of ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bus line number:{0}\nArea of the bus:{1}", BusLineNumber, (Zones)Area);
        }
    }
}
