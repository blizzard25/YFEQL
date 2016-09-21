using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    public class CurrencyCodeViewModel
    {
        public static List<InputCode> InputCodes { get; set; }

        public CurrencyCodeViewModel(List<InputCode> currCodes)
        {

            foreach(InputCode c in currCodes)
            {
                AddCurrencyCodes(c);
            }

            YahooData.Fetch(InputCodes);
        }

        public static void AddCurrencyCodes(InputCode currCode)
        {
            InputCodes.Add(currCode);
        }
    }
}
