using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Utilities
{
    class FactorialPoorMans
    {
        public FactorialPoorMans() { }

        private long N;

        public string Factorial(int n)
        {
            if (n < 0)
            {
                throw new System.ArgumentException(
                 ": n >= 0 required, but was " + n);
            }

            if (n < 2) return "1";

            var p = new DecInteger(1);
            var r = new DecInteger(1);

            N = 1;

            int h = 0, shift = 0, high = 1;
            int log2n = (int)System.Math.Floor(System.Math.Log(n) * 1.4426950408889634);

            while (h != n)
            {
                shift += h;
                h = n >> log2n--;
                int len = high;
                high = (h - 1) | 1;
                len = (high - len) / 2;

                if (len > 0)
                {
                    p = p * Product(len);
                    r = r * p;
                }
            }

            r = r * DecInteger.Pow2(shift);
            return r.ToString();
        }

        private DecInteger Product(int n)
        {
            int m = n / 2;
            if (m == 0) return new DecInteger(N += 2);
            if (n == 2) return new DecInteger((N += 2) * (N += 2));
            return Product(n - m) * Product(m);
        }
    } // endOfFactorialPoorMans
}
