using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3747_8971
{
    enum Choices { Exit, Add_Line,Add_Station,Remove_Line, Remove_Station, Search_Buss_oneStation, Print_opsions,Print_AllLines, Print_Stations}
    class Program
    {
        static int ShowChoices()
        {
            Console.WriteLine("Enter your choice of action: ");
            Console.WriteLine("Click 1 to add a bus line ");
            Console.WriteLine("Click 2 to add a station ");
            Console.WriteLine("Click 3 to remove a bus line ");
            Console.WriteLine("Click 4 to remove a station ");
            Console.WriteLine("Click 5 to search whitch lines pass a station ");
            Console.WriteLine("Click 6 to print the options for travel between 2 stations, without changing buses");
            Console.WriteLine("Click 7 to print all buss lines ");
            Console.WriteLine("Click 8 to print list of all stations and line numbers passing through them ");
            Console.WriteLine("Click 0 to exit the program ");
            int _choice;
            int.TryParse(Console.ReadLine(), out _choice);
            return _choice;
        }
        static void Main(string[] args)
        {
            ListBusLines myBusList;
            SingleBusLine b;
            for (int i = 0; i < 10; i++)
            {
                myBusList.addBusLine();
                for (int j = 0; j < 4; j++)
                { myBusList.AddStation(); }
            }

            int choice = (int)ShowChoices();
            while (choice!=0)
            {
                switch ((Choices) choice)
                {
                    case Choices.Add_Line:
                        Console.WriteLine("enter bus line number: ");
                        string id = Console.ReadLine();
                        myBusList.addBusLine(id);
                        break;
                    case Choices.Add_Station:
                        myBusList.AddStation();
                        break;
                    case Choices.Remove_Line:
                        Console.WriteLine("enter bus line number: ");
                        string lineID = Console.ReadLine();
                        myBusList.deletBusLine(lineID);
                        break;
                    case Choices.Remove_Station:
                        myBusList.DelStation();
                        break;
                    case Choices.Search_Buss_oneStation:
                        Console.WriteLine("enter station number: ");
                        string stationID = Console.ReadLine();
                        //Lines passing through the station according to station number
                        break;
                    case Choices.Print_opsions:
                       
                        break;
                    case Choices.Print_AllLines:
                        
                        break;
                    case Choices.Print_Stations:
                        break;
                    case Choices.Exit:
                    default:
                        break;
                }
            }
        }
    }
}
