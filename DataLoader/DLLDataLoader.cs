using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.Alg;
using VDS_New.Utilities;

namespace VDS_New.DataLoader
{
    class DLLDataLoader : MDataLoader
    {


        public override List<VDSElement> LoadData(string path, double radius)
        {
            List<VDSElement> matrix = new List<VDSElement>();
            using (var reader = new System.IO.StreamReader(path))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    double[] row_value = new double[values.Length - 2];


                    for (int i = 2; i < values.Length; i++)
                    {
                        try
                        {
                            
                            row_value[i - 2] = (double)float.Parse(values[i]);
                            //row_value[i - 2] = DoubleSupporter.parse_from_string(values[i]);
                        }
                        catch (FormatException fe)
                        {
                            int temp = Convert.ToInt32(values[i], 16);
                            row_value[i - 2] = temp;
                            Console.Write(fe.Message);
                        }
                    }
                    double[] basic_features = row_value.Take(Globals.BASIC_FEATURES).ToArray();
                    double[] dlls = new double[row_value.Length - Globals.BASIC_FEATURES];
                    for (int i = 0; i < dlls.Length; i++)
                    {
                        dlls[i] = row_value[Globals.BASIC_FEATURES + i];
                    }
                    VDSElement e = new VDSElement(radius, 
                        new DLLSignature(basic_features,dlls) );
                    matrix.Add(e);
                }
            }
            return matrix;
        }

    }
}
