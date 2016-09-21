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
        public static List<string> currencyCodes = new List<string>();

        [TestMethod]
        public void TestMethod1()
        {
            string coreCode = "USD";
            string targetCode = "GBP";
            currencyCodes.Add(coreCode);
            currencyCodes.Add("EUR");
            currencyCodes.Add(targetCode);
            currencyCodes.Add("AUD");

            // CurrencyCodeViewModel ccvm = new CurrencyCodeViewModel();
            
            CurrencyCodeViewModel.AddCurrencyCodeList(currencyCodes);
            CurrencyCodeViewModel.AddTargetCurrency(targetCode);
            CurrencyCodeViewModel.AddCoreCurrency(coreCode);

            Console.WriteLine(CurrencyCodeViewModel.GetExchangeRate().ToString());
            
            
        }
    }
}
