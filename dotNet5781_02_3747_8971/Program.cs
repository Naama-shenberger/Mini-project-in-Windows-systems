using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Exercise 2 - Mini project in c#
/// Name:Naama Shenberger 
/// Id:211983747
/// Name:Ella Hanzin
/// Id:212028971
/// <summary>
namespace dotNet5781_02_3747_8971
{
    enum Choices { Exit, Add_Line, Add_Station, Remove_Line, Remove_Station, Search_Lines, Print_opsions, Print_AllLines, Print_StationsLines }
    class Program
    {
        /// <summary>
        /// A function that prints a menu to the user and returns the user's selection number
        /// </summary>
        /// <returns></returns>
        static int ShowChoices()
        {
            Console.WriteLine("Enter your choice of action:\n" + "Click 1 to add a bus line\n" + "Click 2 to add a station\n" + "Click 3 to remove a bus line\n"+ "Click 4 to remove a bus station\n" + "Click 5 to search whitch lines pass a station \n" +
                "Click 6 to print the options for travel between 2 stations, without changing buses\n" + "Click 7 to print all buss lines \n"
                + "Click 8 to print list of all stations and line numbers passing through them\n" + "Click 0 to exit the program ");
            int _choice;
            int.TryParse(Console.ReadLine(), out _choice);
            return _choice;
        }
        static void Main(string[] args)
        {
            
            ListBusLines MyBusList = new ListBusLines();
            try
            {
                ///Saves the last and first line number 
                ///the first and last line will  have at least ten similar stations
                string saveLastBusLineNumber = "", saveFirstBusLineNumber = "";
                for (int i = 0; i < 10; i++)
                {
                    saveLastBusLineNumber = BusStation.r.Next(0, 1000).ToString();
                    if (i == 0)
                        saveFirstBusLineNumber = saveLastBusLineNumber;
                    MyBusList.AddBusLine(saveLastBusLineNumber);
                }
                ///A loop that adds stations to lines
                ///The is making sure That there are at least ten stations passing through more than one line
                string NumberStation = "";
                for (int i = 0; i < 10; i++)
                {
                    NumberStation= BusStation.r.Next(0, 1000001).ToString();
                    MyBusList[saveFirstBusLineNumber].AddStation(NumberStation);
                    MyBusList[saveLastBusLineNumber].Stations.Add(MyBusList[saveFirstBusLineNumber].StationSearch(NumberStation));
                }
                ///adds more stations to lines
                for (int i = 0; i < 3; i++)
                {
                    foreach (var Bus in MyBusList)
                    {
                        Bus.AddStation(BusStation.r.Next(0, 1000001).ToString());
                    }
                }
            }
            catch (MyException) { }

            int choice = (int)ShowChoices();

            while (choice != 0)
            {
                try
                {
                    switch ((Choices)choice)
                    {

                        case Choices.Add_Line:
                            Console.WriteLine("Enter bus line number to add:");
                            MyBusList.AddBusLine(Console.ReadLine());
                            break;
                        case Choices.Add_Station:
                            Console.WriteLine("Enter a bus line");
                            string line = Console.ReadLine();
                            bool flage = false;
                            foreach (var Bus in MyBusList)
                                if (Bus.BUSLINE == line)
                                {
                                    flage = true;
                                    Console.WriteLine("Enter a station number to add to the line");
                                    string station = Console.ReadLine();
                                    Bus.AddStation(station);
                                    //if (!myBusList.SameStation(station))
                                    //    throw new MyException("ERORR You must delete the station: 2 stations with the same ID number in different locations");
                                    break;
                                }
                            if(!flage)
                                throw new MyException("The line does not exist");
                            break;
                        case Choices.Remove_Line:
                            Console.WriteLine("enter bus line number to remove: ");
                            MyBusList.DeletBusLine(Console.ReadLine());
                            break;
                        case Choices.Remove_Station:
                            Console.WriteLine("Enter a bus line");
                            string Busline = Console.ReadLine();
                            Console.WriteLine("Enter a station number to remove to the station");
                            bool condition = false;
                            foreach (var Bus in MyBusList)
                                if (Bus.BUSLINE == Busline)
                                {
                                    condition = true;
                                    Bus.DelStation(Console.ReadLine());
                                    break;
                                }
                            if (!condition)
                                throw new MyException("The line does not exist");
                            break;
                        case Choices.Search_Lines:
                            //Lines passing through the station according to the station number and printing the lines
                            Console.WriteLine("enter station number:");
                            foreach (var Bus in MyBusList.FinedID(Console.ReadLine()))
                                Console.WriteLine($"The line {Bus.BUSLINE} is passes through the station");
                            break;
                        case Choices.Print_opsions:
                            //Printing the options for travel between 2 stations
                            //Receives from the user Departure station and destination station and Prints the lines(that pass through the stations Sorted by travel time)
                            Console.WriteLine("enter two station number: ");
                            string Origin = Console.ReadLine(); string destination = Console.ReadLine();
                            foreach (var BusOne in MyBusList.SortedLines())
                                if (BusOne.StationInLine(Origin) && BusOne.FIRSTSTATION.BusStationObject.BUS_STATION_KEY==Origin)
                                {
                                    foreach (var BusTwo in MyBusList)
                                        if (BusTwo.StationInLine(destination) && BusTwo.LASTSTATION.BusStationObject.BUS_STATION_KEY==destination)
                                        {
                                            Console.WriteLine(BusOne.BUSLINE);
                                            break;
                                        }
                                }
                            break;
                        case Choices.Print_AllLines:
                            foreach (var Bus in MyBusList)
                                Console.WriteLine(Bus.BUSLINE);
                            break;
                        case Choices.Print_StationsLines:
                            foreach (var Bus in MyBusList)
                                Console.WriteLine(Bus.ToString());
                            break;
                        case Choices.Exit:
                        default:
                            break;
                    }
                }
                catch (System.ArithmeticException) { }
                catch (ArgumentOutOfRangeException) { }
                catch (MyException) { }
                choice = (int)ShowChoices();
            }
        }

    }

}
