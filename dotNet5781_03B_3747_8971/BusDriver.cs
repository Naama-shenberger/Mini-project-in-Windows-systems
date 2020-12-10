using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Bus driver class
    /// </summary>
    partial class BusDriver 
    {
        /// <summary>
        /// Fields of a bus driver
        /// </summary>
        private string Id;
        private string name;
        private string gender;
        private string lastName;
        private bool isbusy;
        private TimeSpan timeDriveing;
        private TimeSpan totalTravelTime;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_lastName"></param>
        /// <param name="_gender"></param>
        /// <param name="_timeDriveing"></param>
        /// <param name="_isbusy"></param>
        public BusDriver(string _id,string _name,string _lastName,string _gender, TimeSpan _timeDriveing, bool _isbusy)
        {
            Id = _id;
            Name = _name;
            lastName = _lastName;
            gender = _gender;
            TimeDriveing = _timeDriveing;
            isbusy = _isbusy;
        }
        /// <summary>
        /// set and get Properties
        /// </summary>
        public TimeSpan TotalTravelTime
        {
            get { return totalTravelTime; }
            set { totalTravelTime = value;}
        }
        public string ID
        {
            get { return Id; }
            set { Id = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public TimeSpan TimeDriveing
        {
            get { return timeDriveing; }
            set { timeDriveing = value; TotalTravelTime += timeDriveing; }
            
        }
        public bool Isbusy
        {
            get { return isbusy;}
            set { isbusy = value; }
        }
        /// <summary>
        /// string Function override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Id number: {0} , Name: {1} ,Last Name: {2} ,Gender: {3} ,Time Travel : {4} ,on a ride: {5}",
               ID, Name,LastName, Gender, TotalTravelTime,Isbusy);
        }
     


    }
}
