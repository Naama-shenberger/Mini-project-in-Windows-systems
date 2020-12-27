using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;
using BLAPI;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL mydal = BLFactory.GetBL("1");
            try
            {
            }
            catch (Exception e) { Console.WriteLine(e); }
            
            Console.ReadKey();
              
        }
    }
}
