using PirateQuester.Utils.Chain;

namespace PirateQuester.Utils;

public class AccountSettings
{
    public List<Chain.Chain> ChainSettings { get; } = new()
        {
            new()
            {
                Id = 53935,
                Name = "DFK",
                Identifier = "dfk",
                RPC = "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc",
                ChainEnum = Chains.DFK,
                Enabled = true,
                HeroAddress = "0xEb9B61B145D6489Be575D3603F4a704810e143dF",
                QuestAddress = "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154",
                MeditationAddress = "0xD507b6b299d9FC835a0Df92f718920D13fA49B47",
				QuestRewarder = "0x08D93Db24B783F8eBb68D7604bF358F5027330A6"
			},
            new()
            {
                Id = 8217,
                Name = "Klaytn",
                Identifier = "kla",
                RPC = "https://klaytn.rpc.defikingdoms.com",
				ChainEnum = Chains.Klaytn,
				Enabled = true,
                HeroAddress = "0x268CC8248FFB72Cd5F3e73A9a20Fa2FF40EfbA61",
                QuestAddress = "0x8dc58d6327E1f65b18B82EDFb01A361f3AAEf624",
                MeditationAddress = "0xdbEE8C336B06f2d30a6d2bB3817a3Ae0E34f4900",
                QuestRewarder = "0x3fAB563BD19CaFbf8717Cd99a605b3661Cf3391f"
			}
        };
}