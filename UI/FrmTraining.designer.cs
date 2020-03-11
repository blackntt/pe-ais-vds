namespace VDS_New
{
    partial class FrmTraining
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTraining));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSourceBrowser = new System.Windows.Forms.Button();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.sourceBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtTestFolder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.testBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewModel = new System.Windows.Forms.ListView();
            this.columnDataSet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLearner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTrainingAccuracy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTestingAccuracy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnActualVirus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTrueVirus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRun = new System.Windows.Forms.Button();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.allBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Folder";
            // 
            // btnSourceBrowser
            // 
            this.btnSourceBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnSourceBrowser.Image")));
            this.btnSourceBrowser.Location = new System.Drawing.Point(416, 14);
            this.btnSourceBrowser.Name = "btnSourceBrowser";
            this.btnSourceBrowser.Size = new System.Drawing.Size(86, 23);
            this.btnSourceBrowser.TabIndex = 1;
            this.btnSourceBrowser.Text = "Browser";
            this.btnSourceBrowser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSourceBrowser.UseVisualStyleBackColor = true;
            this.btnSourceBrowser.Click += new System.EventHandler(this.btnSourceBrowser_Click);
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Location = new System.Drawing.Point(117, 17);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(293, 20);
            this.txtSourceFolder.TabIndex = 2;
            // 
            // txtTestFolder
            // 
            this.txtTestFolder.Location = new System.Drawing.Point(117, 52);
            this.txtTestFolder.Name = "txtTestFolder";
            this.txtTestFolder.Size = new System.Drawing.Size(293, 20);
            this.txtTestFolder.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(416, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browser";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Testing Folder";
            // 
            // listViewModel
            // 
            this.listViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDataSet,
            this.columnLearner,
            this.columnTrainingAccuracy,
            this.columnTestingAccuracy,
            this.columnActualVirus,
            this.columnTrueVirus,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
            this.listViewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewModel.FullRowSelect = true;
            this.listViewModel.Location = new System.Drawing.Point(3, 16);
            this.listViewModel.Name = "listViewModel";
            this.listViewModel.Size = new System.Drawing.Size(717, 423);
            this.listViewModel.TabIndex = 12;
            this.listViewModel.UseCompatibleStateImageBehavior = false;
            this.listViewModel.View = System.Windows.Forms.View.Details;
            // 
            // columnDataSet
            // 
            this.columnDataSet.Text = "DataSet";
            this.columnDataSet.Width = 100;
            // 
            // columnLearner
            // 
            this.columnLearner.Text = "Learner";
            this.columnLearner.Width = 100;
            // 
            // columnTrainingAccuracy
            // 
            this.columnTrainingAccuracy.Text = "Training Accuracy";
            this.columnTrainingAccuracy.Width = 100;
            // 
            // columnTestingAccuracy
            // 
            this.columnTestingAccuracy.Text = "Testing Accuracy";
            this.columnTestingAccuracy.Width = 100;
            // 
            // columnActualVirus
            // 
            this.columnActualVirus.Text = "Input Virus (Negative)";
            // 
            // columnTrueVirus
            // 
            this.columnTrueVirus.Text = "Detected Virus";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Virus Detected Rate";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Input Benign";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Detected Benign";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Benign Detected Rate";
            // 
            // btnRun
            // 
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.Location = new System.Drawing.Point(203, 88);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(88, 29);
            this.btnRun.TabIndex = 13;
            this.btnRun.Text = "Run";
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // chartPerformance
            // 
            this.chartPerformance.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea1);
            this.chartPerformance.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chartPerformance.Legends.Add(legend1);
            this.chartPerformance.Location = new System.Drawing.Point(0, 123);
            this.chartPerformance.Name = "chartPerformance";
            this.chartPerformance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            this.chartPerformance.Series.Add(series1);
            this.chartPerformance.Series.Add(series2);
            this.chartPerformance.Series.Add(series3);
            this.chartPerformance.Size = new System.Drawing.Size(594, 318);
            this.chartPerformance.TabIndex = 16;
            this.chartPerformance.Text = "Chart";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRun);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSourceBrowser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSourceFolder);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtTestFolder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 123);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewModel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 442);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Performance";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartPerformance);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1321, 442);
            this.splitContainer1.SplitterDistance = 594;
            this.splitContainer1.TabIndex = 17;
            // 
            // FrmTraining
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 442);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTraining";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Form";
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSourceBrowser;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.FolderBrowserDialog sourceBrowserDialog;
        private System.Windows.Forms.TextBox txtTestFolder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog testBrowserDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewModel;
        private System.Windows.Forms.ColumnHeader columnDataSet;
        private System.Windows.Forms.ColumnHeader columnLearner;
        private System.Windows.Forms.ColumnHeader columnTrainingAccuracy;
        private System.Windows.Forms.ColumnHeader columnTestingAccuracy;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.FolderBrowserDialog allBrowserDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader columnActualVirus;
        private System.Windows.Forms.ColumnHeader columnTrueVirus;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

