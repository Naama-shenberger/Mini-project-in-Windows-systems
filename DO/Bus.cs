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
    /// Basic bus department
    /// </summary>
    class Bus
    {
        private string l_licensePlate;//the license plate
        private DateTime d_dateActivity;// the bus date activity
        private DateTime d_dateTreatment;// the bus next date of treatment
        private float k_kilometersTreatment;// the buss kilometer to treatment
        private float k_kilometersGas;//the bus gas kilmeters
        private float t_totalkilometers;//the bus total kilometers
        private float air_tire;// Percentage of tire air
        private bool oil_condition;//Says if you need to fill the oil tank
        public float KILOMETERSGAS
        {
            get { return k_kilometersGas; }
            set { k_kilometersGas = value; }
        }
        public DateTime DATETREATMET
        {
            get { return d_dateTreatment; }
            set { d_dateTreatment = value; }
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

        public string LICENSEPLATE
        {
            get { return l_licensePlate; }
            set { l_licensePlate = value; }
        }
    }
}
