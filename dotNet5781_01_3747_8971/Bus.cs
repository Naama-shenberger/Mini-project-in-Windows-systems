using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3747_8971
{
    class Bus
    {
        private int[] licensePlate;
        private DateTime dateActivity;
        private float kilometers;
        private bool status;
        private float sumKh;
        private static void Mileage(float _kh)
        {
            Bus MyBus = new Bus();
            MyBus.sumKh += _kh;

        }
        static bool FuelCondition()
        {
            Bus MyBus = new Bus();
            if (MyBus.kilometers > 1200)
                return true;
            return false;
        }
        public static void NumberOflicensePlate()
        {
            Bus MyBus = new Bus();
            //DateTime currentDate = DateTime.Now;
)
            if (MyBus.dateActivity <2018)
            {

            }
        }

    }
}



