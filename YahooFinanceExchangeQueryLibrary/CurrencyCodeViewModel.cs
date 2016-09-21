using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YahooFinanceExchangeQueryLibrary
{
    public class CurrencyCodeViewModel
    {
        public List<string> currencyCodeList;
        public List<InputCode> inputCodeList;
        public List<string> targetCurrencyCodes;
        public decimal exchangeRate;
        public string targetCurrencyCode;
        public DateTime queryDate;
        private string baseUrl;
        private string queryUrl;
        private string yahooQuery;
        private static string urlStart = "http://query.yahooapis.com/v1/public/yr1?r=";
        private static string endUrl = "&env=store://datatables.org/alltableswithkeys";
        public string coreCurrencyCode;        

        public CurrencyCodeViewModel()
        {                        
            YahooData yahooData = new YahooData(this.coreCurrencyCode, this.targetCurrencyCode, this.baseUrl, this.currencyCodeList);
            this.inputCodeList = yahooData.inputCodeList;
            this.exchangeRate = yahooData.RetrieveExchangeRate(this.inputCodeList, this.targetCurrencyCode);
            this.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == yahooData.comboCode).Date);
        }

        public CurrencyCodeViewModel(string query)
        {
            this.yahooQuery = query;
            YahooData yahooData = new YahooData(
                CurrencyCodeViewModel.urlStart, this.yahooQuery, CurrencyCodeViewModel.endUrl, this.targetCurrencyCode, this.currencyCodeList);
            this.inputCodeList = yahooData.inputCodeList;
            this.exchangeRate = yahooData.RetrieveExchangeRate(this.inputCodeList, this.targetCurrencyCode);
            this.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == yahooData.comboCode).Date);
        }

        public CurrencyCodeViewModel(bool buildQueryFromCodes)
        {
            if (buildQueryFromCodes == true) {
                YahooData yahooData = new YahooData(this.coreCurrencyCode, this.targetCurrencyCode, this.currencyCodeList);
                this.inputCodeList = yahooData.inputCodeList;
                this.exchangeRate = yahooData.RetrieveExchangeRate(this.inputCodeList, this.targetCurrencyCode);
                this.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == yahooData.comboCode).Date);
            }
        }

        public void AddCurrencyCodeToList(string currCode)
        {
            this.currencyCodeList.Add(currCode);
        }

        public void AddCurrencyCodeList(List<string> currCodes)
        {
            this.currencyCodeList = currCodes;
        }

        public void AddTargetCurrency(string target)
        {
            this.targetCurrencyCode = target;
        }

        public void AddTargetCurrencies(List<string> target)
        {
            this.targetCurrencyCodes = target;
        }

        public void AddCoreCurrency(string core)
        {
            this.coreCurrencyCode = core;
        }

        public decimal GetExchangeRate()
        {
            return this.exchangeRate;
        }

        private string BaseUrl(string input)
        {
            return this.baseUrl = input;
        }

        public static string RetrieveUrlStart()
        {
            return CurrencyCodeViewModel.urlStart;
        }

        public static string RetrieveEndUrl()
        {
            return CurrencyCodeViewModel.endUrl;
        }

    }
}
