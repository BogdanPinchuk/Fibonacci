using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        private ulong[] Big_Expected
            => new ulong[]
            {
                0                   ,
                1                   ,
                1                   ,
                2                   ,
                3                   ,
                5                   ,
                8                   ,
                13                  ,
                21                  ,
                34                  ,
                55                  ,
                89                  ,
                144                 ,
                233                 ,
                377                 ,
                610                 ,
                987                 ,
                1597                ,
                2584                ,
                4181                ,
                6765                ,
                10946               ,
                17711               ,
                28657               ,
                46368               ,
                75025               ,
                121393              ,
                196418              ,
                317811              ,
                514229              ,
                832040              ,
                1346269             ,
                2178309             ,
                3524578             ,
                5702887             ,
                9227465             ,
                14930352            ,
                24157817            ,
                39088169            ,
                63245986            ,
                102334155           ,
                165580141           ,
                267914296           ,
                433494437           ,
                701408733           ,
                1134903170          ,
                1836311903          ,
                2971215073          ,
                4807526976          ,
                7778742049          ,
                12586269025         ,
                20365011074         ,
                32951280099         ,
                53316291173         ,
                86267571272         ,
                139583862445        ,
                225851433717        ,
                365435296162        ,
                591286729879        ,
                956722026041        ,
                1548008755920       ,
                2504730781961       ,
                4052739537881       ,
                6557470319842       ,
                10610209857723      ,
                17167680177565      ,
                27777890035288      ,
                44945570212853      ,
                72723460248141      ,
                117669030460994     ,
                190392490709135     ,
                308061521170129     ,
                498454011879264     ,
                806515533049393     ,
                1304969544928657    ,
                2111485077978050    ,
                3416454622906707    ,
                5527939700884757    ,
                8944394323791464    ,
                14472334024676221   ,
                23416728348467685   ,
                37889062373143906   ,
                61305790721611591   ,
                99194853094755497   ,
                160500643816367088  ,
                259695496911122585  ,
                420196140727489673  ,
                679891637638612258  ,
                1100087778366101931 ,
                1779979416004714189 ,
                2880067194370816120 ,
                4660046610375530309 ,
                7540113804746346429 ,
                12200160415121876738,
                // max value for ulong is 18446744073709551615, so max number is 93
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

        [TestMethod]
        public void FibonacciSlow_OneInputValue()
            => Fibonacci_OneInputValue(Fibonacci.FibonacciSlow);

        [TestMethod]
        public void FibonacciFast_OneInputValue()
            => Fibonacci_OneInputValue(Fibonacci.FibonacciFast);


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
        public void FibonacciBine_OneInputValue_Exception()
            => Fibonacci_OneInputValueException_(Fibonacci.FibonacciBine);

        [TestMethod]
        public void FibonacciBine_ArrayValu_Exception()
            => Fibonacci_ArrayValue_Exception(Fibonacci.FibonacciBine);

        [TestMethod]
        public void FibonacciApproximation_OneInputValue_Exception()
            => Fibonacci_OneInputValueException_(Fibonacci.FibonacciApproximation);

        [TestMethod]
        public void FibonacciApproximation_ArrayValu_Exception()
            => Fibonacci_ArrayValue_Exception(Fibonacci.FibonacciApproximation);



        private void Fibonacci_OneInputValueException_(del_Fibonacci<double, int> fib)
        {
            // avarrage
            int[] stub = new int[] { -71, 71 };

            string expected = "Absolute values more than 70 are not accurate. You should use other method.";

            // act - assert
            foreach (var i in stub)
                Assert.ThrowsException<Exception>(() => fib(i), expected);
        }

        private void Fibonacci_ArrayValue_Exception(del_Fibonacci<double[], int[]> fib)
        {
            // avarrage
            int[] stub = new int[] { -71, 71 };

            string expected = "Absolute values more than 70 are not accurate. You should use other method.";

            // act - assert
            Assert.ThrowsException<Exception>(() => fib(stub), expected);
        }


        private void Fibonacci_OneInputValue(del_Fibonacci<BigInteger, int> fib)
        {
            // avarrage
            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in Stub)
                str.Append(i).Append("\t");

            str.Append("\n\nExpected:\n");
            foreach (var i in Expected)
                str.Append(i).Append("\t");

            // act
            BigInteger[] actual = new BigInteger[Stub.Length];

            for (int i = 0; i < Stub.Length; i++)
                actual[i] = fib(Stub[i]);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual((BigInteger)Expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }


        [TestMethod]
        public void Fibonacci_Slow_OneInputValue_Par_0_93()
            => Fibonacci_OneInputValue_Par_0_93(Fibonacci.FibonacciSlow);

        [TestMethod]
        public void Fibonacci_Fast_OneInputValue_Par_0_93()
            => Fibonacci_OneInputValue_Par_0_93(Fibonacci.FibonacciFast);

        private void Fibonacci_OneInputValue_Par_0_93(del_Fibonacci<BigInteger, int> fib)
        {
            // avarrage
            var str = new StringBuilder("\nResult data:\n\n");

            // act
            BigInteger[] actual = new BigInteger[Big_Expected.Length];

            Parallel.For(0, Big_Expected.Length, (i) => actual[i] = fib(i));

            // assert
            Parallel.For(0, Big_Expected.Length, (i) => Assert.AreEqual(Big_Expected[i], actual[i]));

            // present
            for (int i = 0; i < actual.Length; i++)
                str.Append(i).Append(":\t").Append(actual[i]).Append("\n");

            Debug.WriteLine(str);
        }

        /// <summary>
        /// Method for testing max value before add exception,
        /// for use this method, you should comment "throw" block and change private -> public
        /// </summary>
        [TestMethod]
        private void MaxNumberWithAcurateValue()
        {
            // For present result
            var str = new StringBuilder("\nResult data:\n\n");

            // maximum number for other formula which dependes on acurate of "double" type
            int maxN = 0;

            BigInteger expected = default,
                actual = default;

            // for Binet's formula
            for (maxN = 0; expected == actual; maxN++)
            {
                expected = Fibonacci.FibonacciSlow(maxN);
                actual = (BigInteger)Fibonacci.FibonacciBine(maxN);
            }

            maxN -= 2;
            expected = Fibonacci.FibonacciSlow(maxN);
            actual = (BigInteger)Fibonacci.FibonacciBine(maxN);

            str.Append("Acurate of Binet's formula:\n")
                .Append($"\tmax number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            maxN++;
            expected = Fibonacci.FibonacciSlow(maxN);
            actual = (BigInteger)Fibonacci.FibonacciBine(maxN);

            // next value
            str.Append($"\n\tnext number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            expected = actual = default;

            // for Approximation formula
            for (maxN = 0; Equals(expected, actual); maxN++)
            {
                expected = Fibonacci.FibonacciSlow(maxN);
                actual = (BigInteger)Fibonacci.FibonacciApproximation(maxN);
            }

            maxN -= 2;
            expected = Fibonacci.FibonacciSlow(maxN);
            actual = (BigInteger)Fibonacci.FibonacciApproximation(maxN);

            str.Append("\nAcurate of Approximation formula:\n")
                .Append($"\tmax number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            maxN++;
            expected = Fibonacci.FibonacciSlow(maxN);
            actual = (BigInteger)Fibonacci.FibonacciBine(maxN);

            // next value
            str.Append($"\n\tnext number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            // present
            Debug.WriteLine(str);
        }

        [TestMethod]
        public void CompareSpeedMethods()
        {
            // avarrage
            int[] stub = Enumerable
                .Range(0, 71)
                .ToArray();

            // act
            Stopwatch timer = new Stopwatch();

            timer.Start();
            foreach (var i in stub)
                Fibonacci.FibonacciBine(i);
            timer.Stop();
            Debug.WriteLine($"\nBinet's formula: {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            foreach (var i in stub)
                Fibonacci.FibonacciApproximation(i);
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

            timer.Restart();
            foreach (var i in stub)
                Fibonacci.FibonacciSlow(i);
            timer.Stop();
            Debug.WriteLine($"Base calculate: {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            foreach (BigInteger i in stub)
                Fibonacci.FibonacciSlow(i);
            timer.Stop();
            Debug.WriteLine($"Base calculate (BigInteger): {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            foreach (var i in stub)
                Fibonacci.FibonacciFast(i);
            timer.Stop();
            Debug.WriteLine($"Fast calculate: {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            foreach (BigInteger i in stub)
                Fibonacci.FibonacciFastQueuen(i);
            timer.Stop();
            Debug.WriteLine($"Fast calculate using Queuen (BigInteger): {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            foreach (BigInteger i in stub)
                Fibonacci.FibonacciFast(i);
            timer.Stop();
            Debug.WriteLine($"Fast calculate using 2 values (BigInteger): {timer.Elapsed.TotalMilliseconds} ms\n");

        }

    }
}
