using TanDotNet.Models;
using TanDotNet.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TanDotNet
{
    public interface ITangramClient
    {
        Task<WalletAccount> WalletCreate();
        Task<WalletBalance> WalletBalance(WalletAccount wallet);
        Task<IEnumerable<string>> WalletList();
        Task<WalletProfile> WalletProfile(WalletAccount wallet);
        Task<WalletReceive> WalletReceive(WalletAccount wallet);
        Task<WalletSend> WalletSend(WalletAccount wallet, int amount, string destination, string memo = null);
        Task<IEnumerable<WalletTransaction>> WalletTransactions(WalletAccount wallet);
        Task<WalletVaultUnseal> WalletVaultUnseal(string shard);
    }
}