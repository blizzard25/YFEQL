using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahooFinanceExchangeQueryLibrary;
using System.Collections.Generic;

namespace LibraryTest
{
    [TestClass]
    public class UnitTest1
    {

        //random list of currency codes, only used for unit testing purposes
        //normally this list will come from the application referencing the YahooFinanceExchageQueryLibrary
        public List<string> currencyCodes = new List<string>();
        public decimal exchangeRate;
        private static string baseUrlTemplate = String.Format(
            "{0}{1}{2}", urlStart, urlMid, endUrl);
        public static string urlStart = "http://query.yahooapis.com/v1/public/yql?q=";
        public static string urlMid = "select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20({0})";
        public static string endUrl = "&env=store://datatables.org/alltableswithkeys";

        [TestMethod]
        public void TestMethod1()
        {
            string coreCode = "USD";
            string targetCode = "GBP";
            this.currencyCodes.Add(coreCode);
            this.currencyCodes.Add("EUR");
            this.currencyCodes.Add(targetCode);
            this.currencyCodes.Add("AUD");

            CurrencyCodeViewModel ccvm = new CurrencyCodeViewModel();            
            ccvm.AddCurrencyCodeList(currencyCodes);
            ccvm.AddTargetCurrency(targetCode);
            ccvm.AddCoreCurrency(coreCode);
            exchangeRate = ccvm.GetExchangeRate();             
        }

        [TestMethod]
        public void TestMethod2()
        {
            string coreCode = "USD";
            string targetCode = "GBP";

            this.currencyCodes.Add(coreCode);
            this.currencyCodes.Add("EUR");
            this.currencyCodes.Add(targetCode);
            this.currencyCodes.Add("AUD");

            CurrencyCodeViewModel ccvm = new CurrencyCodeViewModel(urlStart, endUrl, urlMid, targetCode,currencyCodes);
            ccvm.AddCurrencyCodeList(currencyCodes);
            ccvm.AddTargetCurrency(targetCode);
            ccvm.AddCoreCurrency(coreCode);
            exchangeRate = ccvm.GetExchangeRate();
        }

        [TestMethod]
        public void TestMethod3()
        {
            string coreCode = "USD";
            string targetCode = "GBP";

            this.currencyCodes.Add(coreCode);
            this.currencyCodes.Add("EUR");
            this.currencyCodes.Add(targetCode);
            this.currencyCodes.Add("AUD");

            // string yqlStr = "Select * From yahoo.finance.xchange where pair in (\"USDEUR\", \"USDGBP\")";
            CurrencyCodeViewModel ccvm = new CurrencyCodeViewModel(coreCode, targetCode, currencyCodes);
            ccvm.AddCurrencyCodeList(currencyCodes);
            exchangeRate = ccvm.GetExchangeRate();
        }
    }
}
