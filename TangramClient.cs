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

        private async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
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

        private async Task<T> GetAsync<T>(RequestEndPoint endpoint) where T : new()
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

        private async Task<T> PostAsync<T>(RequestEndPoint endpoint, object body) where T : new()
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
        public async Task<WalletAccount> CreateWallet()
        {
            var wallet = await GetAsync<WalletAccount>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Create
            });

            var profile = await PostAsync<WalletProfile>(new RequestEndPoint
            {
                Group = Group.Wallet,
                Method = Profile
            }, new
            {
                Identifier = wallet.Identifier,
                Password = wallet.Password
            });

            wallet.Address = profile.Data.StoreKeys[0].Address;
            wallet.PublicKey = profile.Data.StoreKeys[0].PublicKey;
            wallet.SecretKey = profile.Data.StoreKeys[0].SecretKey;

            return wallet;
        }

        // TODO: get wallet address

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

        public async Task<WalletReceive> WalletReceive(WalletAccount wallet)
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
                FromAddress = wallet.Address
            });
        }

        public async Task<WalletSend> WalletSend(WalletAccount wallet, int amount, string destination, string memo = null)
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
                Memo = memo
            });
        }

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
