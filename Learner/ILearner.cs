namespace VDS_New.Learner
{
    interface ILearner
    {

        double Learn(double[][] observations, int[] labels);
        double Predict(double[][] observations, int[] labels);
        int[] PredictLabel(double[][] observations);
        void Save_Model();
        void Load_Model();
    }
}
