using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Alg
{
    class Distance
    {
        double value;
        public Distance(double value)
        {
            this.value = value;
        }
        public Distance(Distance other):this(other.value)
        {
        }
        public double Value { get => value; }
    }
}
