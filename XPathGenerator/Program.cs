using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using XPathGenerator.TextAnalysis;

namespace XPathGenerator
{
    class Program
    {
        /// <summary>
        /// XPath生成
        /// </summary>
        /// <param name="args">
        /// -i XMLファイルを指定します
        /// -xml XMLを直接コマンド引数で指定します　※「"」のエスケープをすること、-iオプション優先
        /// -o 結果の出力先を指定します
        /// </param>
        static void Main(string[] args)
        {
            string xmlPath = GetParameter(args, "-i");
            string resultPath = GetParameter(args, "-o");

            string xml = GetParameter(args, "-xml");

            if (!string.IsNullOrEmpty(xmlPath))
            {
                xml = File.ReadAllText(xmlPath);
            }
            else if (string.IsNullOrEmpty(xml))
            {
                xml = TestXML;
            }

            if (string.IsNullOrEmpty(xml)) throw new ArgumentNullException("XMLの内容がありません");

            var list = new AnalysisXml(xml);

            var sb = new StringBuilder();
            list.AnalysisList.ForEach(x => sb.AppendLine(x.ToString()));
            string xPathListString = sb.ToString();

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

        /// <summary>
        /// コマンドラインパラメータを解析し、オプション値を取得する
        /// </summary>
        /// <param name="args">コマンドラインパラメータ</param>
        /// <param name="optionName">取り出すオプションの名前</param>
        /// <returns>オプション値</returns>
        private static string GetParameter(string[] args, string optionName)
        {
            int index = Array.IndexOf(args, optionName);
            if (index == -1)
            {
                return null;
            }
            else if (index + 1 >= args.Length)
            {
                throw new ArgumentNullException($"{optionName}の値がありません");
            }
            else
            {
                return args[index + 1];
            }
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
