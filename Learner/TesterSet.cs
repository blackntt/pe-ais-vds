using Accord.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Learner
{
    class TesterSet : ILearner
    {
        List<Tester> learners;
        public TesterSet()
        {
            learners = new List<Tester>();
        }
        public void AddLearner(Tester eLearner)
        {
            learners.Add(eLearner);
        }
        public List<Tester> GetLearners()
        {
            return learners;
        }
        public double Learn(double[][] observations, int[] labels)
        {
            
            foreach (var item in learners)
            {
                item.Learn(observations, labels);
            }
            return -1;
        }

        public void Load_Model()
        {
            foreach (var item in learners)
            {
                item.Load_Model();
            }
        }

        public double Predict(double[][] observations, int[] labels)
        {
            //ScatterplotBox.Show("AND", observations, labels);
            foreach (var item in learners)
            {
                double[][] samples = (double[][])observations.Clone();
                item.Predict(samples, labels);
            }
            return -1;
        }

        public int[] PredictLabel(double[][] observations)
        {
            foreach (var item in learners)
            {
                item.PredictLabel(observations);
            }
            return null;
        }

        public void Save_Model()
        {
            foreach (var item in learners)
            {
                item.Save_Model();
            }
        }
    }
}
