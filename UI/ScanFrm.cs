using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VDS_New.Alg;
using VDS_New.DataLoader;
using VDS_New.Utilities;
using VDS_New.DataLearnerAdapter;
using System.Globalization;

namespace VDS_New.UI
{
    public partial class ScanFrm : Form
    {
        System.Diagnostics.Stopwatch stopwatch;
        int virus_count = 0;
        int benign_count = 0;
        bool is_scan_file = false;


        string modelPath;
        string min_max_path;
        string detector_path;
        string temp_stored_path = "Temp_Stored";
        string training_virus_path;
        string training_benign_path;
        VDS_New.Learner.Tester tester;
        List<double> min;
        List<double> max;
        AntiVirusAlgorithm algorithm;

        string model_name = "";

        BackgroundWorker worker;
        public ScanFrm()
        {
            InitializeComponent();
            Globals.LoadConfig();
            if (!Directory.Exists(temp_stored_path))
                Directory.CreateDirectory(temp_stored_path);
            cbModelType.SelectedIndex = 0;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stopwatch.Stop();
            var elapsedMs = (float) stopwatch.ElapsedMilliseconds/1000;
            lbSecond.Text = elapsedMs.ToString();
            lbVirusCount.Text = virus_count.ToString();
            lbBenignCount.Text = benign_count.ToString();
            MessageBox.Show("Done");
            
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressScanFile.Value = e.ProgressPercentage;
            }
            catch
            {
                progressScanFile.Value = 100;
            }
            ListViewItem lstItem = e.UserState as ListViewItem;
            listView1.Items.Add(lstItem);
            
            if (lstItem.SubItems[1].Text.ToString().CompareTo("Virus")==0) virus_count++; else benign_count++;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!is_scan_file)
            {
                DoScanAsyn((new DirectoryInfo(txtScanFolder.Text)).FullName);
            }
            else
            {
                int label = ScanAFile(txtScanFolder.Text);
                string[] lstItem = new string[] { txtScanFolder.Text, (label == 0) ? "Virus" : "Benign" };
                worker.ReportProgress(100, new ListViewItem(lstItem));
            }

        }

        private void DoScanAsyn(string scanfolder)
        {
            int current_count_file = 1;
            string[] scanFilePaths = Directory.GetFiles(scanfolder);
            for (int i = 0; i < scanFilePaths.Length; i++)
            {
                try
                {
                    string aScanFilePath = scanFilePaths[i];
                    int label = ScanAFile(aScanFilePath);
                    string[] lstItem = new string[] { aScanFilePath, (label == 0) ? "Virus" : "Benign" };
                    worker.ReportProgress((int)Math.Ceiling(((float)(current_count_file++) / scanFilePaths.Length) * 100), new ListViewItem(lstItem));
                    //listView1.Items.Add(new ListViewItem(lstItem));
                    //progressScanFile.Value += (int)Math.Ceiling(((float)1 / scanFilePaths.Length) * 100);
                }
                catch { }

            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                progressScanFile.Value = 0;
                listView1.Items.Clear();
                virus_count = 0;
                benign_count = 0;
                lbBenignCount.Text = "0";
                lbVirusCount.Text = "0";
                stopwatch = System.Diagnostics.Stopwatch.StartNew();
                lbSecond.Text = "0";
                worker.RunWorkerAsync();
            }
        }
        private int ScanAFile(string filePath)
        {
            //dumpbin file
            FilePresentation filePresentation = new FilePresentation();
            filePresentation.ExecuteCommandAFile(filePath, temp_stored_path);
            string[] filepats = Directory.GetFiles(temp_stored_path);
            //vector hoa
            double[] featureVector = filePresentation.VectorizeAFile(Directory.GetFiles(System.IO.Path.Combine(Environment.CurrentDirectory, temp_stored_path))[0]);
            //chuan hoa
            double[] basic_features = featureVector.Take(Globals.BASIC_FEATURES).ToArray();
            double[] dlls = new double[featureVector.Length - Globals.BASIC_FEATURES];
            for (int i = 0; i < dlls.Length; i++)
            {
                dlls[i] = featureVector[Globals.BASIC_FEATURES + i];
            }
            VDSElement e = new VDSElement(Globals.DETECTOR_RADIUS,
                new DLLSignature(basic_features, dlls));
            List<VDSElement> testing_data = new List<VDSElement>();
            testing_data.Add(e);
            //kiem tra

            int[] predictingLabels = algorithm.Learner.PredictLabel(algorithm.ConvertToDataLearnerForPredictLabel(testing_data));

            //Xoa cac file temp
            File.Delete(Directory.GetFiles(temp_stored_path)[0]);
            return predictingLabels[0];//1: virus, 0: benign --> 0 la virus, 1 benign
        }

        private void btnOpenScanFolder_Click(object sender, EventArgs e)
        {
            DialogResult userChoice = scanFolderDialog.ShowDialog();
            if (userChoice == DialogResult.OK)
            {
                is_scan_file = false;
                txtScanFolder.Text = scanFolderDialog.SelectedPath;
            }
        }

        private void btnOpenModelFolder_Click(object sender, EventArgs e)
        {
            DialogResult userChoice = modelFolderDialog.ShowDialog();
            if (userChoice == DialogResult.OK)
            {
                txtModelFolder.Text = modelFolderDialog.SelectedPath;
            }
        }

        private void DoLoadModel(string modelfolerpath, int model_type_index)
        {
            modelPath = Path.Combine(modelfolerpath, "Machine.mc");
            min_max_path = Path.Combine(modelfolerpath, "Min_Max.csv");
            detector_path = Path.Combine(modelfolerpath, "Detector.csv");
            training_virus_path = Path.Combine(modelfolerpath, "Normal_Virus_Training.csv");
            training_benign_path = Path.Combine(modelfolerpath, "Normal_Benign_Training.csv");
            string min_max_learning_path = Path.Combine(modelfolerpath, "Min_Max_Learning.csv");
            //load min_max
            IDataLoader dllDataLoader = new DLLDataLoader();
            min = new List<double>();
            max = new List<double>();
            dllDataLoader.LoadMinMaxData(min_max_path, min, max);

            //load detector
            List<VDSElement> detector_set = new List<VDSElement>();

            using (StreamReader sr = new StreamReader(detector_path))
            {
                string detector_str;
              
                while ((detector_str = sr.ReadLine()) != null)
                {

                    string[] values = detector_str.Split(',');
                    List<double> row_list = new List<double>();
                    double[] row_value;
                    for (int i = 0; i < values.Length; i++)
                    {
                        try
                        {
                           
                            row_list.Add(DoubleSupporter.parse_from_string(values[i]));
                        }
                        catch { }

                    }
                    row_value = row_list.ToArray();
                    double[] basic_features = row_value.Take(Globals.BASIC_FEATURES).ToArray();
                    double[] dlls = new double[row_value.Length - Globals.BASIC_FEATURES];
                    for (int j = 0; j < dlls.Length; j++)
                    {
                        dlls[j] = row_value[Globals.BASIC_FEATURES + j];
                    }
                    VDSElement detector = new VDSElement(Globals.DETECTOR_RADIUS,
                        new DLLSignature(basic_features, dlls));
                    detector.IsNormalize = true;
                    detector_set.Add(detector);

                }
            }


            Globals.MACHINE_PATH = modelPath;
            Globals.MIN_MAX_LEARNING_PATH = min_max_learning_path;

            switch (model_type_index)
            {
                case 0: tester = new Learner.Tester(new Learner.SVMBestLearner()); model_name = "SVM"; break;
                case 1: tester = new Learner.Tester(new Learner.NaiveBayesLearner()); model_name = "NaiveBayes"; break;
                case 2: tester = new Learner.Tester(new Learner.C45DecisionTreeLearner()); model_name = "DecisionTree"; break;
                default:
                    tester = new Learner.Tester(new Learner.SVMBestLearner());
                    model_name = "SVM";
                    break;
            }

            algorithm = new NormalizeAntiVirusAlgorithm(tester, new HammingDLLDistanceAverageConverter());
            tester.Load_Model();
            //algorithm = new NormalizeAntiVirusAlgorithm(tester, new DLLDistanceAverageConveter());
            algorithm.Detector_set = detector_set;
            algorithm.ListMin = min;
            algorithm.ListMax = max;
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            DoLoadModel(txtModelFolder.Text, cbModelType.SelectedIndex);
            MessageBox.Show("Loading model has completed!");
        }

        private void btnFileScanDialog_Click(object sender, EventArgs e)
        {
            if (openFileScanDialog.ShowDialog() == DialogResult.OK)
            {
                is_scan_file = true;
                txtScanFolder.Text = openFileScanDialog.FileName;
            }
        }

        private void linkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileScanDialog.ShowDialog() == DialogResult.OK)
            {
                is_scan_file = true;
                txtScanFolder.Text = openFileScanDialog.FileName;
            }
        }

        private void linkFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult userChoice = scanFolderDialog.ShowDialog();
            if (userChoice == DialogResult.OK)
            {
                is_scan_file = false;
                txtScanFolder.Text = scanFolderDialog.SelectedPath;
            }
        }

        private void linkModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult userChoice = modelFolderDialog.ShowDialog();
            if (userChoice == DialogResult.OK)
            {
                txtModelFolder.Text = modelFolderDialog.SelectedPath;
            }
        }
    }
}
