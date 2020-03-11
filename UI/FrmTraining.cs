using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VDS_New.Alg;
using VDS_New.DataLearnerAdapter;
using VDS_New.Learner;
using VDS_New.DataLoader;
using VDS_New.Utilities;
using System.IO;
using System.ComponentModel;
using Accord.Statistics.Kernels;
using System.Runtime.InteropServices;
using Accord.Statistics.Analysis;

namespace VDS_New
{
    public partial class FrmTraining : Form
    {

        Dictionary<string, double> acc_dict;
        Dictionary<string, double> sen_dict;
        Dictionary<string, List<Ext_CM>> cm_dict;


        AntiVirusAlgorithm algorithm;
        const string VIRUS_INPUT_PATH = "Normal_Virus_Training.csv";
        const string BENIGN_INPUT_PATH = "Normal_Benign_Training.csv";
        const string VIRUS_TEST_PATH = "Virus_Testing.csv";
        const string BENIGN_TEST_PATH = "Benign_Testing.csv";
        const string MIN_MAX_PATH = "Min_Max.csv";
        const string MACHINE_FILE = "Machine.mc";
        const string DETECTOR_PATH = "Detector.csv";
        const string MIN_MAX_LEARNING_PATH = @"Min_Max_Learning.csv";
        internal AntiVirusAlgorithm Algorithm { get => algorithm; set => algorithm = value; }

        TesterSet TesterSet;

        IDataLoader loader;


        BackgroundWorker worker;

        int time_loop;
        public FrmTraining()
        {
            InitializeComponent();
            loader = new DLLDataLoader();
            Globals.LoadConfig();
            TesterSet = new TesterSet();
            TesterSet.AddLearner(new Tester(new SVMBestLearner()));
            TesterSet.AddLearner(new Tester(new NaiveBayesLearner()));
            TesterSet.AddLearner(new Tester(new C45DecisionTreeLearner()));

            acc_dict = new Dictionary<string, double>();
            sen_dict = new Dictionary<string, double>();

            cm_dict = new Dictionary<string, List<Ext_CM>>();

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            List<object[]> list_user_state = e.UserState as List<object[]>;
            for (int i = 0; i < list_user_state.Count; i++)
            {
                string learner_name = list_user_state[i][0] as string;
                chartPerformance.Series[i].Name = learner_name;
            }
            for (int i = 0; i < list_user_state.Count; i++)
            {
                listViewModel.Items.Add(list_user_state[i][1] as ListViewItem);
                chartPerformance.Series[i].Points.AddXY(set_i, list_user_state[i][2]);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            MessageBox.Show("Done");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //DirectoryInfo[] folders = new DirectoryInfo(txtSourceFolder.Text).GetDirectories();
            //foreach (var item in folders)
            //{
            //    string sourcefolder = item.FullName;
            //    string testfolder = item.FullName;

            string sourcefolder = txtSourceFolder.Text;
            string testfolder = txtTestFolder.Text;

            int times_run = time_loop;
                for (int time = 0; time < times_run; time++)
                {
                    DoLearning(sourcefolder);
                    DoTesting(testfolder, sourcefolder);
                }
                ReportProgress(sourcefolder);
            //}
        }

        private void btnSourceBrowser_Click(object sender, EventArgs e)
        {
            if (sourceBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtSourceFolder.Text = sourceBrowserDialog.SelectedPath;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (testBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtTestFolder.Text = testBrowserDialog.SelectedPath;

            }
        }


        private void DoLearning(string sourcefolder)
        {
            Globals.MACHINE_PATH = sourcefolder + "\\" + MACHINE_FILE;
            Globals.DETECTOR_PATH = sourcefolder + "\\" + DETECTOR_PATH;
            Globals.MIN_MAX_LEARNING_PATH = sourcefolder + "\\" + MIN_MAX_LEARNING_PATH;
            Algorithm = new NormalizeAntiVirusAlgorithm(TesterSet, new HammingDLLDistanceAverageConverter());

            // Algorithm = new NormalizeAntiVirusAlgorithm(TesterSet, new DLLDistanceAverageConveter());
            Algorithm.Virus_matrix = loader.LoadData(sourcefolder + "\\" + VIRUS_INPUT_PATH, Globals.DETECTOR_RADIUS);
            Algorithm.Benign_matrix = loader.LoadData(sourcefolder + "\\" + BENIGN_INPUT_PATH, Globals.SELF_RADIUS);
            List<double> listMin = new List<double>();
            List<double> listMax = new List<double>();
            loader.LoadMinMaxData(sourcefolder + "\\" + MIN_MAX_PATH, listMin, listMax);
            Algorithm.ListMax = listMax;
            Algorithm.ListMin = listMin;
            double modelPrecision = Algorithm.Run();
        }

        private void DoTesting(string testfolder, string sourcefolder)
        {
            Globals.MACHINE_PATH = testfolder + "\\" + MACHINE_FILE;
            string path = testfolder;
            string virustestpath = path + "\\" + VIRUS_TEST_PATH;
            string benigntest = path + "\\" + BENIGN_TEST_PATH;
            Globals.MIN_MAX_LEARNING_PATH = sourcefolder + "\\" + MIN_MAX_LEARNING_PATH;
            List<VDSElement> virustest_set = loader.LoadData(virustestpath, Globals.DETECTOR_RADIUS);
            List<VDSElement> benigntest_set = loader.LoadData(benigntest, Globals.SELF_RADIUS);
            double detectionRating = Algorithm.Test(virustest_set, benigntest_set);

            double cur_acc_average = 0;
            double cur_sen_average = 0;
            foreach (var l in TesterSet.GetLearners())
            {
                cur_acc_average += l.Cm.Accuracy;
                cur_sen_average += l.Cm.Specificity;// l.Cm.Sensitivity;
            }

            cur_acc_average /= TesterSet.GetLearners().Count;
            cur_sen_average /= TesterSet.GetLearners().Count;

            double max_acc_average = acc_dict.ContainsKey(sourcefolder)?acc_dict[sourcefolder]:0;
            double max_sen_average = sen_dict.ContainsKey(sourcefolder) ? sen_dict[sourcefolder] : 0;

            if ((cur_acc_average > max_acc_average)||(cur_acc_average == max_acc_average && cur_sen_average > max_sen_average))
            {
                algorithm.SaveDetector_Min_Max();
                TesterSet.Save_Model();
                if (acc_dict.ContainsKey(sourcefolder))
                {
                    acc_dict[sourcefolder] = cur_acc_average;
                    sen_dict[sourcefolder] = cur_sen_average;
                    List<Ext_CM> list_cm = new List<Ext_CM>();
                    foreach (var t in TesterSet.GetLearners())
                    {
                        list_cm.Add(new Ext_CM(t.ToString(),t.Cm,t.Training_accuracy));
                    }
                    cm_dict[sourcefolder] = list_cm;
                }
                else
                {
                    acc_dict.Add(sourcefolder, cur_acc_average);
                    sen_dict.Add(sourcefolder, cur_sen_average);
                    List<Ext_CM> list_cm = new List<Ext_CM>();
                    foreach (var t in TesterSet.GetLearners())
                    {
                        list_cm.Add(new Ext_CM(t.ToString(),t.Cm, t.Training_accuracy));
                    }

                    cm_dict.Add(sourcefolder, list_cm);
                }
            }
        }

        private void ReportProgress(string sourcefolder)
        {
            List<object[]> user_state = new List<object[]>();
            foreach (var l in cm_dict[sourcefolder])
            {
                List<object> temp_userstate = new List<object>();
                temp_userstate.Add(l.Name);


                string dataset = sourcefolder.Split('\\')[sourcefolder.Split('\\').Length - 1];
                string learner_name = l.Name;
                string training_acc = round_and_tostring(l.Training_acc);// Math.Round(l.Training_acc*100, 2).ToString();
                //ListViewItem item = new ListViewItem(new string[] { dataset, learner_name,
                //                                                training_acc,  l.Cm.Accuracy.ToString(),
                //                                                l.Cm.ActualNegatives.ToString(), l.Cm.TrueNegatives.ToString(),
                //                                                 l.Cm.Specificity.ToString(), l.Cm.ActualPositives.ToString(),
                //                                            l.Cm.TruePositives.ToString(),l.Cm.Sensitivity.ToString()});

                ListViewItem item = new ListViewItem(new string[] { dataset, learner_name,training_acc, round_and_tostring( l.Cm.Accuracy)/* l.Cm.Accuracy.ToString()*/,
                                                                 l.Cm.ActualPositives.ToString(),l.Cm.TruePositives.ToString(),round_and_tostring(l.Cm.Sensitivity)/*l.Cm.Sensitivity.ToString()*/,
                                                                l.Cm.ActualNegatives.ToString(), l.Cm.TrueNegatives.ToString(),round_and_tostring(l.Cm.Specificity)/*l.Cm.Specificity.ToString()*/});
                temp_userstate.Add(item);
                temp_userstate.Add(Math.Round(l.Cm.Accuracy, 4));
                user_state.Add(temp_userstate.ToArray());
            }
            worker.ReportProgress(0, user_state);
        }
        string round_and_tostring(double value)
        {
            return Math.Round(value * 100, 2).ToString();
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                time_loop = Properties.Settings.Default.TIME_LOOP;
                worker.RunWorkerAsync();
                this.Cursor = Cursors.WaitCursor;
            }
        }
        string set_i = "";


        class Ext_CM
        {
            string name;
            ConfusionMatrix cm;
            double training_acc;

            public string Name { get => name; set => name = value; }
            public ConfusionMatrix Cm { get => cm; set => cm = value; }
            public double Training_acc { get => training_acc; set => training_acc = value; }

            public Ext_CM() { }
            public Ext_CM(string n, ConfusionMatrix c, double t_a)
            {
                name = n;
                cm = c;
                training_acc = t_a;
            }
            public override string ToString()
            {
                return name;
            }
        }

        string[] make_table()
        {
           
            List<string> total_values = new List<string>();
            int col_acc = 3;
            int col_sen = 6;
            int col_spec = 9;

            int index = 0;
            string titles1 = "Accuracy,SVM, Naive Bayes, Decision Tree";
            string line1 = "Set 1," + listViewModel.Items[index++].SubItems[col_acc].Text +","+ listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text;
            string line2 = "Set 2," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text;
            string line3 = "Set 3," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text;
            string line4 = "Set 4," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text;
            string line5 = "Set 5," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text + "," + listViewModel.Items[index++].SubItems[col_acc].Text;

            index = 0;

            string titles2 = "Sensitivity,SVM, Naive Bayes, Decision Tree";
            string line6 = "Set 1," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text;
            string line7 = "Set 2," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text;
            string line8 = "Set 3," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text;
            string line9 = "Set 4," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text;
            string line10 = "Set 5," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text + "," + listViewModel.Items[index++].SubItems[col_sen].Text;

            index = 0;
            string titles3 = "Specitivity,SVM, Naive Bayes, Decision Tree";
            string line11 = "Set 1," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text;
            string line12 = "Set 2," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text;
            string line13 = "Set 3," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text;
            string line14 = "Set 4," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text;
            string line15 = "Set 5," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text + "," + listViewModel.Items[index++].SubItems[col_spec].Text;

            total_values.Add(titles1);
            total_values.Add(line1);
            total_values.Add(line2);
            total_values.Add(line3);
            total_values.Add(line4);
            total_values.Add(line5);
            total_values.Add(titles2);
            total_values.Add(line6);
            total_values.Add(line7);
            total_values.Add(line8);
            total_values.Add(line9);
            total_values.Add(line10);
            total_values.Add(titles3);
            total_values.Add(line11);
            total_values.Add(line12);
            total_values.Add(line13);
            total_values.Add(line14);
            total_values.Add(line15);


            return total_values.ToArray();
        }
    }
}
