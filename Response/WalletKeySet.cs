using TanDotNet.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Response
{
    public class WalletKeySet
    {
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
