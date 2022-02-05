using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPathGenerator.TextAnalysis
{
    /// <summary>
    /// 解析結果リスト
    /// </summary>
    public class AnalysisList : List<AnalysisItem>
    {
        /// <summary>
        /// 解析結果リスト（空生成）
        /// </summary>
        public AnalysisList() : base()
        {

        }

        /// <summary>
        /// 解析結果リスト（初期化）
        /// </summary>
        /// <param name="items"></param>
        public AnalysisList(IEnumerable<AnalysisItem> items) : base(items)
        {

        }
    }
}
