using TanDotNet.Models;
using TanDotNet.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TanDotNet
{
    public interface ITangramClient
    {
        Task<WalletAccount> CreateWallet();
        Task<WalletBalance> WalletBalance(WalletAccount account);
        Task<WalletReceive> WalletReceive(WalletAccount wallet);
        Task<WalletSend> WalletSend(WalletAccount wallet, int amount, string destination, string memo = null);
        Task<WalletVaultUnseal> WalletVaultUnseal(string shard);
    }
}