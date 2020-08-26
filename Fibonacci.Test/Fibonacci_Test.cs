using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;
using System.Text;

namespace Fibonacci.Test
{
    [TestClass]
    public class Fibonacci_Test
    {
        [TestMethod]
        public void FibonacciBine_long_OneValue()
        {
            // avarrage
            int minN = -8,
                maxN = 20;

            int[] stub = new int[maxN - minN + 1];

            for (int i = 0; i < stub.Length; i++)
                stub[i] = minN + i;

            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in stub)
                str.Append(i).Append("\t");

            long[] expected = new long[]
            {
                -21, 13, -8, 5, -3, 2, -1, 1, 0, 1, 1, 2, 3, 5, 8, 13, 21,
                34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765
            };

            str.Append("\n\nExpected:\n");
            foreach (var i in expected)
                str.Append(i).Append("\t");

            // act
            long[] actual = new long[stub.Length];

            for (int i = 0; i < stub.Length; i++)
                actual[i] = Fibonacci.FibonacciBine_long(stub[i]);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }
    }
}
