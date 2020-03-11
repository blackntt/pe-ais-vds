using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.Alg;

namespace VDS_New.DataLearnerAdapter
{
    class NormalConverter : IDataLearnerAdapter
    {
        public double[] ConvertToDataLearner(VDSElement element, List<VDSElement> detector_set)
        {
            return element.Features;
        }
    }
}
