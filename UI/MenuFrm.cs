using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VDS_New.UI
{
    public partial class MenuFrm : Form
    {
        public MenuFrm()
        {
            InitializeComponent();
        }

        private void btnExtractFeatures_Click(object sender, EventArgs e)
        {
            Form dumpbinOnFile = new FilePresentationFrm();
            dumpbinOnFile.ShowDialog();
        }

        private void btnGenerateDectectors_Click(object sender, EventArgs e)
        {
            Form generateDetectorFrm = new FrmTraining();
            generateDetectorFrm.ShowDialog();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Form scan_form = new ScanFrm();
            scan_form.ShowDialog();
        }
    }
}
