using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    public class CurrencyCodeViewModel
    {
        public static List<string> currencyCodeList = new List<string>();
        public static List<InputCode> inputCodeList = new List<InputCode>();
        public static decimal exchangeRate;
        public static string targetCurrencyCode;
        public static DateTime queryDate;
        private const string baseUrl = "http://query.yahooapis.com/v1/public/yrl" +"?r=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20({0})&env=store://datatables.org/alltableswithkeys";
        public static string coreCurrencyCode;
        

        public CurrencyCodeViewModel()
        {                        
            YahooData yahooData = new YahooData(CurrencyCodeViewModel.coreCurrencyCode, CurrencyCodeViewModel.targetCurrencyCode, CurrencyCodeViewModel.baseUrl, CurrencyCodeViewModel.currencyCodeList);
            CurrencyCodeViewModel.inputCodeList = YahooData.inputCodeList;
            CurrencyCodeViewModel.exchangeRate = YahooData.RetrieveExchangeRate(CurrencyCodeViewModel.inputCodeList, CurrencyCodeViewModel.targetCurrencyCode);
            CurrencyCodeViewModel.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == YahooData.comboCode).Date);
        }

        public static void AddCurrencyCodeToList(string currCode)
        {
            CurrencyCodeViewModel.currencyCodeList.Add(currCode);
        }

        public static void AddCurrencyCodeList(List<string> currCodes)
        {
            CurrencyCodeViewModel.currencyCodeList = currCodes;
        }

        public static void AddTargetCurrency(string target)
        {
            CurrencyCodeViewModel.targetCurrencyCode = target;
        }

        public static void AddCoreCurrency(string core)
        {
            CurrencyCodeViewModel.coreCurrencyCode = core;
        }

        public static decimal GetExchangeRate()
        {
            return CurrencyCodeViewModel.exchangeRate;
        }
    }
}
