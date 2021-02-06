using TanDotNet.Models;
using TanDotNet.Request;
using TanDotNet.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Method = RestSharp.Method;
using Action = TanDotNet.Request.Action;

namespace TanDotNet
{
    /// <summary>
    /// Provides methods to call API methods on a Tangram node
    /// </summary>
    public class TangramClient : IDisposable, ITangramClient
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private RestClient client;

        public TangramClient(string nodeApiUrl)
        {
            client = new RestClient(nodeApiUrl);
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            client = null;
        }

        private async Task<T> ExecuteAsync<T>(RestRequest request)
        {
            var response = await client.ExecuteAsync<T>(request, cancellationTokenSource.Token);

            if (response.ErrorException != null || response.Data == null)
            {
                var ex = new ApplicationException("Error retrieving response. Check log for more info.",
                    response.ErrorException);
                throw ex;
            }

            return response.Data;
        }

        private async Task<T> GetAsync<T>(RequestEndpoint endpoint, object body = null)
        {
            try
            {
                var request = new RestRequest($"{endpoint.Group}/{endpoint.Action}", Method.GET);
                if (body != null)
                    request.AddJsonBody(body);

                return await ExecuteAsync<T>(request);
            }
            catch
            {
                throw;
            }
        }

        private async Task<T> PostAsync<T>(RequestEndpoint endpoint, object body = null)
        {
            try
            {
                var request = new RestRequest($"{endpoint.Group}/{endpoint.Action}", Method.POST);
                if (body != null)
                    request.AddJsonBody(body);

                return await ExecuteAsync<T>(request);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> WalletAddresses(Wallet wallet)
        {
            return await PostAsync<IEnumerable<string>>(
                new RequestEndpoint(Group.Wallet, Action.Addresses),
                wallet);
        }

        /// <inheritdoc />
        public async Task<double> WalletBalance(Wallet wallet)
        {
            return await PostAsync<double>(
                new RequestEndpoint(Group.Wallet, Action.Balance),
                wallet);
        }

        /// <inheritdoc />
        public async Task<Wallet> WalletCreate(string mnemonic = null, string passphrase = null)
        {
            dynamic data = null;

            if (mnemonic != null)
                data = new
                {
                    Mnemonic = mnemonic
                };

            if (passphrase != null)
                data = new
                {
                    Mnemonic = data.Mnemonic ?? null,
                    Passphrase = passphrase
                };

            return await GetAsync<Wallet>(
                new RequestEndpoint(Group.Wallet, Action.Create),
                data);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<WalletTransaction>> WalletHistory(Wallet wallet)
        {
            return await PostAsync<IEnumerable<WalletTransaction>>(
                new RequestEndpoint(Group.Wallet, Action.History),
                wallet);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> WalletList()
        {
            return await GetAsync<IEnumerable<string>>(
                new RequestEndpoint(Group.Wallet, Action.List));
        }

        /// <inheritdoc />
        public async Task<WalletMnemonic> WalletMnemonic(int language = 0, int mnemonicWordCount = 24, int passphraseWordCount = 12)
        {
            return await GetAsync<WalletMnemonic>(
                new RequestEndpoint(Group.Wallet, Action.Mnemonic),
                new
                {
                    language,
                    mnemonicWordCount,
                    passphraseWordCount
                });
        }

        /// <inheritdoc />
        public async Task<WalletReceive> WalletReceive(Wallet wallet, string paymentId)
        {
            return await PostAsync<WalletReceive>(
                new RequestEndpoint(Group.Wallet, Action.Receive),
                new
                {
                    wallet.Identifier,
                    wallet.Passphrase,
                    paymentId
                });
        }

        /// <inheritdoc />
        public async Task<WalletSpend> WalletSpend(Wallet wallet, string address, double amount, string memo = null)
        {
            return await PostAsync<WalletSpend>(
                new RequestEndpoint(Group.Wallet, Action.Spend),
                new
                {
                    wallet.Identifier,
                    wallet.Passphrase,
                    address,
                    amount,
                    memo
                });
        }

        /// <inheritdoc />
        public async Task<WalletProtobuf> WalletTransaction(byte[] payment)
        {
            return await PostAsync<WalletProtobuf>(
                new RequestEndpoint(Group.Wallet, Action.Transaction),
                payment);
        }
    }
}
