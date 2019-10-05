using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Response
{
    public enum TransactionType
    {
        Send,
        Receive
    }

    public class WalletTransaction
    {
        public string TransactionId { get; set; }
        public ulong Amount { get; set; }
        public string Blind { get; set; }
        public string Commitment { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Stamp { get; set; }
        public int Version { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Memo { get; set; }
        public DateTime DateTime { get; set; }
    }
}
