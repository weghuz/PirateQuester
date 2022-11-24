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
				Address = "0xCCb93dABD71c8Dad03Fc4CE5559dC3D89F67a260",
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
				Address = "0x04b9dA42306B023f3572e106B11D82aAd9D32EBb",
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
				Description = "TBA",
				Address = "TBA",
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
				Address = "0x75E8D8676d774C9429FbB148b30E304b5542aC3d",
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
				Address = "0xCd2192521BD8e33559b0CA24f3260fE6A26C28e4",
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
				Address = "0x576C260513204392F0eC0bc865450872025CB1cA",
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
				Address = "0x79fE1fCF16Cc0F7E28b4d7B97387452E3084b6dA",
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
				Address = "0xa61Bac689AD6867a605633520D70C49e1dCce853", 
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
				Address = "0xf2D479DaEdE7F9e270a90615F8b1C52F3C487bC7", 
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
				Address = "0x8D2bC53106063A37bb3DDFCa8CfC1D262a9BDCeB", 
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
				Address = "0x7E121418cC5080C96d967cf6A033B0E541935097",
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
				Address = "0x72F860bF73ffa3FC42B97BbcF43Ae80280CFcdc3",
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
				Address = "0xB78d5580d6D897DE60E1A942A5C1dc07Bc716943",
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
				Address = "0x0776b936344DE7bd58A4738306a6c76835ce5D3F",
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
				Address = "0x848Ac8ddC199221Be3dD4e4124c462B806B6C4Fd",
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
				Address = "0x04B43D632F34ba4D4D72B0Dc2DC4B30402e5Cf88",
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
				Address = "0xc2Ff93228441Ff4DD904c60Ecbc1CfA2886C76eB",
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
				Address = "0xA2cef1763e59198025259d76Ce8F9E60d27B17B5",
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
				Address = "0x60170664b52c035Fcb32CF5c9694b22b47882e5F",
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
				Address = "0x7f46E45f6e0361e7B9304f338404DA85CB94E33D",
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
				Address = "0xd44ee492889C078934662cfeEc790883DCe245f3",
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
				Address = "0xc6030Afa09EDec1fd8e63a1dE10fC00E0146DaF3",
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
				Address = "0x3E022D84D397F18743a90155934aBAC421D5FA4C",
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
				Address = "0x97b25DE9F61BBBA2aD51F1b706D4D7C04257f33A",
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
				Address = "0x6513757978E89e822772c16B60AE033781A29A4F",
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
				Address = "0xeEe5b16Cc49e7cef65391Fe7325cea17f787e245",
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
				Address = "0x9d9ef1Bf6A46b8413bf6b1b54F6A7aAb53c6b1b6",
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
				Address = "0xbd2677c06C9448534A851bdD25dF045872b87cb1",
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
				Address = "0xE410b2BE2Ce1508E15009118567d02C6d7A7038e",
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
				Address = "0xbb5F97358F60cCBa262883A3Ff0C637393FE3aB8",
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLINSCR,
				Image = "Images/Items/insight-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 25,
				TokenName = "DFKLMGHTCR",
				Name = "Lesser Might Crystal",
				Description = "",
				Address = "0x5bAC3cAd961B01Ef9510C8e6c5402A2bB1542831",
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLMGHTCR,
				Image = "Images/Items/might-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 26,
				TokenName = "DFKLSWFTCR",
				Name = "Lesser Swiftness Crystal",
				Description = "",
				Address = "0x6BCA53314dADdA7f4De30A95413f75a93bfAfecF",
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLSWFTCR,
				Image = "Images/Items/swiftness-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 27,
				TokenName = "DFKLVGRCR",
				Name = "Lesser Vigor Crystal",
				Description = "",
				Address = "0x5e4Cf6907CB5fBe2F642E399F6d07E567155d1F8",
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLVGRCR,
				Image = "Images/Items/vigor-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 27,
				TokenName = "DFKLWITCR",
				Name = "Lesser Wit Crystal",
				Description = "",
				Address = "0xC989c916F189D2A2BE0322c020942d7c43aEa830",
				Category = ItemCategory.Material,
				ItemEnum = DFKItemEnum.DFKLWITCR,
				Image = "Images/Items/wit-crystal-lesser.gif",
				Decimals = 0
			},
			new()
			{
				Id = 28,
				TokenName = "DFKBLOATER",
				Name = "Bloater",
				Description = "",
				Address = "0x268CC8248FFB72Cd5F3e73A9a20Fa2FF40EfbA61",
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKBLOATER,
				Image = "Images/Items/bloater.png",
				Decimals = 0
			},
			new()
			{
				Id = 29,
				TokenName = "DFKFBLOATER",
				Name = "Frost Bloater",
				Description = "",
				Address = "0x3bcb9A3DaB194C6D8D44B424AF383E7Db51C82BD",
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKFBLOATER,
				Image = "Images/Items/frost-bloater.png",
				Decimals = 0
			},
			new()
			{
				Id = 30,
				TokenName = "DFKFROSTDRM",
				Name = "Frost Drum",
				Description = "",
				Address = "0xe7a1B580942148451E47b92e95aEB8d31B0acA37",
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
				Address = "0x0096ffda7A8f8E00e9F8Bbd1cF082c14FA9d642e",
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
				Address = "0x60A3810a3963f23Fa70591435bbe93BF8786E202",
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
				Address = "0xBcdD90034eB73e7Aec2598ea9082d381a285f63b",
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
				Address = "0x137995beEEec688296B0118131C1052546475fF3",
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
				Address = "0x68eE50dD7F1573423EE0Ed9c66Fc1A696f937e81",
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
				Address = "0x473A41e71618dD0709Ba56518256793371427d79",
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
				Address = "0x80A42Dc2909C0873294c5E359e8DF49cf21c74E4",
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
				Address = "0xA7CFd21223151700FB82684Cd9c693596267375D",
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
				Address = "0xE7CB27ad646C49dC1671Cb9207176D864922C431",
				Category = ItemCategory.Junk,
				ItemEnum = DFKItemEnum.DFKSPCKLTL,
				Image = "Images/Items/speckle-tail.png",
				Decimals = 0
			},
			new()
			{
				Id = 39,
				TokenName = "DFKETRNLSTY",
				Name = "Pages of the Eternal Story",
				Description = "",
				Address = "0xA37851cCE4B2b65c0b290AA4cC2DFF00314ec85a",
				Category = ItemCategory.Story,
				ItemEnum = DFKItemEnum.DFKETRNLSTY,
				Image = "Images/Items/eternal-story-page-1.png",
				Decimals = 0
			}
		};

		internal static DFKItem GetItem(string address)
		{
			return InventoryItems.FirstOrDefault(item => item.Address == address);
		}
	}
}
