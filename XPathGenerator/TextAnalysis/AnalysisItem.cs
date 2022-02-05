using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPathGenerator.TextAnalysis
{
    /// <summary>
    /// 解析結果のアイテム１個分
    /// </summary>
    public class AnalysisItem
    {
        /// <summary>
        /// XPath
        /// </summary>
        public string XPath { get; }
        /// <summary>
        /// 値
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 解析結果を生成します
        /// </summary>
        /// <param name="xPath">XPath</param>
        /// <param name="value">値</param>
        internal AnalysisItem(string xPath, string value)
        {
            XPath = xPath;
            Value = value;
            
        }

        /// <summary>
        /// 解析結果を文字列化する
        /// </summary>
        /// <returns>XPath[aaa/bbb/ccc], Value[12345] 形式の文字列</returns>
        public override string ToString()
            => $"XPath[{XPath}], Value[{Value}]";

        /// <summary>
        /// 解析結果を{string, string}のタプル形式で文字列化する
        /// </summary>
        /// <returns>{"aaa/bbb/ccc", "12345"} 形式の文字列</returns>
        public string ToStringInitializeCode()
            => $@"{{""{XPath}"", ""{Value}""}}";

        /// <summary>
        /// 解析結果を(string, string)のタプル形式で文字列化する
        /// </summary>
        /// <returns></returns>
        public string ToStringTupleCode()
            => $@"(""{XPath}"", ""{Value}"")";


    }
}
