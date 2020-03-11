﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using VDS_New.DataLearnerAdapter;
using VDS_New.Learner;
using VDS_New.Utilities;

namespace VDS_New.Alg
{
    class AntiVirusAlgorithm
    {
        IDataLearnerAdapter dataLearnerAdapter;
        ILearner learner;
        
        public List<VDSElement> Virus_matrix { get; set; }
        public List<VDSElement> Benign_matrix { get; set; }
        public List<VDSElement> Detector_set { get; set; }
        public List<double> ListMin { get; set; }
        public List<double> ListMax { get; set; }
        internal ILearner Learner { get => learner;}

        /// <summary>
        /// Constructor
        /// </summary>
        public AntiVirusAlgorithm(ILearner learner,IDataLearnerAdapter dataLearnerAdapter)
        {
            Detector_set = new List<VDSElement>();
            Virus_matrix = new List<VDSElement>();
            Benign_matrix = new List<VDSElement>();
            ListMin = new List<double>();
            ListMax = new List<double>();
            this.learner = learner;
            this.dataLearnerAdapter = dataLearnerAdapter;
            
        }

        /// <summary>
        /// NSA
        /// </summary>
        public void NSA()
        {
            foreach (var d1 in Virus_matrix)
            {
                VDSElement d = new VDSElement();
                d1.CopyTo(d);
                foreach (var s in Benign_matrix)
                {
                    double dis = d.GetDistance(s);
                    if (dis - Globals.SELF_RADIUS <= d.Radius)
                    {
                        d.Radius = dis - Globals.SELF_RADIUS;
                    }
                }
                if (d.Radius > Globals.SELF_RADIUS)
                {
                    Detector_set.Add(d);
                }
            }
        }

        /// <summary>
        /// Sort based on Affinity
        /// </summary>
        protected void SortDetectorSet()
        {
            for (int i = 0; i < Detector_set.Count - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < Detector_set.Count; j++)
                {
                    if (Detector_set[max].ComputeAffinity(Detector_set) < Detector_set[j].ComputeAffinity(Detector_set))
                    {
                        max = j;
                    }
                }
                VDSElement temp = Detector_set[i];
                Detector_set[i] = Detector_set[max];
                Detector_set[max] = temp;
            }
        }

        /// <summary>
        /// run CLONALG
        /// </summary>
        protected void CLONALG()
        {
            //Sort based on Affinity
            SortDetectorSet();


            int N = (int)Math.Round(Globals.CHOOSEN_COEFF * Detector_set.Count);
            List<VDSElement> candidates = new List<VDSElement>();
            candidates.AddRange(Detector_set.Take(N));
            List<VDSElement> new_detector_set = new List<VDSElement>();
            foreach (var item in candidates)
            {
                new_detector_set.AddRange(CloneFromADetector(item));
            }
            Detector_set.AddRange(new_detector_set);
        }

        /// <summary>
        /// Clone from a detector
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected List<VDSElement> CloneFromADetector(VDSElement item)
        {
            List<VDSElement> clones = new List<VDSElement>();
            int cloneCount = ComputeCloneCount(item);
            for (int i = 0; i < cloneCount; i++)
            {
                //clones.Add(CreateAClone(item));
                clones.Add(item.CreateAClone());
            }

            return clones;
        }




        /// <summary>
        /// Compute Quantity of Clones Generated by This VDSElement
        /// </summary>
        /// <returns></returns>
        protected int ComputeCloneCount(VDSElement item)
        {
            //return (int)Math.Round(Globals.CLONAL_COEFF * item.ComputeAffinity(Detector_set));
            return (int)Math.Round(Globals.CLONAL_COEFF);
        }

        /// <summary>
        /// Run steps of algorithm in the order
        /// </summary>
        public virtual double Run()
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
            SaveDetectorToFile();
            return Learn();
        }
        public virtual void SaveDetector_Min_Max()
        {
            SaveDetectorToFile();
        }
        protected void SaveDetectorToFile()
        {
            string[] lines = new string[Detector_set.Count];
            for (int i = 0; i < Detector_set.Count; i++)
            {
                lines[i] = Detector_set[i].ToString();
            }
            File.WriteAllLines(Globals.DETECTOR_PATH,lines);
        }

        /// <summary>
        /// Run learner to create model
        /// </summary>
        public double Learn()
        {
            double[][] preparedData;
            int[] labels;
            PrepareData(Virus_matrix,Benign_matrix, out preparedData, out labels);
            return learner.Learn(preparedData, labels);

        }

        

        /// <summary>
        /// Prepare Data for learning or predicting
        /// </summary>
        /// <param name="virus_set"></param>
        /// <param name="benign_set"></param>
        /// <param name="preparedData">Convert data to datastructre used for learner</param>
        /// <param name="labels">Output of correct labels of data</param>


        protected virtual void PrepareData(List<VDSElement> virus_set, List<VDSElement> benign_set,out double[][] preparedData, out int[] labels)
        {
            List<double[]> prepareDataList = new List<double[]>();
            List<int> labelList = new List<int>();
            prepareDataList.AddRange(ConvertToLearnerData(virus_set));
            prepareDataList.AddRange(ConvertToLearnerData(benign_set));

            labelList.AddRange(Enumerable.Repeat(Globals.VIRUS_CODE, virus_set.Count));
            labelList.AddRange(Enumerable.Repeat(Globals.BENIGN_CODE, benign_set.Count));

            ////////////////////////////////////////////////
           
            ///////////////////////////////////
            preparedData = prepareDataList.ToArray();
            labels = labelList.ToArray();
        }
        protected virtual void PrepareDataTest(List<VDSElement> virus_set, List<VDSElement> benign_set, out double[][] preparedData, out int[] labels)
        {
            List<double[]> prepareDataList = new List<double[]>();
            List<int> labelList = new List<int>();
            prepareDataList.AddRange(ConvertToLearnerData(virus_set));
            prepareDataList.AddRange(ConvertToLearnerData(benign_set));

            labelList.AddRange(Enumerable.Repeat(Globals.VIRUS_CODE, virus_set.Count));
            labelList.AddRange(Enumerable.Repeat(Globals.BENIGN_CODE, benign_set.Count));

            
            preparedData = prepareDataList.ToArray();
            labels = labelList.ToArray();
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="virustest_set"></param>
        /// <param name="benigntest_set"></param>
        public double Test(List<VDSElement> virustest_set, List<VDSElement> benigntest_set)
        {
            double[][] preparedData;
            int[] labels;
            PrepareDataTest(virustest_set, benigntest_set, out preparedData, out labels);
            return learner.Predict(preparedData, labels);
        }

        /// <summary>
        /// Convert VDSElement to datastructre for learner use
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected List<double[]> ConvertToLearnerData(List<VDSElement> data)
        {
            List<double[]> proceededData = new List<double[]>();
            NormalizeDataSet(data);
            for (int i = 0; i < data.Count; i++)
            {
                double[] obs = dataLearnerAdapter.ConvertToDataLearner(data[i], Detector_set);
                proceededData.Add(obs);
            }
            return proceededData;
        }

        /// <summary>
        /// Normalize Data
        /// </summary>
        /// <param name="data_set"></param>
        protected void NormalizeDataSet(List<VDSElement> data_set)
        {
            foreach (var item in data_set)
            {
                item.Normalize(ListMin, ListMax);
            }
        }

        public virtual double[][] ConvertToDataLearnerForPredictLabel(List<VDSElement> data)
        {
            List<double[]> convertedData = ConvertToLearnerData(data);
            return convertedData.ToArray();
        }
    }
}
