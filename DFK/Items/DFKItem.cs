namespace PirateQuester.DFK.Items
{
	public class DFKItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TokenName { get; set; }
		public List<ChainContract> Addresses { get; set; } = new();
		public ulong Amount { get; set; }
		public int Decimals { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public ItemCategory Category { get; set; }
		public DFKItemEnum ItemEnum { get; set; }

		public DFKItem()
		{

		}

		public DFKItem(DFKItem dFKItem)
		{
			Id = dFKItem.Id;
			Name = dFKItem.Name;
			Addresses = dFKItem.Addresses;
			Amount = dFKItem.Amount;
			Decimals = dFKItem.Decimals;
			Image = dFKItem.Image;
			Description = dFKItem.Description;
			Category = dFKItem.Category;
			ItemEnum = dFKItem.ItemEnum;
		}
	}
}
