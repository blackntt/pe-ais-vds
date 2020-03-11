namespace VDS_New.UI
{
    partial class FilePresentationFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilePresentationFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenTrainingFolder = new System.Windows.Forms.Button();
            this.txtTrainingFolder = new System.Windows.Forms.TextBox();
            this.Run = new System.Windows.Forms.Button();
            this.trainingFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Training Folder";
            // 
            // btnOpenTrainingFolder
            // 
            this.btnOpenTrainingFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTrainingFolder.Image")));
            this.btnOpenTrainingFolder.Location = new System.Drawing.Point(364, 21);
            this.btnOpenTrainingFolder.Name = "btnOpenTrainingFolder";
            this.btnOpenTrainingFolder.Size = new System.Drawing.Size(86, 23);
            this.btnOpenTrainingFolder.TabIndex = 14;
            this.btnOpenTrainingFolder.Text = "Open folder";
            this.btnOpenTrainingFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenTrainingFolder.UseVisualStyleBackColor = true;
            this.btnOpenTrainingFolder.Click += new System.EventHandler(this.btnOpenTrainingFolder_Click);
            // 
            // txtTrainingFolder
            // 
            this.txtTrainingFolder.Location = new System.Drawing.Point(91, 21);
            this.txtTrainingFolder.Name = "txtTrainingFolder";
            this.txtTrainingFolder.Size = new System.Drawing.Size(267, 20);
            this.txtTrainingFolder.TabIndex = 13;
            // 
            // Run
            // 
            this.Run.Image = ((System.Drawing.Image)(resources.GetObject("Run.Image")));
            this.Run.Location = new System.Drawing.Point(186, 60);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 16;
            this.Run.Text = "Run";
            this.Run.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 104);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(478, 13);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 17;
            // 
            // FilePresentationFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 117);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenTrainingFolder);
            this.Controls.Add(this.txtTrainingFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilePresentationFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FilePresentationFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenTrainingFolder;
        private System.Windows.Forms.TextBox txtTrainingFolder;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.FolderBrowserDialog trainingFolderDialog;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}