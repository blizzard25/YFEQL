using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    public class ExchangeRate : IExchangeRate
    {
        public string baseCurrency { get; set; }
        public string convertToCurrency { get; set; }
        public decimal? exchangeRate { get; set; }
        public decimal convertedCurrency { get; set; }

        public IExchangeRate BaseCurrency(string c)
        {
            this.baseCurrency = c;
            return this;
        }

        public IExchangeRate CurrencyToConvert(string c)
        {
            this.convertToCurrency = c;
            return this;
        }

        public IExchangeRate GetExchangeRate()
        {
            YahooData y = new YahooData(baseCurrency, convertToCurrency);
            decimal? rate = y.GetExchangeRate();
            exchangeRate = rate;
            return this;
        }

        public decimal? RetrieveExchangeRate()
        {
            YahooData y = new YahooData(baseCurrency, convertToCurrency);
            decimal? rate = y.GetExchangeRate();
            exchangeRate = rate;
            return exchangeRate;
        }

        public decimal? CalculateConvertedValue(decimal value)
        {
            return exchangeRate * value;
        }
    }
}
