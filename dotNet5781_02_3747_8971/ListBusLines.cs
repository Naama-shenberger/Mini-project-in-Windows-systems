using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_3747_8971
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    class ListBusLines : SingleBusLine, IEnumerable
    {
        List<SingleBusLine> line_of_buses;

        public System.Collections.IEnumerator GetEnumerator()
        {
            return ((System.Collections.IEnumerable)line_of_buses).GetEnumerator();
        }

        public interface IEnumerable
        {
                IEnumerator GetEnumerator();
        }

            public interface IEnumerator
            {
                object Current { get; }
                bool MoveNext();
                void Reset();
            }

    }
}
   




