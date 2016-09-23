# https---github.com-blizzard25-YahooFinanceExchangeQueryLibrary

<b>Summary</b><br/>
Simple C# class library for working with the Yahoo Finance Exchange database. Library
auto-generates a YQL query, which will return an XML page containing currency exchange
rate data for a given input base currency code and code for the currency to convert to.
Also calculates the converted value if specified.

<b>Usage</b><br/>
To extract the exchange rate, just call an instance of the ExchangeRate class and set the core
code and target code in a method chain along with the exchange rate method. For example:

ExchangeRate e = new ExchangeRate();
decimal convertedValue = er.BaseCurrency(Base_Currency_Code).CurrencyToConvert(Convert_To_Currency_Code).GetExchangeRate();

To calculate the converted value, simply add the conversion method onto the method chain, passing in the value to convert as an decimal:

convertedValue = er.BaseCurrency(Base_Currency_Code).CurrencyToConvert(Convert_To_Currency_Code).GetExchangeRate();.CalculateConvertedValue(Convert.ToDecimal(Value_To_Convert));

