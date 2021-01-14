using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public static class Tools
    {
        public static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }
    }
}
