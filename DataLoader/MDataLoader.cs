using System;
using System.Collections.Generic;
using VDS_New.Alg;
using VDS_New.Utilities;

namespace VDS_New.DataLoader
{
    class MDataLoader : IDataLoader
    {


        public virtual List<VDSElement> LoadData(string path, double radius)
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

                        }
                        catch (FormatException fe)
                        {
                            int temp = Convert.ToInt32(values[i], 16);
                            row_value[i - 2] = temp;
                            Console.Write(fe.Message);
                        }
                    }
                    VDSElement e = new VDSElement(radius, row_value);
                    matrix.Add(e);
                }
            }
            return matrix;
        }

        public virtual string[] LoadFeatureTitle(string path)
        {
            using (var reader = new System.IO.StreamReader(path))
            {

                string[] title;
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                title = new string[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    title[i] = values[i];
                }
                return title;

            }
        }

        public virtual void LoadMinMaxData(string path, List<double> min_list, List<double> max_list)
        {
            using (var reader = new System.IO.StreamReader(path))
            {
                min_list.AddRange( ReadArrayLine(reader));
                max_list.AddRange( ReadArrayLine(reader));
            }
        }
        
        private static List<double> ReadArrayLine(System.IO.StreamReader reader)
        {
            List<double> list = new List<double>();
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            foreach (var item in values)
            {
                list.Add(double.Parse(item));
                //list.Add(DoubleSupporter.parse_from_string(item));
            }
            return list;
        }
    }
}
