using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Models
{
    public class Wallet
    {
        public string Path { get; set; }
        public string Identifier { get; set; }
        public string Mnemonic { get; set; }
        public string Passphrase { get; set; }
        public string Address { get; set; }

        public Wallet()
        {

        }
    }
}
