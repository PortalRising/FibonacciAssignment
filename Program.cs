using System;
using System.Diagnostics;
using System.Dynamic;

namespace FibonacciAssignment
{
    public class Program
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();

            for (int x = 5; x < 50; x += 5)
            {
                sw.Reset();
                sw.Start();
                int fib = FibRecurse(x, false);
                sw.Stop();
                Console.WriteLine("Recursive {1}th Fib={2}, Elapsed={0}", sw.Elapsed, x, fib);
            }
            Console.WriteLine("\n\n");

            for (int x = 5; x < 50; x += 5)
            {
                sw.Reset();
                sw.Start();
                int fib = FibIterate(x, false);
                sw.Stop();
                Console.WriteLine("Iterative {1}th Fib={2}, Elapsed={0}", sw.Elapsed, x, fib);
            }
        }

        //
        // Return the xth fibanacci number using recursion
        // If print set to true, print out debug
        //
        static int FibRecurse(int x, bool print = false)
        {
            if (x < 2)
            {
                // Return the known values of fib to prevent us descending forever (also limit x to non-negative values only)
                // Perhaps use uint next time?
                return Math.Max(x, 0);
            }

            // Calculate fib by calculating the previous two values
            int fib = FibRecurse(x - 1) + FibRecurse(x - 2);

            if (print == true)
                Console.Write("Fibanacci = {0}\n", fib);

            return fib;
        }

        //
        // Return the xth fibanacci number using iteration
        // If print set to true, print out debug
        //
        static int FibIterate(int x, bool print = false)
        {
            // Store the last two values used in the fib sequence
            int[] previousTwo = [0, 1];

            for (int i = 2; i <= x; i++)
            {
                // Calculate next value of fib
                int newFib = previousTwo[0] + previousTwo[1];

                // Shift previous values left by one and insert fib
                previousTwo[0] = previousTwo[1];
                previousTwo[1] = newFib;
            }

            if (print == true)
                Console.Write("Fibanacci = {0}\n", previousTwo[1]);

            // Return the latest value of fib
            return previousTwo[1];
        }
    }
}
