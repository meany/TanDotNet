using TanDotNet.Models;
using TanDotNet.Request;
using TanDotNet.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static TanDotNet.Request.Method;

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
            var response = await client.ExecuteTaskAsync<T>(request, cancellationTokenSource.Token);

            if (response.ErrorException != null || response.Data == null)
            {
                var ex = new ApplicationException("Error retrieving response. Check log for more info.",
                    response.ErrorException);
                throw ex;
            }

            return response.Data;
        }

        private async Task<T> GetAsync<T>(RequestEndPoint endpoint)
        {
            try
            {
                var request = new RestRequest($"{endpoint.Group}/{endpoint.Method}", RestSharp.Method.GET);
                return await ExecuteAsync<T>(request);
            }
            catch
            {
                throw;
            }
        }

        private async Task<T> PostAsync<T>(RequestEndPoint endpoint, object body)
        {
            try
            {
                var request = new RestRequest($"{endpoint.Group}/{endpoint.Method}", RestSharp.Method.POST);
                request.AddJsonBody(body);
                return await ExecuteAsync<T>(request);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<WalletAccount> WalletCreate()
        {
            var wallet = await GetAsync<WalletAccount>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Create
            });

            var profile = await WalletKeySets(wallet);

            wallet.Address = profile.FirstOrDefault().Address;
            wallet.PublicKey = profile.FirstOrDefault().PublicKey;
            wallet.SecretKey = profile.FirstOrDefault().SecretKey;

            return wallet;
        }

        /// <inheritdoc />
        public async Task<WalletBalance> WalletBalance(WalletAccount wallet)
        {
            return await PostAsync<WalletBalance>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Balance
            }, new
            {
                Identifier = wallet.Identifier,
                Password = wallet.Password
            });
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> WalletList()
        {
            return await GetAsync<IEnumerable<string>>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = List
            });
        }

        /// <inheritdoc />
        public async Task<IEnumerable<WalletKeySet>> WalletKeySets(WalletAccount wallet)
        {
            return await PostAsync<IEnumerable<WalletKeySet>>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Profile
            }, new
            {
                Identifier = wallet.Identifier,
                Password = wallet.Password
            });
        }

        /// <inheritdoc />
        public async Task<WalletReceive> WalletReceive(WalletAccount wallet, RedemptionMessage message = null)
        {
            return await PostAsync<WalletReceive>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Receive
            }, new
            {
                Credentials = new
                {
                    Identifier = wallet.Identifier,
                    Password = wallet.Password
                },
                FromAddress = wallet.Address,
                RedemptionMessage = message
            });
        }

        /// <inheritdoc />
        public async Task<WalletSend> WalletSend(WalletAccount wallet, ulong amount, string destination, bool createRedemptionKey = false, string memo = null)
        {
            return await PostAsync<WalletSend>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Send
            }, new
            {
                Credentials = new
                {
                    Identifier = wallet.Identifier,
                    Password = wallet.Password
                },
                Amount = amount,
                ToAddress = destination,
                CreateRedemptionKey = createRedemptionKey,
                Memo = memo
            });
        }

        /// <inheritdoc />
        public async Task<IEnumerable<WalletTransaction>> WalletTransactions(WalletAccount wallet)
        {
            return await PostAsync<IEnumerable<WalletTransaction>>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Transactions
            }, new
            {
                Identifier = wallet.Identifier,
                Password = wallet.Password
            });
        }

        /// <inheritdoc />
        public async Task<WalletVaultUnseal> WalletVaultUnseal(string shard)
        {
            return await PostAsync<WalletVaultUnseal>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = VaultUnseal
            }, new
            {
                Shard = shard
            });
        }
    }
}
