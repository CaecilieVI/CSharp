using System;

namespace BDSA2017.Exercise01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "leap")
            {
                bool is_int = Int32.TryParse(args[1], out int year);
                if (is_int)
                {
                    Console.WriteLine(Calculator.IsLeapYear(year));
                }
                else
                {
                    Console.WriteLine("Year must be a valid int");
                }

                return;
            }

            if (args.Length == 3 && args[1] == "powerof")
            {
                bool number_is_int = Int32.TryParse(args[0], out int number);
                bool power_is_int = Int32.TryParse(args[2], out int power);

                if (!number_is_int || !power_is_int)
                {
                    Console.WriteLine("Number and power must be a valid int");
                    return;
                }

                Console.WriteLine(Calculator.IsPowerOf(number, power));

                return;
            }

            Console.WriteLine("Invalid command");
        }
    }
}