using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceExchangeQueryLibrary
{
    public class YahooData
    {
        static string coreCode = "USD";

        private const string BASE_URL = "http://query.yahooapis.com/v1/public/yrl?r=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20({0})&env=store://datatables.org/alltableswithkeys";


        public static void Fetch(List<InputCode> inputCodes)
        {
            string idList = String.Join(coreCode, inputCodes.Select(w => String.Format("%20{0}",w.Id).ToArray()));
            string url = string.Format(BASE_URL, idList);
            XDocument doc = XDocument.Load(url);
            Parse(inputCodes, doc);
            System.Diagnostics.Debug.WriteLine(url);
            System.Diagnostics.Debug.WriteLine(idList);
        }


        private static void Parse(List<InputCode> inputCodes, XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            foreach (InputCode ic in inputCodes)
            {
                XElement r = results.Elements("rate").FirstOrDefault(w => w.Attribute("id").Value == ic.Id);

                ic.Name = r.Element("Name").Value;
                ic.Rate = GetDecimal(r.Element("Rate").Value);
                ic.Date = GetDateTime(String.Format("{0} {1}", r.Element("Date").Value, r.Element("Time").Value));
                ic.Ask = GetDecimal(r.Element("Ask").Value);
                ic.Bid = GetDecimal(r.Element("Bid").Value);

                ic.LastUpdate = DateTime.Now;
            }
        }

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;
            decimal value;
            if (Decimal.TryParse(input, out value)) return value;
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }
    }
}

