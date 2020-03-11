using Accord.Controls;
using Accord.Statistics.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.Utilities;

namespace VDS_New.Learner
{
    class Tester:ILearner
    {
        ILearner learner;
        ConfusionMatrix test_cm;

        double _training_accuracy;

        public Tester(ILearner eLearner)
        {
            learner = eLearner;
            _training_accuracy = 0;
        }

        public double Training_accuracy { get => _training_accuracy; }
        public ConfusionMatrix Cm { get => test_cm; }

   

        public double Learn(double[][] observations, int[] labels)
        {
            _training_accuracy = learner.Learn(observations, labels);
            return _training_accuracy;
        }

        public void Load_Model()
        {
            learner.Load_Model();
        }

        public double Predict(double[][] observations, int[] labels)
        {
            //_testing_accuracy = learner.Predict(observations, labels);
            int[] predicted = learner.PredictLabel(observations);


            //test_cm = new ConfusionMatrix(predicted, labels,Globals.BENIGN_CODE, Globals.VIRUS_CODE);
            test_cm = new ConfusionMatrix(predicted, labels, Globals.VIRUS_CODE, Globals.BENIGN_CODE);
            return test_cm.Accuracy;
        }

        public int[] PredictLabel(double[][] observations)
        {
            int[] predicted = learner.PredictLabel(observations);
            return predicted;
        }

        public void Save_Model()
        {
            learner.Save_Model();
        }

        public override string ToString()
        {
            return learner.ToString();
        }
    }
}
