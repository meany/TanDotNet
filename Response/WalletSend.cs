using System;
using System.Collections.Generic;
using System.Text;
using TanDotNet.Models;

namespace TanDotNet.Response
{
    public class WalletSend
    {
        public int Balance { get; set; }
        public RedemptionMessage Message { get; set; }
    }
}
