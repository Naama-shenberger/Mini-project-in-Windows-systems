using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    [Serializable]
    public class IdAlreadyExistsException:Exception
    {
        public int ID;
        public IdAlreadyExistsException(int id) : base() => ID = id;
        public IdAlreadyExistsException(int id, string message) : base(message) => ID = id;
        public IdAlreadyExistsException(int id, string message, Exception innerException) : base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad id: {ID}";
    }
}
