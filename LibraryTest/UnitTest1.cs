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
        public static List<InputCode> InputCodes = new List<InputCode>()
        {
            "EUR",
            "GDP",
            "AUD",
            "BRL",
            "BZD",
            "DKK"
        }

        public void AddInputCodesFromList(List<InputCode> lstInputCodes)
        {
            foreach(InputCode l in lstInputCodes)
            {
                InputCodes.Add(newInputCodes(l));
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            AddInputCodesFromList(InputCodes);
            new CurrencyCodeViewModel(InputCodes);
            //System.Diagnostics.Debug.WriteLine(CurrencyCodeViewModel.InputCodes.ToString());
        }
    }
}
