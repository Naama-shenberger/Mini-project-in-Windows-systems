using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3747_8971
{
    enum Choices { Exit, Add_Bus, Choose_Bus, Refuel_Treat, Traveled };
    class Program
    {
        static int ShowChoices()
        {
            Console.WriteLine("Enter your choice of action: ");
            Console.WriteLine("Click 1 to add a bus ");
            Console.WriteLine("Click 2 to choosing a bus for travel ");
            Console.WriteLine("Click 3 to refuel or treat a bus ");
            Console.WriteLine("Click 4 to show travel of the last treatment for all vehicles in the company ");
            Console.WriteLine("Click 0 to exit the program ");
            int _choice = int.Parse(Console.ReadLine());
            return _choice;
        }
        static void Main(string[] args)
        {
            List<Bus> buslist = new List<Bus>();
            int choice = (int)ShowChoices();
            while (choice!=0)
            {
                switch ((Choices)choice)
                {
                    case Choices.Add_Bus:
                        Console.WriteLine("Enter bus license plate: ");
                        //we need to c how we can do this
                        Console.WriteLine("Enter bus starting date: ");
                        break;
                    case Choices.Choose_Bus:
                        Console.WriteLine("Enter bus license plate: ");
                        //?
                        break;
                    case Choices.Refuel_Treat:
                        Console.WriteLine("Enter bus license plate: ");
                        //
                        Console.WriteLine("Click 0 if you want to Refuel");
                        Console.WriteLine("Click 1 if you want to Treat the bus");
                        int temp = int.Parse(Console.ReadLine());
                        if (temp == 0) { }//sync
                        else//sync date
                            break;
                    case Choices.Traveled:

                        break;
                    case Choices.Exit:
                        break;
                    default:
                        break;
                }
                choice = (int)ShowChoices();
            }
        }
    }
}
