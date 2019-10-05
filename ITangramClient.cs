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
        Task<IEnumerable<WalletKeySet>> WalletKeySets(WalletAccount wallet);
        Task<WalletReceive> WalletReceive(WalletAccount wallet, RedemptionMessage message = null);
        Task<WalletSend> WalletSend(WalletAccount wallet, ulong amount, string destination, bool createRedemptionMessage = false, string memo = null);
        Task<IEnumerable<WalletTransaction>> WalletTransactions(WalletAccount wallet);
        Task<WalletVaultUnseal> WalletVaultUnseal(string shard);
    }
}