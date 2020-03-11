namespace VDS_New.UI
{
    partial class ScanFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanFrm));
            this.btnScan = new System.Windows.Forms.Button();
            this.adf = new System.Windows.Forms.Label();
            this.txtModelFolder = new System.Windows.Forms.TextBox();
            this.ScanFolder = new System.Windows.Forms.Label();
            this.txtScanFolder = new System.Windows.Forms.TextBox();
            this.scanFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.modelFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbBenignCount = new System.Windows.Forms.Label();
            this.lbVirusCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkFile = new System.Windows.Forms.LinkLabel();
            this.linkFolder = new System.Windows.Forms.LinkLabel();
            this.linkModel = new System.Windows.Forms.LinkLabel();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbModelType = new System.Windows.Forms.ComboBox();
            this.progressScanFile = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileScanDialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSecond = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Image = ((System.Drawing.Image)(resources.GetObject("btnScan.Image")));
            this.btnScan.Location = new System.Drawing.Point(279, 107);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(88, 31);
            this.btnScan.TabIndex = 20;
            this.btnScan.Text = "Scan";
            this.btnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // adf
            // 
            this.adf.AutoSize = true;
            this.adf.Location = new System.Drawing.Point(46, 30);
            this.adf.Name = "adf";
            this.adf.Size = new System.Drawing.Size(68, 13);
            this.adf.TabIndex = 19;
            this.adf.Text = "Model Folder";
            // 
            // txtModelFolder
            // 
            this.txtModelFolder.Location = new System.Drawing.Point(124, 27);
            this.txtModelFolder.Name = "txtModelFolder";
            this.txtModelFolder.Size = new System.Drawing.Size(267, 20);
            this.txtModelFolder.TabIndex = 17;
            // 
            // ScanFolder
            // 
            this.ScanFolder.AutoSize = true;
            this.ScanFolder.Location = new System.Drawing.Point(46, 66);
            this.ScanFolder.Name = "ScanFolder";
            this.ScanFolder.Size = new System.Drawing.Size(61, 13);
            this.ScanFolder.TabIndex = 23;
            this.ScanFolder.Text = "ScanFolder";
            // 
            // txtScanFolder
            // 
            this.txtScanFolder.Location = new System.Drawing.Point(124, 69);
            this.txtScanFolder.Name = "txtScanFolder";
            this.txtScanFolder.Size = new System.Drawing.Size(267, 20);
            this.txtScanFolder.TabIndex = 21;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 16);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(684, 251);
            this.listView1.TabIndex = 24;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 418;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Label";
            this.columnHeader2.Width = 139;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbSecond);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbBenignCount);
            this.groupBox1.Controls.Add(this.lbVirusCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.linkFile);
            this.groupBox1.Controls.Add(this.linkFolder);
            this.groupBox1.Controls.Add(this.linkModel);
            this.groupBox1.Controls.Add(this.btnLoadModel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbModelType);
            this.groupBox1.Controls.Add(this.progressScanFile);
            this.groupBox1.Controls.Add(this.txtScanFolder);
            this.groupBox1.Controls.Add(this.ScanFolder);
            this.groupBox1.Controls.Add(this.txtModelFolder);
            this.groupBox1.Controls.Add(this.adf);
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 171);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // lbBenignCount
            // 
            this.lbBenignCount.AutoSize = true;
            this.lbBenignCount.Location = new System.Drawing.Point(665, 131);
            this.lbBenignCount.Name = "lbBenignCount";
            this.lbBenignCount.Size = new System.Drawing.Size(13, 13);
            this.lbBenignCount.TabIndex = 36;
            this.lbBenignCount.Text = "0";
            // 
            // lbVirusCount
            // 
            this.lbVirusCount.AutoSize = true;
            this.lbVirusCount.Location = new System.Drawing.Point(555, 131);
            this.lbVirusCount.Name = "lbVirusCount";
            this.lbVirusCount.Size = new System.Drawing.Size(13, 13);
            this.lbVirusCount.TabIndex = 35;
            this.lbVirusCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Benign";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(519, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Virus";
            // 
            // linkFile
            // 
            this.linkFile.AutoSize = true;
            this.linkFile.Location = new System.Drawing.Point(439, 72);
            this.linkFile.Name = "linkFile";
            this.linkFile.Size = new System.Drawing.Size(23, 13);
            this.linkFile.TabIndex = 32;
            this.linkFile.TabStop = true;
            this.linkFile.Text = "File";
            this.linkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFile_LinkClicked);
            // 
            // linkFolder
            // 
            this.linkFolder.AutoSize = true;
            this.linkFolder.Location = new System.Drawing.Point(397, 72);
            this.linkFolder.Name = "linkFolder";
            this.linkFolder.Size = new System.Drawing.Size(36, 13);
            this.linkFolder.TabIndex = 31;
            this.linkFolder.TabStop = true;
            this.linkFolder.Text = "Folder";
            this.linkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFolder_LinkClicked);
            // 
            // linkModel
            // 
            this.linkModel.AutoSize = true;
            this.linkModel.Location = new System.Drawing.Point(397, 29);
            this.linkModel.Name = "linkModel";
            this.linkModel.Size = new System.Drawing.Size(36, 13);
            this.linkModel.TabIndex = 30;
            this.linkModel.TabStop = true;
            this.linkModel.Text = "Model";
            this.linkModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkModel_LinkClicked);
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadModel.Image")));
            this.btnLoadModel.Location = new System.Drawing.Point(186, 107);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(87, 31);
            this.btnLoadModel.TabIndex = 28;
            this.btnLoadModel.Text = "Load model";
            this.btnLoadModel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadModel.UseVisualStyleBackColor = true;
            this.btnLoadModel.Click += new System.EventHandler(this.btnLoadModel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(500, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Model";
            // 
            // cbModelType
            // 
            this.cbModelType.FormattingEnabled = true;
            this.cbModelType.Items.AddRange(new object[] {
            "SVM",
            "NaiveBayes",
            "DecisionTree"});
            this.cbModelType.Location = new System.Drawing.Point(557, 26);
            this.cbModelType.Name = "cbModelType";
            this.cbModelType.Size = new System.Drawing.Size(121, 21);
            this.cbModelType.TabIndex = 25;
            this.cbModelType.Text = "Choose a model";
            // 
            // progressScanFile
            // 
            this.progressScanFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressScanFile.Location = new System.Drawing.Point(3, 147);
            this.progressScanFile.Name = "progressScanFile";
            this.progressScanFile.Size = new System.Drawing.Size(684, 21);
            this.progressScanFile.TabIndex = 24;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(690, 270);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // openFileScanDialog
            // 
            this.openFileScanDialog.FileName = "openFileScanDialog";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "time (s):";
            // 
            // lbSecond
            // 
            this.lbSecond.AutoSize = true;
            this.lbSecond.Location = new System.Drawing.Point(56, 125);
            this.lbSecond.Name = "lbSecond";
            this.lbSecond.Size = new System.Drawing.Size(13, 13);
            this.lbSecond.TabIndex = 38;
            this.lbSecond.Text = "0";
            // 
            // ScanFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScanFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScanFrm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label adf;
        private System.Windows.Forms.TextBox txtModelFolder;
        private System.Windows.Forms.Label ScanFolder;
        private System.Windows.Forms.TextBox txtScanFolder;
        private System.Windows.Forms.FolderBrowserDialog scanFolderDialog;
        private System.Windows.Forms.FolderBrowserDialog modelFolderDialog;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressScanFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbModelType;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.OpenFileDialog openFileScanDialog;
        private System.Windows.Forms.LinkLabel linkFile;
        private System.Windows.Forms.LinkLabel linkFolder;
        private System.Windows.Forms.LinkLabel linkModel;
        private System.Windows.Forms.Label lbBenignCount;
        private System.Windows.Forms.Label lbVirusCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbSecond;
    }
}