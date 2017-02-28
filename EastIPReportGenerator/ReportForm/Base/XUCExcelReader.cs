using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;

namespace EastIPReportGenerator.ReportForm.Base
{
    public partial class XUCExcelReader : XtraUserControl
    {
        private readonly List<string> _listSheetsName;

        public XUCExcelReader()
        {
            InitializeComponent();
            _listSheetsName = new List<string>();
            ExcelSource = new DataTable();
        }

        public DataTable ExcelSource { get; private set; }

        private void xbeFile_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowDefaultWaitForm();
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                xbeFile.Text = openFileDialog.FileName;
                _listSheetsName.LoadSheetNames(xbeFile.Text);
                xlueSheet.Properties.DataSource = _listSheetsName;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message + "\r\n" + exception.StackTrace);
            }
            finally
            {
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void xlueSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowDefaultWaitForm();
                if (xlueSheet.ItemIndex >= 0)
                    ExcelSource = ExcelFormattor.LoadFromExcel(openFileDialog.FileName, xlueSheet.EditValue.ToString());
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message + "\r\n" + exception.StackTrace);
            }
            finally
            {
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }
    }
}