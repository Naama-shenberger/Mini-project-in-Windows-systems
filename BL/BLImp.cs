using BLAPI;
using DalApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    class BLImp:IBL
    {
        IDal dl = DalFactory.GetDal();
    }
}
