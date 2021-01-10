using BL;
using BLAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLAPI
{

    public static class BLFactory
    {
        public static IBL GetBL()
        {
            return BLImp.Instance;
        }
    }

}

