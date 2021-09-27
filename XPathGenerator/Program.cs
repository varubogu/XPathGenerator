using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;

namespace XPathGenerator
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string xmlPath = GetParameter(args, "-i");
            string resultPath = GetParameter(args, "-o");

            string xml;
            if (!string.IsNullOrEmpty(xmlPath))
            {
                xml = File.ReadAllText(xmlPath);
            }
            else
            {
                xml = TestXML;
            }

            string xPathListString = GetXPathList(xml);

            if (string.IsNullOrEmpty(resultPath))
            {
                Console.WriteLine(xPathListString);
                //すぐ終了しないよう停止
                Console.ReadKey();
            }
            else
            {
                File.WriteAllText(resultPath, xPathListString);
            }
        }

        private static string GetXPathList(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var topElement = doc.DocumentElement;

            var list = GetXPathFromChildNodes(topElement.ChildNodes, topElement.Name);

            var sb = new StringBuilder();
            list.ForEach(x => sb.AppendLine(Format(x)));
            return sb.ToString();
        }

        static List<(string, string)> GetXPathFromChildNodes(XmlNodeList nodes, string xPath)
        {
            var list = new List<(string, string)>();
            foreach (XmlNode node in nodes)
            {
                list.AddRange(GetXPathValue(node, xPath));
            }
            return list;
        }

        static List<(string, string)> GetXPathValue(XmlNode node, string xPath)
        {
            if (node.HasChildNodes)
            {
                string newXPath = $"{xPath}/{node.Name}";
                return GetXPathFromChildNodes(node.ChildNodes, newXPath);
            }
            else
            {
                string newXPath;
                if (node.Name.Contains('#'))
                {
                    //属性は省略
                    newXPath = xPath;
                }
                else
                {
                    newXPath = $"{xPath}/{node.Name}";

                }
                return new List<(string, string)> { (newXPath, node.Value) };
            }
        }

        private static string Format((string, string) x) => $@"""{x.Item1}"", ""{x.Item2}""";

        private static string GetParameter(string[] args, string optionName)
        {
            int index = Array.IndexOf(args, optionName);
            if (index == -1)
            {
                return null;
            }

            return args[index + 1];
        }

        private static string TestXML =>
            @"<?xml version=""1.0"" encoding=""UTF-8""?> 
            <root> 
                <node>
                <data type=""text"">データ1</data> 
                </node>
                <node> 
                <data type=""text"">データ2</data> 
                </node>
                <node>
                <data type=""text"">データ3</data>
                <data2>
                    <value>aaa</value>
                    <value2/>
                    <value3></value3>
                </data2>
                </node>
            </root>
        ";
    }
}
