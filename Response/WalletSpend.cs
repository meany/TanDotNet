using System;
using System.Collections.Generic;
using System.Text;
using TanDotNet.Models;

namespace TanDotNet.Response
{
    public class WalletSpend
    {
        public double Balance { get; set; }
        public string PaymentId { get; set; }
    }
}
