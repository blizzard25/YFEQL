using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YahooFinanceExchangeQueryLibrary
{
    public class YahooData
    {
        public string coreCode;
        public string baseUrl;
        public List<string> targetCodeList = new List<string>();
        public List<string> codeComboList = new List<string>();
        public List<InputCode> inputCodeList = new List<InputCode>();
        public string comboCode;
        private string pairCodeTemplate = "\"{0}\"";
        private string yqlQuery;
        public string queryUrl;
        public XDocument xdoc;
        public decimal exchangeRate;
        public DateTime queryDate;

        public YahooData(string coreCode, string targetCode, string baseUrl, List<string> targetCodeList)
        {
            this.coreCode = coreCode;
            this.baseUrl = baseUrl;
            this.targetCodeList = targetCodeList;

            GenerateInputCodeList();
            SubmitQuery(this.queryUrl);
        }

        public YahooData(string urlBegin, string query, string urlEnd, string targetCode, List<string> targetCodeList)
        {
            this.targetCodeList = targetCodeList;
            
            GenerateUrlFromInputQuery(urlBegin, urlEnd, query);
            SubmitQuery(this.queryUrl);

        }

        public YahooData(string coreCode, string targetCode, List<string> targetCodes)
        {
            this.targetCodeList = targetCodes;
            this.coreCode = coreCode;

            GenerateUrlFromInputQuery(YqlQueryBuilder(this.coreCode, this.targetCodeList));
            SubmitQuery(this.queryUrl);

        }

        public string CombineCoreAndTargetCode(string core, string targetCode)
        {
            this.comboCode = String.Format(core, targetCode);
            return this.comboCode;
        }

        public void GenerateCodeComboList()
        {
            foreach (string t in this.targetCodeList)
            {
                this.codeComboList.Add(String.Format(pairCodeTemplate, CombineCoreAndTargetCode(this.coreCode, t)));
            }
        }

        public void GenerateInputCodeList()
        {
            GenerateCodeComboList();
            foreach (string cc in this.codeComboList)
            {
                InputCode listObj = new InputCode();
                listObj.Id = cc;
                this.inputCodeList.Add(listObj);
            }
        }

        private string CreateQueryUrl(List<string> comboList, string baseUrl)
        {
            string idList = String.Join(",%20", comboList);
            string url = String.Format(baseUrl, idList);
            return url;
        }

        private string YqlQueryBuilder(string coreCode, List<string> targetCodes)
        {
            this.targetCodeList = targetCodes;
            this.coreCode = coreCode;
            this.GenerateCodeComboList();

            string yqlParams = String.Format("({0})", this.codeComboList);
            string yqlStatement = "Select * From yahoo.finance.xchange Where pair in {0}";
            string yqlQuery = String.Format(yqlStatement, yqlParams);

            return yqlQuery;
        }

        private string GenerateUrlFromInputQuery(string urlBegin, string urlEnd, string query)
        {
            string yqlQuery = query;
            string unencodedUrl = String.Format("{0}{1}{2}", urlBegin, yqlQuery, urlEnd);
            this.queryUrl = HttpUtility.UrlEncode(unencodedUrl);
            return this.queryUrl;
        }

        private string GenerateUrlFromInputQuery(string query)
        {
            return this.queryUrl = HttpUtility.UrlEncode(
                String.Format("{0}{1}{2}", CurrencyCodeViewModel.RetrieveUrlStart(), query, CurrencyCodeViewModel.RetrieveEndUrl()));
        }

        public void SubmitQuery(string queryUrl)
        {
            this.queryUrl = CreateQueryUrl(this.codeComboList, this.coreCode);
            this.xdoc = XDocument.Load(queryUrl);
            ParseReturnedXml(this.inputCodeList, this.xdoc);
        }

        public decimal RetrieveExchangeRate(List<InputCode> ic, string combo)
        {
            InputCode input = FindCodeByTargetId(ic, combo);
            this.exchangeRate = Convert.ToDecimal(input.Rate);
            return this.exchangeRate;
        }

        public InputCode FindCodeByTargetId(List<InputCode> ic, string combo)
        {
            InputCode targetInputCode = ic.First(i => i.Id == combo);
            return targetInputCode;
        }

        private void ParseReturnedXml(List<InputCode> inputCodes, XDocument doc)
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

        public List<InputCode> GetInputCodeList()
        {
            return this.inputCodeList;
        }
    }
}

