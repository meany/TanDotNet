# TanDotNet
[![NuGet](https://img.shields.io/nuget/v/TanDotNet.svg?style=flat&logo=nuget&color=blue)](https://www.nuget.org/packages/TanDotNet)
![build status](https://github.com/meany/TanDotNet/workflows/build/badge.svg "build status")


.NET Standard 2.0 library for working with the Tangram wallet REST API ([Cypher](https://github.com/tangramproject/Cypher))

Features
----
* Wrapper around API calls by creating a `TangramClient` and calling the methods within it

Roadmap
----
* wen nodes
* wen mainnet

Required Dependencies
----
* Newtonsoft.Json
* RestSharp

Example usage
----
```c#
using (var client = new TangramClient("http://localhost:8001"))
{
    var wallet = await client.WalletBalance(new WalletAccount
    {
        Identifier = "id_a1b2c3d4e5f6...",
        Password = "pingpong was hurled into the sun..."
    });

    Console.WriteLine($"Wallet balance: {wallet.Balance}");

    var wallet2 = await client.WalletSend(new WalletAccount
    {
        Identifier = "id_a1b2c3d4e5f6...",
        Password = "pingpong was hurled into the sun..."
    },
        amount: 1000,
        destination: "tgm_P8DqGWNZ...",
        memo: "for the pizza");

    Console.WriteLine($"New balance: {wallet2.Balance}");
}
```

Contribution
----
PRs and issues are appreciated. 
