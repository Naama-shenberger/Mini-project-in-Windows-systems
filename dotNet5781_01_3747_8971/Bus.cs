using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3747_8971
{
    /// <summary>
    /// Bus class, which allows access to the fields by methods 'set' and 'get'
    /// Functions that check the integrity and update of buses
    /// </summary>
    class Bus
    {
        public static Random r = new Random();
        private string l_licensePlate;
        private DateTime d_dateActivity;
        private DateTime d_dateTreatment;
        private float k_kilometersTreatment;
        private float k_kilometersGas;
        private float t_totalkilometers;
        /// <summary>
        /// set and get functions for private fields
        /// </summary>
        public float KILOMETERSGAS
        {
            get { return k_kilometersGas; }
            set { k_kilometersGas = value; }
        }
        public DateTime DATETREATMET
        {
            get { return d_dateTreatment; }
            set { d_dateTreatment= value; }
        }
        public DateTime DATEACTIVITY
        {
            get { return d_dateActivity; }
            set { d_dateActivity = value; }
        }
        public float TOTALKILOMETERS
        {
            get { return t_totalkilometers; ; }
            ///private set,doesn't allowed to change the total kilometers
            private set { t_totalkilometers = value; }

        }
        public float kILOMETERS_TREATMENT
        {
            get { return k_kilometersTreatment; }
            set { k_kilometersTreatment = value; }
        }
    
        public string  LICENSEPLATE
        {
            get { return l_licensePlate; }
            set { l_licensePlate = value; }
        }
        /// <summary>
        /// A function that receives a parameter and adds to the total kilometers
        /// </summary>
        /// <param name="_kilometers"></param>
        public void Mileage(float _kilometers)
        {
            this.TOTALKILOMETERS += _kilometers;
        }
        /// <summary>
        /// A function that checks if the vehicle needs to be refueled
        /// </summary>
        /// <returns></returns>
        public bool FuelCondition()
        {
            if (this.k_kilometersGas > 1200)
                return true;
            return false;
        }
        /// <summary>
        /// A function that checks how many numbers the user needs to type for the number plate 
        /// </summary>
        /// <returns></returns>
        public int NumberOflicensePlate()
        {
            int year; 
            int.TryParse(this.d_dateActivity.Year.ToString(),out year);
            return year < 2018 ? 7 : 8;
        }
        /// <summary>
        /// Function that checks if the bus needs treatment - 
        /// treatment bus defined if the bus has traveled more than 20,000 kilometers or a year has passed since the last treatment
        /// </summary>
        /// <returns></returns>
        public bool TreatmentIsNeeded()
        {
            if (this.k_kilometersTreatment >= 20000 || dateCheck())
                return true;
            return false;
        }/// <summary>
         /// A function that checks if a year has passed since the last treatment
         /// </summary>
         /// <returns></returns>
        bool dateCheck()
        {
            int day;
            int month;
            int year;
            int.TryParse(this.d_dateTreatment.Day.ToString(),out day);
            int.TryParse(this.d_dateTreatment.Month.ToString(),out month);
            int.TryParse(this.d_dateTreatment.Year.ToString(),out year);
            DateTime currentDate = DateTime.Now;
            if (int.Parse(currentDate.Day.ToString()) == day && int.Parse(currentDate.Month.ToString()) == month && int.Parse(currentDate.Year.ToString()) <year || int.Parse(currentDate.Year.ToString()) < year)
                return true;
            return false;
        }

    }
}