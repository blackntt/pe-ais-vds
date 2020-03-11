using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.Alg;

namespace VDS_New.DataLearnerAdapter
{
    class DLLDistanceAverageConveter:DistanceAverageConverter
    {
        public override double[] ConvertToDataLearner(VDSElement element, List<VDSElement> detector_set)
        {
            double average = 0;
            foreach (var d in detector_set)
            {
                average += element.GetDistance(d);
            }
            double[] features = new double[((DLLSignature)element.GetSignature()).Dlls.Length+1];
            for (int i = 0; i < ((DLLSignature)element.GetSignature()).Dlls.Length; i++)
            {
                features[i] = ((DLLSignature)element.GetSignature()).Dlls[i];
            }
            features[features.Length - 1] = average / detector_set.Count;
            //return new double[] { average / detector_set.Count };
            return features;
        }
    }
}
