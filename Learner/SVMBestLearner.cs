using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Kernels;
using Accord.IO;
using VDS_New.Utilities;
using Accord.Statistics;
using Accord.MachineLearning.Performance;
using Accord.MachineLearning;
using Accord.Controls;

namespace VDS_New.Learner
{
    class SVMBestLearner : ILearner
    {

        SupportVectorMachine machine;

        public double Learn(double[][] observations, int[] labels)
        {
           
            var gridsearch = GridSearch<double[], int>.Create(

                ranges: new
                {
                    Tolerance = GridSearch.Range(1e-10, 1.0, stepSize: 0.05)
                },

                learner: (p) => new LinearDualCoordinateDescent
                {
                    Complexity = 1e+10,
                    Tolerance = p.Tolerance
                },

                fit: (teacher, x, y, w) => teacher.Learn(x, y, w),

                loss: (actual, expected, m) => new ZeroOneLoss(expected).Loss(actual)
            );

            gridsearch.ParallelOptions.MaxDegreeOfParallelism = 2;

            var result = gridsearch.Learn(observations, labels);

            machine = result.BestModel;
            bool[] output = machine.Decide(observations);
            int[] zeroOneAnswers = output.ToZeroOne();

            double ratio = 1 - (new AccuracyLoss(labels).Loss(zeroOneAnswers));
            return ratio;
        }

        public void Load_Model()
        {
            machine = Serializer.Load<SupportVectorMachine>(Globals.MACHINE_PATH.Insert(Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
        }

        public double Predict(double[][] observations, int[] labels)
        {
            bool[] output = machine.Decide(observations);
            int[] zeroOneAnswers = output.ToZeroOne();

            return 1 - (new AccuracyLoss(labels).Loss(zeroOneAnswers));
        }

        public int[] PredictLabel(double[][] observations)
        {
            bool[] output = machine.Decide(observations);
            int[] predicted = output.ToZeroOne();
            return predicted;
        }

        public void Save_Model()
        {
            Serializer.Save(machine,
                Globals.MACHINE_PATH.Insert(Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
        }

        public override string ToString()
        {
            return "SVM";
        }
    }
}
