using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS_New.Utilities;

namespace VDS_New.Learner
{
    class C45DecisionTreeLearner : ILearner
    {
        DecisionTree machine;
        public double Learn(double[][] observations, int[] labels)
        {
            int max = observations[0].Length;
            DecisionVariable[] a = new DecisionVariable[max];
            for (int i = 0; i < max; i++)
            {
                a[i] = DecisionVariable.Continuous(i.ToString());
            }
            C45Learning teacher = new C45Learning(a);

            // Use the learning algorithm to induce the tree
            machine = teacher.Learn(observations, labels);

            // Classify the samples using the model
            int[] predicted = machine.Decide(observations);
            double error = new AccuracyLoss(labels).Loss(predicted);

            return 1 - error;
        }

        public void Load_Model()
        {
            machine = Accord.IO.Serializer.Load<DecisionTree>(VDS_New.Utilities.Globals.MACHINE_PATH.Insert(VDS_New.Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "C45"));
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
            Accord.IO.Serializer.Save(machine, Globals.MACHINE_PATH.Insert(Globals.MACHINE_PATH.LastIndexOf('.'), "C45"));
        }

        public override string ToString()
        {
            return "C45 Decision Tree";
        }
    }
}
