using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Response
{
    public class WalletTransaction
    {
        public DateTime DateTime { get; set; }
        public string CoinType { get; set; }
        public string Memo { get; set; }
        public string MoneyOut { get; set; }
        public string Fee { get; set; }
        public string MoneyIn { get; set; }
        public string Reward { get; set; }
        public string Balance { get; set; }
    }
}
