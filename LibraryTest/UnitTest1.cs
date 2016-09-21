using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahooFinanceExchangeQueryLibrary;
using System.Collections.Generic;

namespace LibraryTest
{
    [TestClass]
    public class UnitTest1
    {
        public static List<InputCode> InputCodes = new List<InputCode>();


        [TestMethod]
        public void TestMethod1()
        {
            InputCodes.Add(new InputCode("USD"));
            InputCodes.Add(new InputCode("EUR"));
            InputCodes.Add(new InputCode("GBP"));
            CurrencyCodeViewModel cm = new CurrencyCodeViewModel(InputCodes);
            System.Diagnostics.Debug.WriteLine(CurrencyCodeViewModel.InputCodes.ToString());
            
        }
    }
}
