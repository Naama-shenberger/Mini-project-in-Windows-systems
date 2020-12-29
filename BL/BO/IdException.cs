using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    [Serializable]
    public class IdException:Exception
    {
        public int ID;

        public IdException(string message) : base(message) { }
        public IdException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.IdException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad Bus Line id: {ID}";
    }
}