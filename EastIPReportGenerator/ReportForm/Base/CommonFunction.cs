using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraPivotGrid;

namespace EastIPReportGenerator.ReportForm.Base
{
    public static class CommonFunction
    {
        public static void GenerateGridFields(this PivotGridControl pivotGridControl, DataTable dataTable)
        {
            if (pivotGridControl.Fields.Count != 0) return;
            foreach (var column in dataTable.Columns.Cast<DataColumn>())
            {
                var filed = pivotGridControl.Fields.Add();
                filed.Caption = column.ColumnName;
                filed.FieldName = column.ColumnName;
                filed.Visible = false;
                if (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(double))
                {
                    filed.SummaryType = PivotSummaryType.Sum;
                }
                else
                {
                    filed.SummaryType = PivotSummaryType.Count;
                }
            }
        }
    }
}
