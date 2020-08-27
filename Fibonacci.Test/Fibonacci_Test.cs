using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;
using System.Text;

namespace Fibonacci.Test
{
    [TestClass]
    public class Fibonacci_Test
    {
        private delegate T del_Fibonacci<T, K>(K n);

        private int[] Stub
        {
            get
            {
                int minN = -8, maxN = 20;

                int[] stub = new int[maxN - minN + 1];

                for (int i = 0; i < stub.Length; i++)
                    stub[i] = minN + i;

                return stub;
            }
        }
        private double[] Expected
            => new double[]
            {
                -21, 13, -8, 5, -3, 2, -1, 1, 0, 1, 1, 2, 3, 5, 8, 13, 21,
                34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765
            };


        [TestMethod]
        public void FibonacciBine_OneInputValue()
            => Fibonacci_OneInputValue(Fibonacci.FibonacciBine);

        [TestMethod]
        public void FibonacciApproximation_OneInputValue()
            => Fibonacci_OneInputValue(Fibonacci.FibonacciApproximation);

        [TestMethod]
        public void FibonacciBine_ArrayValue()
            => Fibonacci_ArrayValue(Fibonacci.FibonacciBine);

        [TestMethod]
        public void FibonacciApproximation_ArrayValue()
            => Fibonacci_ArrayValue(Fibonacci.FibonacciApproximation);
        
        [TestMethod]
        public void FibonacciBine_Parallel()
            => Fibonacci_ArrayValue(Fibonacci.FibonacciBine_Par);

        [TestMethod]
        public void FibonacciApproximation_Parallel()
            => Fibonacci_ArrayValue(Fibonacci.FibonacciApproximation_Par);


        private void Fibonacci_OneInputValue(del_Fibonacci<double, int> fib)
        {
            // avarrage
            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in Stub)
                str.Append(i).Append("\t");

            str.Append("\n\nExpected:\n");
            foreach (var i in Expected)
                str.Append(i).Append("\t");

            // act
            double[] actual = new double[Stub.Length];

            for (int i = 0; i < Stub.Length; i++)
                actual[i] = fib(Stub[i]);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual(Expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }
        
        private void Fibonacci_ArrayValue(del_Fibonacci<double[], int[]> fib)
        {
            // avarrage
            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in Stub)
                str.Append(i).Append("\t");

            str.Append("\n\nExpected:\n");
            foreach (var i in Expected)
                str.Append(i).Append("\t");

            // act
            double[] actual = fib(Stub);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual(Expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }


        [TestMethod]
        public void CompareFibMethods()
        {
            // avarrage
            int[] stub = new int[101];

            for (int i = 0; i < stub.Length; i++)
                stub[i] = i;

            // act
            Stopwatch timer = new Stopwatch();

            timer.Start();
            for (int i = 0; i < stub.Length; i++)
                Fibonacci.FibonacciBine(stub[i]);
            timer.Stop();

            Debug.WriteLine($"\nBinet's formula: {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            for (int i = 0; i < stub.Length; i++)
                Fibonacci.FibonacciApproximation(stub[i]);
            timer.Stop();
            Debug.WriteLine($"Approximation formula: {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            Fibonacci.FibonacciBine(stub);
            timer.Stop();
            Debug.WriteLine($"Binet's formula (array): {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            Fibonacci.FibonacciApproximation(stub);
            timer.Stop();
            Debug.WriteLine($"Approximation formula (array): {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            Fibonacci.FibonacciBine_Par(stub);
            timer.Stop();
            Debug.WriteLine($"Binet's formula (parallel): {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            Fibonacci.FibonacciApproximation_Par(stub);
            timer.Stop();
            Debug.WriteLine($"Approximation formula (parallel): {timer.Elapsed.TotalMilliseconds} ms\n");

        }

    }
}
