using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VDS_New.Utilities;

namespace VDS_New.Alg
{
    class DLLSignature:Signature
    {
        double[] dlls;
        public DLLSignature(DLLSignature other):this(other.Values,other.Dlls)
        {

        }
        public DLLSignature(double[] values, double[] dlls):base(values)
        {
            this.dlls = new double[dlls.Length];
            for (int i = 0; i < dlls.Length; i++)
            {
                this.dlls[i] = dlls[i];
            }
        }

        public double[] Dlls { get => dlls; }

        public override Signature CreateAClone()
        {
            int mutation_count = (int)Math.Round(dlls.Length*Globals.DLLS_MUATION);
            double[] new_dlls = new double[dlls.Length];
            Array.Copy(dlls, new_dlls, dlls.Length);
            List<int> tempdlls = new List<int>();
            for (int i = 0; i < Dlls.Length; i++)
            {
                tempdlls.Add(i);
            }
            Random rd = new Random();
            for (int i = 0; i < mutation_count; i++)
            {
                int r = rd.Next(tempdlls.Count);
                new_dlls[tempdlls[r]] = (new_dlls[tempdlls[r]] == 0) ? 1 : 0;
                tempdlls.RemoveAt(r);
            }

            Signature new_signature = new DLLSignature(base.CreateAClone().Values, new_dlls);
            return new_signature;
        }

        public double GetHammingDistance(DLLSignature other)
        {
            double hammingdis = 0;
            for (int i = 0; i < Dlls.Length; i++)
            {
                hammingdis += (Dlls[i] == other.Dlls[i])?1:0;
            }
            return (double)hammingdis/dlls.Length;
        }
        public override double ComputeEuclideanDistance(Signature other)
        {
            return base.ComputeEuclideanDistance(other) + GetHammingDistance((DLLSignature)other);///Dlls.Length;
        }
        bool isAffScaleCalculated = false;
        double affScaleCalculate;
        public override double GetAffScale(List<VDSElement> detector_set)
        {

            if (!isAffScaleCalculated)
            {
                affScaleCalculate = 0;
                foreach (var item in detector_set)
                {
                    affScaleCalculate += GetHammingDistance((DLLSignature)item.GetSignature());// / Dlls.Length;
                }
                affScaleCalculate /= detector_set.Count;
                isAffScaleCalculated = true;
            }
            return affScaleCalculate;
        }
        public override Signature CopyTo(Signature des)
        {
            des = base.CopyTo(des);
            DLLSignature new_sign = new DLLSignature((DLLSignature)des);
            new_sign.dlls = new double[dlls.Length];
            Array.Copy(dlls, new_sign.dlls, dlls.Length);
            des = new_sign;
            return des;
        }
        public override string ToString()
        {
            string line = base.ToString()+",";
            for (int i = 0; i < dlls.Length; i++)
            {
                line += dlls[i].ToString();
                if(i<dlls.Length)
                    line+=",";
            }
            return line;
        }
    }
}
