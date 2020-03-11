using System;

namespace VDS_New.Utilities
{
    class MRandom
    {
        private static Random random = new Random();


        public static double CauchyStandardRandom()
        {
            return Accord.Statistics.Distributions.Univariate.CauchyDistribution.Random(0, 1);

        }
        public static double NormalDistributionRandom()
        {
            return Accord.Statistics.Distributions.Univariate.NormalDistribution.Random(0, 1);


        }
    }
}
