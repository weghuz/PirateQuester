using Microsoft.JSInterop;
using Newtonsoft.Json;
using PirateQuester.Utils.Chain;
using Syncfusion.Blazor.Diagram;

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
                Id = 53935,
                Name = "DFK",
                Identifier = "dfk",
				RPC = dtos.Count > 0 ? dtos.First(dto => dto.Name == "DFK").RPC : "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc",
                ChainEnum = ChainEnum.DFK,
				Enabled = dtos.Count > 0 ? dtos.First(dto => dto.Name == "DFK").Enabled : true,
				HeroAddress = "0xEb9B61B145D6489Be575D3603F4a704810e143dF",
                QuestAddress = "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154",
                MeditationAddress = "0xD507b6b299d9FC835a0Df92f718920D13fA49B47",
                QuestRewarder = "0x08D93Db24B783F8eBb68D7604bF358F5027330A6",
				SettingsManager = this
			},
            new()
            {
                Id = 8217,
                Name = "Klaytn",
                Identifier = "kla",
				RPC = dtos.Count > 0 ? dtos.First(dto => dto.Name == "Klaytn").RPC : "https://klaytn.rpc.defikingdoms.com",
                ChainEnum = ChainEnum.Klaytn,
				Enabled = dtos.Count > 0 ? dtos.First(dto => dto.Name == "Klaytn").Enabled : true,
				HeroAddress = "0x268CC8248FFB72Cd5F3e73A9a20Fa2FF40EfbA61",
                QuestAddress = "0x8dc58d6327E1f65b18B82EDFb01A361f3AAEf624",
                MeditationAddress = "0xdbEE8C336B06f2d30a6d2bB3817a3Ae0E34f4900",
                QuestRewarder = "0x3fAB563BD19CaFbf8717Cd99a605b3661Cf3391f",
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