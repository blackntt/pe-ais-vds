using System.Collections.Generic;
using VDS_New.Alg;

namespace VDS_New.DataLearnerAdapter
{
    internal interface IDataLearnerAdapter
    {
        double[] ConvertToDataLearner(VDSElement element,List<VDSElement> detector_set);
    }
}