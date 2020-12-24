using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    [Serializable]
    public class IdAlreadyExistsException : Exception
    {
        public int ID;

        public IdAlreadyExistsException(string message) : base(message) { }
        public IdAlreadyExistsException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.IdAlreadyExistsException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad Bus Line id: {ID}";
    }
}
