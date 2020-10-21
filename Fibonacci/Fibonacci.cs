#region Resources
// 1. https://en.wikipedia.org/wiki/Fibonacci_number
// 2. http://www.maths.surrey.ac.uk/hosted-sites/R.Knott/Fibonacci/fibFormula.html
// 3. https://uk.wikipedia.org/wiki/Золотий_перетин
// 4. https://uk.wikipedia.org/wiki/Послідовність_Фібоначчі
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Fibonacci.Test")]
namespace Fibonacci
{
    /// <summary>
    /// Fibonacci number
    /// </summary>
    public static class Fibonacci
    {
        internal const string error = 
            "Absolute values more than 70 are not accurate. You should use other method.";

        /// <summary>
        /// Binet's formula
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static double FibonacciBine(int n)
        {
            // fast answer
            if (n == 0)
                return 0;
            else if (Math.Abs(n) == 1)
                return 1;
            else if (Math.Abs(n) == 2)
                return (n > 0) ? 1 : -1;
            else if (Math.Abs(n) > 70)
                throw new Exception(error);

            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 + 1);
            double phi_n = Math.Pow(phi, n);

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
            // accurate
            if (array.Max() > 70 || array.Min() < -70)
                throw new Exception(error);

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
                    // fast answer
                    if (*_a == 0)
                    {
                        *_r = 0;
                        continue;
                    }
                    else if (Math.Abs(*_a) == 1)
                    {
                        *_r = 1;
                        continue;
                    }
                    else if (Math.Abs(*_a) == 2)
                    {
                        *_r = (*_a > 0) ? 1 : -1;
                        continue;
                    }

                    *_p = Math.Pow(phi, *_a);
                    *_r = (*_p - Math.Pow(-1, *_a) / *_p) / sr5;
                    *_r = Math.Round(*_r, MidpointRounding.AwayFromZero);
                }
            }

            return res;
        }

        /// <summary>
        /// Binet's formula using parallel calculations
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static double[] FibonacciBine_Par(int[] array)
        {
            // accurate
            if (array.Max() > 70 || array.Min() < -70)
                throw new Exception(error);

            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 + 1);

            double[] res = new double[array.Length];

            Parallel.For(0, array.Length, (i) =>
            {
                // fast answer
                if (array[i] == 0)
                    res[i] = 0;
                else if (Math.Abs(array[i]) == 1)
                    res[i] = 1;
                else if (Math.Abs(array[i]) == 2)
                    res[i] = (array[i] > 0) ? 1 : -1;

                double phi_n = Math.Pow(phi, array[i]);
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
            // fast answer
            if (n == 0)
                return 0;
            else if (Math.Abs(n) == 1)
                return 1;
            else if (Math.Abs(n) == 2)
                return (n > 0) ? 1 : -1;
            else if (Math.Abs(n) > 70)
                throw new Exception(error);

            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            // Result
            double res = ((n >= 0) ? Math.Pow(phi + 1, n) : -Math.Pow(-phi, n)) / sr5;

            return Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Approximation formula
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static unsafe double[] FibonacciApproximation(int[] array)
        {
            // accurate
            if (array.Max() > 70 || array.Min() < -70)
                throw new Exception(error);

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
                    // fast answer
                    if (*_a == 0)
                    {
                        *_r = 0;
                        continue;
                    }
                    else if (Math.Abs(*_a) == 1)
                    {
                        *_r = 1;
                        continue;
                    }
                    else if (Math.Abs(*_a) == 2)
                    {
                        *_r = (*_a > 0) ? 1 : -1;
                        continue;
                    }

                    *_r = ((*_a >= 0) ? Math.Pow(phi + 1, *_a) : -Math.Pow(-phi, *_a)) / sr5;
                    *_r = Math.Round(*_r, MidpointRounding.AwayFromZero);
                }
            }

            return res;
        }

        /// <summary>
        /// Approximation formula using parallel calculations
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Fibonacci number</returns>
        public static double[] FibonacciApproximation_Par(int[] array)
        {
            // accurate
            if (array.Max() > 70 || array.Min() < -70)
                throw new Exception(error);

            // Square root of 5
            double sr5 = Math.Sqrt(5);

            // Golden ratio value
            double phi = 0.5 * (sr5 - 1);

            double[] res = new double[array.Length];

            Parallel.For(0, array.Length, (i) =>
            {
                // fast answer
                if (array[i] == 0)
                    res[i] = 0;
                else if (Math.Abs(array[i]) == 1)
                    res[i] = 1;
                else if (Math.Abs(array[i]) == 2)
                    res[i] = (array[i] > 0) ? 1 : -1;

                // Result
                double result = ((array[i] >= 0) ? Math.Pow(phi + 1, array[i]) : -Math.Pow(-phi, array[i])) / sr5;

                res[i] = Math.Round(result, MidpointRounding.AwayFromZero);
            });

            return res;
        }

        /// <summary>
        /// Slow calculate but accurate result, can calculate absolute value more then 70
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static unsafe BigInteger FibonacciSlow(int n)
        {
            // absolutely value of n 
            int value = Math.Abs(n);

            // sing of result : Math.Pow(-1, value + 1)
            int sign = (value % 2 == 0) ? -1 : 1;

            // fast answer or start data for calculate
            if (value == 0)
                return 0;
            else if (value == 1)
                return 1;
            else if (value == 2)
                return (n > 0) ? 1 : -1;

            #region Explanation
            /*
            0   1   2   3   4   5   6   7   -> № (number of sequence)
            0   1   1   2   3   5   8   13  -> FN (fibonacci number)

            1. we have values: FN[1, 1, 1] -> №[1, 2, 2]
            2. we get (remove): FN{1, 1} -> №{1, 2}
            3. balance: FN[1] -> №[2]
            4. calculate next value: FN(2) -> №(3)
            5. we set (insert): FN{2, 2} -> №{3, 3}
            6. we have: FN[1, 2, 3] -> №[2, 3, 3]
            
            7. repeat 1-6 for to get necessary result

            */
            #endregion

            Queue<BigInteger> neighbor = new Queue<BigInteger>();

            // start value
            for (int i = 0; i < 3; i++)
                neighbor.Enqueue(1);

            // calculate
            BigInteger result = default;

            for (int i = 0; i < value - 2; i++)
            {
                result = neighbor.Dequeue() + neighbor.Dequeue();
                neighbor.Enqueue(result);
                neighbor.Enqueue(result);
            }

            return (n > 0) ? result : sign * result;
        }

        /// <summary>
        /// Slow calculate but accurate result, can calculate absolute value more then 70
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static unsafe BigInteger FibonacciSlow(BigInteger n)
        {
            // absolutely value of n 
            BigInteger value = BigInteger.Abs(n);

            // sing of result : Math.Pow(-1, value + 1)
            int sign = (value % 2 == 0) ? -1 : 1;

            // fast answer or start data for calculate
            if (value == 0)
                return 0;
            else if (value == 1)
                return 1;
            else if (value == 2)
                return (n > 0) ? 1 : -1;

            Queue<BigInteger> neighbor = new Queue<BigInteger>();

            // start value
            for (int i = 0; i < 3; i++)
                neighbor.Enqueue(1);

            // calculate
            BigInteger result = default;

            for (BigInteger i = 0; i < value - 2; i++)
            {
                result = neighbor.Dequeue() + neighbor.Dequeue();
                neighbor.Enqueue(result);
                neighbor.Enqueue(result);
            }

            return (n > 0) ? result : sign * result;
        }

        /// <summary>
        /// Fast calculate but accurate result, can calculate absolute value more then 70, uning Queuen
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static unsafe BigInteger FibonacciFastQueuen(int n)
        {
            // absolutely value of n 
            int value = Math.Abs(n);

            // fast answer or start data for calculate
            if (value == 0)
                return 0;
            else if (value == 1)
                return 1;
            else if (value == 2)
                return (n > 0) ? 1 : -1;

            // sing of result : Math.Pow(-1, value + 1)
            int sign = (value % 2 == 0) ? -1 : 1;

            if (value <= 70)
                return (BigInteger)((n > 0) ? 
                    FibonacciApproximation(value) : sign * FibonacciApproximation(value));

            Queue<BigInteger> neighbor = new Queue<BigInteger>();

            // start value
            {
                BigInteger temp = (BigInteger)FibonacciApproximation(69);
                neighbor.Enqueue(temp);
                temp = (BigInteger)FibonacciApproximation(70);
                neighbor.Enqueue(temp);
                neighbor.Enqueue(temp);
            }

            // calculate use base method
            BigInteger result = default;

            for (int i = 70; i < value; i++)
            {
                result = neighbor.Dequeue() + neighbor.Dequeue();
                neighbor.Enqueue(result);
                neighbor.Enqueue(result);
            }

            return (n > 0) ? result : sign * result;
        }

        /// <summary>
        /// Fast calculate but accurate result, can calculate absolute value more then 70, uning Queuen
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static unsafe BigInteger FibonacciFastQueuen(BigInteger n)
        {
            // absolutely value of n 
            BigInteger value = BigInteger.Abs(n);

            // fast answer or start data for calculate
            if (value == 0)
                return 0;
            else if (value == 1)
                return 1;
            else if (value == 2)
                return (n > 0) ? 1 : -1;

            // sing of result : Math.Pow(-1, value + 1)
            int sign = (value % 2 == 0) ? -1 : 1;

            if (value <= 70)
                return (BigInteger)((n > 0) ?
                    FibonacciApproximation((int)value) : sign * FibonacciApproximation((int)value));

            Queue<BigInteger> neighbor = new Queue<BigInteger>();

            // start value
            {
                BigInteger temp = (BigInteger)FibonacciApproximation(69);
                neighbor.Enqueue(temp);
                temp = (BigInteger)FibonacciApproximation(70);
                neighbor.Enqueue(temp);
                neighbor.Enqueue(temp);
            }

            // calculate use base method
            BigInteger result = default;

            for (BigInteger i = 70; i < value; i++)
            {
                result = neighbor.Dequeue() + neighbor.Dequeue();
                neighbor.Enqueue(result);
                neighbor.Enqueue(result);
            }

            return (n > 0) ? result : sign * result;
        }

        /// <summary>
        /// Fast calculate but accurate result, can calculate absolute value more then 70, uning 2 values
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static unsafe BigInteger FibonacciFast(int n)
        {
            // absolutely value of n 
            int value = Math.Abs(n);

            // fast answer or start data for calculate
            if (value == 0)
                return 0;
            else if (value == 1)
                return 1;
            else if (value == 2)
                return (n > 0) ? 1 : -1;

            // sing of result : Math.Pow(-1, value + 1)
            int sign = (value % 2 == 0) ? -1 : 1;

            if (value <= 70)
                return (BigInteger)((n > 0) ?
                    FibonacciApproximation(value) : sign * FibonacciApproximation(value));

            BigInteger first = (BigInteger)FibonacciApproximation(69),
                second = (BigInteger)FibonacciApproximation(70);

            // calculate use base method
            BigInteger result = default;

            for (int i = 70; i < value; i++)
            {
                result = first + second;
                first = second;
                second = result;
            }

            return (n > 0) ? result : sign * result;
        }

        /// <summary>
        /// Fast calculate but accurate result, can calculate absolute value more then 70, uning 2 values
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Fibonacci number</returns>
        public static unsafe BigInteger FibonacciFast(BigInteger n)
        {
            // absolutely value of n 
            BigInteger value = BigInteger.Abs(n);

            // fast answer or start data for calculate
            if (value == 0)
                return 0;
            else if (value == 1)
                return 1;
            else if (value == 2)
                return (n > 0) ? 1 : -1;

            // sing of result : Math.Pow(-1, value + 1)
            int sign = (value % 2 == 0) ? -1 : 1;

            if (value <= 70)
                return (BigInteger)((n > 0) ?
                    FibonacciApproximation((int)value) : sign * FibonacciApproximation((int)value));

            BigInteger first = (BigInteger)FibonacciApproximation(69),
                second = (BigInteger)FibonacciApproximation(70);

            // calculate use base method
            BigInteger result = default;

            for (BigInteger i = 70; i < value; i++)
            {
                result = first + second;
                first = second;
                second = result;
            }

            return (n > 0) ? result : sign * result;
        }

    }

}
