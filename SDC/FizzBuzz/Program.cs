//Write a program that prints the numbers from 1 to 100.
//But for multiples of three print "Fizz" instead of the number and for the multiples of five print "Buzz".
//For numbers which are multiples of both three and five print "FizzBuzz".

using System;
using System.Linq;

namespace FizzBuzz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 100).ToList();
            numbers.ForEach(PrintToConsole);

            Console.ReadLine();
        }

        private static void PrintToConsole(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
                return;
            }
            if (number % 3 == 0)
            {
                Console.WriteLine("Fizz");
                return;
            }
            if (number % 5 == 0)
            {
                Console.WriteLine("Buzz");
                return;
            }
            Console.WriteLine(number);
        }
    }
}
