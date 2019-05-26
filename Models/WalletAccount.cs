using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Models
{
    public class WalletAccount
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }

        public WalletAccount()
        {

        }
    }
}
