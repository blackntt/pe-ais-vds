using Accord.Controls;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Kernels;
using Accord.IO;
using VDS_New.Utilities;
using Accord.Statistics;

namespace VDS_New.Learner
{
    class SVMWithPoLyLearner: ILearner
    {
        
        SupportVectorMachine<NormalizedPolynomial> machine;
        public double Learn(double[][] observations, int[] labels)
        {
            var learn = new SequentialMinimalOptimization<NormalizedPolynomial>()
            {
                UseKernelEstimation = true,
                Kernel = new NormalizedPolynomial(1)
            };
            
            machine = learn.Learn(observations, labels);
            bool[] output = machine.Decide(observations);
            int[] zeroOneAnswers = output.ToZeroOne();
            
            double ratio =  1 - (new AccuracyLoss(labels).Loss(zeroOneAnswers));
            return ratio;
        }

        public void Load_Model()
        {
            machine = Serializer.Load<SupportVectorMachine<NormalizedPolynomial>>(Globals.MACHINE_PATH.Insert(Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
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
