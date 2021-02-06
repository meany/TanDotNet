using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Response
{
    public class WalletReceive
    {
        public string Memo { get; set; }
        public string Received { get; set; }
        public double Balance { get; set; }
    }
}
