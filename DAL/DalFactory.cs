using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace DALApi
{
    public class DalFactory
    {
        public static IDal getDal(string typeDal)
        {
            switch(typeDal)
            {
               // case 
            }
            return DalObject.Instance;
        }
    }
}
