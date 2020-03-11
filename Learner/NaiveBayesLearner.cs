using Accord.MachineLearning.Bayes;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Learner
{
    class NaiveBayesLearner : ILearner
    {
        NaiveBayes<CauchyDistribution> machine;
        public double Learn(double[][] observations, int[] labels)
        {


            var teacher = new NaiveBayesLearning<CauchyDistribution>();
            
            //teacher.Options.InnerOption = new NormalOptions()
            //{
            //    Regularization = 1e-5
            //};


            // Use the learning algorithm to learn
            machine = teacher.Learn(observations, labels);

            // Classify the samples using the model
            int[] predicted = machine.Decide(observations);
            
            double error = new AccuracyLoss(labels).Loss(predicted);


            return 1 - error;
        }

        public void Load_Model()
        {
            machine = Accord.IO.Serializer.Load<NaiveBayes<CauchyDistribution>>(Utilities.Globals.MACHINE_PATH.Insert(Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "NB"));
        }

        public double Predict(double[][] observations, int[] labels)
        {
            int[] predicted = machine.Decide(observations);

            double error = new AccuracyLoss(labels).Loss(predicted);
            return 1 - error;
        }

        public int[] PredictLabel(double[][] observations)
        {

            int[] predicted = machine.Decide(observations);

            return predicted;
        }

        public void Save_Model()
        {
            Accord.IO.Serializer.Save(machine, Utilities.Globals.MACHINE_PATH.Insert(Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "NB"));
        }

        public override string ToString()
        {
            return "Naive Bayes";
        }
    }
}
