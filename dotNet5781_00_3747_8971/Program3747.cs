using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_3747_8971
{
      partial class Program
      {
          static void Main(string[] args)
          {
                Welcome3747();
                Welcome8971();
                Console.ReadKey();
          }
           static partial void Welcome8971();
           private static void Welcome3747()
           {
              Console.Write("Enter your name: ");
              string name = Console.ReadLine();
              Console.WriteLine("{0}, welcome to my first console application", name);
           }

       }
    
}
