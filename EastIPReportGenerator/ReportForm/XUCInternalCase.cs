using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using EastIPReportGenerator.ReportForm.Base;

namespace EastIPReportGenerator.ReportForm
{
    public partial class XUCInternalCase : XtraUserControl, IGenerateReport
    {
        public XUCInternalCase()
        {
            InitializeComponent();
            Text = "国内申请";
        }

        public DataTable SourceProcess(DataTable dtSource)
        {
            var dtResult = dtSource.Clone();
            dtResult.Columns.Add("案件阶段");
            dtResult.Columns.Add("案件类型（中文）");
            dtResult.Columns.Add("是否是分案");
            dtResult.Columns.Add("是否是复审");
            dtResult.Columns.Add("是否是无效");
            dtResult.Columns.Add("ID");
            foreach (var dataRow in dtSource.Select(GetCondtionString()).ToList())
            {
                dataRow["是否是撰写"] = dataRow["是否是撰写"].ToString().ToLower().Contains("y") ? "撰写案" : "非撰写案";
                var listArray = dataRow.ItemArray.ToList();
                listArray.Add(DicCollection.案件阶段.GetNameByKey(dataRow["类别"].ToString()));
                listArray.Add(string.IsNullOrEmpty(dataRow["案件类型"].ToString())?GetCaseType(dataRow["卷号"].ToString()): DicCollection.案件类型.GetNameByKey(dataRow["案件类型"].ToString()));
                listArray.Add(dataRow["申请号/备注"].ToString().Contains("分案") ? "分案" : "非分案");
                listArray.Add(DicCollection.复审.GetNameByKey(dataRow["类别"].ToString()));
                listArray.Add(DicCollection.无效.GetNameByKey(dataRow["类别"].ToString()));
                listArray.Add(Guid.NewGuid());

                dtResult.Rows.Add(listArray.ToArray());
            }
            return dtResult;
        }

        public string GetCondtionString()
        {
            if (xdeBegin.DateTime.Date == DateTime.MinValue || xdeEnd.DateTime == DateTime.MinValue)
                return "1 = 1";
            return $"日期 >= '{xdeBegin.DateTime.Date}' AND 日期 < '{xdeEnd.DateTime.Date.AddDays(1)}'";
        }

        public void ExportToExcel(string sType)
        {
            var dt = xpgcResult.DataSource as DataTable;
            xsscReport.LoadDocument(Application.StartupPath + "\\交案.xlsx");

            var baseCell = xsscReport.ActiveWorksheet.Cells["B5"];
            for (int i = 0; i < DicCollection.案件阶段.Select(d => d.Name).Distinct().Count(); i++)
            {
                var sCaseProcess = DicCollection.案件阶段.Select(d => d.Name).Distinct().ToList()[i];
                baseCell[i, 0].Value = sCaseProcess;
                baseCell[i, 2].Value = dt.Select($"是否是撰写 = '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-发明'").Length;
                baseCell[i, 3].Value = dt.Select($"是否是撰写 = '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-实用新型'").Length;
                baseCell[i, 4].Value = dt.Select($"是否是撰写 = '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-外观'").Length;

                baseCell[i, 5].Value = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-发明'").Length;
                var n复审 = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-发明' AND 是否是复审 = '复审'").Length;
                if (n复审 > 0)
                    AddComments(baseCell[i, 5], "System", $"{baseCell[i, 5].Value}/{n复审}(复审)\r\n");
                var n分案 = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-发明' AND 是否是分案 = '分案'").Length;
                if (n分案 > 0)
                    AddComments(baseCell[i, 5], "System", $"{baseCell[i, 5].Value}/{n分案}(分案)\r\n");


                baseCell[i, 6].Value = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-实用新型'").Length;
                n复审 = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-实用新型' AND 是否是复审 = '复审'").Length;
                if (n复审 > 0)
                    AddComments(baseCell[i, 6], "System", $"{baseCell[i, 6].Value}/{n复审}(复审)\r\n");
                n分案 = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-实用新型' AND 是否是分案 = '分案'").Length;
                if (n分案 > 0)
                    AddComments(baseCell[i, 6], "System", $"{baseCell[i, 6].Value}/{n分案}(分案)\r\n");


                baseCell[i, 7].Value = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-外观'").Length;
                n复审 = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-外观' AND 是否是复审 = '复审'").Length;
                if (n复审 > 0)
                    AddComments(baseCell[i, 7], "System", $"{baseCell[i, 7].Value}/{n复审}(复审)\r\n");
                n分案 = dt.Select($"是否是撰写 <> '撰写案' AND 案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '国内-外观' AND 是否是分案 = '分案'").Length;
                if (n分案 > 0)
                    AddComments(baseCell[i, 7], "System", $"{baseCell[i, 7].Value}/{n分案}(分案)\r\n");


                baseCell[i, 8].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= 'PCT进中国-发明'").Length;
                n复审 = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= 'PCT进中国-发明' AND 是否是复审 = '复审'").Length;
                if (n复审 > 0)
                    AddComments(baseCell[i, 8], "System", $"{baseCell[i, 8].Value}/{n复审}(复审)\r\n");
                n分案 = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= 'PCT进中国-发明' AND 是否是分案 = '分案'").Length;
                if (n分案 > 0)
                    AddComments(baseCell[i, 8], "System", $"{baseCell[i, 8].Value}/{n分案}(分案)\r\n");


                baseCell[i, 9].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= 'PCT进中国-实用新型'").Length;

                baseCell[i, 10].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= 'PCT国际'").Length;

                baseCell[i, 21].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '保密审查'").Length;
                baseCell[i, 22].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 案件类型（中文）= '集成电路'").Length;

                baseCell[i, 31].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 是否是无效= '无效案'").Length;
                baseCell[i, 32].Value = dt.Select($"案件阶段 = '{sCaseProcess}' AND 是否是无效= '无效答辩'").Length;
            }
        }

        private void AddComments(Range range, string author, string text)
        {
            var comments = xsscReport.ActiveWorksheet.Comments.GetComments(range);
            if (comments.Count > 0)
            {
                comments[0].Text += text;
            }
            else
            {
                xsscReport.ActiveWorksheet.Comments.Add(range, author, text);
            }
        }

        public void SetGridStyle()
        {
        }

        public string GetCaseType(string sCaseNo)
        {
            var sCaseNoPattern = @"(?<=\d{2})[A-z]+?(?=\d+)";
            var match = Regex.Match(sCaseNo, sCaseNoPattern);
            if (match.Success)
                return DicCollection.案件类型.GetNameByKey(match.Value);
            return string.Empty;
        }

        private void xsbOk_Click(object sender, EventArgs e)
        {
            var dt = SourceProcess(xucExcelReader.ExcelSource);
            xpgcResult.DataSource = dt;
            xpgcResult.GenerateGridFields(dt);
            ExportToExcel(null);
        }

        private void xsbExport_Click(object sender, EventArgs e)
        {
            xsscReport.SaveDocumentAs();
        }
    }
}