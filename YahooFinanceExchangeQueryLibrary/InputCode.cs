using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YahooFinanceExchangeQueryLibrary
{
    public class InputCode
    {

        public InputCode(string combinedCodes)
        {
            id = combinedCodes;
        }

        private string name;
        private string id;
        private decimal? rate;
        private decimal? ask;
        private decimal? bid;
        private DateTime? date;
        private DateTime lastUpdate;

        public DateTime LastUpdate
        {
            get
            {
                return lastUpdate;
            }
            set
            {
                lastUpdate = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public decimal? Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
            }
        }
        public DateTime? Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public decimal? Ask
        {
            get
            {
                return ask;
            }
            set
            {
                ask = value;
            }
        }
        public decimal? Bid
        {
            get
            {
                return bid;
            }
            set
            {
                bid = value;
            }
        }
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
