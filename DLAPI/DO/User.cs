using System;

namespace DO
{
    /// <summary>
    /// class user
    ///  This class constitutes a data contract of the dl layer
    /// </summary>
    public class User
    {
        public int Salt { get; set; }//salt
        public string HashedPassword { get; set; }//HashedPassword
        public string UserName { get; set; }//users Name
        public bool AllowingAccess { get; set; }//User access
        public string password { set; get; }//password 
        public bool DelUser { get; set; }//User delete field (no actual deletion)
    }
}