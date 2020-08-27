#region Resources
// 1. https://en.wikipedia.org/wiki/Fibonacci_number
// 2. http://www.maths.surrey.ac.uk/hosted-sites/R.Knott/Fibonacci/fibFormula.html
// 3. https://uk.wikipedia.org/wiki/Золотий_перетин
// 4. https://uk.wikipedia.org/wiki/Послідовність_Фібоначчі
#endregion

using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

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
        public static double FibonacciBine(int n)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 + 1);
            double phi_n = checked(Math.Pow(phi, n));

            // Result
            double res = (phi_n - Math.Pow(-1, n) / phi_n) / sr5;

            return Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Binet's formula
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static unsafe double[] FibonacciBine(int[] array)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 + 1);

            double[] phi_n = new double[array.Length],
                res = new double[array.Length];

            fixed (double* p = phi_n, r = res)
            fixed (int* a = array)
            {
                double* _p = p, _r = r;
                int* _a = a;

                for (int i = 0; i < array.Length; i++, _p++, _a++, _r++)
                {
                    *_p = checked(Math.Pow(phi, *_a));
                    *_r = (*_p - Math.Pow(-1, *_a) / *_p) / sr5;
                    *_r = Math.Round(*_r, MidpointRounding.AwayFromZero);
                }
            }

            return res;
        }

        /// <summary>
        /// Binet's formula using threads
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static double[] FibonacciBine_Par(int[] array)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 + 1);

            double[] res = new double[array.Length];

            Parallel.For(0, array.Length, (i) =>
            {
                double phi_n = checked(Math.Pow(phi, array[i]));
                // Result
                double result = (phi_n - Math.Pow(-1, array[i]) / phi_n) / sr5;

                res[i] = Math.Round(result, MidpointRounding.AwayFromZero);
            });

            return res;
        }

        /// <summary>
        /// Approximation formula
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static double FibonacciApproximation(int n)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            // Result
            double res = checked(((n >= 0) ? Math.Pow(phi + 1, n) : -Math.Pow(-phi, n)) / sr5);

            return Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Approximation formula
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static unsafe double[] FibonacciApproximation(int[] array)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            double[] res = new double[array.Length];

            fixed (double* r = res)
            fixed (int* a = array)
            {
                double* _r = r;
                int* _a = a;

                for (int i = 0; i < array.Length; i++, _a++, _r++)
                {
                    *_r = checked(((*_a >= 0) ? Math.Pow(phi + 1, *_a) : -Math.Pow(-phi, *_a)) / sr5);
                    *_r = Math.Round(*_r, MidpointRounding.AwayFromZero);
                }
            }

            return res;
        }

        /// <summary>
        /// Approximation formula using threads
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static double[] FibonacciApproximation_Par(int[] array)
        {
            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            double[] res = new double[array.Length];

            Parallel.For(0, array.Length, (i) =>
            {
                // Result
                double result = checked(((array[i] >= 0) ? Math.Pow(phi + 1, array[i]) : -Math.Pow(-phi, array[i])) / sr5);

                res[i] = Math.Round(result, MidpointRounding.AwayFromZero);
            });

            return res;
        }

    }

}
