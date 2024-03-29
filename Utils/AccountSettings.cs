﻿using Microsoft.JSInterop;
using Newtonsoft.Json;
using PirateQuester.Utils.Chain;

namespace PirateQuester.Utils;

public class AccountSettings
{
    public IJSInProcessRuntime Js { get; }
    public AccountSettings(IJSInProcessRuntime js)
    {
        Js = js;
        var json = js.Invoke<string>("localStorage.getItem", "ChainSettings");
        List<ChainDTO> dtos = new();

        if (json is not null)
        {
            dtos = JsonConvert.DeserializeObject<List<ChainDTO>>(json);
        }

        ChainSettings = new()
        {
            new()
            {
                Id = 0,
                ChainId = 53935,
                Name = "DFK",
                Identifier = "dfk",
                RPC = dtos.Count > 0 ? dtos.FirstOrDefault(dto => dto.Name == "DFK")?.RPC ?? "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc": "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc",
                ChainEnum = ChainEnum.DFK,
                Enabled = dtos.Count > 0 ? dtos.FirstOrDefault(dto => dto.Name == "DFK")?.Enabled ?? true : true,
                HeroAddress = "0xEb9B61B145D6489Be575D3603F4a704810e143dF",
                QuestAddress = "0x530fff22987E137e7C8D2aDcC4c15eb45b4FA752",
                MeditationAddress = "0xD507b6b299d9FC835a0Df92f718920D13fA49B47",
                QuestRewarder = "0x08D93Db24B783F8eBb68D7604bF358F5027330A6",
                HeroSale = "0xc390fAA4C7f66E4D62E59C231D5beD32Ff77BEf0",
                NativeToken = "0x04b9dA42306B023f3572e106B11D82aAd9D32EBb",
                ItemConsumer = "0xc9A9F352Aa188f422A8f8902B547FB3E59D37210",
                SettingsManager = this
            },
            new()
            {
                Id = 1,
                ChainId = 8217,
                Name = "Klaytn",
                Identifier = "kla",
                RPC = dtos.Count > 0 ? dtos.FirstOrDefault(dto => dto.Name == "Klaytn")?.RPC ?? "https://klaytn.rpc.defikingdoms.com" : "https://klaytn.rpc.defikingdoms.com",
                ChainEnum = ChainEnum.Klaytn,
                Enabled = dtos.Count > 0 ? dtos.FirstOrDefault(dto => dto.Name == "Klaytn")?.Enabled ?? true : true,
                HeroAddress = "0x268CC8248FFB72Cd5F3e73A9a20Fa2FF40EfbA61",
                QuestAddress = "0x1Ac6Cd893EDdb6Cac15E5A9FC549335b8b449015",
                MeditationAddress = "0xdbEE8C336B06f2d30a6d2bB3817a3Ae0E34f4900",
                QuestRewarder = "0x3fAB563BD19CaFbf8717Cd99a605b3661Cf3391f",
                HeroSale = "0x7F2B66DB2D02f642a9eb8d13Bc998d441DDe17A8",
                NativeToken = "0xB3F5867E277798b50ba7A71C0b24FDcA03045eDF",
                ItemConsumer = "0xF78cA21d7Da3227457138714F5bEd08D2604A156",
                SettingsManager = this
            },
            new()
            {
                Id = 2,
                ChainId = 43114,
                Name = "Avalanche",
                Identifier = "ava",
                RPC = dtos.Count > 0 ? dtos.FirstOrDefault(dto => dto.Name == "Avalanche")?.RPC ?? "https://api.avax.network/ext/bc/C/rpc" : "https://api.avax.network/ext/bc/C/rpc",
                ChainEnum = ChainEnum.Avalanche,
                Enabled = dtos.Count > 0 ? dtos.FirstOrDefault(dto => dto.Name == "Avalanche")?.Enabled ?? true : true,
                SettingsManager = this
            }
        };
    }

    public void SaveChainSettings()
    {
        Js.InvokeVoid("localStorage.setItem", "ChainSettings", JsonConvert.SerializeObject(ChainSettings.Select(c => new ChainDTO()
        {
            Id = c.Id,
            Name = c.Name,
            RPC = c.RPC,
            Enabled = c.Enabled,
        })));
    }

    public List<Chain.Chain> ChainSettings { get; private set; }
}