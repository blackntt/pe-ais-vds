using System;
using System.Collections.Generic;
using VDS_New.Utilities;

namespace VDS_New.Alg
{
    class VDSElement
    {
        static int COUNT = 0;
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public VDSElement()
        {
            Id = VDSElement.COUNT;
            VDSElement.COUNT++;
        }

        public VDSElement(double radius, double[] row_value)
        {
            Id = VDSElement.COUNT;
            VDSElement.COUNT++;
            Radius = radius;
            Features = row_value;
        }
        public VDSElement(double radius, Signature signature)
        {
            Id = VDSElement.COUNT;
            VDSElement.COUNT++;
            Radius = radius;
            this.features = signature;
        }
        private Signature features;
        public double[] Features
        {
            get { return features.Values; }
            set
            {
                features = new Signature(value);
            }
        }
        public Signature GetSignature()
        {
            return features;
        }
        private double radius;
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public bool IsNormalize
        {
            get { return features.IsNormlized; }
            set { features.IsNormlized = value; }
        }

        public double GetDistance(VDSElement other)
        {
            return features.ComputeEuclideanDistance(other.features);
        }

        bool isComputeAffinity=false;
        public bool IsComputeAffinity { get => isComputeAffinity; set => isComputeAffinity = value; }
        double aff;
        public double ComputeAffinity(List<VDSElement> detector_set)
        {
            // tong hop tinh ket qua tu VOL and OLP
            if (!isComputeAffinity)
            {
                aff = ComputeNonSelfCoverage() - Globals.AFF_SCALE * ComputeOverlapPunishment(detector_set);
                aff += features.GetAffScale(detector_set);
                IsComputeAffinity = true;
            }
            return aff;
        }

        /// <summary>
        /// Compute Overlap Punishment between Dectetor -- OLP
        /// </summary>
        /// <param name="detector_set"></param>
        /// <returns></returns>
        private double ComputeOverlapPunishment(List<VDSElement> detector_set)
        {
            double olp = 0;
            foreach (var other in detector_set)
            {
                if (Id != other.Id)
                {
                    olp = ComputeOLP2Dectectors(olp, other);
                }
            }

            return olp;
        }

        /// <summary>
        /// Compute Overlap Punishment between 2 Detectors
        /// </summary>
        /// <param name="olp"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        private double ComputeOLP2Dectectors(double olp, VDSElement other)
        {
            double olp_d = 0;
            if (this.GetDistance(other) >= Radius + other.Radius)
            {
                olp_d = 0;
            }
            else
            {
                olp_d = Math.Pow(Math.Exp((Radius + other.Radius - GetDistance(other)) / (Radius + other.Radius)) - 1, Features.Length);
            }
            olp += olp_d;
            return olp;
        }

        /// <summary>
        /// Compute Non-self Space Coverage --- VOL
        /// </summary>
        /// <returns></returns>
        private double ComputeNonSelfCoverage()
        {
            double vol = 0;
            if (this.Features.Length % 2 == 0)
            {
                FactorialPoorMans f = new FactorialPoorMans();
                vol = Math.Pow(Math.PI, this.Features.Length / 2) * Math.Pow(this.Radius, this.Features.Length);
                vol /= long.Parse(f.Factorial(this.Features.Length / 2));
            }
            else
            {
                vol = Math.Pow(Math.PI, this.Features.Length / 2) * Math.Pow(this.Radius, this.Features.Length);
                double temp = 1;
                for (int i = 1; i <= 2 * ((this.Features.Length + 1) / 2) - 1; i += 2)
                {
                    temp *= i;
                }
                temp /= 2 * ((this.Features.Length + 1) / 2);
                temp *= Math.Sqrt(Math.PI);
                vol /= temp;
            }

            return vol;
        }

        public void Normalize(List<double> listMin, List<double> listMax)
        {
                features.Normalize(listMin.ToArray(),listMax.ToArray());
        }
        public double[] MultipleCoff(double[] coffs)
        {
            return features.Multiple(coffs);
        }

        /// <summary>
        /// Create A New Clone From The VDSElement
        /// </summary>
        /// <returns></returns>
        public VDSElement CreateAClone()
        {
            
            return new VDSElement(radius,features.CreateAClone());
        }

        public VDSElement CopyTo(VDSElement des)
        {
            if (des.features == null)
                if (features is DLLSignature)
                {
                    des.features = new DLLSignature((DLLSignature)features);
                }
                else {
                    des.features = new Signature(features.Values);
                }
            des.radius = radius;
            des.features = features.CopyTo(des.features);
            return des;
        }
        public override string ToString()
        {
            return features.ToString();
        }
    }
}
