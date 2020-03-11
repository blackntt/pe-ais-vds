using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using VDS_New.Utilities;

namespace VDS_New.UI
{
    public partial class FilePresentationFrm : Form
    {
        BackgroundWorker worker;
        public FilePresentationFrm()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBar1.Value += e.ProgressPercentage;
            }
            catch
            {
                progressBar1.Value = 100;
            }
            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            this.Cursor = Cursors.Default;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //string[] trainingSetPath = Directory.GetDirectories(trainingFolderDialog.SelectedPath);
            //foreach (var aTrainingSetPath in trainingSetPath)
            //{
            //    FilePresentation filePresentation = new FilePresentation(aTrainingSetPath);
            //    filePresentation.PresentFiles();
            //}

            string aTrainingSetPath = txtTrainingFolder.Text;
            FilePresentation filePresentation = new FilePresentation(aTrainingSetPath);
            filePresentation.PresentFiles(worker.ReportProgress);
            
        }

        private void btnOpenTrainingFolder_Click(object sender, EventArgs e)
        {
            DialogResult userChoice = trainingFolderDialog.ShowDialog();
            if (userChoice == DialogResult.OK)
            {
                txtTrainingFolder.Text = trainingFolderDialog.SelectedPath;
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
                progressBar1.Value = 0;
                //Cursor.Current = Cursors.WaitCursor;
                this.Cursor = Cursors.WaitCursor;
            }
        }
    }
}
