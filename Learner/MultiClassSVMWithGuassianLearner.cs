using Accord.Controls;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Learner
{
    class MultiClassSVMWithGuassianLearner : ILearner
    {
        
        MulticlassSupportVectorMachine<Gaussian> machine;
        public double Learn(double[][] observations, int[] labels)
        {
            var teacher = new MulticlassSupportVectorLearning<Gaussian>()
            {
                Learner = (param) => new SequentialMinimalOptimization<Gaussian>()
                {
                    UseKernelEstimation = true,
                }
            };
            machine = teacher.Learn(observations, labels);

            int[] predicted = machine.Decide(observations);

            double error = new AccuracyLoss(labels).Loss(predicted);

            return 1 - error;
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
            Accord.IO.Serializer.Save(machine,
               Utilities.Globals.MACHINE_PATH.Insert(Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
        }
        public void Load_Model()
        {
            machine = Accord.IO.Serializer.Load<MulticlassSupportVectorMachine<Gaussian>>(Utilities.Globals.MACHINE_PATH.Insert(Utilities.Globals.MACHINE_PATH.LastIndexOf('.'), "SVM"));
        }
        public override string ToString()
        {
            return "SVM";
        }
    }
}
