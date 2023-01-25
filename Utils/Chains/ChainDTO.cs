namespace PirateQuester.Utils.Chain
{
	public class ChainDTO
	{
		// override object.Equals
		public override bool Equals(object obj)
		{

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			var chain = obj as Chain;
			if (chain is null)
				return false;
			Id = chain.Id;
			Name = chain.Name;
			RPC = chain.RPC;
			Enabled = chain.Enabled;

			return base.Equals(obj);
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string RPC { get; set; }
		public bool Enabled { get; set; }
	}
}
