namespace PirateQuester.DFK.Items
{
    public class DFKInventory : Contract
	{
		public List<DFKItem> Items { get; set; } = new();
		public delegate void UpdateItems();
		public static event UpdateItems ItemsUpdated;
		public void AddItem(DFKItem item)
		{
			DFKItem currentItem = Items.FirstOrDefault(item => item.Addresses.Any(a => item.Addresses.Any(ad => a.Address == ad.Address && a.Chain.Id == ad.Chain.Id)));
			if (currentItem == null)
			{
				Items.Add(item);
			}
			else
			{
				currentItem.Amount += item.Amount;
			}
			ItemsUpdated?.Invoke();
		}

		public void AddItems(List<DFKItem> items)
		{
			foreach(DFKItem item in items)
			{
				AddItem(item);
			}
		}
    }
}
