using TanDotNet.Models;
using TanDotNet.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TanDotNet
{
    public interface ITangramClient
    {
        Task<IEnumerable<string>> WalletAddresses(Wallet wallet);
        Task<double> WalletBalance(Wallet wallet);
        Task<Wallet> WalletCreate(string mnemonic = null,
            string passphrase = null);
        Task<IEnumerable<WalletTransaction>> WalletHistory(Wallet wallet);
        Task<IEnumerable<string>> WalletList();
        Task<WalletMnemonic> WalletMnemonic(int language = 0,
            int mnemonicWordCount = 24,
            int passphraseWordCount = 12);
        Task<WalletReceive> WalletReceive(Wallet wallet,
            string paymentId);
        Task<WalletSpend> WalletSpend(Wallet wallet,
            string address,
            double amount,
            string memo = null);
        Task<WalletProtobuf> WalletTransaction(byte[] payment);
    }
}