using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class User
    {
        public string UserName { get; set; }//users Name
        public int Password { get; set; }//user password
        public bool AllowingAccess { get; set; }//User access
    }
}
