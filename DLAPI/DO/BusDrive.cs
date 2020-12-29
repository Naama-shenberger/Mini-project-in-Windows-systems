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
    /// a class thet is for a bus is traveling
    /// </summary>
    public class BusDrive
    {
        public int BusIdOnTheGo { get; set; }
        public string LicensePlate { get; set; }
        public TimeSpan ExitStart { get; set; }
        public int LastStasion { get; set; }
        public TimeSpan TimeDrive { get; set; }
        public TimeSpan TimeNextStop { get; set; }
        public string BusDriverFirstName { get; set; }
        public string BusDriverLastName { get; set; }
        public string BusDriverId { get; set; }
        public bool Active { set; get; }
        public int ID { set; get; }
    }

}