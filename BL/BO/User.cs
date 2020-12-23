using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public abstract class User
    {
        /// <summary>
        /// sets and gets
        /// </summary>
        public string UserName { get; set; }//users Name
        public int Password { get; set; }//user password
        public bool AllowingAccess { get; set; }//User access
        public bool FlageActive { set; get; }//A flage for Deletion
        public override string ToString() => this.ToStringProperty();
    }
}
