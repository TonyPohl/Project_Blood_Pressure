using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPressure
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            int menuChoice;
            char cont = 'y';  
            // Creating the instance db
            Database db = new Database();

            while(cont == 'y')
            {
                Console.WriteLine("===============================\n\tPressure database\t\n===============================");
                Console.WriteLine($"Today: {DateTime.Now}\n");
                Console.WriteLine("Select choice:\n1. Listing count of records\n2. Add new record\n3. Delete record\n4. Listing all records\n5. Average of systolic pressure\n6. Average of diastolic pressure\n7. Average of heart rate\n");
                menuChoice = Console.ReadKey().KeyChar;

                switch(menuChoice)
                {
                    // Listing count of records
                    case '1':
                        Console.WriteLine();
                        db.CountOfRecords();
                        break;

                    // Add new record
                    case '2':
                        Console.WriteLine();
                        db.AddRecord();
                        break;
                    
                    // Delete record    
                    case '3':
                        Console.WriteLine();
                        db.RemoveRecord();
                        break;

                    // Listing all records
                    case '4':
                        Console.WriteLine();
                        db.ListingHeader();
                        db.ListingAllRecords();
                        break;

                    // Average of systolic pressure
                    case '5':
                        Console.WriteLine();
                        db.CalculateSystolicAvg();
                        break;
                    
                    // Average of diastolic pressure
                    case '6':
                        Console.WriteLine();
                        db.CalculateDiastolicAvg();
                        break;


                    // Average of heart rate
                    case '7':
                        Console.WriteLine();
                        db.CalculatePulseAvg();
                        break;

                    default:
                        Console.WriteLine("Invalid value, please insert a numeric value 1 - 7");
                        break;
                }
                Console.ReadKey();
                Console.WriteLine("Do you want to continue? [y/n]");
                cont = Console.ReadKey().KeyChar;
                Console.Clear();
            }
        }
    }
}

