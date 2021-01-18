using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusDrive
    {
        /// <summary>
        /// sets and gets 
        /// </summary>
        int ID { get; set; }//id of bus drive
        int BusLineNumber { get; set; }//bus line number
        public int CodeLastStasion { get; set; }//code last stasion
        public TimeSpan TimeDriveExit { get; set; }//Time Drive
        public string NameLastStasion { get; set; }//Last Stasion name
        public TimeSpan CurTimeStasion { get; set; }// current Time Drive
    }
}
