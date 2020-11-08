﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    class ListBusLines : SingleBusLine, IEnumerable
    {
        List<SingleBusLine> line_of_buses;

        public int Count { get; private set; }
        int capacity = 2;

        //...

        public IEnumerator GetEnumerator()
        {
            return line_of_buses.GetEnumerator();
        }

        public void addBusLine(SingleBusLine busline)
        {
            int count = 0;
            foreach (SingleBusLine item in line_of_buses)
                if (item == busline)
                    count++;
            if (count < 2)
                line_of_buses.Add(busline);
        }
        public void deletBusLine(SingleBusLine busline)
        {
            foreach (SingleBusLine item in line_of_buses)
                if (item == busline)
                    line_of_buses.Remove(item);
        }
        public void finedID(string id)
        {
            foreach (SingleBusLine item in line_of_buses)
                if (item.UniqueTest(id))
                    Console.WriteLine(item);
        }
        public ListBusLines back()
        {
            foreach (SingleBusLine item in line_of_buses)
                for (int i = 1; i < line_of_buses)
                    SingleBusLine temp = item++;
        }
        public SingleBusLine this[int index]
        {
            get
            {
                foreach (SingleBusLine item in line_of_buses)
                    if (item.BUSLINE.Equals(index))
                        return item;
                throw;
            }
        }
    }
}

   



