using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    public interface IExchangeRate
    {
        IExchangeRate BaseCurrency(string c);
        IExchangeRate CurrencyToConvert(string c);
        IExchangeRate GetExchangeRate();
        decimal? RetrieveExchangeRate();
        decimal? CalculateConvertedValue(decimal baseValue);
    }
}
