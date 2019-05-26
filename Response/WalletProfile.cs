using TanDotNet.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanDotNet.Response
{
    public class WalletProfile
    {
        public Guid RequestId { get; set; }
        public string LeaseId { get; set; }
        public bool Renewable { get; set; }
        public int LeaseDuration { get; set; }
        public WalletProfileData Data { get; set; }
        public string WrapInfo { get; set; }
        public string Warnings { get; set; }
        public string Auth { get; set; }
    }

    public class WalletProfileData
    {
        public List<WalletProfileDataStoreKey> StoreKeys { get; set; }
    }

    public class WalletProfileDataStoreKey
    {
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
