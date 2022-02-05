using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XPathGenerator.TextAnalysis
{
    /// <summary>
    /// XML解析
    /// </summary>
    public class AnalysisXml : AnalysisTextBase
    {
        public AnalysisXml(string text) : base(text)
        {
        }

        /// <summary>
        /// XML解析を行う
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected override AnalysisList Analyze(string text)
        {
            var doc = new XmlDocument();
            doc.LoadXml(text);
            var topElement = doc.DocumentElement;

            return new AnalysisList(GetXPathValues(topElement, string.Empty));
        }

        /// <summary>
        /// (再帰関数)XML解析を行い、結果を返していく
        /// </summary>
        /// <param name="node">現在地点のNode</param>
        /// <param name="xPath">現在地点のXPath</param>
        /// <returns></returns>
        private static IEnumerable<AnalysisItem> GetXPathValues(XmlNode node, string xPath)
        {
            if (node.HasChildNodes)
            {
                string newXPath = CombineXPath(xPath, node.Name);
                foreach (XmlNode child in node.ChildNodes)
                {
                    foreach (var item in GetXPathValues(child, newXPath))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                //#（属性）の場合はパスに含めない
                var newXPath = node.Name.Contains('#') ? xPath : CombineXPath(xPath, node.Name);
                yield return new AnalysisItem(newXPath, node.Value);
            }
        }
    }
}
