using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Response
{
    public class WalletTransaction
    {
        public int Amount { get; set; }
        public string Blind { get; set; }
        public string Commitment { get; set; }
        public string Hash { get; set; }
        public string Stamp { get; set; }
        public int Version { get; set; }
        public int TransactionType { get; set; }
        public string Memo { get; set; }
        public DateTime DateTime { get; set; }
    }
}
