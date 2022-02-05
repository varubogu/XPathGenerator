using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPathGenerator.TextAnalysis
{
    /// <summary>
    /// テキスト解析ベースクラス
    /// </summary>
    public abstract class AnalysisTextBase
    {
        /// <summary>
        /// 解析前のテキスト
        /// </summary>
        public string Origin { get; }

        /// <summary>
        /// 解析結果リスト
        /// </summary>
        public AnalysisList AnalysisList { get; }

        /// <summary>
        /// テキスト解析実行
        /// </summary>
        /// <param name="text">解析対象</param>
        internal AnalysisTextBase(string text)
        {
            Origin = text;
            AnalysisList = Analyze(text);
        }

        /// <summary>
        /// 解析処理を実行する本体
        /// 継承先で実装する
        /// </summary>
        /// <param name="text">解析対象</param>
        /// <returns>解析結果のリスト</returns>
        protected abstract AnalysisList Analyze(string text);

        /// <summary>
        /// XPathの結合を行う
        /// </summary>
        /// <param name="xPath">結合元</param>
        /// <param name="add">追加対象</param>
        /// <returns></returns>
        protected static string CombineXPath(string xPath, string add)
            => string.IsNullOrWhiteSpace(xPath) ? add : $"{xPath}/{add}";
    }
}
