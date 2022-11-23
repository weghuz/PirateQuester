namespace PirateQuester.DFK.Items
{
	public class DFKItem
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string TokenName { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ItemCategory Category { get; set; }
        public DFKItemEnum ItemEnum { get; set; }

        public DFKItem()
		{

		}
	}
}
