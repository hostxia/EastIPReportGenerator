using System.Data;

namespace EastIPReportGenerator.ReportForm.Base
{
    internal interface IGenerateReport
    {
        DataTable SourceProcess(DataTable dtSource);

        string GetCondtionString();

        void SetGridStyle();

        void ExportToExcel(string sType);
    }
}