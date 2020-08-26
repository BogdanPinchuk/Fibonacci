#region Resources
// 1. https://en.wikipedia.org/wiki/Fibonacci_number
// 2. http://www.maths.surrey.ac.uk/hosted-sites/R.Knott/Fibonacci/fibFormula.html
// 3. https://uk.wikipedia.org/wiki/Золотий_перетин
// 4. https://uk.wikipedia.org/wiki/Послідовність_Фібоначчі
#endregion

using System;

namespace Fibonacci
{
    /// <summary>
    /// Fibonacci number
    /// </summary>
    public static class Fibonacci
    {
        /// <summary>
        /// Binet's formula
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static long FibonacciBine_long(int n)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);
            
            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            // Result
            double res = (Math.Pow(phi + 1, n) - Math.Pow(-phi, n)) / sr5;

            return (long)Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Approximation formula
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static long FibonacciApproximation_long(int n)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            // Result
            double res = ((n >= 0) ? Math.Pow(phi + 1, n) : -Math.Pow(-phi, n)) / sr5;

            return (long)Math.Round(res, MidpointRounding.AwayFromZero);
        }
    }

}
