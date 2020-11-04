using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    class BusStation
    {
        private string  BusCode;
        protected double Latitude, Longitude;

        //public static  place(double latitude,double longitude)
        //{
        //    this.Latitude = latitude;
        //    this.Longitude = longitude;
        //}
        public string BUSCODE
        {
            get { return BusCode; }
            set { BusCode = value; }
        }
    }
}
