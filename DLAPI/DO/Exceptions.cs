using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// class of Exceptions, The class heiress from Exception
    /// </summary>
    [Serializable]
    public class IdAlreadyExistsException:Exception
    {
        public int ID;
        public string Code;
        public IdAlreadyExistsException(int id) : base() => ID = id;
        public IdAlreadyExistsException(int id, string message) : base(message) => ID = id;
        public IdAlreadyExistsException(int id, string message, Exception innerException) : base(message, innerException) => ID = id;
        public IdAlreadyExistsException(string code) : base() => Code =code;
        public IdAlreadyExistsException(string code, string message) : base(message) => Code = code;
        public IdAlreadyExistsException(string code, string message, Exception innerException) : base(message, innerException) => Code = code;
        public override string ToString() => base.ToString() + $"bad id: {ID}";
    }
}
