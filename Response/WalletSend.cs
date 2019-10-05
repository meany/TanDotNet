using System;
using System.Collections.Generic;
using System.Text;
using TanDotNet.Models;

namespace TanDotNet.Response
{
    public class WalletSend
    {
        public ulong Balance { get; set; }
        public RedemptionMessage Message { get; set; }
    }
}
