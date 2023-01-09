using PirateQuester.Utils;
using PirateQuester.Utils.Chain;

namespace PirateQuester.DFK.Items
{
	public partial class ItemContractDefinitions
	{
		public static readonly List<DFKItem> InventoryItems = new List<DFKItem>()
		{
			new()
			{
				Id = 0,
				TokenName = "JEWEL",
				Name = "Jewel",
				Description = "",
				Addresses = new(){
					new()
					{
						Address = "0xCCb93dABD71c8Dad03Fc4CE5559dC3D89F67a260",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x30C103f8f5A3A732DFe2dCE1Cc9446f545527b43",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    }
				},
				Category = ItemCategory.Currency,
				ItemEnum = DFKItemEnum.Jewel,
				Image = "Images/Jewel.png",
				Decimals = 18
			},
			new()
			{
				Id = 1,
				TokenName = "CRYSTAL",
				Name = "Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x04b9dA42306B023f3572e106B11D82aAd9D32EBb",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    }
                },
				Category = ItemCategory.Currency,
				ItemEnum = DFKItemEnum.Crystal,
				Image = "Images/Crystal.png",
				Decimals = 18
			},
			new()
			{
				Id = 2,
				TokenName = "JADE",
				Name = "Jade",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xB3F5867E277798b50ba7A71C0b24FDcA03045eDF",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    }
                },
				Category = ItemCategory.Currency,
				ItemEnum = DFKItemEnum.Crystal,
				Image = "Images/Jade.png",
				Decimals = 18
			},
			new()
			{
				Id = 3,
				TokenName = "DFKSHVAS",
				Name = "Shvas Rune",
				Description = "This rune pulses with power. Watching it, you begin to breathe in its rhythm...",
                Addresses = new(){
                    new()
                    {
                        Address = "0x75E8D8676d774C9429FbB148b30E304b5542aC3d",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x907a98319AEB249e387246637149f4B2e7D21dB7",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Image = "Images/Items/shvas-rune.gif",
				Category = ItemCategory.Rune,
				ItemEnum = DFKItemEnum.DFKSHVAS,
				Decimals = 0
			},
			new()
			{
				Id = 4,
				TokenName = "DFKMOKSHA",
				Name = "Moksha Rune",
				Description = "The rune pulses in your palm, the burdens on your mind witness liberation...",
                Addresses = new(){
                    new()
                    {
                        Address = "0xCd2192521BD8e33559b0CA24f3260fE6A26C28e4",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xd0223143057Eb44065e789b202E03A5869a6006C",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Rune,
				ItemEnum = DFKItemEnum.DFKMOKSHA,
				Image = "Images/Items/moksha-rune.gif",
				Decimals = 0
			},
			new()
			{
				Id = 5,
				TokenName = "DFKGOLD",
				Name = "Gold",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x576C260513204392F0eC0bc865450872025CB1cA",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xe7a1B580942148451E47b92e95aEB8d31B0acA37",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Currency,
				ItemEnum = DFKItemEnum.Gold,
				Image = "Images/Items/gold-pile.png",
				Decimals = 3
			},
			new()
			{
				Id = 6,
				TokenName = "DFKTEARS",
				Name = "Gaia’s Tears",
				Description = "A crystal that, when attuned properly, can summon heroes from faraway lands.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x79fE1fCF16Cc0F7E28b4d7B97387452E3084b6dA",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x8Be0cbA3c8c8F392408364ef21dfCF714A918234",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Summoning,
				ItemEnum = DFKItemEnum.DFKTEARS,
				Image = "Images/Items/gaias-tear.png",
				Decimals = 0
			},
			new()
			{
				Id = 7,
				TokenName = "DFKBLUEEGG", 
				Name = "Blue Pet Egg", 
				Description = "An aquatic-looking egg.",
                Addresses = new(){
                    new()
                    {
                        Address = "0xa61Bac689AD6867a605633520D70C49e1dCce853",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x29ADd7D022c591D56eb4aFd262075dA900C67ab1",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Pets, 
				ItemEnum = DFKItemEnum.DFKBLUEEGG,
				Image = "Images/Items/pet-egg-blue.png",
				Decimals = 0
			},
			new()
			{
				Id = 8,
				TokenName = "DFKGOLDEGG", 
				Name = "Golden Pet Egg", 
				Description = "Looks like real gold. Is it though?",
                Addresses = new(){
                    new()
                    {
                        Address = "0xf2D479DaEdE7F9e270a90615F8b1C52F3C487bC7",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xc9731BE04F217543E3010cCbf903E858EFde840f",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Pets, 
				ItemEnum = DFKItemEnum.DFKGOLDEGG,
				Image = "Images/Items/pet-egg-golden.gif",
				Decimals = 0
			},
			new()
			{
				Id = 9,
				TokenName = "DFKGREENEGG", 
				Name = "Green Pet Egg", 
				Description = "If you put your ear against it, you can hear a soft noise.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x8D2bC53106063A37bb3DDFCa8CfC1D262a9BDCeB",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xb1Ec534fBBfEBd4563A4B0055E744286CE490f26",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Pets, 
				ItemEnum = DFKItemEnum.DFKGREENEGG,
				Image = "Images/Items/pet-egg-green.png",
				Decimals = 0
			},
			new()
			{
				Id = 10,
				TokenName = "DFKGREGG",
				Name = "Grey Pet Egg",
				Description = "An egg that reminds you of the forest.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x7E121418cC5080C96d967cf6A033B0E541935097",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xfd29ebdE0dd1331C19BBF54518df94b442ACb38C",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Pets,
				ItemEnum = DFKItemEnum.DFKGREGG,
				Image = "Images/Items/pet-egg-grey.png",
				Decimals = 0
			},
			new()
			{
				Id = 11,
				TokenName = "DFKYELOWEGG",
				Name = "Grey Pet Egg",
				Description = "You sense something slumbering inside.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x72F860bF73ffa3FC42B97BbcF43Ae80280CFcdc3",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x0A73aF98781bad9BCb80A71241F129EA877eF1b7",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Pets,
				ItemEnum = DFKItemEnum.DFKYELOWEGG,
				Image = "Images/Items/pet-egg-yellow.png",
				Decimals = 0
			},
			new()
			{
				Id = 12,
				TokenName = "DFKAMBRTFY",
				Name = "Ambertaffy",
				Description = "It bends but it doesn’t break. Doesn’t taste great, though.",
                Addresses = new(){
                    new()
                    {
                        Address = "0xB78d5580d6D897DE60E1A942A5C1dc07Bc716943",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x75E8D8676d774C9429FbB148b30E304b5542aC3d",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKAMBRTFY,
				Image = "Images/Items/ambertaffy.png",
				Decimals = 0
			},
			new()
			{
				Id = 13,
				TokenName = "DFKBLUESTEM",
				Name = "Bluestem",
				Description = "Beautiful leaves. Why does blue always remind you of mana?",
                Addresses = new(){
                    new()
                    {
                        Address = "0x0776b936344DE7bd58A4738306a6c76835ce5D3F",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xDbd4fA2D2C62C6c60957a126970e412Ed6AC1bD6",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKBLUESTEM,
				Image = "Images/Items/bluestem.png",
				Decimals = 0
			},
			new()
			{
				Id = 14,
				TokenName = "DFKDRKWD",
				Name = "Darkweed",
				Description = "A root, always found in dark places, that can deliver others from darkness.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x848Ac8ddC199221Be3dD4e4124c462B806B6C4Fd",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xEDFBe9EEf42FfAf8909EC9Ce0d79850BA0C232FE",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKDRKWD,
				Image = "Images/Items/darkweed.png",
				Decimals = 0
			},
			new()
			{
				Id = 15,
				TokenName = "DFKIRONSCALE",
				Name = "Ironscale",
				Description = "The Knight of the Lake. Its scales are as hard as armor.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x04B43D632F34ba4D4D72B0Dc2DC4B30402e5Cf88",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xBcdD90034eB73e7Aec2598ea9082d381a285f63b",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKIRONSCALE,
				Image = "Images/Items/ironscale.png",
				Decimals = 0
			},
			new()
			{
				Id = 15,
				TokenName = "DFKLANTERNEYE",
				Name = "Lanterneye",
				Description = "Known to have a connection to magic. Don’t go toward the light...",
                Addresses = new(){
                    new()
                    {
                        Address = "0xc2Ff93228441Ff4DD904c60Ecbc1CfA2886C76eB",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x80A42Dc2909C0873294c5E359e8DF49cf21c74E4",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLANTERNEYE,
				Image = "Images/Items/lanterneye.png",
				Decimals = 0
			},
			new()
			{
				Id = 16,
				TokenName = "DFKMILKWEED",
				Name = "Milkweed",
				Description = "Pure white, like its namesake. Feeder of butterflies and provider of magic resistance.",
                Addresses = new(){
                    new()
                    {
                        Address = "0xA2cef1763e59198025259d76Ce8F9E60d27B17B5",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xE408814828f2b51649473c1a05B861495516B920",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKMILKWEED,
				Image = "Images/Items/milkweed.png",
				Decimals = 0
			},
			new()
			{
				Id = 17,
				TokenName = "DFKRCKRT",
				Name = "Rockroot",
				Description = "Linked to healing. Its ability to grow in such inhospitable conditions is remarkable.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x60170664b52c035Fcb32CF5c9694b22b47882e5F",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xf2D479DaEdE7F9e270a90615F8b1C52F3C487bC7",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKRCKRT,
				Image = "Images/Items/rockroot.png",
				Decimals = 0
			},
			new()
			{
				Id = 18,
				TokenName = "DFKSAILFISH",
				Name = "Sailfish",
				Description = "Its dorsal fin resembles a sail, and it’s an incredibly fast swimmer.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x7f46E45f6e0361e7B9304f338404DA85CB94E33D",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xc6030Afa09EDec1fd8e63a1dE10fC00E0146DaF3",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKSAILFISH,
				Image = "Images/Items/sailfish.png",
				Decimals = 0
			},
			new()
			{
				Id = 18,
				TokenName = "DFKSHIMMERSKIN",
				Name = "Shimmerskin",
				Description = "The iridescent beauty of its scales hints at great power.",
                Addresses = new(){
                    new()
                    {
                        Address = "0xd44ee492889C078934662cfeEc790883DCe245f3",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xa61Bac689AD6867a605633520D70C49e1dCce853",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKSHIMMERSKIN,
				Image = "Images/Items/shimmerskin.png",
				Decimals = 0
			},
			new()
			{
				Id = 18,
				TokenName = "DFKSKNSHADE",
				Name = "Skunk Shade",
				Description = "A dark striped plant with a malodorous musk.",
                Addresses = new(){
                    new()
                    {
                        Address = "0xc6030Afa09EDec1fd8e63a1dE10fC00E0146DaF3",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x874FC0015ece1d77ba3D5668F16c46ba72913239",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKSKNSHADE,
				Image = "Images/Items/skunkShade.png",
				Decimals = 0
			},
			new()
			{
				Id = 18,
				TokenName = "DFKSPIDRFRT",
				Name = "Spiderfruit",
				Description = "A dark striped plant with a malodorous musk.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x3E022D84D397F18743a90155934aBAC421D5FA4C",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x08D93Db24B783F8eBb68D7604bF358F5027330A6",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKSPIDRFRT,
				Image = "Images/Items/spider-fruit.png",
				Decimals = 0
			},
			new()
			{
				Id = 19,
				TokenName = "DFKSWFTHSL",
				Name = "Swift-Thistle",
				Description = "The purple flowers are known to enhance speed when used correctly, hence the name.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x97b25DE9F61BBBA2aD51F1b706D4D7C04257f33A",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xCd2192521BD8e33559b0CA24f3260fE6A26C28e4",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKSWFTHSL,
				Image = "Images/Items/swift-thistle.png",
				Decimals = 0
			},
			new()
			{
				Id = 20,
				TokenName = "DFKTHREEL",
				Name = "Three-Eyed Eel",
				Description = "An eel whose gelatinous eyes can be used in various concoctions.",
                Addresses = new(){
                    new()
                    {
                        Address = "0x6513757978E89e822772c16B60AE033781A29A4F",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x7E1298EBF3a8B259561df6E797Ff8561756E50EA",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKTHREEL,
				Image = "Images/Items/threeEyedEel.png",
				Decimals = 0
			},
			new()
			{
				Id = 21,
				TokenName = "DFKLCHSCR",
				Name = "Lesser Chaos Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xeEe5b16Cc49e7cef65391Fe7325cea17f787e245",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x537E800b8fD22Dc76A438Af8b9923986A5487853",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLCHSCR,
				Image = "Images/Items/chaos-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 22,
				TokenName = "DFKLFINCR",
				Name = "Lesser Finesse Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x9d9ef1Bf6A46b8413bf6b1b54F6A7aAb53c6b1b6",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xC3B36a02f360c3d18042bF3533be602cb775007A",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLFINCR,
				Image = "Images/Items/insight-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 23,
				TokenName = "DFKLFRTICR",
				Name = "Lesser Fortitude Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xbd2677c06C9448534A851bdD25dF045872b87cb1",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x1E672a8385b39E13267efA2Fb39f574a2a23AE9F",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLFRTICR,
				Image = "Images/Items/fortitude-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 24,
				TokenName = "DFKLFRTUCR",
				Name = "Lesser Fortune Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xE410b2BE2Ce1508E15009118567d02C6d7A7038e",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x8baD15B5C531d119b328d0F716a6B9D90CeDa88A",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLFRTUCR,
				Image = "Images/Items/fortune-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 25,
				TokenName = "DFKLINSCR",
				Name = "Lesser Insight Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xbb5F97358F60cCBa262883A3Ff0C637393FE3aB8",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x5f967E325E91977B42D2591Fc2f57da75Ee4490B",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLINSCR,
				Image = "Images/Items/insight-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 26,
				TokenName = "DFKLMGHTCR",
				Name = "Lesser Might Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x5bAC3cAd961B01Ef9510C8e6c5402A2bB1542831",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x80Ab38fc9fA0a484b98d5600147e7C695627747D",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLMGHTCR,
				Image = "Images/Items/might-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 27,
				TokenName = "DFKLSWFTCR",
				Name = "Lesser Swiftness Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x6BCA53314dADdA7f4De30A95413f75a93bfAfecF",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x32Cbbfd741EB7634818aa2e3E8502367cB6602BE",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLSWFTCR,
				Image = "Images/Items/swiftness-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 28,
				TokenName = "DFKLVGRCR",
				Name = "Lesser Vigor Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x5e4Cf6907CB5fBe2F642E399F6d07E567155d1F8",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x6C7AF7483b050a00b5fbC4241eD06944c5f0bD77",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLVGRCR,
				Image = "Images/Items/vigor-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 29,
				TokenName = "DFKLWITCR",
				Name = "Lesser Wit Crystal",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xC989c916F189D2A2BE0322c020942d7c43aEa830",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xf15035b5eD13Feb18f63D829ABc1c3139041e7C2",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLWITCR,
				Image = "Images/Items/wit-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 30,
				TokenName = "DFKBLOATER",
				Name = "Bloater",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x268CC8248FFB72Cd5F3e73A9a20Fa2FF40EfbA61",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x72F860bF73ffa3FC42B97BbcF43Ae80280CFcdc3",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKBLOATER,
				Image = "Images/Items/bloater.png",
				Decimals = 0
			},
			new()
			{
				Id = 31,
				TokenName = "DFKFBLOATER",
				Name = "Frost Bloater",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x3bcb9A3DaB194C6D8D44B424AF383E7Db51C82BD",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x18cB286EeCE992f79f601E49acde1D1F5dE32a30",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKFBLOATER,
				Image = "Images/Items/frost-bloater.png",
				Decimals = 0
			},
			new()
			{
				Id = 32,
				TokenName = "DFKFROSTDRM",
				Name = "Frost Drum",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xe7a1B580942148451E47b92e95aEB8d31B0acA37",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xD69542aBE74413242e387Efb9e55BE6A4863ca10",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKFROSTDRM,
				Image = "Images/Items/frost-drum.png",
				Decimals = 0
			},
			new()
			{
				Id = 31,
				TokenName = "DFKGLDVN",
				Name = "Goldvein",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x0096ffda7A8f8E00e9F8Bbd1cF082c14FA9d642e",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xeaF833A0Ae97897f6F69a728C9c17916296cecCA",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKGLDVN,
				Image = "Images/Items/goldvein.png",
				Decimals = 0
			},
			new()
			{
				Id = 32,
				TokenName = "DFKKINGPNCR",
				Name = "King Pincer",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x60A3810a3963f23Fa70591435bbe93BF8786E202",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xB4A516bf36e44c0CE9E3E6769D3BA87341Cd9959",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKKINGPNCR,
				Image = "Images/Items/king-pincer.png",
				Decimals = 0
			},
			new()
			{
				Id = 33,
				TokenName = "DFKKNAPROOT",
				Name = "Knaproot",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xBcdD90034eB73e7Aec2598ea9082d381a285f63b",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xFceFA4Abcb18a7053393526f75Ad33fac5F25dc9",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKKNAPROOT,
				Image = "Images/Items/knaproot.png",
				Decimals = 0
			},
			new()
			{
				Id = 34,
				TokenName = "DFKRGWD",
				Name = "Ragweed",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x137995beEEec688296B0118131C1052546475fF3",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x4cD7025BD6e1b77105b90928362e6715101d0b5a",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKRGWD,
				Image = "Images/Items/ragweed.png",
				Decimals = 0
			},
			new()
			{
				Id = 35,
				TokenName = "DFKREDGILL",
				Name = "Redgill",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x68eE50dD7F1573423EE0Ed9c66Fc1A696f937e81",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x8D2bC53106063A37bb3DDFCa8CfC1D262a9BDCeB",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKREDGILL,
				Image = "Images/Items/redgill.png",
				Decimals = 0
			},
			new()
			{
				Id = 36,
				TokenName = "DFKRDLF",
				Name = "Redleaf",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x473A41e71618dD0709Ba56518256793371427d79",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xadbF23Fe3B47857614940dF31B28179685aE9B0c",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKRDLF,
				Image = "Images/Items/redleaf.png",
				Decimals = 0
			},
			new()
			{
				Id = 37,
				TokenName = "DFKSHAGCAP",
				Name = "Shaggy Caps",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x80A42Dc2909C0873294c5E359e8DF49cf21c74E4",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0xCe370D379f0CCf746B3426E3BD3923f3aDF0DC1a",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKSHAGCAP,
				Image = "Images/Items/shaggyCaps.png",
				Decimals = 0
			},
			new()
			{
				Id = 38,
				TokenName = "DFKSILVERFIN",
				Name = "Silverfin",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xA7CFd21223151700FB82684Cd9c693596267375D",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x7E121418cC5080C96d967cf6A033B0E541935097",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKSILVERFIN,
				Image = "Images/Items/silverfin.png",
				Decimals = 0
			},
			new()
			{
				Id = 39,
				TokenName = "DFKSPCKLTL",
				Name = "Silverfin",
				Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xE7CB27ad646C49dC1671Cb9207176D864922C431",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x48d9fC80A47cee2d52DE950898Bc6aBF54223F81",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKSPCKLTL,
				Image = "Images/Items/speckle-tail.png",
				Decimals = 0
			},
            new()
            {
                Id = 40,
                TokenName = "DFKETRNLSTY",
                Name = "Pages of the Eternal Story",
                Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0xA37851cCE4B2b65c0b290AA4cC2DFF00314ec85a",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x26bdcB310313eFf8D580e43762e2020B23f3e728",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Story,
                ItemEnum = DFKItemEnum.DFKETRNLSTY,
                Image = "Images/Items/eternal-story-page-1.png",
                Decimals = 0
            },
            new()
            {
                Id = 41,
                TokenName = "DFKSTMNPTN",
                Name = "Stamina Potion",
                Description = "",
                Addresses = new(){
                    new()
                    {
                        Address = "0x242078edFDca25ef2A497C8D9f256Fd641472E5F",
                        Chain = Constants.ChainsList[(int)ChainEnum.DFK],
                    },
                    new()
                    {
                        Address = "0x4546DBaAb48Bf1BF2ad7B56d04952d946Ab6e2a7",
                        Chain = Constants.ChainsList[(int)ChainEnum.Klaytn],
                    },
                },
                Category = ItemCategory.Consumable,
                ItemEnum = DFKItemEnum.DFKSTMNPTN,
                Image = "Images/Items/stamina-potion.png",
                Decimals = 0
            }
        };
        
		internal static DFKItem GetItem(ChainContract contract)
		{
            return InventoryItems.FirstOrDefault(item => item.Addresses.Any(a => a.Address == contract.Address && a.Chain.Id == contract.Chain.Id));
        }
	}
}
