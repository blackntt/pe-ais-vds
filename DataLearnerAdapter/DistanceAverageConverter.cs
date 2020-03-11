using System.Collections.Generic;
using VDS_New.Alg;

namespace VDS_New.DataLearnerAdapter
{
    class DistanceAverageConverter : IDataLearnerAdapter
    {
        /// <summary>
        /// Compute Average of all distance from the current element to detector of detector set
        /// </summary>
        /// <param name="element"></param>
        /// <param name="detector_set"></param>
        /// <returns></returns>
        public virtual double[] ConvertToDataLearner(VDSElement element, List<VDSElement> detector_set)
        {
            double average = 0;
            foreach (var d in detector_set)
            {
                average += element.GetDistance(d);
            }
            return new double[] { average / detector_set.Count };
        }
    }
}
