using System.Collections.Generic;
using System.Linq;

namespace EastIPReportGenerator.ReportForm.Base
{
    public class DicCollection : List<DictionaryItem>
    {
        private readonly DicType _dicType;

        public DicCollection(DicType dicType)
        {
            _dicType = dicType;
        }

        public static DicCollection 案件阶段
        {
            get
            {
                var dic = new DicCollection(DicType.案件阶段)
                {
                    {"E-A", "新申请"},
                    {"E-B", "新申请"},
                    {"E-U", "新申请"},
                    {"P", "新申请"},
                    {"SE", "新申请"},
                    {"A", "新申请"},
                    {"B", "新申请"},
                    {"U", "新申请"},
                    {"DJ", "新申请"},
                    {"E-W", "新申请"},
                    {"E-U", "新申请"},
                    {"W", "新申请"},
                    {"E-D", "中间"},
                    {"E-S", "中间"},
                    {"D", "中间"},
                    {"S", "中间"},
                    {"E-G", "中间"},
                    {"P-D", "中间"},
                    {"G", "中间"},
                    {"E-E", "OA答辩"},
                    {"E", "OA答辩"},
                    {"E-F", "OA答辩"},
                    {"F", "OA答辩"},
                    {"E-X", "OA答辩"},
                    {"X", "OA答辩"},
                    {"C", "办登及年费"},
                    {"E-Y", "中间"},
                    {"Y", "中间"},
                };
                return dic;
            }
        }

        public static DicCollection 案件类型
        {
            get
            {
                var dic = new DicCollection(DicType.案件类型)
                {
                    {"P", "PCT国际"},
                    {"PI", "PCT进中国-发明"},
                    {"PU", "PCT进中国-实用新型"},
                    {"I", "国内-发明"},
                    {"U", "国内-实用新型"},
                    {"D", "国内-外观"},
                    {"NI", "国内-发明"},
                    {"NU", "国内-实用新型"},
                    {"ND", "国内-外观"},
                    {"SE", "保密审查"},
                    {"DJ", "集成电路"},
                };
                return dic;
            }
        }

        public static DicCollection 无效
        {
            get
            {
                var dic = new DicCollection(DicType.无效)
                {
                    {"E-W", "无效案"},
                    {"W", "无效案"},
                    {"E-X", "无效案"},
                    {"X", "无效案"},
                    {"E-Y", "无效答辩"},
                    {"Y", "无效答辩"},
                };
                return dic;
            }
        }

        public static DicCollection 复审
        {
            get
            {
                var dic = new DicCollection(DicType.复审)
                {
                    {"E-F", "复审"},
                    {"F", "复审"}
                };
                return dic;
            }
        }

        public List<DictionaryItem> Add(string sId, string sName)
        {
            Add(new DictionaryItem { Type = _dicType, Id = sId, Name = sName });
            return this;
        }

        public List<DictionaryItem> AddRange(List<KeyValuePair<string, string>> listKeyValuePairs)
        {
            listKeyValuePairs.ForEach(k => Add(k.Key, k.Value));
            return this;
        }

        public string GetNameByKey(string sId)
        {
            return this.FirstOrDefault(d => d.Id == sId)?.Name;
        }
    }

    public class DictionaryItem
    {
        public DicType Type { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
    }

    public enum DicType
    {
        案件阶段,
        案件类型,
        无效,
        复审
    }
}