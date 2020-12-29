using System;
namespace DO
{
    /// <summary>
    /// class of Exceptions, The class heiress from Exception
    /// </summary>
    [Serializable]
    public class IdException : Exception
    {
        public int ID;
        public string Code;
        public IdException(int id) : base() => ID = id;
        public IdException(int id, string message) : base(message) => ID = id;
        public IdException(int id, string message, Exception innerException) : base(message, innerException) => ID = id;
        public IdException(string code) : base() => Code = code;
        public IdException(string code, string message) : base(message) => Code = code;
        public IdException(string code, string message, Exception innerException) : base(message, innerException) => Code = code;
        public override string ToString() => base.ToString() + $"bad id: {ID}";
    }
}
