


using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DS
{
    public static class Configuration
    {
        public static int IdentificationNumberBusLine { set; get; } = 1000;//Identification number for bus line
        public static int IdentificationNumberBusDrive { set; get; } = 0;//Identification number for Bus Drive
        public static int IdentificationNumberUser { set; get; } = 10000;//Identification number for Bus Drive
        public static void UniquenessTest(BusLine busLine)
        {
            foreach (BusLine bus in DataSource.BusLines)
            {
                if (bus.BusLineNumber == busLine.BusLineNumber && bus.FirstStopNumber == busLine.FirstStopNumber && bus.LastStopNumber == busLine.LastStopNumber)
                {
                    busLine.ID = bus.ID;
                    return;
                }
            }
            IdentificationNumberBusLine += 1;
            busLine.ID = IdentificationNumberBusLine;
        }
    }
}
