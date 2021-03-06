﻿using System;
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
        public string targetCode;
        public string comboCode;

        public List<string> codeComboList = new List<string>();
        public List<InputCode> inputCodeList = new List<InputCode>();
        public InputCode inputCode = new InputCode();
        public string yqlQuery;
        public string queryUrl;

        public XDocument xdoc;
        public decimal? exchangeRate;
        public DateTime queryDate;

        public string url = "http://query.yahooapis.com/v1/public/yql?q=" +
            @"select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(""{0}"")" +
            "&env=store://datatables.org/alltableswithkeys";

        public YahooData(string core, string target)
        {
            coreCode = core;
            targetCode = target;
            comboCode = CombineCoreAndTargetCode(core, target);
            GenerateInputCodeList();
            queryUrl = CreateQueryUrl(codeComboList, url);
            SubmitQuery(queryUrl);
            ParseReturnedXml(inputCodeList, xdoc);
            exchangeRate = inputCodeList[0].Rate;

        }

        public string CombineCoreAndTargetCode(string core, string targetCode)
        {
            string inputOne = core;
            string inputTwo = targetCode;
            string code = String.Format("{0}{1}", inputOne, inputTwo);
            return code;
        }

        public void GenerateCodeComboList()
        {
            codeComboList.Add(comboCode);
        }

        public void GenerateInputCodeList()
        {
            GenerateCodeComboList();
            foreach (string cc in codeComboList)
            {
                InputCode listObj = new InputCode();
                listObj.Id = cc;
                inputCodeList.Add(listObj);
            }
        }

        private string CreateQueryUrl(List<string> comboList, string baseUrl)
        {
            string idList = comboCode;
            string url = String.Format(baseUrl, idList);
            return url;
        }

        private string GenerateUrlFromInputQuery(string query)
        {
            string urlStart = "http://query.yahooapis.com/v1/public/yql?q=";
            string urlEnd = "&env=store://datatables.org/alltableswithkeys";
            return HttpUtility.UrlEncode(String.Format("{0}{1}{2}", urlStart, query, urlEnd));
        }

        public void SubmitQuery(string queryUrl)
        {
            xdoc = XDocument.Load(queryUrl);
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
            inputCodeList = inputCodes;
        }

        public decimal? GetExchangeRate()
        {
            return exchangeRate;
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
    }
}

