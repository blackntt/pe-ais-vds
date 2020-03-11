using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.Alg;

namespace VDS_New.DataLearnerAdapter
{
    class HammingDLLDistanceAverageConverter:DLLDistanceAverageConveter
    {
        public override double[] ConvertToDataLearner(VDSElement element, List<VDSElement> detector_set)
        {
            DLLSignature new_element = (DLLSignature)element.GetSignature();
            double average = 0;
            double hammingdis = 0;
            foreach (var d in detector_set)
            {
                average += element.GetDistance(d);
                hammingdis += new_element.GetHammingDistance((DLLSignature)d.GetSignature());
            }
            double[] features = new double[2];
            //double[] features = new double[new_element.Values.Length+new_element.Dlls.Length+2];
            //for (int i = 0; i < new_element.Values.Length; i++)
            //{
            //    features[i] = new_element.Values[i];
            //}
            //for (int i = 0; i < new_element.Dlls.Length; i++)
            //{
            //    features[new_element.Values.Length+i] = new_element.Dlls[i];
            //}
            //for (int i = 0; i < ((DLLSignature)element.GetSignature()).Dlls.Length; i++)
            //{
            //    features[i] = ((DLLSignature)element.GetSignature()).Dlls[i];
            //}
            features[features.Length - 2] = average / detector_set.Count;
            features[features.Length - 1] = hammingdis / detector_set.Count;
            //return new double[] { average / detector_set.Count };
            return features;
        }
    }
}
