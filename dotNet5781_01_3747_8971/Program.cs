using dotNet5781_01_3747_8971;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Exercise 1 - Mini project in c#, Bus system
/// Name:Naama Shenberger 
/// Id:211983747
/// Name:Ella Hanzin
/// Id:212028971
/// <summary>

namespace dotNet5781_01_3747_8971
{
    enum Choices { Exit, Add_Bus, Choose_Bus, Refuel_Treat, Traveled }
    class Program
    {
        /// <summary>
        /// A function that asks the user to select an action
        /// </summary>
        static int ShowChoices()
        {
            Console.WriteLine("Enter your choice of action: ");
            Console.WriteLine("Click 1 to add a bus ");
            Console.WriteLine("Click 2 to choosing a bus for travel ");
            Console.WriteLine("Click 3 to refuel or treat a bus ");
            Console.WriteLine("Click 4 to show travel of the last treatment for all vehicles in the company ");
            Console.WriteLine("Click 0 to exit the program ");
            int _choice;
            int.TryParse(Console.ReadLine(), out _choice);
            return _choice;
        }
        static void Main(string[] args)
        {
            DateTime currentDate = DateTime.Now;
            ///<summary>
            ///Function that receives from user a plate number,and Returns it
            ///<summary>
            string Licenseplate()
            {
                Console.WriteLine("Enter bus license plate: ");
                string licensePlate = Console.ReadLine();
                return licensePlate;
            }
            List<Bus> BusList = new List<Bus>();
            int choice = (int)ShowChoices();
            while (choice != 0)
            {
                switch ((Choices)choice)
                {
                    case Choices.Add_Bus:
                        try
                        {
                            Console.WriteLine("Enter bus starting date: ");
                            var userInput = Console.ReadLine();
                            DateTime Dateinput;
                            DateTime.TryParse(userInput, out Dateinput);
                            ///Check if the date makes sense (in 1896 Countries began to use a number of plates)
                            if (!(Dateinput.Day>0 && Dateinput.Day<32) ||!(Dateinput.Month>0 && Dateinput.Month<13) ||!(Dateinput.Year>1895 && Dateinput.Year<2021))
                            {
                                Console.WriteLine("Wrong date");
                                break;
                            }
                            Bus AddBus = new Bus();
                            AddBus.DATEACTIVITY = Dateinput;
                            string licenNumber = Licenseplate();
                            ///input integrity check (-2 because license plates have 2 '-')
                            if (licenNumber.Length - 2 != AddBus.NumberOflicensePlate())
                            {
                                Console.WriteLine("Error: You typed the wrong number of license plaste");
                                break;
                            }
                            AddBus.LICENSEPLATE = licenNumber;
                            ///Check if the bus is already in the system
                            for (int i = 0; i < BusList.Count; i++)
                            {
                                if (BusList[i].LICENSEPLATE == licenNumber)
                                    throw new ArithmeticException("The plate number is already in the system");
                            }
                            BusList.Add(AddBus);
                            Console.WriteLine("Enter Number of kilometers");
                            ///Absorption from the user number kilometers,
                            ///and notification to the user if the bus needs Treatment or refueling
                            float k;
                            float.TryParse(Console.ReadLine(), out k);
                            AddBus.KILOMETERSGAS = k;
                            AddBus.Mileage(k);
                            if (AddBus.TreatmentIsNeeded())
                                Console.WriteLine("The Bus Needs A Treatment");
                            if (AddBus.FuelCondition())
                                Console.WriteLine("The Bus Needs A refuel");
                        }
                        catch
                        {
                            Console.WriteLine("The plate number is already in the system");
                        }
                        break;
                    case Choices.Choose_Bus:
                        bool text = false;
                        string input = Licenseplate();
                        for (int i = 0; i < BusList.Count; i++)
                        {
                            if (BusList[i].LICENSEPLATE == input)
                            {
                                text = true;
                                int save = Bus.r.Next(0, 1201);///Saveing the Random number
                                ///Check if the bus is allowed to travel
                                if (BusList[i].TreatmentIsNeeded())
                                    Console.WriteLine("ERROR: Unable to make a trip, you must send for handling ");
                                if (BusList[i].FuelCondition())
                                    Console.WriteLine("ERROR: Unable to make a trip, you must send for refueling ");
                                else
                                {
                                    ///Adds the miles
                                    BusList[i].KILOMETERSGAS += save;
                                    BusList[i].kILOMETERS_TREATMENT += save;
                                    BusList[i].Mileage(BusList[i].KILOMETERSGAS);
                                    Console.WriteLine("Bus traveled successfully");
                                    break;
                                }
                            }
                        }
                        if (!text)
                            Console.WriteLine("The bus does not exist in the system");
                        break;
                    case Choices.Refuel_Treat:
                        string lnumber = Licenseplate();
                        Console.WriteLine("Click 0 if you want to Refuel");
                        Console.WriteLine("Click 1 if you want to Treat the bus");
                        int temp;
                        int.TryParse(Console.ReadLine(), out temp);
                        bool text1 = false;
                        if (temp == 0)
                        {
                            for (int i = 0; i < BusList.Count; i++)
                            {
                                if (BusList[i].LICENSEPLATE == lnumber)
                                {
                                    text1=true;
                                    BusList[i].KILOMETERSGAS= 0;///Reset,because the problem is solved
                                    Console.WriteLine("Fueling was done successfully");
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < BusList.Count; i++)
                            {
                                if (BusList[i].LICENSEPLATE == lnumber)
                                {
                                    text1 = true;
                                    BusList[i].DATETREATMET = currentDate;
                                    BusList[i].kILOMETERS_TREATMENT = 0;///Reset,because the problem is solved
                                    Console.WriteLine("Treatment was done successfully");

                                }
                            }

                        }
                        if(!text1)
                            Console.WriteLine("The bus does not exist in the system");
                        break;
                    case Choices.Traveled:
                        ///Prints all details of the buss 
                        int counter = 0;
                        for (int i = 0; i < BusList.Count; i++)
                        {
                            counter++;
                            Console.WriteLine("Bus details {0}", counter);
                            Console.WriteLine("number license plaste :{0}", BusList[i].LICENSEPLATE);
                            Console.WriteLine("Date of ascent to the road :{0} ", BusList[i].DATEACTIVITY);
                            Console.WriteLine("Date of last Treatment :{0} ", BusList[i].DATETREATMET);
                            Console.WriteLine("Number of kilometers :{0}  ", BusList[i].kILOMETERS_TREATMENT);
                        }
                        break;
                    case Choices.Exit:
                    default:
                        break;
                }
                choice = (int)ShowChoices();
            }
        }
    }
}
/// <summary>
/// output:Enter your choice of action:
//Click 1 to add a bus
//Click 2 to choosing a bus for travel
//Click 3 to refuel or treat a bus
//Click 4 to show travel of the last treatment for all vehicles in the company
//Click 0 to exit the program
//1
//Enter bus starting date:
//12 / 09 / 2017
//Enter bus license plate:
//12 - 345 - 56
//Enter Number of kilometers
//1300
//Enter your choice of action:
//Click 1 to add a bus
//Click 2 to choosing a bus for travel
//Click 3 to refuel or treat a bus
//Click 4 to show travel of the last treatment for all vehicles in the company
//Click 0 to exit the program
//2
//Enter bus license plate:
//12 - 345 - 56
//ERROR: Unable to make a trip, you must send for refueling
//Enter your choice of action:
//Click 1 to add a bus
//Click 2 to choosing a bus for travel
//Click 3 to refuel or treat a bus
//Click 4 to show travel of the last treatment for all vehicles in the company
//Click 0 to exit the program
//3
//Enter bus license plate:
//12 - 345 - 56
//Click 0 if you want to Refuel
//Click 1 if you want to Treat the bus
//0
//Fueling was done successfully
//Enter your choice of action:
//Click 1 to add a bus
//Click 2 to choosing a bus for travel
//Click 3 to refuel or treat a bus
//Click 4 to show travel of the last treatment for all vehicles in the company
//Click 0 to exit the program
//2
//Enter bus license plate:
//12 - 345 - 56
//Bus traveled successfully
//Enter your choice of action:
//Click 1 to add a bus
//Click 2 to choosing a bus for travel
//Click 3 to refuel or treat a bus
//Click 4 to show travel of the last treatment for all vehicles in the company
//Click 0 to exit the program
//4
//Bus details 1
//number license plaste :12 - 345 - 56
//Date of ascent to the road :12 / 09 / 2017 00:00:00
//Date of last Treatment :01 / 01 / 0001 00:00:00
//Number of kilometers :125
//Enter your choice of action:
//Click 1 to add a bus
//Click 2 to choosing a bus for travel
//Click 3 to refuel or treat a bus
//Click 4 to show travel of the last treatment for all vehicles in the company
//Click 0 to exit the program
/// </summary>
