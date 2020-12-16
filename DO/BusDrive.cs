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
    class BusDrive
    {
        private int busIdOnTheGo;//License Number (Entity Identifying Feature, Unique)
        private string l_licensePlate;//the license plate
        private TimeSpan exitStart;//Actual departure time
        private int lastStasion;//Last stop number on the line that the bus passed
        private TimeSpan timeDrive;//Transit time at the last stop mentioned above//????
        private TimeSpan timeNextStop;
        private string busDriverFirstName;
        private string busDriverLastName;
        private string busDriverId;



        public int BusIdOnTheGo
        {
            get {return busIdOnTheGo; }
            set { busIdOnTheGo = value; }
        }
        public string LICENSEPLATE
        {
            get { return l_licensePlate; }
            set { l_licensePlate = value; }
        }
        public TimeSpan ExitStart
        {
            get { return exitStart; }
            set { exitStart = value; }
        }
        public int LastStasion
        {
            get { return lastStasion; }
            set {lastStasion = value; }
        }
        public TimeSpan TimeDrive
        {
            get { return timeDrive; }
            set { timeDrive = value; }
        }
        public TimeSpan TimeNextStop
        {
            get { return timeNextStop; }
            set { timeNextStop = value; }
        }
        public string BusDriverFirstName
        {
            get { return busDriverFirstName; }
            set { busDriverFirstName = value; }
        }
        public string BusDriverLastName
        {
            get { return busDriverLastName; }
            set { busDriverLastName = value; }
        }
        public string BusDriverId
        {
            get { return busDriverId; }
            set { busDriverId = value; }
        }
    }

}
