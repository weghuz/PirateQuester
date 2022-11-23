namespace PirateQuester.DFK.Items
{
	public partial class ItemContractDefinitions
	{
		public static readonly List<DFKItem> InventoryItems = new List<DFKItem>()
		{
			new()
			{
				Id = 0,
				Address = "0xCCb93dABD71c8Dad03Fc4CE5559dC3D89F67a260",
				Category = ItemCategory.Currency,
				Description = "",
				ItemEnum = DFKItemEnum.Jewel,
				Name = "Jewel",
				TokenName = "JEWEL",
				Image = "Images/Jewel.png"
			},
			new()
			{
				Id = 1,
				Address = "0x04b9dA42306B023f3572e106B11D82aAd9D32EBb",
				Category = ItemCategory.Currency,
				Description = "",
				ItemEnum = DFKItemEnum.Crystal,
				Name = "Crystal",
				TokenName = "CRYSTAL",
				Image = "Images/Crystal.png"
			},
			new()
			{
				Id = 2,
				Address = "TBA",
				Category = ItemCategory.Currency,
				Description = "",
				ItemEnum = DFKItemEnum.Crystal,
				Name = "Jade",
				TokenName = "JADE",
				Image = "Images/Jade.png"
			},
			new()
			{
				Id = 3,
				Address = "0x75E8D8676d774C9429FbB148b30E304b5542aC3d",
				Name = "Shvas Rune",
				TokenName = "DFKSHVAS",
				Description = "This rune pulses with power. Watching it, you begin to breathe in its rhythm...",
				Image = "Images/Items/shvas-rune.gif",
				Category = ItemCategory.Rune,
				ItemEnum = DFKItemEnum.DFKSHVAS
			},
			new()
			{
				Id = 4,
				Address = "0xCd2192521BD8e33559b0CA24f3260fE6A26C28e4",
				Name = "Moksha Rune",
				TokenName = "DFKMOKSHA",
				Category = ItemCategory.Rune,
				ItemEnum = DFKItemEnum.DFKMOKSHA,
				Image = "Images/Items/moksha-rune.gif",
				Description = "The rune pulses in your palm, the burdens on your mind witness liberation..."
			},
			new()
			{
				Id = 5,
				Address = "0x576C260513204392F0eC0bc865450872025CB1cA",
				Category = ItemCategory.Currency,
				Image = "Images/Items/gold-pile.png",
				ItemEnum = DFKItemEnum.Gold,
				Name = "Gold",
				TokenName = "DFKGOLD"
			},
			new()
			{
				Id = 6,
				Address = "0x79fE1fCF16Cc0F7E28b4d7B97387452E3084b6dA",
				Category = ItemCategory.Summoning,
				ItemEnum = DFKItemEnum.DFKTEARS,
				TokenName = "DFKTEARS",
				Description = "A crystal that, when attuned properly, can summon heroes from faraway lands.",
				Name = "Gaia’s Tears",
				Image = "Images/Items/gaias-tear.png",
			},
		};

		internal static DFKItem GetItem(string address)
		{
			return InventoryItems.FirstOrDefault(item => item.Address == address);
		}
	}
}
