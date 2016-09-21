using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    class MainClass
    {
        public static List<InputCode> InputCodes = new List<InputCode>();

        static int Main(string[] args)
        {
            InputCodes.Add(new InputCode("USD"));
            InputCodes.Add(new InputCode("EUR"));
            InputCodes.Add(new InputCode("GBP"));
            CurrencyCodeViewModel cm = new CurrencyCodeViewModel(InputCodes);
            System.Diagnostics.Debug.WriteLine(CurrencyCodeViewModel.InputCodes.ToString());

            return 0;
        }
    }
}
