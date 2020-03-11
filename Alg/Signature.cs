using System;
using System.Collections.Generic;
using System.Globalization;
using VDS_New.Utilities;

namespace VDS_New.Alg
{
    class Signature
    {
        private double[] values;

        public double[] Values { get => values; }
        public bool IsNormlized { get => isNormlized; set => isNormlized = value; }
 

        bool isNormlized = false;
        

        protected Signature() {

            starge_parameters = new double[Globals.STRATEGY_PARAS.Length];
            Array.Copy(Globals.STRATEGY_PARAS, starge_parameters, Globals.STRATEGY_PARAS.Length);
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="other"></param>
        public Signature(Signature other):this(other.Values)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="values"></param>
        public Signature(double[] values):this()
        {
            this.values = new double[values.Length];
            for (int i = 0; i < this.values.Length; i++)
            {
                this.values[i] = values[i];
            }
        }
        /// <summary>
        /// Calculate the euclid distances between THIS and OTHER
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual double ComputeEuclideanDistance(Signature other)
        {
            double dis = 0;
            for (int i = 0; i < Values.Length; i++)
            {
                dis += (Values[i] - other.Values[i]) * (Values[i] - other.Values[i]);
            }
            return Math.Sqrt(dis);
        }

        /// <summary>
        /// Normalize the signature by using min-max method
        /// </summary>
        /// <param name="mins"></param>
        /// <param name="maxs"></param>
        public void Normalize(double[] mins, double[] maxs)
        {
            if (!IsNormlized)
            {

                for (int i = 0; i < Values.Length; i++)
                {
                    if (mins[i] == maxs[i])
                    {
                        Values[i] = 0.5;
                    }
                    else
                        Values[i] = (Values[i] - mins[i]) / (maxs[i] - mins[i]);
                }
            }
            isNormlized = true;
        }

        public virtual double GetAffScale(List<VDSElement> detector_set)
        {
            return 0;
        }

        /// <summary>
        /// This signature multiple to coffs
        /// </summary>
        /// <param name="coffs"></param>
        /// <returns></returns>
        public double[] Multiple(double[] coffs)
        {
            double[] rs = new double[Values.Length];
            for (int i = 0; i < Values.Length; i++)
            {
                rs[i] = Values[i] * coffs[i];
            }
            return rs;
        }

        public virtual Signature CreateACloneOld()
        {
            Signature new_signature = new Signature(this.Values);
            for (int j = 0; j < Values.Length; j++)
            {
                double ra = Math.Pow(Math.Sqrt(2 * Math.Sqrt(Values.Length)), -1);
                double rb = Math.Pow(Math.Sqrt(2 * Values.Length), -1);
                double new_n = Values[j] * Math.Exp(ra * MRandom.NormalDistributionRandom() + rb * MRandom.NormalDistributionRandom());
                new_signature.Values[j] = Values[j] + MRandom.CauchyStandardRandom() * new_n;
            }
            return new_signature;
        }
        double[] starge_parameters;
        public double[] Starge_parameters { get => starge_parameters; set => starge_parameters = value; }
        public virtual Signature CreateAClone()
        {
            return CreateACloneWithCauchyMO();
        }
        public virtual Signature CreateACloneWithCauchyMO()
        {
            Signature new_signature = new Signature(this.Values);
            double temp_random_for_N = MRandom.NormalDistributionRandom();
            for (int j = 0; j < Values.Length; j++)
            {
                double ra = Math.Pow(Math.Sqrt(2 * Math.Sqrt(Values.Length)), -1);
                double rb = Math.Pow(Math.Sqrt(2 * Values.Length), -1);
                double new_n = starge_parameters[j] * Math.Exp(ra * temp_random_for_N + rb * MRandom.NormalDistributionRandom());
                new_signature.Values[j] = Values[j] + MRandom.CauchyStandardRandom() * starge_parameters[j];
                new_signature.starge_parameters[j] = new_n;
            }
            return new_signature;
        }
        public virtual Signature CreateACloneWithGuassianMO()
        {
            Signature new_signature = new Signature(this.Values);
            double temp_random_for_N = MRandom.NormalDistributionRandom();
            for (int j = 0; j < Values.Length; j++)
            {
                double ra = Math.Pow(Math.Sqrt(2 * Math.Sqrt(Values.Length)), -1);
                double rb = Math.Pow(Math.Sqrt(2 * Values.Length), -1);
                double new_n = starge_parameters[j] * Math.Exp(ra * temp_random_for_N + rb * MRandom.NormalDistributionRandom());
                new_signature.Values[j] = Values[j] + MRandom.CauchyStandardRandom() * new_n;
                new_signature.starge_parameters[j] = new_n;
            }
            return new_signature;
        }
        public virtual Signature CopyTo(Signature des)
        {
            des.starge_parameters = new double[starge_parameters.Length];
            Array.Copy(starge_parameters, des.starge_parameters, starge_parameters.Length);
            des.values = new double[values.Length];
            Array.Copy(values, des.values, values.Length);
            return des;
        }
        public override string ToString()
        {
            string line = "";
            for (int i   = 0; i< Values.Length;i++)
            {
                line += Values[i].ToString(CultureInfo.InvariantCulture);
                if (i < Values.Length)
                    line += ",";
            }
            return line;
        }

    }
}
