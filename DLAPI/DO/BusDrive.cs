﻿using System;
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
        public int CodeLastStasion { get; set; }//code last stasion
        public TimeSpan TimeDriveExit { get; set; }//Time Drive
        public string NameLastStasion { get; set; }//Last Stasion name
        public TimeSpan CurTimeStasion { get; set; }// current Time Drive
        public int BusLineNumber { get; set; }//bus line number
        public int ID { set; get; }
    }

}