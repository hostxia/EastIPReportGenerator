using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastIPReportGenerator.ReportForm.Base
{
    public static class ExcelFormattor
    {
        public static DataTable LoadFromExcel(string sFilePath, string sSheetName)
        {

            var sConnectionString = $"Provider=Microsoft.Ace.OleDb.12.0;data source={sFilePath};Extended Properties='Excel 12.0;'";
            using (var conn = new OleDbConnection(sConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from [" + sSheetName + "]";
                    var ds = new DataSet();
                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(ds, sSheetName);
                        return ds.Tables[0];
                    }
                }
            }
        }

        public static List<string> LoadSheetNames(this List<string> listSheetName, string sFilePath)
        {
            var sConnectionString = $"Provider=Microsoft.Ace.OleDb.12.0;data source={sFilePath};Extended Properties='Excel 12.0;'";
            using (var conn = new OleDbConnection(sConnectionString))
            {
                conn.Open();
                listSheetName.Clear();
                var dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtSheetName == null) return listSheetName;
                listSheetName.AddRange(dtSheetName.Rows.Cast<DataRow>().Select(r => r[2].ToString()));//获取Excel的表名
                return listSheetName;
            }
        }
    }
}
