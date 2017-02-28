using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using EastIPReportGenerator.ReportForm;

namespace EastIPReportGenerator
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        public FrmMain()
        {
            InitializeComponent();
            AddReport(new XUCInternalCase());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            xnfReport.SelectedPageIndex = 0;
        }

        private void AddReport(UserControl control)
        {
            var page = new NavigationPage();
            page.Caption = control.Text;
            page.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            xnfReport.Pages.Add(page);
        }
    }
}
