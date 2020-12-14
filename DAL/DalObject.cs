using System;
using System.Collections.Generic;
using System.Text;
using DALApi;

namespace DAL
{
    internal sealed class DalObject : IDal
    {
        static DalObject instance;
        public static DalObject Instance
        {
            get
            {
                if (instance == null)
                    instance = new DalObject();
                return instance;
            }
        }
        static DalObject() { }
        private DalObject() { }
        //data source.Add
        //צריך לממש את הפונקציות של הממשק Idal
    }
}
