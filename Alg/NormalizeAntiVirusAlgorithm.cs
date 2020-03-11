using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.DataLearnerAdapter;
using VDS_New.Learner;
using VDS_New.Utilities;

namespace VDS_New.Alg
{
    class NormalizeAntiVirusAlgorithm : AntiVirusAlgorithm
    {
        public NormalizeAntiVirusAlgorithm(ILearner learner, IDataLearnerAdapter dataLearnerAdapter) : base(learner, dataLearnerAdapter)
        {
        }

        public override void SaveDetector_Min_Max()
        {
            SaveDetectorToFile();
            SaveMinMaxListForPredict();
        }
        /// <summary>
        /// Run steps of algorithm in the order
        /// </summary>
        public override double Run()
        {
            foreach (var item in Virus_matrix)
            {
                item.IsNormalize = true;
            }
            foreach (var item in Benign_matrix)
            {
                item.IsNormalize = true;
            }
            NSA();
            CLONALG();
            //SaveDetectorToFile();
            return Learn();
        }
        double[] temp_min_learning=null;
        double[] temp_max_learning=null;
        protected override void PrepareData(List<VDSElement> virus_set, List<VDSElement> benign_set, out double[][] preparedData, out int[] labels)
        {
            List<double[]> prepareDataList = new List<double[]>();
            List<int> labelList = new List<int>();
            prepareDataList.AddRange(ConvertToLearnerData(virus_set));
            prepareDataList.AddRange(ConvertToLearnerData(benign_set));

            labelList.AddRange(Enumerable.Repeat(Globals.VIRUS_CODE, virus_set.Count));
            labelList.AddRange(Enumerable.Repeat(Globals.BENIGN_CODE, benign_set.Count));

            ////////////////////////////////////////////////
            temp_min_learning = new double[prepareDataList[0].Length];
            temp_max_learning = new double[prepareDataList[0].Length];

            for (int i = 0; i < prepareDataList[0].Length; i++)
            {
                temp_min_learning[i] = double.MaxValue;
                temp_max_learning[i] = double.MinValue;
            }

            for (int i = 0; i < prepareDataList.Count; i++)
            {
                for (int j = 0; j < prepareDataList[i].Length; j++)
                {
                    if (temp_min_learning[j] > prepareDataList[i][j])
                        temp_min_learning[j] = prepareDataList[i][j];
                    if (temp_max_learning[j] < prepareDataList[i][j])
                        temp_max_learning[j] = prepareDataList[i][j];
                }

            }
            for (int i = 0; i < prepareDataList.Count; i++)
            {
                for (int j = 0; j < prepareDataList[i].Length; j++)
                {
                    if (temp_min_learning[j] == temp_max_learning[j])
                        prepareDataList[i][j] = 0.5;
                    else
                        prepareDataList[i][j] = (prepareDataList[i][j] - temp_min_learning[j]) / (temp_max_learning[j] - temp_min_learning[j]);
                }

            }

            ////
           // SaveMinMaxListForPredict();
            ///////////////////////////////////
            preparedData = prepareDataList.ToArray();
            labels = labelList.ToArray();
        }
        private void SaveMinMaxListForPredict()
        {
            string path =Globals.MIN_MAX_LEARNING_PATH;
            string[] values = new string[2];
            for (int i = 0; i <temp_min_learning.Length; i++)
            {
                if (i < temp_min_learning.Length - 1)
                {
                    values[0] += temp_min_learning[i]+",";
                    values[1] += temp_max_learning[i] + ",";
                }
                else
                {
                    values[0] += temp_min_learning[i];
                    values[1] += temp_max_learning[i];
                }
            }

            File.WriteAllLines(path,values);

        }
        private void LoadMinMaxLearningPath()
        {
            string[] values = File.ReadAllLines(Globals.MIN_MAX_LEARNING_PATH);
            temp_min_learning = new double[values[0].Length];
            temp_max_learning = new double[temp_min_learning.Length];

            string[] sub_values = values[0].Split(',');
            for (int i = 0; i < sub_values.Length; i++)
            {
                double cur_val = Double.Parse(sub_values[i], CultureInfo.InvariantCulture);
                temp_min_learning[i] = cur_val;
            }
            sub_values = values[1].Split(',');
            for (int i = 0; i < sub_values.Length; i++)
            {
                double cur_val = Double.Parse(sub_values[i], CultureInfo.InvariantCulture);
                temp_max_learning[i] = cur_val;
            }
        }
        protected override void PrepareDataTest(List<VDSElement> virus_set, List<VDSElement> benign_set, out double[][] preparedData, out int[] labels)
        {
            List<double[]> prepareDataList = new List<double[]>();
            List<int> labelList = new List<int>();
            prepareDataList.AddRange(ConvertToLearnerData(virus_set));
            prepareDataList.AddRange(ConvertToLearnerData(benign_set));

            labelList.AddRange(Enumerable.Repeat(Globals.VIRUS_CODE, virus_set.Count));
            labelList.AddRange(Enumerable.Repeat(Globals.BENIGN_CODE, benign_set.Count));

            ////////////////////////////////////////////////
            for (int i = 0; i < prepareDataList.Count; i++)
            {
                for (int j = 0; j < prepareDataList[i].Length; j++)
                {
                    if (temp_min_learning[j] == temp_max_learning[j])
                        prepareDataList[i][j] = 0.5;
                    else
                        prepareDataList[i][j] = (prepareDataList[i][j] - temp_min_learning[j]) / (temp_max_learning[j] - temp_min_learning[j]);
                }

            }
            ///////////////////////////////////
            preparedData = prepareDataList.ToArray();
            labels = labelList.ToArray();
        }
        public override double[][] ConvertToDataLearnerForPredictLabel(List<VDSElement> data)
        {
            LoadMinMaxLearningPath();
            List<double[]> prepareDataList = new List<double[]>();
            prepareDataList.AddRange(ConvertToLearnerData(data));
            for (int i = 0; i < prepareDataList.Count; i++)
            {
                for (int j = 0; j < prepareDataList[i].Length; j++)
                {
                    if (temp_min_learning[j] == temp_max_learning[j])
                        prepareDataList[i][j] = 0.5;
                    else
                        prepareDataList[i][j] = (prepareDataList[i][j] - temp_min_learning[j]) / (temp_max_learning[j] - temp_min_learning[j]);
                }

            }
            return prepareDataList.ToArray();
        }
    }
}
