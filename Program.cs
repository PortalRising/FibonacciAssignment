using System;
using System.ComponentModel;
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
            // Create a local 'inner' function that takes in a cache instead of making the outer function have a
            // default array param to prevent external sources altering our cache and allowing our logic to return invalid results
            static int InnerFibRecurse(ref int[] cache, int x)
            {
                if (x < 2)
                {
                    // Return the known values of fib to prevent us descending forever (also limit x to non-negative values only)
                    // Perhaps use uint next time?
                    return Math.Max(x, 0);
                }

                // Remove two from x because we do not store the first two values of fib
                int lookupIndex = x - 2;

                // Since 0 should not be stored in the cache, only when the array is initalised,
                // we can check if the value at x in the cache is valid by checking if its not equal to 0
                int lookup = cache[lookupIndex];
                if (lookup != 0)
                {
                    // We have already calculated this value
                    return lookup;
                }

                // Calculate fib by calculating the previous two values
                int fib = InnerFibRecurse(ref cache, x - 1) + InnerFibRecurse(ref cache, x - 2);

                // Add fib to the cache
                cache[lookupIndex] = fib;

                // Return fib
                return fib;
            }

            // Run the inner function to get fib with an empty cache that is the size of x - 1,
            // we remove one because we do not store the first two values of fib but still need to store the latest value of x
            int[] cache = new int[x - 1];
            int fib = InnerFibRecurse(ref cache, x);

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

            // Return the first value of fib if x is 0 because f(0) != 1
            if (x == 0)
            {
                return 0;
            }

            // Return the latest value of fib
            return previousTwo[1];
        }
    }
}
