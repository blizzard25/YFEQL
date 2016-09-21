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
        private string baseUrl = "";
        private string urlStart = "";
        private string urlEnd = "";
        private string queryUrl = "";
        private string yqlQuery = "";
        public string coreCurrencyCode;        

        // requires the manual setting of the required values
        public CurrencyCodeViewModel()
        {                        
            YahooData yahooData = new YahooData(this.coreCurrencyCode, this.targetCurrencyCode, this.baseUrl, this.currencyCodeList);
            this.inputCodeList = yahooData.inputCodeList;
            this.queryUrl = yahooData.queryUrl;
            this.exchangeRate = yahooData.RetrieveExchangeRate(this.inputCodeList, this.targetCurrencyCode);
            this.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == yahooData.comboCode).Date);
        }

        // for piecing together the query url given url inputs
        public CurrencyCodeViewModel(string urlStart, string urlEnd, string query, string targetCode, List<string> currencyCodes)
        {
            this.urlStart = urlStart;
            this.urlEnd = urlEnd;
            this.yqlQuery = query;
            this.targetCurrencyCode = targetCode;
            this.currencyCodeList = currencyCodes;

            YahooData yahooData = new YahooData(urlStart, query, urlEnd, targetCode, currencyCodes);

            this.inputCodeList = yahooData.inputCodeList;
            this.queryUrl = yahooData.queryUrl;
            this.exchangeRate = yahooData.RetrieveExchangeRate(this.inputCodeList, this.targetCurrencyCode);
            this.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == yahooData.comboCode).Date);
        }

        // auto-generates the YQL query and auto-generates the encoded URL
        public CurrencyCodeViewModel(string coreCode, string targetCode, List<string> currencyCodes)
        {
            this.coreCurrencyCode = coreCode;
            this.targetCurrencyCode = targetCode;
            this.currencyCodeList = currencyCodes;

            YahooData yahooData = new YahooData(coreCode, targetCode, currencyCodes);

            this.inputCodeList = yahooData.inputCodeList;
            this.queryUrl = yahooData.queryUrl;
            this.exchangeRate = yahooData.RetrieveExchangeRate(this.inputCodeList, this.targetCurrencyCode);
            this.queryDate = Convert.ToDateTime(inputCodeList.First(i => i.Id == yahooData.comboCode).Date);
        }

        public void AddBaseUrl(string url)
        {
            this.baseUrl = url;
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

        public string RetrieveUrlStart()
        {
            return this.urlStart;
        }

        public string RetrieveEndUrl()
        {
            return this.urlEnd;
        }

        public string RetrieveQueryUrl()
        {
            return this.queryUrl;
        }

        public void SetYqlQuery(string query)
        {
            this.yqlQuery = query;

        }
    }
}
