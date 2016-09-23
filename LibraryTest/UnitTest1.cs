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
            ExchangeRate er = new ExchangeRate();
            var convertedValue = er.BaseCurrency("USD").CurrencyToConvert("GBP").GetExchangeRate().CalculateConvertedValue(300);
            var convertedValueTwo = er.BaseCurrency("USD").CurrencyToConvert("AUD").GetExchangeRate().CalculateConvertedValue(9900);
            var convertedValueThree = er.BaseCurrency("USD").CurrencyToConvert("EUR").GetExchangeRate().CalculateConvertedValue(9900);

            var rateTest = er.BaseCurrency("USD").CurrencyToConvert("EUR").RetrieveExchangeRate();
        }

    }
}
