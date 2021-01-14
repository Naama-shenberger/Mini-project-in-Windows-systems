using System;

namespace DO
{
    [Serializable]
    public class User
    {
        public int Salt { get; set; }
        public string HashedPassword { get; set; }
        public string UserName { get; set; }//users Name
        public bool AllowingAccess { get; set; }//User access
        public string password { set; get; }
        public bool DelUser { get; set; }//User delete field (no actual deletion)
    }
}