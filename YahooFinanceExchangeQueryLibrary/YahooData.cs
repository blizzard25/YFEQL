using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    public class YahooData
    {
        public static string coreCode;
        public static string baseUrl;
        public static List<string> targetCodeList = new List<string>();
        public static List<string> codeComboList = new List<string>();
        public static List<InputCode> inputCodeList = new List<InputCode>();
        public static string comboCode;
        public static string queryUrl;
        public static XDocument xdoc;
        public static decimal exchangeRate;
        public static DateTime queryDate;

        public YahooData(string coreCode, string targetCode, string baseUrl, List<string> targetCodeList)
        {
            YahooData.coreCode = coreCode;
            YahooData.baseUrl = baseUrl;
            YahooData.targetCodeList = targetCodeList;
            
            foreach(string t in targetCodeList)
            {
                YahooData.codeComboList.Add(CombineCoreAndTargetCode(coreCode, t));
            }

            foreach(string cc in YahooData.codeComboList)
            {
                InputCode listObj = new InputCode();
                listObj.Id = cc;
                YahooData.inputCodeList.Add(listObj);
            }

            YahooData.queryUrl = CreateQueryUrl(YahooData.codeComboList, coreCode);
            SubmitQuery(YahooData.queryUrl);
            ParseReturnedXml(YahooData.inputCodeList, YahooData.xdoc);

        }

        public static string CombineCoreAndTargetCode(string core, string targetCode)
        {
            string combo = String.Format(core, targetCode);
            return combo;
        }

        public static string CreateQueryUrl(List<string> comboList, string coreCode)
        {
            string idList = String.Join("%20, ", comboList);
            string url = String.Format(coreCode, idList);
            return url;
        }

        public static void SubmitQuery(string queryUrl)
        {
            YahooData.xdoc = XDocument.Load(queryUrl);
        }

        public static decimal RetrieveExchangeRate(List<InputCode> ic, string combo)
        {
            InputCode input = FindCodeByTargetId(ic, combo);
            YahooData.exchangeRate = Convert.ToDecimal(input.Rate);
            return YahooData.exchangeRate;
        }

        public static InputCode FindCodeByTargetId(List<InputCode> ic, string combo)
        {
            InputCode targetInputCode = ic.First(i => i.Id == combo);
            return targetInputCode;
        }

        private static void ParseReturnedXml(List<InputCode> inputCodes, XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            foreach (InputCode ic in inputCodes)
            {
                XElement r = results.Elements("rate").First(w => w.Attribute("id").Value == ic.Id);
                ic.Name = r.Element("Name").Value;
                ic.Rate = GetDecimal(r.Element("Rate").Value);
                ic.Date = GetDateTime(String.Format("{0} {1}", r.Element("Date").Value, r.Element("Time").Value));
                ic.Ask = GetDecimal(r.Element("Ask").Value);
                ic.Bid = GetDecimal(r.Element("Bid").Value);
            }
        }

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;
            decimal value;
            if (Decimal.TryParse(input, out value)) return value;
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }

        public static List<InputCode> GetInputCodeList()
        {
            return YahooData.inputCodeList;
        }
    }
}

