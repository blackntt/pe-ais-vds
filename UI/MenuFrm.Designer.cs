namespace VDS_New.UI
{
    partial class MenuFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuFrm));
            this.btnExtractFeatures = new System.Windows.Forms.Button();
            this.btnGenerateDectectors = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExtractFeatures
            // 
            this.btnExtractFeatures.Image = ((System.Drawing.Image)(resources.GetObject("btnExtractFeatures.Image")));
            this.btnExtractFeatures.Location = new System.Drawing.Point(31, 39);
            this.btnExtractFeatures.Name = "btnExtractFeatures";
            this.btnExtractFeatures.Size = new System.Drawing.Size(153, 111);
            this.btnExtractFeatures.TabIndex = 0;
            this.btnExtractFeatures.Text = "ExtractFeature";
            this.btnExtractFeatures.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExtractFeatures.UseVisualStyleBackColor = true;
            this.btnExtractFeatures.Click += new System.EventHandler(this.btnExtractFeatures_Click);
            // 
            // btnGenerateDectectors
            // 
            this.btnGenerateDectectors.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateDectectors.Image")));
            this.btnGenerateDectectors.Location = new System.Drawing.Point(190, 39);
            this.btnGenerateDectectors.Name = "btnGenerateDectectors";
            this.btnGenerateDectectors.Size = new System.Drawing.Size(160, 111);
            this.btnGenerateDectectors.TabIndex = 1;
            this.btnGenerateDectectors.Text = "Generate Dectector";
            this.btnGenerateDectectors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerateDectectors.UseVisualStyleBackColor = true;
            this.btnGenerateDectectors.Click += new System.EventHandler(this.btnGenerateDectectors_Click);
            // 
            // btnScan
            // 
            this.btnScan.Image = ((System.Drawing.Image)(resources.GetObject("btnScan.Image")));
            this.btnScan.Location = new System.Drawing.Point(356, 39);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(160, 111);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "Scan";
            this.btnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // MenuFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 203);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnGenerateDectectors);
            this.Controls.Add(this.btnExtractFeatures);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExtractFeatures;
        private System.Windows.Forms.Button btnGenerateDectectors;
        private System.Windows.Forms.Button btnScan;
    }
}