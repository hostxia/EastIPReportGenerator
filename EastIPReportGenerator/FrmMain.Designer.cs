namespace EastIPReportGenerator
{
    partial class FrmMain
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
            this.xnfReport = new DevExpress.XtraBars.Navigation.NavigationFrame();
            ((System.ComponentModel.ISupportInitialize)(this.xnfReport)).BeginInit();
            this.SuspendLayout();
            // 
            // xnfReport
            // 
            this.xnfReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xnfReport.Location = new System.Drawing.Point(0, 0);
            this.xnfReport.Name = "xnfReport";
            this.xnfReport.SelectedPage = null;
            this.xnfReport.Size = new System.Drawing.Size(713, 494);
            this.xnfReport.TabIndex = 0;
            this.xnfReport.Text = "案件统计";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 494);
            this.Controls.Add(this.xnfReport);
            this.Name = "FrmMain";
            this.Text = "案件统计";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xnfReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.NavigationFrame xnfReport;
    }
}

