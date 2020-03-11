using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Statistics.Kernels;

namespace VDS_New.Learner
{
    class SVMLearner: ILearner
    {
        SupportVectorMachine machine;
        public double Learn(double[][] observations, int[] labels)
        {
            //var learn = new LinearDualCoordinateDescent()
            //{
            //    Loss = Loss.L2,
            //    Complexity = 1000,
            //    Tolerance = 1e-5
            //};
            SequentialMinimalOptimization learn = new SequentialMinimalOptimization()
            {
                UseComplexityHeuristic = true,
                UseKernelEstimation = false
            };
            machine = learn.Learn(observations, labels);
            bool[] output = machine.Decide(observations);
            int[] zeroOneAnswers = output.ToZeroOne();
         
            return 1-( new AccuracyLoss(labels).Loss(zeroOneAnswers));
        }

        public void Load_Model()
        {
            machine = Accord.IO.Serializer.Load<SupportVectorMachine>(VDS_New.Utilities.Globals.MACHINE_PATH.Insert(VDS_New.Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
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
            Accord.IO.Serializer.Save(machine,
                VDS_New.Utilities.Globals.MACHINE_PATH.Insert(VDS_New.Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
        }

        public override string ToString()
        {
            return "SVM";
        }
    }
}
